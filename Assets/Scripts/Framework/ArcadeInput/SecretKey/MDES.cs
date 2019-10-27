using System.Security.Cryptography;
using System.IO;

namespace JW.Framework.ArcadeInput
{
    public class MDES
    {

        /// <summary>  
        /// C# DES解密方法  
        /// </summary>  
        /// <param name="encryptedValue">待解密的字符串</param>  
        /// <param name="key">密钥</param>  
        /// <param name="iv">向量</param>  
        /// <returns>解密后的字符串</returns>  
        public static byte[] DESDecrypt(byte[] encryptedValue, byte[] ikey)
        {
            using (DESCryptoServiceProvider sa =
                new DESCryptoServiceProvider { Key = ikey, Padding = PaddingMode.None, Mode = CipherMode.ECB })
            {
                //using (ICryptoTransform ct = sa.CreateDecryptor())
                {
                    byte[] byt = encryptedValue;
                    using (var ms = new MemoryStream(encryptedValue.Length))
                    {
                        using (var cs = new CryptoStream(ms, sa.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(byt, 0, byt.Length);
                            cs.FlushFinalBlock();
                        }
                        return ms.GetBuffer();//Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
        }
    }
}
