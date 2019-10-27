/* 
 * This class aims to decoupling the event producer and event consumer. It uses delegates
 * and generics to provide type-checked events between event producers and
 * event consumers, without the need for producers or consumers to be aware of
 * each other.
 * Usage examples:
 * 1. EventRouter.Instance.AddEventHandler<GameObject>("underAttack", HandleUnderAttack);
 *    EventRouter.Instance.BroadcastEvent<GameObject>("underAttack", gameObj);
 *    EventRouter.Instance.RemoveEventHandler<GameObject>("underAttack", HandleUnderAttack);
 *    
 * 2. EventRouter.Instance.AddEventHandler<float, GameObject>("speedChanged", SpeedChanged);
 *    EventRouter.Instance.BroadcastEvent<float, GameObject>("speedChanged", 0.5f, gameObj);
 *    EventRouter.Instance.RemoveEventHandler<float, GameObject>("speedChanged", SpeedChanged);
 * Make sure the event handler is detached by calling EventRouter.Instance.RemoveEventHandler()
 * before its parent game object is destroyed. It's suggested that the attaching of handler is
 * done in OnEnable() while detaching of handler is done within OnDisable().
 * 
 * For more information, please refer to:
 * http://wiki.unity3d.com/index.php/CSharpMessenger
 * http://wiki.unity3d.com/index.php?title=CSharpMessenger_Extended
 * http://wiki.unity3d.com/index.php/Advanced_CSharp_Messenger
 */
using System;
using JW.Common;

namespace JW.Framework.Event
{
    public class EventRouter : Singleton<EventRouter>
    {
        public JWObjDictionary<string, Delegate> m_eventTable = new JWObjDictionary<string, Delegate>();

        public override bool Initialize()
        {
            return true;
        }

        public override void Uninitialize()
        {
            
        }

        public void AddEventHandler(string eventType, Action handler)
        {
            if (OnHandlerAdding(eventType, handler))
            {
                m_eventTable[eventType] = (Action)m_eventTable[eventType] + handler;
            }
        }

        public void AddEventHandler<T1>(string eventType, Action<T1> handler)
        {
            if (OnHandlerAdding(eventType, handler))
            {
                m_eventTable[eventType] = (Action<T1>)m_eventTable[eventType] + handler;
            }
        }

        public void AddEventHandler<T1, T2>(string eventType, Action<T1, T2> handler)
        {
            if (OnHandlerAdding(eventType, handler))
            {
                m_eventTable[eventType] = (Action<T1, T2>)m_eventTable[eventType] + handler;
            }
        }

        public void AddEventHandler<T1, T2, T3>(string eventType, Action<T1, T2, T3> handler)
        {
            if (OnHandlerAdding(eventType, handler))
            {
                m_eventTable[eventType] = (Action<T1, T2, T3>)m_eventTable[eventType] + handler;
            }
        }

        public void AddEventHandler<T1, T2, T3, T4>(string eventType, Action<T1, T2, T3, T4> handler)
        {
            if (OnHandlerAdding(eventType, handler))
            {
                m_eventTable[eventType] = (Action<T1, T2, T3, T4>)m_eventTable[eventType] + handler;
            }
        }

        public void RemoveEventHandler(string eventType, Action handler)
        {
            if (OnHandlerRemoving(eventType, handler))
            {
                m_eventTable[eventType] = (Action)m_eventTable[eventType] - handler;
            }
        }

        public void RemoveEventHandler<T1>(string eventType, Action<T1> handler)
        {
            if (OnHandlerRemoving(eventType, handler))
            {
                m_eventTable[eventType] = (Action<T1>)m_eventTable[eventType] - handler;
            }
        }

        public void RemoveEventHandler<T1, T2>(string eventType, Action<T1, T2> handler)
        {
            if (OnHandlerRemoving(eventType, handler))
            {
                m_eventTable[eventType] = (Action<T1, T2>)m_eventTable[eventType] - handler;
            }
        }

        public void RemoveEventHandler<T1, T2, T3>(string eventType, Action<T1, T2, T3> handler)
        {
            if (OnHandlerRemoving(eventType, handler))
            {
                m_eventTable[eventType] = (Action<T1, T2, T3>)m_eventTable[eventType] - handler;
            }
        }

