/********************************************************************
	created:	17:5:2018   
	filename: 	MemoryOperator
	author:		jordenwu
	
	purpose:	内存数据操作器
*********************************************************************/
using System;
namespace JW.Common
{
    public class MemoryOperator
    {
        /// <summary>
        /// 写入字节数据
        /// </summary>
        /// <param name="value">写入值</param>
        /// <param name="data">数据区</param>
        /// <param name="offset">偏移</param>
        public static void WriteByte(byte value, byte[] data, ref int offset)
        {
            data[offset] = (byte)value;

            offset++;
        }

        /// <summary>
        /// 写入short数据[高高低低]
        /// </summary>
        /// <param name="value"></param>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        public static void WriteShort(short value, byte[] data, ref int offset)
        {
            data[offset] = (byte)value;
            data[offset + 1] = (byte)(value >> 8);
            offset += 2;
        }

        /// <summary>
        /// 写入int数据[高高低低]
        /// </summary>
        /// <param name="value"></param>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        public static void WriteInt(int value, byte[] data, ref int offset)
        {
            data[offset] = (byte)value;
            data[offset + 1] = (byte)(value >> 8);
            data[offset + 2] = (byte)(value >> 16);
            data[offset + 3] = (byte)(value >> 24);
            offset += 4;
        }

        /// <summary>
        /// 写入long数据[高高低低]
        /// </summary>
        /// <param name="value"></param>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        public static void WriteLong(long value, byte[] data, ref int offset)
        {
            int value1 = (int)value;
            int value2 = (int)(value >> 32);

            WriteInt(value1, data, ref offset);
            WriteInt(value2, data, ref offset);
        }

        /// <summary>
        /// 读出byte数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static byte ReadByte(byte[] data, ref int offset)
        {
            byte value = data[offset];

            offset++;

            return value;
        }

        /// <summary>
        /// 读出short数据[高高低低]
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static short ReadShort(byte[] data, ref int offset)
        {
            short value = (short)((data[offset + 1] << 8) | data[offset]);

            offset += 2;

            return value;
        }

        /// <summary>
        /// 读出int数据[高高低低]
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static int ReadInt(byte[] data, ref int offset)
        {
            int value = ((data[offset + 3] << 24) | (data[offset + 2] << 16) | (data[offset + 1] << 8) | data[offset]);

            offset += 4;

            return value;
        }

        /// <summary>
        /// 读出long数据[高高低低]
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static long ReadLong(byte[] data, ref int offset)
        {
            int value1 = ReadInt(data, ref offset);
            int value2 = ReadInt(data, ref offset);

            return (long)((ulong)(((long)value2) << 32) | (ulong)((long)value1));
        }

        /// <summary>
        /// 写入字符串数据按UTF-8编码，长度占2字节
        /// </summary>
        /// <param name="str"></param>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        public static void WriteString(string str, byte[] data, ref int offset)
        {
            //写入字符串数据
            int length = System.Text.Encoding.UTF8.GetBytes(str, 0, str.Length, data, offset + 2);
            //写入字符串数据长度
            WriteShort((short)length, data, ref offset);

            offset += length;
        }

        /// <summary>
        /// 读入字符串数据按UTF-8编码，长度占2字节
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static string ReadString(byte[] data, ref int offset)
        {
            //读出字符串数据长度
            int length = ReadShort(data, ref offset);

            //读出字符串
            string str = System.Text.Encoding.UTF8.GetString(data, offset, length);

            offset += length;

            return str;
        }

        public static void WriteBytes(byte[] src, int length, byte[] dst, ref int offset)
        {
            Buffer.BlockCopy(src, 0, dst, offset, length);
            offset += length;
        }

        public static void ReadBytes(byte[] dst, int length, byte[] src, ref int offset)
        {
            Buffer.BlockCopy(src, offset, dst, 0, length);
            offset += length;
        }

        /// <summary>
        /// 写入DateTime
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        public static void WriteDateTime(ref DateTime dateTime, byte[] data, ref int offset)
        {
            byte[] buffer = BitConverter.GetBytes(dateTime.Ticks);

            for (int i = 0; i < buffer.Length; i++)
            {
                data[offset] = buffer[i];
                offset++;
            }
        }

        /// <summary>
        /// 读入DateTime
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static DateTime ReadDateTime(byte[] data, ref int offset)
        {
            long ticks = BitConverter.ToInt64(data, offset);
            offset += 8;

            if (ticks < DateTime.MaxValue.Ticks && ticks > DateTime.MinValue.Ticks)
            {
                DateTime dateTime = new DateTime(ticks);
                return dateTime;
            }

            return new DateTime();
        }
    };
}

