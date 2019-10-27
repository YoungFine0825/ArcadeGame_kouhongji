/********************************************************************
	created:	22:5:2018   
	filename: 	EventService
	author:		jordenwu
	
	purpose:	事件中心服务
*********************************************************************/
using JW.Common;

namespace JW.Framework.Event
{
    public class EventService : Singleton<EventService>
    {
        //事件处理
        private JWArrayList<EventHandler> _eventHandler;

        public override bool Initialize()
        {
            _eventHandler = new JWArrayList<EventHandler>(200, 8);

            return true;
        }

        public override void Uninitialize()
        {
            _eventHandler.Clear();
        }

        /// <summary>
        /// 添加事件接收器
        /// </summary>
        /// <param name="id">事件ID</param>
        /// <param name="instance">事件接收对象</param>
        /// <param name="methodName">事件接收函数名</param>
        public void AddEventHandler(uint id, object instance, string methodName)
        {
            if (instance == null || methodName == null)
            {
                return;
            }

            for (int i = 0; i < _eventHandler.Count; i++)
            {
                EventHandler h = _eventHandler[i];
                if (h.IsEqual(id, instance))
                {
                    return;
                }
            }

            EventHandler handler = new EventHandler();
            if (!handler.Create(id, instance, methodName))
            {
                JW.Common.Log.LogE("EventService.AddEventHandler error - id:{0} instance:{1} methodName:{2}", id, instance, methodName);
                return;
            }

            _eventHandler.Add(handler);
        }

        /// <summary>
        /// 移除事件接收器
        /// </summary>
        /// <param name="instance">事件接收对象</param>
        public void RemoveEventHandler(object instance)
        {
            if (instance == null)
            {
                return;
            }

            for (int i = 0; i < _eventHandler.Count; i++)
            {
                EventHandler handler = _eventHandler[i];
                if (handler.IsEqual(instance))
                {
                    handler.Destroy();
                    _eventHandler[i] = handler;
                }
            }
        }

        /// <summary>
        /// 移除事件接收器
        /// </summary>
        /// <param name="id">事件ID</param>
        /// <param name="instance">事件接收对象</param>
        public void RemoveEventHandler(uint id, object instance)
        {
            if (instance == null)
            {
                return;
            }

            for (int i = 0; i < _eventHandler.Count; i++)
            {
                EventHandler handler = _eventHandler[i];
                if (handler.IsEqual(id, instance))
                {
                    handler.Destroy();
                    _eventHandler[i] = handler;
                    return;
                }
            }
        }

        /// <summary>
        /// 同步发送事件
        /// </summary>
        /// <param name="id">事件ID</param>
        /// <param name="arg">事件参数</param>
        public void SendEvent(uint id, EventArg arg)
        {
            bool discard = false;
            int count = _eventHandler.Count;
            for (int i = 0; i < count && i < _eventHandler.Count; i++)
            {
                EventHandler handler = _eventHandler[i];
                if (!handler.Valid)
                {
                    discard = true;
                    continue;
                }

                if (!handler.IsEqual(id))
                {
                    continue;
                }

                handler.Invoke(ref arg);
            }

            if (discard)
            {
                for (int i = _eventHandler.Count-1; i >= 0; --i)
                {
                    if (!_eventHandler[i].Valid)
                    {
                        _eventHandler.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// 同步发送事件 无事件参数
        /// </summary>
        /// <param name="id">事件ID</param>
        public void SendEvent(uint id)
        {
            EmptyEventArg arg;
            SendEvent(id, arg);
        }
    }
}
