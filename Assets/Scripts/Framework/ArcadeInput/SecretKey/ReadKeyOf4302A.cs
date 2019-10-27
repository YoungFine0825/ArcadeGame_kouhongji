using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using EarmarkProduceTerminalBLL;

namespace JW.Framework.ArcadeInput
{
    public class ReadKeyOf4302A
    {
        static public byte[] GetKey()
        {
            PCSCReader pcscReader = new PCSCReader();
            pcscReader.Initialize();
            string[] readList;
            readList = PCSCReader.GetPCSCReaderList();
            if (readList != null)
            {
                string len;
                len = readList.Length.ToString();
                if (pcscReader.ConnectReader(readList[0]) == true)
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


                    byte[] revBuf = new byte[100];
                    pcscReader.SendCommand(sendDataBuf, revBuf);

                    // for(int i=0; i<revBuf.Length; i++)
                    // {
                    // 	Debug.Log("revBuf = " + i+ "  " + revBuf[i].ToString("X"));
                    // }
                    if (revBuf[1] == 0x19)
                    {
                        byte[] revCmdBuf = { 0x00, 0xC0, 0x00, 0x00, 0x19 };
                        pcscReader.SendCommand(revCmdBuf, revBuf);


                        // for(int i=0; i<revBuf.Length; i++)
                        // {
                        // 	Debug.Log("3333333333333333333333revBuf = " + i+ "  " + revBuf[i].ToString("X"));
                        // }

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
                            pcscReader.DisConnect();
                            byte[] gameKeyBuf = new byte[16];
                            Buffer.BlockCopy(deogameKeyBuf, 0, gameKeyBuf, 0, 16);

                            return gameKeyBuf;
                        }

                        //byte[] retBuf = new byte[16];
                        //Buffer.BlockCopy(gameKey, 0, retBuf, 0, 16);

                        pcscReader.DisConnect();

                        return null;
                    }
                }
            }
            pcscReader.DisConnect();
            return null;
        }
    }
}
