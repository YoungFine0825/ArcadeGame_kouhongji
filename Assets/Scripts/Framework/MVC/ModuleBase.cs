using JW.Common;
using JW.Framework.Asset;

namespace JW.Framework.MVC
{
    public abstract class ModuleBase
    {
        private readonly JWArrayList<UIMediator> _allMediator = new JWArrayList<UIMediator>(2, 1);

        /// <summary>
        /// 创建
        /// </summary>
        public void Create()
        {
            OnInitializeModule();
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Destroy()
        {
            for (int i = 0; i < _allMediator.Count; i++)
            {
                _allMediator[i].Destroy();
            }

            _allMediator.Clear();

            OnUninitializeModule();
        }

        /// <summary>
        /// 定时流程控制
        /// </summary>
        public void ProcessControl()
        {
            for (int i = 0; i < _allMediator.Count; i++)
            {
                if (_allMediator[i] != null)
                {
                    _allMediator[i].ProcessControl();
                }
            }
        }

        /// <summary>
        /// 创建Mediator
        /// </summary>
        /// <typeparam name="T">Mediator类型</typeparam>
        public T CreateMediator<T>() where T : UIMediator, new()
        {
            T mediator = new T();
            mediator.Create(this);

            _allMediator.Add(mediator);
            return mediator;
        }

        /// <summary>
        /// 操作
        /// </summary>
        /// <param name="id">操作ID标识</param>
        /// <param name="param">操作参数</param>
        public void Action(string id, object param)
        {
            OnAction(id, param);
        }

        /// <summary>
        /// 更新UI
        /// </summary>
        /// <param name="id">更新ID标识</param>
        /// <param name="param">更新参数</param>
        protected void UpdateUI(string id, object param)
        {
            for (int i = 0; i < _allMediator.Count; i++)
            {
                _allMediator[i].UpdateUI(id, param);
            }
        }

        /// <summary>
        /// 初始化模块
        /// </summary>
        protected abstract void OnInitializeModule();

        /// <summary>
        /// 反初始化模块
        /// </summary>
        protected abstract void OnUninitializeModule();

        /// <summary>
        /// 游戏状态进入
        /// </summary>
        /// <param name="reason">跳转原因，取值Because×××</param>
        /// <param name="state">进入的状态</param>
        /// <param name="fromState">上一个状态</param>
        /// <param name="userData">自定义数据</param>
        public virtual void OnStateEnter(string state, string fromState, object userData)
        {
        }

        /// <summary>
        /// 游戏状态离开
        /// </summary>
        /// <param name="reason">跳转原因，取值Because×××</param>
        /// <param name="state">离开的状态</param>
        /// <param name="toState">下一个状态</param>
        /// <param name="userData">自定义数据</param>
        public virtual void OnStateLeave(string state, string toState, object userData)
        {
        }

        /// <summary>
        /// 游戏状态离开栈顶
        /// </summary>
        /// <param name="state">离开栈顶的状态</param>
        /// <param name="toState">下一个状态</param>
        /// <param name="userData">自定义数据</param>
        public virtual void OnStateOverride(string state, string toState, object userData)
        {
        }

        /// <summary>
        /// 游戏状态恢复到栈顶
        /// </summary>
        /// <param name="state">恢复到栈顶的状态</param>
        /// <param name="fromState">上一个状态</param>
        /// <param name="userData">自定义数据</param>
        public virtual void OnStateResume(string state, string fromState, object userData)
        {
        }

        /// <summary>
        /// 操作
        /// </summary>
        /// <param name="id">操作ID标识</param>
        /// <param name="param">操作参数</param>
        protected virtual void OnAction(string id, object param)
        {
        }

    }
}
