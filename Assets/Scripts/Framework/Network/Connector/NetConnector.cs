using System;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using SimpleJson;
using Pomelo.DotNetClient;
using JW.Common;

namespace JW.Framework.Network
{
    /// <summary>
    /// 网络连接器
    /// </summary>
    public class NetConnector
    {
        //当前工作状态
        public enum WorkState
        {
            ConnectGateState,
            ConnectGateWaitingState,
            ConnectGateFailedState,
            ConnectGateRetryState,
            ConnectGateQueryGSvrState,
            ConnectGateQueryGSvrWaitingState,
            //
            ConnectGameSvrState,
            ConnectGameSvrWaitingState,
            ConnectGameSvrFailedState,
            ConnectGameSvrRetryState,
            //成功
            ConnectGameSvrSuccessed,
        }

        //完整事务状态
        public enum SessionState
        {
            None = 0,
            SessionStart = 1,
            SessionError = 2,
            SessionRetry = 3,
            SessionSuccessed = 4,
        }

        //回调
        public System.Action<string, string> RecvMsgHook;
        public System.Action<int> ConnectHook;

        //网关相关
        private  string MSG_Query_Connector;
        private  string GateIp;
        private  int GatePort ;
        //
        protected SafedQueue<Message> mReceiveQueue = new SafedQueue<Message>();
        /// 发送字典
        private IList<uint> mSendCmdList = new List<uint>();
        private IDictionary<uint, NetSendPacket> mSendDict = new Dictionary<uint, NetSendPacket>();
        private uint mReqId = 1;
      
        //当前工作状态
        private WorkState _workState;
        private int _gateRetryedCnt = 0;
        private int _gsvrRetryedCnt = 0;
        //工作线程
        private Thread _workThread;
        private bool _isWorking = false;
        private PomeloClient _pomeloClient;
        //当前底层pomelo 网络状态
        private NetWorkState _curNetworkState = NetWorkState.CONNECTING;
        //
        private SessionState _cachedSessionState = SessionState.None;
        public SessionState CurSessionState = SessionState.None;

        //最终连接
        private string _gameSvrIp = string.Empty;
        private int _gameSvrPort = 0;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="gateIp">网关地址</param>
        /// <param name="gatePort">网关端口号</param>
        /// <param name="gateRoute">网关查询真正服务器路由Key</param>
        /// <returns></returns>
        public bool Initialize(string gateIp,int gatePort,string gateRoute)
        {
            GateIp = gateIp;
            GatePort = gatePort;
            MSG_Query_Connector = gateRoute;
            return true;
        }

       
        public void Uninitialize()
        {
            _isWorking = false;
            if (_workThread != null)
            {
                _workThread.Abort();
                _workThread = null;
            }
            if (_pomeloClient != null)
            {
                _pomeloClient.disconnect();
                _pomeloClient.Dispose();
                _pomeloClient = null;
            }
            //销毁Net缓存器
            NetVendor.DestroyInstance();
        }

        //清理接收到的数据队列
        public void ClearReceiveQueue()
        {
            if (mReceiveQueue.Count > 0)
            {
                mReceiveQueue.Clear();
            }
        }

        /// 清除发送队列
        public void ClearSendMsg()
        {
            mSendCmdList.Clear();
            //
            var itor = mSendDict.GetEnumerator();
            while (itor.MoveNext())
            {
                itor.Current.Value.Release();
            }
            mSendDict.Clear();
        }

        //打开连接
        public void OpenConnect()
        {
            _cachedSessionState = SessionState.SessionStart;
            StartNet();
        }

        //关闭连接
        public void CloseConnect()
        {
            _cachedSessionState=SessionState.None;
            CloseNet();
        }

        //驱动
        public void LogicUpdate(float deltaTime)
        {
            //
            UpdateInternal(deltaTime);

            //状态变化
            if (CurSessionState != _cachedSessionState)
            {
                _cachedSessionState = CurSessionState;
                if (ConnectHook != null)
                {
                    ConnectHook((int)CurSessionState);
                }
            }
        }

