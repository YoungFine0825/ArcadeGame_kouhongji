/********************************************************************
	created:	17:5:2018   
	filename: 	StateMachine
	author:		jordenwu
	
	purpose:	状态机
*********************************************************************/
using System.Collections.Generic;

namespace JW.Common
{
    public interface IState
    {
        /// <summary>
        ///     初始化状态
        /// </summary>
        void InitializeState();

        /// <summary>
        /// 反初始化状态
        /// </summary>
        void UninitializeState();

        /// <summary>
        /// 状态名
        /// </summary>
        /// <returns></returns>
        string Name();

        /// <summary>
        /// 状态进入状态栈
        /// </summary>
        void OnStateEnter(object usrData = null);

        /// <summary>
        /// 状态退出状态栈
        /// </summary>
        void OnStateLeave();

        /// <summary>
        /// 状态由栈顶变成非栈顶
        /// </summary>
        void OnStateOverride();

        /// <summary>
        /// 状态由非栈顶变成栈顶
        /// </summary>
        void OnStateResume();
    }

    public interface IStateEvent
    {
        /// <summary>
        /// 状态切换前事件
        /// </summary>
        /// <param name="sourceState">源状态</param>
        /// <param name="targetState">目标状态</param>
        void OnStatePrevChange(uint sourceState, uint targetState);

        /// <summary>
        /// 状态切换后事件
        /// </summary>
        /// <param name="sourceState">源状态</param>
        /// <param name="targetState">目标状态</param>
        void OnStatePostChange(uint sourceState, uint targetState);
    }

    /// <summary>
    /// 基于栈的状态机
    /// </summary>
    public class StateMachine
    {
        public const uint InvalidID = uint.MaxValue;

        private struct Data
        {
            public uint Id;
            public IState State;
        }

        private JWArrayList<IStateEvent> _stateEvent = new JWArrayList<IStateEvent>();

        private JWArrayList<Data> _registedState = new JWArrayList<Data>();
        private readonly Stack<IState> _stateStack = new Stack<IState>();

        /// <summary>
        /// 栈顶状态
        /// </summary>
        public IState TopState
        {
            get
            {
                return _stateStack.Count > 0 ? _stateStack.Peek() : null;
            }
        }

        /// <summary>
        /// 栈顶状态ID
        /// </summary>
        public uint TopStateID
        {
            get
            {
                return _stateStack.Count > 0 ? GetStateId(_stateStack.Peek()) : InvalidID;
            }
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="e">事件接口</param>
        public void RegisterEvent(IStateEvent e)
        {
            if (e == null || _stateEvent.Contains(e))
            {
                return;
            }

            _stateEvent.Add(e);
        }

        /// <summary>
        /// 反注册事件
        /// </summary>
        /// <param name="e">事件接口</param>
        public void UnregisterEvent(IStateEvent e)
        {
            _stateEvent.Remove(e);
        }

        /// <summary>
        /// 注册状态
        /// </summary>
        /// <param name="id">状态ID</param>
        /// <param name="state">状态</param>
        public void RegisteState(uint id, IState state)
        {
            if (id == InvalidID || state == null)
            {
                return;
            }

            if (GetState(id) != null || GetStateId(state) != InvalidID)
            {
                return;
            }

            state.InitializeState();

            Data data;
            data.Id = id;
            data.State = state;
            _registedState.Add(data);
        }

        /// <summary>
        /// 反注册所有状态
        /// </summary>
        /// <returns></returns>
        public void UnregisterAllState()
        {
            PopAll();

            for (int i = 0; i < _registedState.Count; i++)
            {
                Data data = _registedState[i];
                data.State.UninitializeState();
            }

            _registedState.Clear();
        }

        /// <summary>
        /// 压入状态
        /// </summary>
        /// <param name="id">状态ID</param>
        /// <param name="userData">自定义数据</param>
        /// <returns></returns>
        public void Push(uint id, object userData = null)
        {
            IState newState = GetState(id);
            if (newState == null)
            {
                return;
            }

            if (_stateStack.Count > 0)
            {
                _stateStack.Peek().OnStateOverride();
            }

            _stateStack.Push(newState);
            newState.OnStateEnter(userData);
        }

        /// <summary>
        /// 弹出状态
        /// </summary>
        /// <returns>被弹出的状态</returns>
        public IState Pop()
        {
            if (_stateStack.Count <= 0)
            {
                return null;
            }

            IState state = _stateStack.Pop();
            state.OnStateLeave();

            if (_stateStack.Count > 0)
            {
                _stateStack.Peek().OnStateResume();
            }

            return state;
        }

        /// <summary>
        /// 弹出所有状态
        /// </summary>
        /// <returns></returns>
        public void PopAll()
        {
            while (Pop() != null)
            {

            }
        }

        /// <summary>
        /// 修改栈顶状态
        /// </summary>
        /// <param name="id">状态ID</param>
        /// <param name="userData">自定义数据</param>
        /// <returns></returns>
        public void ChangeState(uint id, object userData = null)
        {
            IState newState = GetState(id);
            if (newState == null)
            {
                return;
            }

            JW.Common.Log.LogD("GameStateChange" + newState.Name());
            uint oldStateId = InvalidID;
            //string oldStateName = "NULL";
            if (_stateStack.Count > 0)
            {
                IState oldState = _stateStack.Peek();
                if (oldState == newState)
                {
                    return;
                }
                oldStateId = GetStateId(oldState);
                for (int i = 0; i < _stateEvent.Count; i++)
                {
                    _stateEvent[i].OnStatePrevChange(oldStateId, id);
                }

                _stateStack.Pop();
                oldState.OnStateLeave();
                //oldStateName = oldState.Name();
            }
            else
            {

                for (int i = 0; i < _stateEvent.Count; i++)
                {
                    _stateEvent[i].OnStatePrevChange(oldStateId, id);
                }
            }
            _stateStack.Push(newState);
            newState.OnStateEnter(userData);
            for (int i = 0; i < _stateEvent.Count; i++)
            {
                _stateEvent[i].OnStatePostChange(oldStateId, id);
            }
        }

        private IState GetState(uint id)
        {
            for (int i = 0; i < _registedState.Count; i++)
            {
                Data data = _registedState[i];

                if (data.Id == id)
                {
                    return data.State;
                }
            }

            return null;
        }

        private uint GetStateId(IState state)
        {
            for (int i = 0; i < _registedState.Count; i++)
            {
                Data data = _registedState[i];

                if (data.State == state)
                {
                    return data.Id;
                }
            }

            return InvalidID;
        }
    }
}
