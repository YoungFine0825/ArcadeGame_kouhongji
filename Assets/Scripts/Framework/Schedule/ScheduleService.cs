/********************************************************************
	created:	22:5:2018   
	filename: 	ScheduleService
	author:		jordenwu
	
	purpose:	时序调度器服务
*********************************************************************/
using System;
using JW.Common;
using UnityEngine;

namespace JW.Framework.Schedule
{
    /// <summary>
    /// 调度服务
    /// </summary>
    public class ScheduleService : MonoSingleton<ScheduleService>
    {
        /// <summary>
        /// 内部数据结构
        /// </summary>
        private struct Data
        {
            // 调度ID
            public uint ID;

            // 间隔（定时器：ms，帧：帧数）
            public int Interval;

            // 剩余（定时器：剩余时间ms，帧：剩余帧数）
            public int Remain;

            // 是否重复
            public bool Repeat;

            // 回调
            public IScheduleHandler Handler;
        }

        private uint _currentID = 1;

        private readonly JWArrayList<IScheduleHandler> _updates = new JWArrayList<IScheduleHandler>(50);
        private readonly JWArrayList<Data> _timers = new JWArrayList<Data>(20);
        private readonly JWArrayList<Data> _frames = new JWArrayList<Data>(20);

        public override bool Initialize()
        {
            return true;
        }

        public override void Uninitialize()
        {
            _currentID = 1;

            _updates.Clear();
            _timers.Clear();
            _frames.Clear();
        }

        /// <summary>
        /// 添加更新
        /// </summary>
        /// <param name="handler">更新回调接口</param>
        /// <param name="isFirst">添加到List头部，目前用于Time更新，其余情况不要使用</param>
        /// <returns></returns>
        public void AddUpdate(IScheduleHandler handler, bool isFirst = false)
        {
            if (handler == null)
            {
                return;
            }

            for (int i = 0; i < _updates.Count; i++)
            {
                if (_updates[i] == handler)
                {
                    return;
                }
            }

            if (isFirst)
            {
                _updates.Insert(0, handler);
            }
            else
            {
                _updates.Add(handler);
            }            
        }

        /// <summary>
        /// 移除更新
        /// </summary>
        /// <param name="handler">更新回调接口</param>
        /// <returns></returns>
        public void RemoveUpdate(IScheduleHandler handler)
        {
            if (handler == null)
            {
                return;
            }

            for (int i = 0; i < _updates.Count; i++)
            {
                if (_updates[i] == handler)
                {
                    _updates[i] = null;
                }
            }
        }

        /// <summary>
        /// 添加定时回调
        /// </summary>
        /// <param name="interval">定时器间隔，单位：ms</param>
        /// <param name="repeat">是否重复触发</param>
        /// <param name="handler">回调接口</param>
        /// <returns>定时器ID（==0添加失败）</returns>
        public uint AddTimer(int interval, bool repeat, IScheduleHandler handler)
        {
            if (interval <= 0 || handler == null)
            {
                return 0;
            }

            interval *= 100;

            Data data;
            data.ID = _currentID++;
            data.Interval = interval;
            data.Remain = interval;
            data.Repeat = repeat;
            data.Handler = handler;
            _timers.Add(data);

            return data.ID;
        }

        /// <summary>
        /// 移除定时回调
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void RemoveTimer(uint id)
        {
            Remove(_timers, id);
        }

        /// <summary>
        /// 移除回调接口相关的所有定时回调
        /// </summary>
        /// <param name="handler">回调接口</param>
        /// <returns></returns>
        public void RemoveTimer(IScheduleHandler handler)
        {
            Remove(_timers, handler);
        }

        /// <summary>
        /// 添加定帧回调
        /// </summary>
        /// <param name="interval">间隔帧数</param>
        /// <param name="repeat">是否重复</param>
        /// <param name="handler">回调接口</param>
        /// <returns>定帧ID（==0添加失败）</returns>
        public uint AddFrame(int interval, bool repeat, IScheduleHandler handler)
        {
            if (handler == null)
            {
                return 0;
            }

            Data data;
            data.ID = _currentID++;
            data.Interval = interval;
            data.Remain = interval;
            data.Repeat = repeat;
            data.Handler = handler;
            _frames.Add(data);

            return data.ID;
        }