        private void UpdateInternal(float deltaTime)
        {
            //先处理网络消息
            HandleNetMsgUpdate();
            //
            if (_isWorking == false)
            {
                return;
            }

            //首次连接时候判断网络状态
            if ((_curNetworkState == NetWorkState.CLOSED
                || _curNetworkState == NetWorkState.ERROR
                || _curNetworkState == NetWorkState.TIMEOUT
                || _curNetworkState == NetWorkState.DISCONNECTED)
                && (_workState != WorkState.ConnectGameSvrSuccessed) && (CurSessionState == SessionState.SessionStart))
            {
                JW.Common.Log.LogD("Pomelo 事务错误");
                //关闭网络
                CloseNet();

                CurSessionState = SessionState.SessionError;
                //回位
                _workState = WorkState.ConnectGateState;
                _curNetworkState = NetWorkState.CONNECTING;
                return;
            }

            //正常情况判断网络状态
            if ((_curNetworkState == NetWorkState.CLOSED
                || _curNetworkState == NetWorkState.ERROR
                || _curNetworkState == NetWorkState.TIMEOUT
                || _curNetworkState == NetWorkState.DISCONNECTED)
                && (_workState == WorkState.ConnectGameSvrSuccessed) && (CurSessionState == SessionState.SessionSuccessed)
                )
            {
                //网络断开 等待重试阶段
                JW.Common.Log.LogD("Pomelo 网络错误:准备重试连接");
                Change2Retry();
                return;
            }
            //
            //重试情况判断网络状态
            if ((_curNetworkState == NetWorkState.CLOSED
                || _curNetworkState == NetWorkState.ERROR
                || _curNetworkState == NetWorkState.TIMEOUT
                || _curNetworkState == NetWorkState.DISCONNECTED)
                && (_workState != WorkState.ConnectGameSvrSuccessed) && (CurSessionState == SessionState.SessionRetry)
                )
            {
                //网络断开 等待重试阶段
                JW.Common.Log.LogD("Pomelo 网络错误:继续重试连接");
                Change2Retry();
                return;
            }
            //
            HandleSendMsgUpdate();
        }

        private void Change2Retry()
        {
            CurSessionState = SessionState.SessionRetry;
            _curNetworkState = NetWorkState.CONNECTING;
            _workState = WorkState.ConnectGateState;

            if (string.IsNullOrEmpty(_gameSvrIp))
            {
                _workState = WorkState.ConnectGateState;
            }
            else
            {
                _workState = WorkState.ConnectGameSvrState;
            }
        }

        //开始
        private void StartNet()
        {
            CurSessionState = SessionState.SessionStart;
            _curNetworkState = NetWorkState.CONNECTING;
            _isWorking = true;
            //开始连接网关
            _workState = WorkState.ConnectGateState;
            //
            ClearReceiveQueue();
            ClearSendMsg();
            //
            _workThread = new Thread(new ThreadStart(delegate ()
            {
                //
                DoWork();
                //
            }));
            //
            _workThread.Start();
        }

        //关闭
        private void CloseNet()
        {
            _isWorking = false;

            ClearReceiveQueue();
            ClearSendMsg();
            //
            if (_workThread != null)
            {
                _workThread.Abort();
                _workThread = null;
            }
            if (_pomeloClient != null)
            {
                _pomeloClient.disconnect();
                _pomeloClient.Dispose();
                _pomeloClient = null;
            }
        }

