using EarmarkProduceTerminalBLL;
using UnityEngine;
using System;
using System.Text;

namespace JW.Framework.ArcadeInput
{
    public class ReadKey
    {
        static public string GetKey()
        {
            byte[] key = null;
            //默认用低配置街机的 密钥读取方式
            key = ReadKeyOf4302A.GetKey();
            if (key == null)
            {
                //高配置街机 是另外的一个密钥设备
                key = ReadKeyOf5300.GetKey();
            }
            //
            if (key != null)
            {
                string gameKey = "";
                for (int i = 0; i < key.Length; i++)
                {
                    gameKey += Chr(key[i]);
                }
                return gameKey;
            }
            return null;
        }

        public static string Chr(int asciiCode) /*ASCII 转化为 字符*/
        {
            if (asciiCode >= 0 && asciiCode <= 255)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                byte[] byteArray = new byte[] { (byte)asciiCode };
                string strCharacter = asciiEncoding.GetString(byteArray);
                return (strCharacter);
            }
            else
            {
                throw new Exception("ASCII Code is not valid.");
            }
        }

    }
}
