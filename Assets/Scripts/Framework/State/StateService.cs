/********************************************************************
	created:	22:5:2018   
	filename: 	StateService
	author:		jordenwu
	
	purpose:	游戏状态管理器
*********************************************************************/
using JW.Common;
using System.Collections.Generic;

namespace JW.Framework.State
{
    /// <summary>
    /// 状态接口
    /// </summary>
    public interface IStateCallback
    {
        /// <summary>
        /// 状态进入
        /// </summary>
        /// <param name="state">进入的状态</param>
        /// <param name="fromState">前一个状态</param>
        /// <param name="userData">自定义数据</param>
        void OnStateEnter(string state, string fromState, object userData);

        /// <summary>
        /// 状态离开
        /// </summary>
        /// <param name="reason">跳转原因，取值Because×××</param>
        /// <param name="state">离开的状态</param>
        /// <param name="toState">下一个状态</param>
        /// <param name="userData">自定义数据</param>
        void OnStateLeave(string state, string toState, object userData);

        /// <summary>
        /// 状态离开栈顶 目前无用
        /// </summary>
        /// <param name="state">离开栈顶的状态</param>
        /// <param name="toState">下一个状态</param>
        /// <param name="userData">自定义数据</param>
        void OnStateOverride(string state, string toState, object userData);

        /// <summary>
        /// 状态恢复到栈顶 目前无用
        /// </summary>
        /// <param name="state">恢复到栈顶的状态</param>
        /// <param name="fromState">前一个状态</param>
        /// <param name="userData">自定义数据</param>
        void OnStateResume(string state, string fromState, object userData);
    }

    /// <summary>
    /// 游戏状态服务 
    /// </summary>
    public class StateService : Singleton<StateService>
    {
        private readonly List<IStateCallback> _callback = new List<IStateCallback>();
        //
        private JWObjDictionary<string, IState> _registedState = new JWObjDictionary<string, IState>();
        //当前状态
        private IState _curState = null;

        /// <summary>
        /// 当前状态
        /// </summary>
        public string CurrentState
        {
            get
            {
                return (_curState!=null)? _curState.Name() : string.Empty;
            }
        }

        public override bool Initialize()
        {
            return true;
        }

        public override void Uninitialize()
        {
            _curState = null;
            if (_registedState != null)
            {
                _registedState.Clear();
                _registedState = null;
            }
        }

        /// <summary>
        /// 注册游戏状态
        /// </summary>
        /// <param name="name"></param>
        /// <param name="state"></param>
        public void RegisteState(string name,IState state)
        {
            if (_registedState == null)
            {
                _registedState = new JWObjDictionary<string, IState>();
            }
            if (_registedState.ContainsKey(name))
            {
                JW.Common.Log.LogE("StateService:Already Registed:"+name);
                return;
            }
            if (state != null)
            {
                state.InitializeState();
            }
            _registedState.Add(name, state);
        }

        /// <summary>
        /// 添加状态回调
        /// </summary>
        /// <param name="callback">回调接口</param>
        public void AddCallback(IStateCallback callback)
        {
            if (callback == null)
            {
                JW.Common.Log.LogE("StateService.AddCallback : invalid parameter");
                return;
            }

            if (_callback.Contains(callback))
            {
                JW.Common.Log.LogE("StateService.AddCallback : duplicate add call back - {0}", callback.GetType().FullName);
                return;
            }

            _callback.Add(callback);
        }

        /// <summary>
        /// 移除状态回调
        /// </summary>
        /// <param name="callback">回调接口</param>
        public void RemoveCallback(IStateCallback callback)
        {
            if (callback == null)
            {
                JW.Common.Log.LogE("StateService.RemoveCallback : invalid parameter");
                return;
            }
            _callback.Remove(callback);
        }

        /// <summary>
        /// 切换状态，将移除现有的所有状态，切换成新的状态
        /// </summary>
        /// <param name="targetState">目标状态</param>
        /// <param name="userData">用户自定义数据</param>
        public void ChangeState(string targetState, object userData = null)
        {
            if (string.IsNullOrEmpty(targetState))
            {
                JW.Common.Log.LogE("StateService.ChangeState : invalid parameter");
                return;
            }
            if (string.Equals(this.CurrentState,targetState))
            {
                JW.Common.Log.LogE("StateService.ChangeState : already existed");
                return;
            }
            string fromState = this.CurrentState;
            if (_curState != null)
            {
                _curState.OnStateLeave();
                for (int j = 0; j < _callback.Count; j++)
                {
                    _callback[j].OnStateLeave(this.CurrentState, targetState,userData);
                }
                _curState = null;
            }
            //
            IState cState = null;
            if (_registedState.TryGetValue(targetState, out cState))
            {
                _curState = cState;
                _curState.OnStateEnter(userData);
                //回调
                for (int j = 0; j < _callback.Count; j++)
                {
                    _callback[j].OnStateEnter(targetState, fromState, userData);
                }
            }
            else
            {
                JW.Common.Log.LogE("StateService.ChangeState : not registed");
            }
            //
           // JW.Common.Log.LogD("<color=yellow>StateService</color>--->Switch Game State from {0} to {1}<----", fromState, CurrentState);
        }
    }
}