        //工作
        private void DoWork()
        {
            while (_isWorking)
            {
                //
                if (_workState == WorkState.ConnectGateState)
                {
                    JW.Common.Log.LogD("Pomelo 开始连接网关");
                    //释放
                    if (_pomeloClient != null)
                    {
                        _pomeloClient.NetWorkStateChangedEvent -= OnPemeloNetworkStateChange;
                        _pomeloClient.disconnect();
                        _pomeloClient.Dispose();
                        _pomeloClient = null;
                    }
                    _pomeloClient = new PomeloClient();
                    _pomeloClient.NetWorkStateChangedEvent += OnPemeloNetworkStateChange;
                    //
                    _pomeloClient.initClient(GateIp, GatePort, delegate ()
                    {
                        //
                        _pomeloClient.on(OnPomeloReceiveNetMsg);
                        //套接字建立成功
                        _pomeloClient.connect(null, delegate (JsonObject data)
                            {
                            //握手回来
                            //开始查询游戏服务器地址
                            JW.Common.Log.LogD("Pomelo网关握手回来");
                                _workState = WorkState.ConnectGateQueryGSvrState;

                            });
                    });
                    //
                    _workState = WorkState.ConnectGateWaitingState;
                    Thread.Sleep(200);
                    continue;
                }

                if (_workState == WorkState.ConnectGateWaitingState)
                {
                    //
                    Thread.Sleep(200);
                }
                //请求查询游戏服务器地址
                if (_workState == WorkState.ConnectGateQueryGSvrState)
                {
                    JW.Common.Log.LogD("Pomelo 请求查询游戏服务器地址和端口");
                    string route = MSG_Query_Connector;
                    JsonObject jsonObject = new JsonObject();
                    SendJsonMsg(route, jsonObject);
                    //
                    _workState = WorkState.ConnectGateQueryGSvrWaitingState;
                    Thread.Sleep(200);
                    continue;
                }
                //
                if (_workState == WorkState.ConnectGateQueryGSvrWaitingState)
                {
                    //
                    Thread.Sleep(200);
                }

                //连接游戏服务器
                if (_workState == WorkState.ConnectGameSvrState)
                {
                    //释放网关用过的
                    if (_pomeloClient != null)
                    {
                        //去掉下
                        _pomeloClient.NetWorkStateChangedEvent -= OnPemeloNetworkStateChange;
                        _pomeloClient.disconnect();
                        _pomeloClient.Dispose();
                        _pomeloClient = null;
                    }
                    //
                    _pomeloClient = new PomeloClient();
                    _pomeloClient.NetWorkStateChangedEvent += OnPemeloNetworkStateChange;
                    //
                    JW.Common.Log.LogD("Pomelo 连接游戏服务器:{0}:{1}", _gameSvrIp, _gameSvrPort);
                    _pomeloClient.initClient(_gameSvrIp, _gameSvrPort, delegate ()
                    {
                        //套接字建立成功
                        _pomeloClient.on(OnPomeloReceiveNetMsg);
                        //
                        _pomeloClient.connect(null, delegate (JsonObject data)
                            {
                            //握手回来
                            JW.Common.Log.LogD("------------>Pomelo Good Net<-----------------");
                                _workState = WorkState.ConnectGameSvrSuccessed;
                                if (CurSessionState == SessionState.SessionRetry)
                                {
                                    JW.Common.Log.LogD("Pomelo Retry Connect Successed");
                                }
                            //成功
                            CurSessionState = SessionState.SessionSuccessed;
                            });
                        //
                    });
                    _workState = WorkState.ConnectGameSvrWaitingState;
                    Thread.Sleep(200);
                    continue;
                }

                if (_workState == WorkState.ConnectGameSvrWaitingState)
                {
                    //
                    Thread.Sleep(200);
                }

                if (_workState == WorkState.ConnectGameSvrRetryState)
                {
                    //
                    Thread.Sleep(200);
                }
                //
                if (_workState == WorkState.ConnectGameSvrSuccessed)
                {
                    Thread.Sleep(1000);
                }
            }
        }

        //工作线程回调
        private void OnPemeloNetworkStateChange(NetWorkState st)
        {
            JW.Common.Log.LogD("<color=green>OnPemeloNetworkStateChange:</color>" + st.ToString());
            _curNetworkState = st;
        }

        //工作线程回调
        private void OnPomeloReceiveNetMsg(Message msg)
        {
            JW.Common.Log.LogD("<color=yellow>[Pomelo Net Recv]</color>:Type:{0},Mid:{1},Route:{2},Data;{3}", msg.type, msg.id, msg.route, msg.mStrMsg);
            if (IsMessageSuccess(msg.data))
            {
                //加入队列等待主线程处理
                mReceiveQueue.Enqueue(msg);
            }
            else
            {
                JW.Common.Log.LogE("<color=yellow>[Pomelo Net Recv Error 500]:</color>" + msg.route);
                JW.Common.Log.LogE("Pomelo 网络收包错误500");
                Change2Retry();
            }
        }

        /// 主线程处理网络收到的数据
        private void HandleNetMsgUpdate()
        {
            try
            {
                if (mReceiveQueue.Count > 0)
                {
                    Message msg = mReceiveQueue.Dequeue();
                    //回复
                    if (msg.type == MessageType.MSG_RESPONSE)
                    {
                        //网关查询游戏服务器回包
                        if (msg.route.Equals(MSG_Query_Connector))
                        {
                            JW.Common.Log.LogD("Pomelo 收到网关查询服务器回复");
                            //
                            HandleRecvCommonRspMsg(msg.route, msg, true);
                            //
                            //网关查询游戏服务器回包
                            String host = (String)msg.data["host"];
                            int port = Convert.ToInt32(msg.data["port"]);
                            //
                            _gameSvrIp = host;
                            _gameSvrPort = port;
                            //开始连接游戏服务器
                            _workState = WorkState.ConnectGameSvrState;
                            //
                        }
                        else
                        {
                            //非网关查询
                            HandleRecvCommonRspMsg(msg.route, msg, false);
                            if (RecvMsgHook != null)
                            {
                                RecvMsgHook(msg.route, msg.mStrMsg);
                            }
                        }
                    }
                    else
                    {
                        if (RecvMsgHook != null)
                        {
                            RecvMsgHook(msg.route, msg.mStrMsg);
                        }
                    }
                }
            }
            catch (System.Exception error)
            {
                JW.Common.Log.LogE("HandleNetMsgUpdate Error:{0}", error);
            }
        }

