using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace EarmarkProduceTerminalBLL
{ 
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public class LKT5300Reader {

		[DllImport("LKT5103U")]  //为什么不是5300U.Dll呢？ 因为那个是32位的，
        private static extern int  EK_Open(int p);

        [DllImport("LKT5103U")]
        private static extern int EK_Close();

        [DllImport("LKT5103U")]
        private static extern int EK_Reset(out int AtrLen, IntPtr atr);

        [DllImport("LKT5103U")]
        private static extern int EK_Exchange_APDU(int CmdLen, IntPtr cmd, IntPtr ResBuf, IntPtr ResLen);

        private int f_Connected = 0;

        private static int AtrL;

        public int Ascii2Hex(string str0, byte[] dat0) //字符型数组 转化成16进制数组
        {
            int leth, i, j, k ;
            char h ,l;
            string h_t, l_t ;

            leth = str0.Length;
            if (leth % 2 != 0 || leth < 10 )
            {
                return 0;  
            }
            leth >>= 1;

            for (k = 0; k < leth; k++)
            {
                h_t= str0.Substring(2*k, 1);  //url.substring(1,2)
                l_t = str0.Substring(2 * k + 1, 1);

                i = 16;
                j = 16;

                h = Convert.ToChar(h_t);
                l = Convert.ToChar(l_t);

                if ((h <= '9') && (h >= '0'))
                    i = (h - '0');
                else if ((h <= 'F') && (h >= 'A'))
                    i = (h - 'A' + 10);
                else
                {
                    return 0;
                }

                if ((l <= '9') && (l >= '0'))
                    j = (l - '0');
                else if ((l <= 'F') && (l >= 'A'))
                    j = (l - 'A' + 10);

                else return 0;
                dat0[k] = Convert.ToByte(((i << 4) + j));
                
            }
            return (leth);
        }

         public string Hex2Ascii( byte[] Read)//Hex  转换成  Ascii 
         {
             string str = BitConverter.ToString(Read);
             Console.WriteLine(str);

             str = BitConverter.ToString(Read).Replace("-", "");
             Console.WriteLine(str);
             return str;
         }

        public int ConnectReader()
        {
            f_Connected = EK_Open(1);   //LKT5300U.dll中的连接接口
            return f_Connected;
        }

        public int DisConnect()
        {
            int res;
            res = EK_Close();   //LKT5300U.dll中的断开连接接口
            if (0 != res)
                return res;
            else
                return 0;
        }

        public int SendCommand(int cmdlen, byte[] cmd, byte[] res, int[] reslen)        
        {
            int r;
            r = EK_Exchange_APDU(cmdlen, Marshal.UnsafeAddrOfPinnedArrayElement(cmd, 0),
                Marshal.UnsafeAddrOfPinnedArrayElement(reslen, 0), Marshal.UnsafeAddrOfPinnedArrayElement(res, 0));//LKT5300U.dll中的发送APDU接口
            if (0 != r)
                return r;
            else
                return 0;
        }

        public int ResetReader(byte[] atr)
        {
            int res;
            res = EK_Reset(out AtrL, Marshal.UnsafeAddrOfPinnedArrayElement(atr, 0));   //LKT5300U.dll中的复位接口
            if (0 != res)
                return res;
            else
            return AtrL;
        }
	}
}