        public void RemoveEventHandler<T1, T2, T3, T4>(string eventType, Action<T1, T2, T3, T4> handler)
        {
            if (OnHandlerRemoving(eventType, handler))
            {
                m_eventTable[eventType] = (Action<T1, T2, T3, T4>)m_eventTable[eventType] - handler;
            }
        }

        public void BroadCastEvent(string eventType)
        {
            if (OnBroadCasting(eventType))
            {
                Action callback = m_eventTable[eventType] as Action;

                if (callback != null)
                {
                    callback();
                }
                else
                {
                    JW.Common.Log.LogE("There is no handler registered for event [" + eventType + "]!");
                }
            }
        }

        public void BroadCastEvent<T1>(string eventType, T1 arg1)
        {
            if (OnBroadCasting(eventType))
            {
                Action<T1> callback = m_eventTable[eventType] as Action<T1>;

                if (callback != null)
                {
                    callback(arg1);
                }
                else
                {
                    JW.Common.Log.LogE("There is no handler registered for event [" + eventType + "]!");
                }
            }
        }

        public void BroadCastEvent<T1, T2>(string eventType, T1 arg1, T2 arg2)
        {
            if (OnBroadCasting(eventType))
            {
                Action<T1, T2> callback = m_eventTable[eventType] as Action<T1, T2>;

                if (callback != null)
                {
                    callback(arg1, arg2);
                }
                else
                {
                    JW.Common.Log.LogE("There is no handler registered for event [" + eventType + "]!");
                }
            }
        }

        public void BroadCastEvent<T1, T2, T3>(string eventType, T1 arg1, T2 arg2, T3 arg3)
        {
            if (OnBroadCasting(eventType))
            {
                Action<T1, T2, T3> callback = m_eventTable[eventType] as Action<T1, T2, T3>;

                if (callback != null)
                {
                    callback(arg1, arg2, arg3);
                }
                else
                {
                    JW.Common.Log.LogE("There is no handler registered for event [" + eventType + "]!");
                }
            }
        }

        public void BroadCastEvent<T1, T2, T3, T4>(string eventType, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (OnBroadCasting(eventType))
            {
                Action<T1, T2, T3, T4> callback = m_eventTable[eventType] as Action<T1, T2, T3, T4>;

                if (callback != null)
                {
                    callback(arg1, arg2, arg3, arg4);
                }
                else
                {
                    JW.Common.Log.LogE("There is no handler registered for event [" + eventType + "]!");
                }
            }
        }

        private bool OnHandlerAdding(string eventType, Delegate handler)
        {
            bool result = true;

            if (!m_eventTable.ContainsKey(eventType))
            {
                m_eventTable.Add(eventType, null);
            }

            Delegate d = m_eventTable[eventType];

            if (d != null && d.GetType() != handler.GetType())
            {
                JW.Common.Log.LogE("There is no handler registered for event [" + eventType + "]!");
                result = false;
            }

            return result;
        }

        private bool OnHandlerRemoving(string eventType, Delegate handler)
        {
            bool result = true;

            if (m_eventTable.ContainsKey(eventType))
            {
                Delegate d = m_eventTable[eventType];

                if (d != null)
                {
                    if (d.GetType() != handler.GetType())
                    {
                        JW.Common.Log.LogE("Failed to remove handler of type [" + handler.GetType().Name + "] for event [" + eventType + "] which is expected handler of type [" + d.GetType().Name + "]");
                        result = false;
                    }
                }
                else
                {
                    JW.Common.Log.LogE("Failed to remove handler of type [" + handler.GetType().Name + "] which was not found!");
                    result = false;
                }
            }
            else
            {
                JW.Common.Log.LogE("Failed to remove handler of type [ " + handler.GetType().Name + "], event [" + eventType + "] was not found!");
                result = false;
            }

            return result;
        }

        private bool OnBroadCasting(string eventType)
        {
            if (m_eventTable.ContainsKey(eventType))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ClearAllEvents()
        {
            m_eventTable.Clear();
        }


    }
}