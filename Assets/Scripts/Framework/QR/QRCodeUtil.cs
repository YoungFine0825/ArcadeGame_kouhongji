/********************************************************************
	created:	2018-10-16
	author:		jordenwu
	
	purpose:	二维码工具
*********************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace JW.Framework.QR
{


    public static class QRCodeUtil
    {
        private static Color32[] Encode(string textForEncoding, int width, int height)
        {
            BarcodeWriter writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    //				DisableECI = true,  
                    //				CharacterSet = "UTF-8",
                    Height = height,
                    Width = width,
                    Margin = 0,
                    ErrorCorrection = ErrorCorrectionLevel.H
                }
            };
            return writer.Write(textForEncoding);
        }

        public static void CreateQRCodeTexture2D(string url, ref Texture2D texture2D, int codePix=256)
        {
            if (texture2D == null)
            {
                JW.Common.Log.LogE("CreateQRCodeTexture2D Error Texture Ref");
                return;
            }
            if (!string.IsNullOrEmpty(url))
            {
                int cut = (256 - codePix) / 2;
                int count = 0;
                Color32[] color32 = Encode(url, 256, 256);
                Color32[] color = new Color32[codePix * codePix];
                for (int i = 0; i < 256; i++)
                {
                    if (i < cut || i >= 256 - cut)
                    {
                        continue;
                    }
                    for (int j = 0; j < 256; j++)
                    {
                        if (j < cut || j >= 256 - cut)
                        {
                            continue;
                        }
                        color[count++] = color32[i * 256 + j];
                    }
                }
                //根据转换来的32位颜色值来计算二维码的像素
                texture2D.SetPixels32(color);
                //生成二维码
                texture2D.Apply();
            }
            else
            {
                JW.Common.Log.LogE("CreateQRCodeTexture2D Error Url");
            }
        }

    }

}
