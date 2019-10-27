/********************************************************************
	created:	17:5:2018   
	filename: 	BitOperator
	author:		jordenwu
	
	purpose:	位操作器封装
*********************************************************************/
using System;
namespace JW.Common
{
    public static class BitOperator
    {
        /// <summary>
        /// 设置位
        /// </summary>
        /// <param name="data">待设置的数据</param>
        /// <param name="pos">位位置</param>
        /// <param name="value">设置的值</param>
        public static void SetBitByPos(ref uint data, int pos, bool value)
        {
            if (value)
            {
                data |= (uint) 1 << pos;
            }
            else
            {
                data &= ~((uint) 1 << pos);
            }
        }

        /// <summary>
        /// 设置位
        /// </summary>
        /// <param name="data">待设置的数据</param>
        /// <param name="mask">位掩码</param>
        /// <param name="value">设置的值</param>
        public static void SetBitByMask(ref uint data, uint mask, bool value)
        {
            if (value)
            {
                data |= mask;
            }
            else
            {
                data &= ~mask;
            }
        }

        /// <summary>
        /// 设置位
        /// </summary>
        /// <param name="data">待设置的数据</param>
        /// <param name="mask">位掩码</param>
        /// <param name="value">设置的值</param>
        public static void SetBitByData(ref uint data, uint mask, uint value)
        {
            data &= ~mask;
            data |= value & mask;
        }

        /// <summary>
        /// 获取位
        /// </summary>
        /// <param name="data">获取的数据源</param>
        /// <param name="pos">位位置</param>
        /// <returns>位值</returns>
        public static bool GetBitByPos(uint data, int pos)
        {
            return (data & ((uint) 1 << pos)) != 0;
        }

        /// <summary>
        /// 获取位
        /// </summary>
        /// <param name="data">获取的数据源</param>
        /// <param name="mask">掩码</param>
        /// <returns>位值</returns>
        public static bool GetBitByMask(uint data, uint mask)
        {
            return (data & mask) != 0;
        }

        /// <summary>
        /// 循环左移
        /// </summary>
        /// <param name="x">待左移的值</param>
        /// <param name="nBits">移动位数</param>
        /// <returns>最终结果</returns>
        public static uint RotateLeft(uint x, int nBits)
        {
            nBits &= 0x1f;
            return (x << nBits) | (x >> (32 - nBits));
        }

        /// <summary>
        /// 循环左移
        /// </summary>
        /// <param name="x">待左移的值</param>
        /// <param name="nBits">移动位数</param>
        /// <returns>最终结果</returns>
        public static UInt64 RotateLeft(this UInt64 x, int nBits)
        {
            nBits &= 0x3f;
            return (x << nBits) | (x >> (64 - nBits));
        }

        /// <summary>
        /// 循环右移
        /// </summary>
        /// <param name="x">待右移的值</param>
        /// <param name="nBits">移动位数</param>
        /// <returns>最终结果</returns>
        public static uint RotateRight(this uint x, int nBits)
        {
            nBits &= 0x1f;
            return (x >> nBits) | (x << (32 - nBits));
        }

        /// <summary>
        /// 循环右移
        /// </summary>
        /// <param name="x">待右移的值</param>
        /// <param name="nBits">移动位数</param>
        /// <returns>最终结果</returns>
        public static UInt64 RotateRight(this UInt64 x, int nBits)
        {
            nBits &= 0x3f;
            return (x >> nBits) | (x << (64 - nBits));
        }
    }
}
