/********************************************************************
	created:	2018-11-6
	author:		jordenwu
	
	purpose:	显示二维码图片组件
*********************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;
using JW.Framework.UGUI;

namespace JW.Framework.QR
{
    [RequireComponent(typeof(RawImage))]
    public class UIQRImage : UIComponent
    {
        public string QRUrl;
        //UGUI Image
        private RawImage _image;
        private Texture2D _qrTexture;

        public override void Initialize(UIForm formScript)
        {
            if (_isInited)
            {
                return;
            }

            base.Initialize(formScript);
            //
            _image = this.gameObject.GetComponent<RawImage>();
            //
            if (this.gameObject.activeInHierarchy && !string.IsNullOrEmpty(QRUrl))
            {
                LoadTexture(QRUrl);
            }
        }

        //显示
        public override void OnAppear()
        {
            if (this._image == null)
            {
                _image = this.gameObject.GetComponent<RawImage>();
            }
            if (_isInited && !string.IsNullOrEmpty(QRUrl))
            {
                LoadTexture(QRUrl);
            }
        }

        //关闭
        public override void OnClose()
        {
            if (_image != null)
            {
                _image.texture = null;
            }
            if (_qrTexture != null)
            {
                Destroy(_qrTexture);
                _qrTexture = null;
            }
            base.OnClose();
        }

        public void SetQRUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return;
            }

            QRUrl = url;
            if (_image != null)
            {
                LoadTexture(QRUrl);
            }
        }

        public void SetNativeSize()
        {
            if (_image != null)
            {
                _image.SetNativeSize();
            }
        }

      
        private void LoadTexture(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                JW.Common.Log.LogE("Qr url is Error");
                return;
            }
            //释放以前
            if (_qrTexture != null)
            {
                Destroy(_qrTexture);
                _qrTexture = null;
            }
            //
            //RectTransform rt = this.transform as RectTransform;
            //Vector2 size = rt.sizeDelta;
            //
            Texture2D tt = new Texture2D(256, 256);
            //tt.alphaIsTransparency = true;
            tt.filterMode = FilterMode.Bilinear;
            tt.wrapMode = TextureWrapMode.Clamp;
            //
            QRCodeUtil.CreateQRCodeTexture2D(QRUrl,ref tt);
            //
            if (_image != null)
            {
                _image.texture = tt;
                _qrTexture = tt;
            }
        }

#if UNITY_EDITOR
        [ContextMenu("生成二维码")]
        private void TestShow()
        {
            if (this._image == null)
            {
                _image = this.gameObject.GetComponent<RawImage>();
            }
            LoadTexture(QRUrl);
        }
#endif
    };
};