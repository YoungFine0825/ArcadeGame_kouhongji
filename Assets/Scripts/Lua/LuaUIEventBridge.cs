/********************************************************************
	created:	2018-07-04
	filename: 	LuaEventBridge
	author:		jordenwu
	
	purpose:	Lua 事件对接
*********************************************************************/
using System.Collections.Generic;
using JW.Common;
using JW.Framework.Event;
using JW.Framework.UGUI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using XLua;

namespace JW.Lua
{
    public class LuaUIEventBridge
    {
        public delegate void OnUIEventDelegate(int id);
        public delegate void OnUIEventListnerDelegate(int id, PointerEventData dd);

        private class EventData
        {
            private System.Action<int> _callback;
            private System.Action<int,PointerEventData> _callbackListener;

            private int _id;
            private bool _oneShot;

            public int ID
            {
                get
                {
                    return _id;
                }
            }

            public void Create(System.Action<int> callback, UnityEvent list, int id, bool oneShot)
            {
                _callback = callback;
                _id = id;
                _oneShot = oneShot;
                list.AddListener(OnUIEvent);
            }

            public void Create(System.Action<int,PointerEventData> callback, UIListenerEvent list, int id, bool oneShot)
            {
                _callbackListener = callback;
                _id = id;
                _oneShot = oneShot;
                list.AddListener(OnUIEventListener);
            }

            public void Destroy(UnityEvent list)
            {
                _callback = null;
                _id = 0;
                _oneShot = false;
                if (list != null)
                {
                    list.RemoveListener(OnUIEvent);
                }
            }

            public void DestroyListener(UIListenerEvent list)
            {
                _callbackListener = null;
                _id = 0;
                _oneShot = false;
                if (list != null)
                {
                    list.RemoveListener(OnUIEventListener);
                }
            }

            private void OnUIEvent()
            {
                System.Action<int> cb = _callback;
                int id = _id;

                if (_oneShot)
                {
                    Destroy(null);
                }
                else
                {
                    if (cb != null && id > 0)
                    {
                        cb(id);
                    }
                }

            }

            private void OnUIEventListener(PointerEventData dd)
            {
                System.Action<int,PointerEventData> cb = _callbackListener;
                int id = _id;
                if (_oneShot)
                {
                    DestroyListener(null);
                }
                else
                {
                    if (cb != null && id > 0)
                    {
                        cb(id,dd);
                    }
                }
            }

        }

        private CircleBuffer<EventData> _eventDataCache;
        private JWObjList<EventData> _eventData;

        private OnUIEventDelegate _onUIEvent;
        private OnUIEventListnerDelegate _onUIEventListener;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize(LuaEnv lua)
        {
            _eventDataCache = new CircleBuffer<EventData>(100);
            _eventData = new JWObjList<EventData>();
            _onUIEvent = lua.Global.GetInPath<OnUIEventDelegate>("EventService.OnUIEvent");
            _onUIEventListener = lua.Global.GetInPath<OnUIEventListnerDelegate>("EventService.OnUIEventListener");
        }

        /// <summary>
        /// 反初始化
        /// </summary>
        public void UnInitialize()
        {
            if (_eventData != null)
            {
                for (int i = 0; i < _eventData.Count; i++)
                {
                    if (_eventData[i].ID > 0)
                    {
                        JW.Common.Log.LogE("LuaEvent.Uninitialize error : event data is not empty");
                        _eventData[i].Destroy(null);
                    }
                }
                _eventData.Clear();
                _eventData = null;
            }
            if (_eventDataCache != null)
            {
                _eventDataCache.Clear();
                _eventDataCache = null;
            }
            _onUIEvent = null;
        }

        /// <summary>
        /// 添加UI事件回调
        /// </summary>
        /// <param name="list">UI事件</param>
        /// <param name="id">回调ID</param>
        /// <param name="oneShot">是否回调一次</param>
        public void AddUIEventHandle(UnityEvent list, int id, bool oneShot)
        {
            if (list == null || id <= 0)
            {
                return;
            }
            EventData data;
            if (!_eventDataCache.Pop(out data))
            {
                data = new EventData();
            }
            data.Create(OnUIEventCSCall, list, id, oneShot);
            _eventData.Add(data);
        }

        /// <summary>
        /// 添加UI事件回调
        /// </summary>
        /// <param name="list">UI事件</param>
        /// <param name="id">回调ID</param>
        /// <param name="oneShot">是否回调一次</param>
        public void AddUIEventListenerHandle(UIListenerEvent list, int id, bool oneShot)
        {
            if (list == null || id <= 0)
            {
                return;
            }
            EventData data;
            if (!_eventDataCache.Pop(out data))
            {
                data = new EventData();
            }
            data.Create(OnUIEventListenerCSCall, list, id, oneShot);
            _eventData.Add(data);
        }


        //隔离
        private void OnUIEventCSCall(int id)
        {
            if (_onUIEvent != null)
            {
                _onUIEvent(id);
            }
        }


        //隔离
        private void OnUIEventListenerCSCall(int id,PointerEventData pd)
        {
            if (_onUIEventListener != null)
            {
                _onUIEventListener(id,pd);
            }
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="list">事件</param>
        /// <param name="id">回调ID</param>
        public void RemoveUIEventHandle(UnityEvent list, int id)
        {
            if (list == null || id <= 0)
            {
                return;
            }
            for (int i = 0; i < _eventData.Count; i++)
            {
                EventData data = _eventData[i];
                if (data.ID != id)
                {
                    continue;
                }
                data.Destroy(list);
                break;
            }
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="list">事件</param>
        /// <param name="id">回调ID</param>
        public void RemoveUIEventListenerHandle(UIListenerEvent list, int id)
        {
            if (list == null || id <= 0)
            {
                return;
            }
            for (int i = 0; i < _eventData.Count; i++)
            {
                EventData data = _eventData[i];
                if (data.ID != id)
                {
                    continue;
                }
                data.DestroyListener(list);
                break;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            if (_eventData == null) return;
            if (_eventDataCache == null) return;
            for (int i = _eventData.Count - 1; i >= 0; i--)
            {
                EventData data = _eventData[i];
                if (data.ID > 0)
                {
                    continue;
                }
                _eventData.RemoveAt(i);
                _eventDataCache.Push(data);
            }
        }
    }
}

