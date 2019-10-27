using SimpleJson;
using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Pomelo.DotNetClient
{
    /// <summary>
    /// network state enum
    /// </summary>
    public enum NetWorkState
    {
        [Description("initial state")]
        CLOSED,

        [Description("connecting server")]
        CONNECTING,

        [Description("server connected")]
        CONNECTED,

        [Description("disconnected with server")]
        DISCONNECTED,

        [Description("connect timeout")]
        TIMEOUT,

        [Description("netwrok error")]
        ERROR
    }

    public class PomeloClient : IDisposable
    {
        /// <summary>
        /// netwrok changed event
        /// </summary>
        public event Action<NetWorkState> NetWorkStateChangedEvent;


        private NetWorkState netWorkState = NetWorkState.CLOSED;   //current network state

        private EventManager eventManager;
        private Socket socket;
        private Protocol protocol;
        private bool disposed = false;
        private uint reqId = 1;

        private ManualResetEvent timeoutEvent = new ManualResetEvent(false);
        private int timeoutMSec = 3000;    //connect timeout count in millisecond

        private int netFlag = 0;           //ipv4或者ipv6的标识

		internal string mIp;
		internal int mPort;
        public PomeloClient()
        {
        }

        /// <summary>
        /// initialize pomelo client
        /// </summary>
        /// <param name="host">server name or server ip (www.xxx.com/127.0.0.1/::1/localhost etc.)</param>
        /// <param name="port">server port</param>
        /// <param name="callback">socket successfully connected callback(in network thread)</param>
        public void initClient(string host, int port, Action callback = null)
        {
            timeoutEvent.Reset();
            eventManager = new EventManager();
            NetWorkChanged(NetWorkState.CONNECTING);
            IPAddress ipAddress = null;
            try
            {
                //IPAddress[] addresses = Dns.GetHostEntry(host).AddressList;
                IPAddress[] addresses = Dns.GetHostAddresses(host);
                foreach (var item in addresses)
                {
                    if (item.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = item;

                        netFlag = 4;
                        break;
                    }
                    else if (item.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        ipAddress = item;

                        netFlag = 6;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                NetWorkChanged(NetWorkState.ERROR);
                return;
            }

            if (ipAddress == null)
            {
                throw new Exception("can not parse host : " + host);
            }
            if (netFlag == 4)
            {
                this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            else if (netFlag == 6)
            {
                this.socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
            }    
            IPEndPoint ie = new IPEndPoint(ipAddress, port);
			mIp = host;
			mPort = port;
            socket.BeginConnect(ie, new AsyncCallback((result) =>
            {
                try
                {
                    this.socket.EndConnect(result);
                    this.protocol = new Protocol(this, this.socket);
                    NetWorkChanged(NetWorkState.CONNECTED);

                    if (callback != null)
                    {
                        //NGUI//Debug.Log("callbackcallbackcallbackcallbackcallbackcallback=" + callback);
                        callback();
                    }
                }
                catch (SocketException e)
                {
                    if (netWorkState != NetWorkState.TIMEOUT)
                    {
                        NetWorkChanged(NetWorkState.ERROR);
                    }
                    Dispose();
                }
                finally
                {
                    timeoutEvent.Set();
                }
            }), this.socket);

            if (timeoutEvent.WaitOne(timeoutMSec, false))
            {
                if (netWorkState != NetWorkState.CONNECTED && netWorkState != NetWorkState.ERROR)
                {
                    NetWorkChanged(NetWorkState.TIMEOUT);
                    Dispose();
                }
            }
            else
            {
                if (netWorkState != NetWorkState.CONNECTED && netWorkState != NetWorkState.ERROR)
                {
                    disconnect();
                }
            }
        }

        /// <summary>
        /// 网络状态变化
        /// </summary>
        /// <param name="state"></param>
        private void NetWorkChanged(NetWorkState state)
        {
            netWorkState = state;

            if (NetWorkStateChangedEvent != null)
            {
                NetWorkStateChangedEvent(state);
            }
        }

        public void connect()
        {
            connect(null, null);
        }

        public void connect(JsonObject user)
        {
            connect(user, null);
        }

        public void connect(Action<JsonObject> handshakeCallback)
        {
            connect(null, handshakeCallback);
        }

        public bool connect(JsonObject user, Action<JsonObject> handshakeCallback)
        {
            try
            {
				//NGUI//Debug.Log("protocol.start....");
                protocol.start(user, handshakeCallback);
				//NGUI//Debug.Log("protocol.stop...");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        private JsonObject emptyMsg = new JsonObject();
        public void request(string route, Action<JsonObject> action)
        {
            this.request(route, emptyMsg, action);
        }

        /// <summary>
        /// 客户端请求数据，请求成功，获取服务器反馈之后，调用action
        /// </summary>
        /// <param name="route">路由</param>
        /// <param name="msg">携带的信息</param>
        /// <param name="action">回调函数</param>
        public void request(string route, JsonObject msg, Action<JsonObject> action)
        {
            this.eventManager.AddCallBack(reqId, action);
            protocol.send(route, reqId, msg);
			//Log.d ("request pomelo ip=", mIp, " port=", mPort);
            reqId++;
        }

		public void request(string route,uint ireqId,JsonObject msg)
		{
			//Log.d ("request pomelo ip=", mIp, " port=", mPort);
			//Log.d ("request,route=", route, " reqId=", ireqId);
			protocol.send (route, ireqId, msg);
			reqId++;
		}
        /// <summary>
        /// 客户端通知服务器的数据，不需要反馈
        /// </summary>
        /// <param name="route">路由</param>
        /// <param name="msg">携带的信息</param>
        public void notify(string route, JsonObject msg)
        {
            protocol.send(route, msg);
        }

        /// <summary>
        /// 注册网络事件，也就是当接收到服务器push数据的时候，会触发action
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="action">触发的函数</param>
		public void on(Action<Message> action)
        {
			if (null != eventManager) {
                eventManager.AddOnEvent(action);
			}
           // eventManager.AddOnEvent(eventName, action);
        }

        internal void processMessage(Message msg)
        {
			//JW.Common.Log.LogD("progressMessage----------route="+msg.route);
            if (msg.type == MessageType.MSG_RESPONSE)
            {
                //msg.data["__route"] = msg.route;
                //msg.data["__type"] = "resp";
				eventManager.InvokeOnEvent(msg);
                //eventManager.InvokeCallBack(msg.id, msg.data);
            }
            else if (msg.type == MessageType.MSG_PUSH)
            {
                //msg.data["__route"] = msg.route;
                //msg.data["__type"] = "push";
				eventManager.InvokeOnEvent(msg);
            }
        }

     
        public void disconnect()
        {
           //NGUI//Debug.Log("disconnectdisconnectdisconnect");
          //  if (!_hasdis)//zeng jia pang duan lilin
            {
               // _hasdis = true;
                Dispose();
                NetWorkChanged(NetWorkState.DISCONNECTED);
            }
           
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // The bulk of the clean-up code
        protected virtual void Dispose(bool disposing)
        {
            //if (this.disposed)
            //    return;

            if (disposing)
            {
                // free managed resources
                if (this.protocol != null)
                {
                    this.protocol.close();
                }

                if (this.eventManager != null)
                {
                    this.eventManager.Dispose();
                }

                try
                {
                    if (this.socket != null)
                    {
                        this.socket.Shutdown(SocketShutdown.Both);
                        this.socket.Close();
                        this.socket = null;
                    }
                }
                catch (Exception)
                {
                    //todo : 有待确定这里是否会出现异常，这里是参考之前官方github上pull request。emptyMsg
                }

                this.disposed = true;
            }
        }
    }
}