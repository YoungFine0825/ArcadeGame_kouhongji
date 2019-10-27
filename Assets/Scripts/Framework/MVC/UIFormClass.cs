using JW.Common;
using JW.Framework.Asset;
using UnityEngine;
using JW.Framework.UGUI;

namespace JW.Framework.MVC
{
    /// <summary>
    /// 窗口类
    /// </summary>
    public abstract class UIFormClass : UIFormAsset
    {
        //计数
        private static int _instanceCount;
        private readonly string _className;
        private readonly int _instanceID;
        private int _customID;

        /// 获取类名
        public string ClassName
        {
            get
            {
                return _className;
            }
        }

        /// 获取实例ID
        public int InstanceID
        {
            get
            {
                return _instanceID;
            }
        }

        /// <summary>
        /// 获取自定义ID
        /// </summary>
        public int CustomID
        {
            get
            {
                return _customID;
            }
        }

        // 当前创建预制件挂接组件
        protected UIForm Form;

        // 所在的模块Mediator
        protected UIMediator Mediator;

        //构造函数
        protected UIFormClass()
        {
            _className = GetType().Name;
            _instanceID = ++_instanceCount;
        }

        #region 窗口控制

        public void ActiveForm(bool active)
        {
            if (Form != null)
            {
                Form.SetActive(active);
            }
        }

        #endregion



        #region 外部不要调用

        /// <summary>
        /// 创建，请不要使用
        /// </summary>
        /// <param name="mediator">所在的Mediator</param>
        /// <param name="customID">自定义ID</param>
        /// <param name="parameter">初始化参数</param>
        public void Create(UIMediator mediator, int customID, object parameter)
        {
            Mediator = mediator;
            _customID = customID;
            //打开窗口 加入
            UGUIRoot.GetInstance().OpenForm(Form);
            // 逻辑初始化
            OnInitialize(parameter);
        }

        /// <summary>
        /// 销毁，请不要使用
        /// </summary>
        public void Destroy()
        {
            // 逻辑反初始化
            OnUninitialize();
            // 关闭窗口 开始淡出动画
            UGUIRoot.GetInstance().CloseForm(Form);
            //
            Mediator = null;
        }

        //这里才是真正的移除并销毁或回收
        public void OnFormRemoved()
        {
            AssetService.GetInstance().Unload(this);
            Form = null;
        }

        /// <summary>
        /// 资源池创建
        /// </summary>
        public sealed override void OnFormAssetCreate()
        {
            if (RootGo == null)
            {
                JW.Common.Log.LogE("UIPrefabClass.PoolCreate error - failed to create : {0}", GetPath());
                return;
            }
            //获取组件
            Form = RootGo.ExtGetComponent<UIForm>();
            if (Form == null)
            {
                JW.Common.Log.LogE("UIPrefabClass.PoolCreate error - failed to get UIPrefab component : {0}", GetPath());
                return;
            }
            Form.FormPath = GetPath();
            Form.Controller = this;
            OnResourceLoaded();
        }

        /// <summary>
        /// 资源池销毁对象
        /// </summary>
        public sealed override void OnFormAssetDestroy()
        {
            OnResourceUnLoaded();
            if (Form != null)
            {
                Form.Controller = null;
                Form = null;
            }
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
        }

        /// <summary>
        /// 操作
        /// </summary>
        /// <param name="id">操作ID标识</param>
        /// <param name="param">操作参数</param>
        /// <param name="sponsor">内部使用，标识是否是操作发起者，调用时不管这个参数</param>
        public void Action(string id, object param, bool sponsor = true)
        {
            if (!sponsor)
            {
                if (OnAction(id, param))
                {
                    return;
                }
            }
            if (Mediator != null)
            {
                Mediator.Action(id, param, false);
            }
        }

        /// <summary>
        /// 资源加载后回调
        /// </summary>
        protected virtual void OnResourceLoaded()
        {
        }

        /// <summary>
        /// 资源卸载后回调
        /// </summary>
        protected virtual void OnResourceUnLoaded()
        {
        }

        /// <summary>
        /// 逻辑初始化
        /// </summary>
        /// <param name="parameter">初始化参数</param>
        protected virtual void OnInitialize(object parameter)
        {
        }

        /// <summary>
        /// 逻辑反初始化
        /// </summary>
        protected virtual void OnUninitialize()
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
            return true;
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
        /// 添加自身的组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <returns>组件</returns>
        protected T AddComponent<T>() where T : Component
        {
            return null != RootGo ? RootGo.ExtAddComponent<T>() : null;
        }

        /// <summary>
        /// 获取自身的组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <returns>组件</returns>
        protected T GetComponent<T>() where T : Component
        {
            return null != RootGo ? RootGo.ExtGetComponent<T>() : null;
        }

        /// <summary>
        /// 获取组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="index">index下标</param>
        /// <returns>组件</returns>
        protected T GetComponent<T>(int index) where T : Component
        {
            Component com = Form.GetCacheComponent(index);
            if (com == null)
            {
                GameObject gameObject = GetGameObject(index);
                com = gameObject.ExtGetComponent<T>();
            }

            return com ? com as T : null;
        }

        /// <summary>
        /// 获取组件
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected Component GetComponent(int index)
        {
            return Form.GetCacheComponent(index);
        }

        /// <summary>
        /// 获取组件
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected Component GetComponent(int index,string typeName)
        {
            return Form.GetCacheComponent(index,typeName);
        }

        /// <summary>
        /// 获取Transform
        /// </summary>
        /// <param name="index">index下标</param>
        /// <returns>Transform</returns>
        protected Transform GetTransform(int index)
        {
            return Form.GetCacheTransform(index);
        }

        /// <summary>
        /// 获取GameObject
        /// </summary>
        /// <param name="index">index下标</param>
        /// <returns>GameObject</returns>
        protected GameObject GetGameObject(int index)
        {
            return Form.GetCacheGameObject(index);
        }

    }
}