        /// <summary>
        /// 移除定帧回调
        /// </summary>
        /// <param name="id">定帧ID</param>
        /// <returns></returns>
        public void RemoveFrame(uint id)
        {
            Remove(_frames, id);
        }

        /// <summary>
        /// 移除回调接口相关的所有定帧回调
        /// </summary>
        /// <param name="handler">回调接口</param>
        /// <returns></returns>
        public void RemoveFrame(IScheduleHandler handler)
        {
            Remove(_frames, handler);
        }

        /// <summary>
        /// Mono Behavior 驱动
        /// </summary>
        protected void Update()
        {
            // Update
            bool updateDiscard = false;

            int count = _updates.Count;
            for (int i = 0; i < count && i < _updates.Count; i++)
            {
                IScheduleHandler handler = _updates[i];
                if (handler != null)
                {
                    try
                    {
                        handler.OnScheduleHandle(ScheduleType.Updator, 0);
                    }
                    catch (Exception e)
                    {
                        JW.Common.Log.LogE("ScheduleService.Update : update exception {0} at handler {1}", e, handler.GetType().Name);
                        throw;
                    }
                }
                else
                {
                    updateDiscard = true;
                }
            }

            // Timer
            int deltaTime = (int)(Time.deltaTime * 100000.0f);

            bool timerDiscard = false;

            count = _timers.Count;
            for (int i = 0; i < count && i < _timers.Count; i++)
            {
                Data data = _timers[i];
                IScheduleHandler handler = data.Handler;
                if (handler == null)
                {
                    timerDiscard = true;
                    continue;
                }

                data.Remain -= deltaTime;
                if (data.Remain > 0)
                {
                    _timers[i] = data;
                    continue;
                }

                if (data.Repeat)
                {
                    data.Remain += data.Interval;
                }
                else
                {
                    data.Handler = null;
                }

                _timers[i] = data;

                try
                {
                    handler.OnScheduleHandle(ScheduleType.Timer, data.ID);
                }
                catch (Exception e)
                {
                    JW.Common.Log.LogE("ScheduleService.Update : timer exception {0} at handler {1}", e, handler.GetType().Name);
                    throw;
                }
            }

            // 定帧
            bool frameDiscard = false;

            count = _frames.Count;
            for (int i = 0; i < count && i < _frames.Count; i++)
            {
                Data data = _frames[i];
                IScheduleHandler handler = data.Handler;
                if (handler == null)
                {
                    frameDiscard = true;
                    continue;
                }

                --data.Remain;
                if (data.Remain > 0)
                {
                    _frames[i] = data;
                    continue;
                }

                if (data.Repeat)
                {
                    data.Remain = data.Interval;
                }
                else
                {
                    data.Handler = null;
                }

                _frames[i] = data;

                try
                {
                    handler.OnScheduleHandle(ScheduleType.Frame, data.ID);
                }
                catch (Exception e)
                {
                    JW.Common.Log.LogE("ScheduleService.Update : frame exception {0} at handler {1}", e, handler.GetType().Name);
                    throw;
                }
            }

            // remove discard
            if (updateDiscard)
            {
                for (int i = _updates.Count - 1; i >= 0; --i)
                {
                    if (_updates[i] == null)
                    {
                        _updates.RemoveAt(i);
                    }
                }
            }

            if (timerDiscard)
            {
                for (int i = _timers.Count - 1; i >= 0; --i)
                {
                    if (_timers[i].Handler == null)
                    {
                        _timers.RemoveAt(i);
                    }
                }
            }

            if (frameDiscard)
            {
                for (int i = _frames.Count - 1; i >= 0; --i)
                {
                    if (_frames[i].Handler == null)
                    {
                        _frames.RemoveAt(i);
                    }
                }
            }
        }

        private void Remove(JWArrayList<Data> datas, uint id)
        {
            for (int i = 0; i < datas.Count; i++)
            {
                if (datas[i].ID != id)
                {
                    continue;
                }

                Data data = datas[i];
                data.Handler = null;
                datas[i] = data;

                return;
            }
        }

        private void Remove(JWArrayList<Data> datas, IScheduleHandler handler)
        {
            for (int i = 0; i < datas.Count; i++)
            {
                if (datas[i].Handler != handler)
                {
                    continue;
                }

                Data data = datas[i];
                data.Handler = null;
                datas[i] = data;
            }
        }
    }
}
