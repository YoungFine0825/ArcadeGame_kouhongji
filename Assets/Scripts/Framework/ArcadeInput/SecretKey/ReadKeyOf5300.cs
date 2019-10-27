using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EarmarkProduceTerminalBLL;
using System;
using System.Text;

namespace JW.Framework.ArcadeInput
{
    public class ReadKeyOf5300
    {

        static public byte[] GetKey()
        {

            LKT5300Reader reader = new LKT5300Reader();

            try
            {
                if (reader.ConnectReader() == 1)
                {
                    byte[] key1Buf = Encoding.Default.GetBytes(new char[] { '$', 'b', '#', 'D', '0', '*', '!', 'X', '9', 'Q', '1', 'O', 'S', '3', '7', 'c' });
                    byte[] key3Buf = Encoding.Default.GetBytes(new char[] { '!', 'C', 'm', 'P', 'P', 'H', '3', 'b' });
                    byte[] cmdHaedText = new byte[6] { 0x80, 0x08, 0x00, 0x00, 0x11, 0x10 };
                    byte[] cmdDateText = new byte[16];//{0x12, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88};

                    System.Random rand = new System.Random(unchecked((int)DateTime.Now.Ticks));
                    rand.NextBytes(cmdDateText);
                    cmdDateText[0] = 0x12;

                    byte[] ecmdDateText = AES.Encrypt(cmdDateText, key1Buf, AES.Mode.ECB, null, AES.Padding.NONE);

                    // for(int i=0; i<ecmdDateText.Length; i++)
                    // {
                    // 	Debug.Log("ecmdDateText = " + i+ "  " + ecmdDateText[i].ToString("x"));
                    // }

                    byte[] sendDataBuf = new byte[cmdHaedText.Length + ecmdDateText.Length];
                    //Debug.Log("sendDataBuf.Length = "+ sendDataBuf.Length);
                    Buffer.BlockCopy(cmdHaedText, 0, sendDataBuf, 0, cmdHaedText.Length);//这种方法仅适用于字节数组
                    Buffer.BlockCopy(ecmdDateText, 0, sendDataBuf, cmdHaedText.Length, ecmdDateText.Length);

                    // for(int i=0; i<sendDataBuf.Length; i++)
                    // {
                    // 	Debug.Log("1111111111sendDataBuf = " + i+ "  " + sendDataBuf[i].ToString("X"));
                    // }
                    byte[] resBuf = new byte[500];
                    reader.ResetReader(resBuf);


                    int[] reslen = new int[2];
                    byte[] revBuf = new byte[100];

                    string sendstr = reader.Hex2Ascii(sendDataBuf);
                    //Debug.Log(" sendstr="+ sendstr);	

                    int r = reader.SendCommand(sendDataBuf.Length, sendDataBuf, revBuf, reslen);

                    if (r == 0)
                    {
                        byte[] v_res = new byte[reslen[0] + reslen[1]];
                        Array.Copy(revBuf, 0, v_res, 0, reslen[0] + reslen[1]);


                        string str = reader.Hex2Ascii(v_res);

                        //Debug.Log(" ============="+ str);

                        byte[] ogameKeyBuf = new byte[24];
                        Buffer.BlockCopy(revBuf, 1, ogameKeyBuf, 0, ogameKeyBuf.Length);

                        byte[] deogameKeyBuf = MDES.DESDecrypt(ogameKeyBuf, key3Buf);


                        // for(int i=0; i<cmdDateText.Length; i++)
                        // {
                        // 	Debug.Log("44444444444444444444cmdDateText = " + i+ "  " + cmdDateText[i].ToString("X"));
                        // }

                        // for(int i=0; i<deogameKeyBuf.Length; i++)
                        // {
                        // 	Debug.Log("------------------------------------------gameKey = " + i+ "  " + deogameKeyBuf[i].ToString("X"));
                        // }

                        if ((deogameKeyBuf[16] == cmdDateText[0]) && (deogameKeyBuf[17] == cmdDateText[1]) && (deogameKeyBuf[18] == cmdDateText[2]) && (deogameKeyBuf[19] == cmdDateText[3]))
                        {
                            reader.DisConnect();
                            byte[] gameKeyBuf = new byte[16];
                            Buffer.BlockCopy(deogameKeyBuf, 0, gameKeyBuf, 0, 16);

                            string gameKeyBufstr = reader.Hex2Ascii(gameKeyBuf);

                            //Debug.Log(" gameKeyBuf = "+ gameKeyBufstr);

                            return gameKeyBuf;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(" Exception = " + e);
            }
            finally
            {
                reader.DisConnect();

            }
            return null;
        }

    }
}
