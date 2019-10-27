/********************************************************************
	created:	2018-05-28   
	filename: 	UIStateService
	author:		jordenwu
	
	purpose:	UI 状态管理
*********************************************************************/
using System;
using System.Collections.Generic;
using JW.Common;
using JW.Framework.State;
using JW.Framework.Asset;


namespace JW.Framework.MVC
{

    public delegate void ChangeUIStateHookDelegate(int callType, int changeType, string oldStateName, string newStateName, object stateParam);

    /// <summary>
    /// UI状态管理器
    /// </summary>
    public class UIStateService : MonoSingleton<UIStateService>
    {
        //状态直接切换
        public const int UIStateChangeTypeChange = 0;
        //压状态
        public const int UIStateChangeTypePush = 1;
        //推出顶状态
        public const int UIStateChangeTypePop = 2;

        //
        public ChangeUIStateHookDelegate Hook;

        /// <summary>
        /// UI中间件数据定义
        /// </summary>
        private struct MediatorData
        {
            public UIMediator Mediator;
            public string[] StateName;

            public bool IsBelongsState(string stateName)
            {
                if (string.IsNullOrEmpty(stateName) || StateName == null)
                {
                    return false;
                }

                for (int i = 0; i < StateName.Length; i++)
                {
                    if (StateName[i].Equals(stateName, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// 状态数据
        /// </summary>
        private struct StateData
        {
            public string StateName;
            public object Param;
        }

        // 所有Mediator
        private JWArrayList<MediatorData> _mediator;

        // 状态
        private JWArrayList<string> _stateHistory;
        private JWArrayList<StateData> _state;
        private JWArrayList<KeyValuePair<UIMediator, int>> _switchMediatorData;

        // 运行时
        private bool _switchState;

        /// <summary>
        /// 获取当前的UI状态名
        /// </summary>
        public string CurrentStateName
        {
            get
            {
                if (_state == null || _state.Count == 0)
                {
                    return string.Empty;
                }

                return _state[_state.Count - 1].StateName;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns>初始化成功/失败</returns>
        public override bool Initialize()
        {
            _mediator = new JWArrayList<MediatorData>();

            _stateHistory = new JWArrayList<string>();
            _state = new JWArrayList<StateData>();
            _switchMediatorData = new JWArrayList<KeyValuePair<UIMediator, int>>();
            return true;
        }

        /// <summary>
        /// 反初始化
        /// </summary>
        public override void Uninitialize()
        {
            _stateHistory = null;

            _state.Release();
            _state = null;

            _switchMediatorData.Release();
            _switchMediatorData = null;

            for (int i = 0; i < _mediator.Count; i++)
            {
                MediatorData data = _mediator[i];
                if (data.Mediator == null)
                {
                    continue;
                }

                JW.Common.Log.LogE("UIStateService.Uninitialize error - mediator not destroy {0}", data.Mediator.GetType().FullName);
            }

            _mediator.Release();
            _mediator = null;

        }

        /// <summary>
        /// 添加Mediator
        /// </summary>
        /// <param name="mediator">mediaotr</param>
        /// <param name="stateName">mediator所处的状态名数组</param>
        public void AddMediator(UIMediator mediator, string[] stateName)
        {
            if (mediator == null)
            {
                return;
            }

            for (int i = 0; i < _mediator.Count; i++)
            {
                if (_mediator[i].Mediator == mediator)
                {
                    JW.Common.Log.LogE("UIStateService.AddMediator error - duplicate mediator {0}", mediator.GetType().FullName);
                    return;
                }
            }

            MediatorData data;
            data.Mediator = mediator;
            data.StateName = stateName;

            _mediator.Add(data);
        }

        /// <summary>
        /// 移除Mediaotr
        /// </summary>
        /// <param name="mediator">mediator</param>
        public void RemoveMediator(UIMediator mediator)
        {
            if (mediator == null)
            {
                return;
            }

            for (int i = 0; i < _mediator.Count; i++)
            {
                if (_mediator[i].Mediator == mediator)
                {
                    _mediator.RemoveAt(i);
                    return;
                }
            }
        }

        /// <summary>
        /// 设置状态参数
        /// </summary>
        /// <param name="stateName">状态名</param>
        /// <param name="stateParam">状态参数</param>
        public void SetStateParam(string stateName, object stateParam)
        {
            if (string.IsNullOrEmpty(stateName))
            {
                JW.Common.Log.LogE("UIStateService.SetStateParam : invalid parameter");
                return;
            }

            for (int i = 0; i < _state.Count; i++)
            {
                StateData data = _state[i];
                if (stateName.Equals(data.StateName, StringComparison.OrdinalIgnoreCase))
                {
                    data.Param = stateParam;
                    _state[i] = data;
                    return;
                }
            }
        }

        /// <summary>
        /// 切换状态，并清空之前的状态
        /// </summary>
        /// <Param name="stateName">状态名</Param>
        /// <Param name="stateParam">状态参数</Param>
        public void ChangeState(string stateName, object stateParam = null)
        {
            if (string.IsNullOrEmpty(stateName))
            {
                return;
            }
            //正在切换
            if (_switchState)
            {
                JW.Common.Log.LogE("UIStateService.ChangeState : state is switching");
                return;
            }

            // 状态相同，无须跳转
            string oldStateName = _state.Count > 0 ? _state[_state.Count - 1].StateName : string.Empty;
            if (stateName.Equals(oldStateName, StringComparison.OrdinalIgnoreCase))
            {
                JW.Common.Log.LogD("UIStateService.ChangeState error - already in state {0}", stateName);
                return;
            }
            _switchState = true;
            // 跳转
            _state.Clear();
            StateData stateData;
            stateData.StateName = stateName;
            stateData.Param = stateParam;
            _state.Add(stateData);
            AddStateHistory(stateName);

            SwitchState(UIStateChangeTypeChange, oldStateName, stateName, stateParam);

            _switchState = false;
        }

        /// <summary>
        /// 压入状态
        /// </summary>
        /// <param name="stateName">状态名</param>
        /// <param name="stateParam">状态参数</param>
        /// <param name="allowLoadAsset">不要使用</param>
        /// <param name="oldStateName">不要使用</param>
        public bool PushState(string stateName, object stateParam = null, bool allowLoadAsset = false, string oldStateName = null)
        {
            if (string.IsNullOrEmpty(stateName))
            {
                return false;
            }

            if (_switchState)
            {
                JW.Common.Log.LogE("UIStateService.PushState : state is switching");
                return false;
            }

            // 状态相同，无须跳转
            if (string.IsNullOrEmpty(oldStateName))
            {
                oldStateName = _state.Count > 0 ? _state[_state.Count - 1].StateName : string.Empty;
            }

            if (stateName.Equals(oldStateName, StringComparison.OrdinalIgnoreCase))
            {
                JW.Common.Log.LogE("UIStateService.PushState error - already in state {0}", stateName);
                return false;
            }
            _switchState = true;

            JW.Common.Log.LogD("<color=yellow>UIStateService</color>--------PushState----->" + stateName);
            // 跳转
            for (int i = 0; i < _state.Count; i++)
            {
                if (_state[i].StateName.Equals(stateName, StringComparison.OrdinalIgnoreCase))
                {
                    _state.RemoveAt(i);
                    break;
                }
            }

            StateData stateData;
            stateData.StateName = stateName;
            stateData.Param = stateParam;
            _state.Add(stateData);
            AddStateHistory(stateName);

            SwitchState(UIStateChangeTypePush, oldStateName, stateName, stateParam);

            _switchState = false;

            return true;
        }

        /// <summary>
        /// 压入多个状态
        /// </summary>
        /// <param name="stateName">多个状态名（以/分割多个状态名）</param>
        /// <param name="lastStateParam">最后一个状态参数</param>
        public void PushMultiState(string stateName, object lastStateParam = null)
        {
            if (string.IsNullOrEmpty(stateName))
            {
                JW.Common.Log.LogE("UIStateService.PushMultiState : invalid parameter");
                return;
            }

            string[] stateNames = stateName.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (stateNames.Length == 0)
            {
                JW.Common.Log.LogE("UIStateService.PushMultiState : invalid parameter - {0}", stateName);
                return;
            }

            string oldStateName = _state.Count > 0 ? _state[_state.Count - 1].StateName : null;

            //
            for (int i = 0; i < stateNames.Length - 1; i++)
            {
                string sn = stateNames[i];
                if (string.IsNullOrEmpty(sn))
                {
                    continue;
                }

                for (int j = 0; j < _state.Count; j++)
                {
                    if (_state[j].StateName.Equals(sn, StringComparison.OrdinalIgnoreCase))
                    {
                        _state.RemoveAt(j);
                        break;
                    }
                }

                StateData stateData;
                stateData.StateName = sn;
                stateData.Param = null;
                _state.Add(stateData);
                AddStateHistory(sn);
            }

            //
            if (!PushState(stateNames[stateNames.Length - 1], lastStateParam, false, oldStateName))
            {
                JW.Common.Log.LogE("UIStateService.PushMultiState : push state failed - {0}", stateName);
                return;
            }

            //
            if (!_state[_state.Count-1].StateName.Equals(stateNames[stateNames.Length-1], StringComparison.OrdinalIgnoreCase))
            {
                JW.Common.Log.LogE("UIStateService.PushMultiState : unknown error - {0}", stateName);
            }
        }

        /// <summary>
        /// 弹出状态
        /// </summary>
        public void PopState(bool callChangeStateHandler = true, object newPopStateParam = null)
        {
            if (_switchState)
            {
                JW.Common.Log.LogE("UIStateService.PopState : state is switching");
                return;
            }

            if (_state.Count == 0)
            {
                return;
            }

            string oldStateName = _state[_state.Count - 1].StateName;
            string newStateName = string.Empty;
            object stateParam = null;
            if (_state.Count > 1)
            {
                int lastStateIndex = _state.Count - 2;
                newStateName = _state[lastStateIndex].StateName;
                
                if (newPopStateParam != null)
                {
                    StateData state = _state[lastStateIndex];
                    state.Param = newPopStateParam;
                    _state[lastStateIndex] = state;
                    stateParam = newPopStateParam;
                }
                else
                {
                    stateParam = _state[lastStateIndex].Param;
                }
            }

            _switchState = true;
            JW.Common.Log.LogD("<color=yellow>UIStateService</color>-------->PopUIState---->" + newStateName);
            // 跳转
            _state.RemoveAt(_state.Count - 1);
            AddStateHistory(newStateName);

            SwitchState(UIStateChangeTypePop, oldStateName, newStateName, stateParam);

            _switchState = false;
        }

        /// <summary>
        /// 清除状态
        /// </summary>
        /// <param name="stateName">待清除的状态，不能为栈顶的状态，=null时清除所有状态</param>
        public void ClearState(string stateName = null)
        {

            if (_state.Count == 0)
            {
                return;
            }

            string topStateName = _state[_state.Count - 1].StateName;
            if (string.IsNullOrEmpty(stateName))
            {
                _state.Clear();
                SwitchState(UIStateChangeTypeChange, topStateName, string.Empty, null);
                return;
            }

            if (stateName.Equals(topStateName, StringComparison.OrdinalIgnoreCase))
            {
                JW.Common.Log.LogE("UIStateService.ClearState : failed to clear top state - {0}", stateName);
                return;
            }

            for (int i = 0; i < _state.Count; i++)
            {
                if (stateName.Equals(_state[i].StateName, StringComparison.OrdinalIgnoreCase))
                {
                    _state.RemoveAt(i);
                    return;
                }
            }
        }

        //切换状态
        private void SwitchState(int changeType, string oldStateName, string newStateName, object stateParam)
        {
            // 新状态信息应用
            if (string.IsNullOrEmpty(newStateName))
            {
                JW.Common.Log.LogE("UIStateService.ChangeState error stateName");
                return;
            }

            // 通知Mediator
            _switchMediatorData.Clear();
            for (int i = 0; i < _mediator.Count; i++)
            {
                MediatorData mediatorData = _mediator[i];
                if (mediatorData.Mediator == null)
                {
                    continue;
                }

                bool oldState = mediatorData.IsBelongsState(oldStateName);
                bool newState = mediatorData.IsBelongsState(newStateName);
                if (oldState == newState)
                {
                    if (newState)
                    {
                        _switchMediatorData.Add(new KeyValuePair<UIMediator, int>(mediatorData.Mediator, 0));
                    }
                    continue;
                }

                if (oldState)
                {
                    _switchMediatorData.Add(new KeyValuePair<UIMediator, int>(mediatorData.Mediator, 1));
                }

                if (newState)
                {
                    _switchMediatorData.Add(new KeyValuePair<UIMediator, int>(mediatorData.Mediator, 2));
                }
            }
            // out
            for (int i = 0; i < _switchMediatorData.Count; i++)
            {
                KeyValuePair<UIMediator, int> data = _switchMediatorData[i];
                if (data.Value == 1)
                {
                    data.Key.ChangeState(changeType, false, oldStateName, newStateName, null);
                }
            }

            if (Hook!=null)
            {
                Hook(1, changeType, oldStateName, newStateName, stateParam);
            }


            //清理资源
            if (StateService.IsValidate())
            {
               AssetService.GetInstance().ClearUIStateCache(_stateHistory);
            }

            for (int i = 0; i < _switchMediatorData.Count; i++)
            {
                KeyValuePair<UIMediator, int> data = _switchMediatorData[i];
                if (data.Value == 2)
                {
                    data.Key.ChangeState(changeType, true, oldStateName, newStateName, stateParam);
                }
            }

            if (Hook != null)
            {
                Hook(2, changeType, oldStateName, newStateName, stateParam);
            }

            //stay
            for (int i = 0; i < _switchMediatorData.Count; i++)
            {
                KeyValuePair<UIMediator, int> data = _switchMediatorData[i];
                if (data.Value == 0)
                {
                    data.Key.RefreshState(changeType, oldStateName, newStateName, stateParam);
                }
            }

            if (Hook != null)
            {
                Hook(0, changeType, oldStateName, newStateName, stateParam);
            }

            JW.Common.Log.LogD("<color=yellow>UIStateService</color>--->Switch UI State from {0} to {1}<----", oldStateName, newStateName);
        }

        private void AddStateHistory(string stateName)
        {
            if (string.IsNullOrEmpty(stateName))
            {
                return;
            }

            for (int i = 0; i < _stateHistory.Count; i++)
            {
                if (_stateHistory[i].Equals(stateName, StringComparison.OrdinalIgnoreCase))
                {
                    _stateHistory.RemoveAt(i);
                    break;
                }
            }
            _stateHistory.Add(stateName);

            if (_stateHistory.Count > 100)
            {
                _stateHistory.RemoveAt(0);
            }
        }
    }
}
