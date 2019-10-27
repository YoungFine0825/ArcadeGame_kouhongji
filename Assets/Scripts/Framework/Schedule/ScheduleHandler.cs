/********************************************************************
	created:	22:5:2018   
	filename: 	ScheduleHandler
	author:		jordenwu
	
	purpose:	调度接口
*********************************************************************/
namespace JW.Framework.Schedule
{
    /// <summary>
    /// 调度类型
    /// </summary>
    public enum ScheduleType
    {
        Timer,      // 定时
        Frame,      // 定帧
        Updator     // 每帧更新
    }

    /// <summary>
    /// 调度回调接口
    /// </summary>
    public interface IScheduleHandler
    {
        /// <summary>
        /// 调度函数
        /// </summary>
        /// <param name="type">调度类型</param>
        /// <param name="id">调度ID</param>
        void OnScheduleHandle(ScheduleType type, uint id);
    }

    /// <summary>
    /// 辅助方法 添加直接支持
    /// </summary>
    public static class ExtIScheduleHandler
    {
        public static void AddUpdate(this IScheduleHandler handler, bool isFirst = false)
        {
            ScheduleService.GetInstance().AddUpdate(handler, isFirst);
        }

        public static void RemoveUpdate(this IScheduleHandler handler)
        {
            ScheduleService.GetInstance().RemoveUpdate(handler);
        }

        public static uint AddTimer(this IScheduleHandler handler, int interval, bool repeat)
        {
            return ScheduleService.GetInstance().AddTimer(interval, repeat, handler);
        }

        public static void RemoveTimer(this IScheduleHandler handler, uint id)
        {
            ScheduleService.GetInstance().RemoveTimer(id);
        }

        public static void RemoveTimer(this IScheduleHandler handler)
        {
            ScheduleService.GetInstance().RemoveTimer(handler);
        }

        public static uint AddFrame(this IScheduleHandler handler, int interval, bool repeat)
        {
            return ScheduleService.GetInstance().AddFrame(interval, repeat, handler);
        }

        public static void RemoveFrame(this IScheduleHandler handler, uint id)
        {
            ScheduleService.GetInstance().RemoveFrame(id);
        }

        public static void RemoveFrame(this IScheduleHandler handler)
        {
            ScheduleService.GetInstance().RemoveFrame(handler);
        }

        public static void RemoveAllSchedule(this IScheduleHandler handler)
        {
            handler.RemoveUpdate();
            handler.RemoveTimer();
            handler.RemoveFrame();
        }
    }
}