        //处理回复的消息
        private void HandleRecvCommonRspMsg(string route, Message msg, bool isGateQuery = false)
        {
            if (string.IsNullOrEmpty(route))
            {
                return;
            }
            if (mSendDict.ContainsKey(msg.id))
            {
                //判断SendDict里面是否含有这个字段,如果有,则说明是发出去的消息
                if (msg.route != mSendDict[msg.id].mRoute)
                {
                    JW.Common.Log.LogE("Pomelo Net Recv Route Is Error Not Send");
                }
                //清理掉发送队列的 
                mSendDict[msg.id].Release();
                mSendDict.Remove(msg.id);
                //暂时如此
                for (int i = 0; i < mSendCmdList.Count; ++i)
                {
                    if (mSendCmdList[i] == msg.id)
                    {
                        mSendCmdList.RemoveAt(i);
                        break;
                    }
                }
            }
            else
            {
                JW.Common.Log.LogE("Can't Handle Msg route={0}", route);
            }
        }


        //判断接收到的信息是否正确
        private Boolean IsMessageSuccess(JsonObject result)
        {
            bool flag = true;
            try
            {
                int code = Convert.ToInt32(result["code"]);
                if (code == 500)
                {
                    flag = false;
                    Console.WriteLine("msg failed");
                }
            }
            catch (System.Exception ex)
            {

            }
            return flag;
        }

        //-------------------------------发送相关-------------------------------------
        /// <summary>
        /// 处理发送队列的消息
        /// </summary>
        private void HandleSendMsgUpdate()
        {
            if (mSendCmdList.Count > 0 && (_pomeloClient != null))
            {
                if ((_workState == WorkState.ConnectGameSvrSuccessed || _workState == WorkState.ConnectGateQueryGSvrState || _workState == WorkState.ConnectGateQueryGSvrWaitingState))
                {
                    NetSendPacket packet = null;
                    for (int i = 0; i < mSendCmdList.Count; i++)
                    {
                        if (mSendDict.TryGetValue(mSendCmdList[i], out packet))
                        {
                            //查询服务器地址
                            if (packet.mRoute.Equals(MSG_Query_Connector))
                            {
                                if (!packet.mSended)
                                {
                                    //设置已经发送标识
                                    packet.SetSended();
                                    //Real Send
                                    _pomeloClient.request(packet.mRoute, packet.mId, packet.mData);
                                    return;
                                }
                            }
                            else
                            {
                                //判断是否已经发送过了,发送过了就不用再发送了
                                if (!packet.mSended)
                                {
                                    //设置已经发送标识
                                    packet.SetSended();
                                    if (packet.ReSended)
                                    {
                                        JW.Common.Log.LogD("<color=yellow>[Net Send Again]</color>:Type:{0}", packet.mRoute);
                                    }
                                    //Real Send
                                    _pomeloClient.request(packet.mRoute, packet.mId, packet.mData);
                                    return;
                                }
                            }

                        }
                    }
                }
            }
        }
        //
        public void SendMsg(string route, string msg)
        {
            JsonObject data = (JsonObject)SimpleJson.SimpleJson.DeserializeObject(msg);
            SendJsonMsg(route, data);
        }
        //
        public void SendJsonMsg(string route, JsonObject data)
        {
            if (string.IsNullOrEmpty(route))
            {
                return;
            }

            if (route.Equals(MSG_Query_Connector))
            {
                //最前面 //清理掉久的
                if (mSendCmdList.Count > 0)
                {
                    uint first = mSendCmdList[0];
                    if (mSendDict.ContainsKey(first) &&(mSendDict[first].mRoute == MSG_Query_Connector))
                    {
                        mSendDict[first].Release();
                        mSendDict.Remove(first);
                        mSendCmdList.RemoveAt(0);
                    }
                }

                //暂时如此加入字典,为后续报错确定位置
                NetSendPacket msg = NetSendPacket.New(route, data, mReqId);
                //追加
                mSendCmdList.Insert(0, msg.mId);
                mSendDict[msg.mId] = msg;
                if (mReqId == uint.MaxValue)
                {
                    mReqId = 0;
                }
                mReqId++;
                return;
            }
            else
            {
                //暂时如此加入字典,为后续报错确定位置
                NetSendPacket msg = NetSendPacket.New(route, data, mReqId);
                //追加
                mSendCmdList.Add(msg.mId);
                mSendDict[msg.mId] = msg;
                if (mReqId == uint.MaxValue)
                {
                    mReqId = 0;
                }
                mReqId++;
            }
        }
    }
}

