/********************************************************************
	created:	20:11:2017   15:27
	author:		jordenwu
	purpose:	日期时间工具
*********************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace JW.Common
{
    public static class DateTimeHelper
    {
        private static readonly DateTime _localOrgTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static readonly DateTime _localOrgTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc) + new TimeSpan(TimeZoneOffSetSecs * TimeSpan.TicksPerSecond);
        public const int TimeZoneOffSetSecs = 8 * 3600; // 东八区时间相对UTC的偏移
        public const ulong SecsPerDay = 24 * 3600; // 一天的秒数
        public const ulong SecsPerHour = 3600;
        public const ulong SecsPerMin = 60;

        /// <summary>
        /// 相对于UTC 的时间偏移秒
        /// </summary>
        /// <returns></returns>
        public static int GetTimeZoneOffSetSecs()
        {
            return TimeZoneOffSetSecs;
        }


        /// <summary>
        /// 获取本地日期
        /// </summary>
        /// <returns></returns>
        public static DateTime CurrentLocalDateTime
        {
            get { return DateTime.Now; }
        }


        /// <summary>
        /// 获取本地utc时间
        /// </summary>
        /// <returns></returns>
        public static long CurrentLocalUTCTime
        {
            get { return GetTimeTicksSecond(DateTime.Now); }
        }

        /// <summary>
        /// 当前时间偏差1970的秒数
        /// </summary>
        public static ulong DiffLocalTimeSec
        {
            get
            {
                DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
                DateTime local = CurrentLocalDateTime;
                TimeSpan diff = local - epoch;
                ulong diffSec = (ulong)Math.Floor(diff.TotalSeconds);
                return diffSec;
            }
        }

        /// <summary>
        /// 根据时间差计算还剩多少时间
        /// </summary>
        /// <param name="leftTime">时间差</param>
        /// <param name="leftDay">天</param>
        /// <param name="leftHour">时</param>
        /// <param name="leftMinute">分</param>
        /// <param name="leftSec">秒</param>
        public static void CalculateLeftTime(ulong leftTime, ref int leftDay, ref int leftHour, ref int leftMinute, ref int leftSec)
        {
            leftDay = leftHour = leftMinute = leftSec = 0;
            if (leftTime > 0)
            {
                leftDay = (int)(leftTime / SecsPerDay);
                leftTime = leftTime % SecsPerDay;
                leftHour = (int)(leftTime / SecsPerHour);
                leftTime = leftTime % SecsPerHour;
                leftMinute = (int)(leftTime / SecsPerMin);
                leftSec = (int)(leftTime % SecsPerMin);
            }
        }

        /// <summary>
        /// 根据时间差计算还剩多少时间
        /// </summary>
        /// <param name="leftTime">时间差</param>
        /// <param name="leftDay">天</param>
        /// <param name="leftHour">时</param>
        /// <param name="leftMinute">分</param>
        /// <param name="leftSec">秒</param>
        public static void CalculateLeftTimeLua(long leftTime, out int leftDay, out int leftHour, out int leftMinute, out int leftSec)
        {
            int _leftDay = 0, _leftHour = 0, _leftMinute = 0, _leftSec = 0;
            CalculateLeftTime((ulong)leftTime, ref _leftDay, ref _leftHour, ref _leftMinute, ref _leftSec);
            leftDay = _leftDay;
            leftHour = _leftHour;
            leftMinute = _leftMinute;
            leftSec = _leftSec;
        }


        /// <summary>
        /// 返回自1970年1月1日 00:00:00 GMT 的时间戳
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public static long GetTimeTicksSecond(DateTime time)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(_localOrgTimeUtc);
            TimeSpan toNow = time.Subtract(dtStart);
            return (long)(toNow.TotalSeconds);
        }

        /// <summary>
        /// 从时间戳获取对应时间
        /// </summary>
        /// <param name="timeTicks">时间戳(秒)</param>
        /// <returns></returns>
        public static DateTime GetDateTimeBySecondTicks(long timeTicks)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(_localOrgTimeUtc);
            DateTime dtRst = dtStart.AddSeconds(timeTicks);
            return dtRst;
        }


    }
}

