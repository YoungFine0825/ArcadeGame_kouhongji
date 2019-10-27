using System;
using JW.Common;
using JW.Framework.Asset;

namespace JW.Framework.MVC
{
    public abstract class UIMediator
    {
        protected ModuleBase Module;

        // 管理的窗口列表
        private JWArrayList<UIFormClass> _forms;

        // 是否在UIMediator所属的状态中
        private bool _inState;

#region 外部不要调用
        /// <summary>
        /// 创建，外部不要调用
        /// </summary>
        public void Create(ModuleBase module)
        {
            Module = module;

            _forms = new JWArrayList<UIFormClass>(1);
            _inState = false;
            //注入到UI状态服务
            UIStateService.GetInstance().AddMediator(this, GetBelongsUIStateName());

            OnInitialize();
        }

        /// <summary>
        /// 销毁对象，外部不要调用
        /// </summary>
        public void Destroy()
        {
            OnUninitialize();

            UIStateService.GetInstance().RemoveMediator(this);

            for (int i = 0; i < _forms.Count; i++)
            {
                if (_forms[i] != null)
                {
                    JW.Common.Log.LogE("UIMediator.Destroy : FormClass {0} of {1} is living", _forms[i].GetType().FullName, GetType().FullName);
                }
            }
            _inState = false;
            _forms.Clear();
            Module = null;
        }
    
        //延迟清理
        public void ProcessControl()
        {
            for (int i = _forms.Count - 1; i >= 0; --i)
            {
                if (_forms[i] == null)
                {
                    _forms.RemoveAt(i);
                    continue;
                }
            }
        }

        /// UIStateService UI状态回掉，外部不要调用
        public void ChangeState(int changeType, bool inState, string oldStateName, string newStateName, object stateParam)
        {
            if (inState)
            {
                _inState = true;
                OnUIStateIn(changeType, oldStateName, newStateName, stateParam);
            }
            else
            {
                _inState = false;
                OnUIStateOut(changeType, oldStateName, newStateName);
            }
        }

        /// UIStateService 刷新状态，外部不要调用
        public void RefreshState(int changeType, string oldStateName, string newStateName, object stateParam)
        {
            OnUIStateStay(changeType, oldStateName, newStateName, stateParam);
        }

#endregion

        /// <summary>
        /// 更新UI
        /// </summary>
        /// <param name="id">更新ID标识</param>
        /// <param name="param">更新参数</param>
        public void UpdateUI(string id, object param)
        {
            if (OnUpdateUI(id, param))
            {
                return;
            }

            for (int i = _forms.Count - 1; i >= 0; --i)
            {
                if (_forms[i] != null)
                {
                    _forms[i].UpdateUI(id, param);
                }
            }
        }

        /// <summary>
        /// 操作
        /// </summary>
        /// <param name="id">操作ID标识</param>
        /// <param name="param">操作参数</param>
        /// <param name="sponsor">是否是操作发起者，调用时可以不用管这个参数</param>
        public void Action(string id, object param, bool sponsor = true)
        {
            if (!sponsor)
            {
                if (OnAction(id, param))
                {
                    return;
                }
            }

            if (Module != null)
            {
                Module.Action(id, param);
            }
        }

        /// <summary>
        /// 所属的UIState，可以重载，不重载时表明该Mediator不属于任何状态
        /// </summary>
        /// <returns>uistate的数组</returns>
        protected virtual string[] GetBelongsUIStateName()
        {
            return null;
        }

        /// <summary>
        /// 当初始化时调用
        /// </summary>
        protected virtual void OnInitialize()
        {
        }

        /// <summary>
        /// 当销毁时调用调用
        /// </summary>
        protected virtual void OnUninitialize()
        {
        }

        /// <summary>
        /// 当UIState切入时调用
        /// </summary>
        /// <param name="changeType">切换类型</param>
        /// <param name="oldStateName">老状态名，可能为空和null</param>
        /// <param name="newStateName">新状态名，可能为空和null</param>
        /// <param name="stateParam">状态参数</param>
        protected virtual void OnUIStateIn(int changeType, string oldStateName, string newStateName, object stateParam)
        {
        }

        /// <summary>
        /// 当UIState切出时调用
        /// <param name="changeType">切换类型</param>
        /// <param name="oldStateName">老状态名，可能为空和null</param>
        /// <param name="newStateName">新状态名，可能为空和null</param>
        /// </summary>
        protected virtual void OnUIStateOut(int changeType, string oldStateName, string newStateName)
        {
        }

        /// <summary>
        /// 状态刷新时调用
        /// <param name="changeType">切换类型</param>
        /// <param name="oldStateName">老状态名，可能为空和null</param>
        /// <param name="newStateName">新状态名，可能为空和null</param>
        /// <param name="stateParam">状态参数</param>
        /// </summary>
        protected virtual void OnUIStateStay(int changeType, string oldStateName, string newStateName, object stateParam)
        {
        }


        /// <summary>
        /// 更新UI
        /// </summary>
        /// <param name="id">更新ID标识</param>
        /// <param name="param">更新参数</param>
        /// <returns>是否阻断向下传递刷新</returns>
        protected virtual bool OnUpdateUI(string id, object param)
        {
            return false;
        }

        /// <summary>
        /// 操作
        /// </summary>
        /// <param name="id">操作ID标识</param>
        /// <param name="param">操作参数</param>
        /// <returns>是否阻断向上传递操作</returns>
        protected virtual bool OnAction(string id, object param)
        {
            return false;
        }

        /// <summary>
        /// 获取是否在UIMediator所属的状态中
        /// </summary>
        /// <returns>是否在UIMediator所属的状态中</returns>
        protected bool IsInState()
        {
            return _inState;
        }

        /// <summary>
        /// 创建窗口对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="usePool">是否使用对象池</param>
        /// <param name="customID">自定义ID</param>
        /// <param name="parameter">预制件初始化参数</param>
        /// <returns>预制件对象</returns>
        protected T CreateFormClass<T>(bool usePool, int customID = 0, object parameter = null) where T : UIFormClass, new()
        {
            T prefabClass = UIFormHelper.CreateFormClass<T>(usePool, this, customID, parameter);
            if (prefabClass == null)
            {
                JW.Common.Log.LogE("UIMediator.CreatePrefabClass : failed to create {0}", typeof(T).FullName);
                return null;
            }

            _forms.Add(prefabClass);
            return prefabClass;
        }

        /// 销毁窗口对象
        protected void DisposeFormClass<T>(ref T prefabClass) where T : UIFormClass, new()
        {
            if (null == prefabClass)
            {
                return;
            }

            for (int i = 0; i < _forms.Count; i++)
            {
                if (_forms[i] == prefabClass)
                {
                    _forms[i] = null;
                    break;
                }
            }
            //todo 这里应该等Form FadeOut动画完才能销毁
            UIFormHelper.DisposeFormClass(ref prefabClass);
        }


        /// <summary>
        /// 删除所有的窗口
        /// </summary>
        protected void DestroyAllFormClass()
        {
            for (int i = 0; i < _forms.Count; i++)
            {
                UIFormClass pc = _forms[i];
                if (pc == null)
                {
                    continue;
                }
                _forms[i] = null;
                UIFormHelper.DisposeFormClass(pc);
            }
        }
    }
}

