using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace EarmarkProduceTerminalBLL
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct SCARD_IO_REQUEST
    {
        public uint dwProtocol;
        public uint cbPciLength;
    }

    public class PCSCReader
    {
        private bool m_isConnect = false;
        [DllImport("Winscard.dll")]
        private static extern int SCardEstablishContext(uint dwScope, uint nNotUsed1, uint nNotUsed2, ref UInt64 phContext);

        [DllImport("Winscard.dll")]
        private static extern int SCardReleaseContext(UInt64 phContext);

        [DllImport("Winscard.dll")]
        private static extern int SCardConnect(UInt64 hContext, string cReaderName, uint dwShareMode, uint dwPrefProtocol, ref UInt64 phCard, ref uint ActiveProtocol);

        [DllImport("Winscard.dll")]
        private static extern int SCardReconnect(UInt64 hContext, uint dwShareMode, uint dwPrefProtocol,  uint dwInitialization, ref uint ActiveProtocol); 
        
        [DllImport("Winscard.dll")]
        private static extern int SCardDisconnect(UInt64 hCard, uint Disposition);

        [DllImport("Winscard.dll")]
        private static extern int SCardListReaderGroups(UInt64 hContext, ref string cGroups, ref int nStringSize);

        [DllImport("Winscard.dll")]
        private static extern int SCardListReaders(UInt64 hContext, string groups, IntPtr cReaderLists, ref int nReaderCount);

        [DllImport("Winscard.dll")]
        private static extern int SCardFreeMemory(UInt64 hContext, string cResourceToFree);

        [DllImport("Winscard.dll")]
        private static extern int SCardGetAttrib(UInt64 hContext, uint dwAttrId, ref byte[] bytRecvAttr, ref int nRecLen);

        [DllImport("Winscard.dll")]
        private static extern int SCardTransmit(UInt64 hCard, ref SCARD_IO_REQUEST pioSendPci, byte[] pbSendBuffer, int cbSendLength, int pioRecvPci, byte[] pbRecvBuffer, ref int pcbRecvLength);  //IntPtr pioRecvPci 
        
        [DllImport("Winscard.dll")]
        private static extern int SCardState(UInt64 hCard, ref int state, ref uint protocol, byte[] pbAtr, ref int atrlen);
        
        [DllImport("WINSCARD.DLL")]
        private static extern int SCardBeginTransaction(UInt64 hCard);

        //[DllImport("Kernel32")]
        //public static extern IntPtr GetProcAddress(IntPtr handle, String funcname);

        //[DllImport("Kernel32")]
        //public static extern IntPtr LoadLibrary(String funcname);

        //[DllImport("Kernel32")]
        //public static extern int FreeLibrary(IntPtr handle);


        public PCSCReader()
        {
            //  Initialize();
        }
       

        private static UInt64 _phContext = 0;
        private static UInt64 _hCard = 0;
        private bool _isOpen = false;
        private static uint _active = 0;
        private string _readername = "";
        public bool IsOpen
        {
            get { return _isOpen; }
        }


       

        public bool Initialize()
        {
            int flag = SCardEstablishContext(0, 0, 0, ref _phContext);
            return flag == 0;
        }

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


        public static string[] GetPCSCReaderList()
        {
            int size = 2000;
            IntPtr readername = Marshal.AllocHGlobal(size);
            int flag = SCardListReaders(_phContext, null, readername, ref size);
            if (flag == 0)
            {
                byte[] buf = new byte[size];
                Marshal.Copy(readername, buf, 0, size);
                Marshal.FreeHGlobal(readername);
                string names = Encoding.Default.GetString(buf);
                string[] tmp =  names.Split('\0');
                List<string> nlist = new List<string>();
                foreach (string s in tmp)
                {
                    if (s.Trim() != "") nlist.Add(s);
                }
                return nlist.ToArray();
            }
            else
            {
                return null;
            }
        }

        public bool ConnectReader(string rname)
        {
            _readername = rname;
            int flag = SCardDisconnect(_hCard, 0);
            if (flag != 0)
            {
                _hCard = 0x00;
            }
            m_isConnect = SCardConnect(_phContext, rname, 1, 3, ref _hCard, ref _active) == 0;
            return m_isConnect;
        }

        public bool DisConnect()
        {
            int flag = SCardDisconnect(_hCard, 0);
            if (flag == 0)
            {
                _isOpen = false;
                _hCard = 0;
                return true;
            }
            m_isConnect = false;
            return false;
        }

        public /*byte[]*/ int SendCommand(byte[] cmd , byte[] Read)
        {
            SCARD_IO_REQUEST isend;
            isend.cbPciLength = 8;
            isend.dwProtocol =  _active;
            int rlength =2000;
         //   byte[] temp = new byte[rlength];

            int flag = SCardTransmit(_hCard, ref isend, cmd, cmd.Length , 0, Read , ref rlength);
            if (flag != 0x00)
                return 0;          
            return rlength;
        }

        public int ResetReader(byte[] atr)
        {
            uint protocol = _active;
            int atrlen = 2000;
            int state = 0;

            SCardReconnect(_hCard, 1, 3, 2, ref _active); //对读卡器进行断电重连
            int flag = SCardState(_hCard, ref state, ref protocol,atr, ref atrlen); //获取ATR信息
            if (flag != 0x00)
                return 0;

            return atrlen;
        }

        public bool IsConnect
        {
            get
            {
                return m_isConnect;
            }

        }
    }
}
