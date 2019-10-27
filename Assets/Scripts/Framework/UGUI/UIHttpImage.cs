using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;
using JW.Framework.NetAsset;

namespace JW.Framework.UGUI
{
    public enum UIHttpImageState
    {
        Unload,
        Loading,
        Loaded
    };

    [RequireComponent(typeof(Image))]
    public class UIHttpImage : UIComponent
    {

        public string ImageUrl;

        public bool IsSetNativeSize;
        
        public bool IsCacheTexture = true;

        public float CachedTextureValidDays = 2f;

        public bool IsForceSetImageUrl = false;

        public GameObject LoadingCover;

        public int CachedNetAssetWidth = 0;
        public int CachedNetAssetHeight = 0;

        //UGUI Image
        private Image _image;
        private Sprite _imageDefaultSprite;

        private UIHttpImageState _httpImageState = UIHttpImageState.Unload;

        public override void Initialize(UIForm formScript)
        {
            if (_isInited)
            {
                return;
            }

            base.Initialize(formScript);
            _image = this.gameObject.GetComponent<Image>();
            _imageDefaultSprite = _image.sprite;
            _httpImageState = UIHttpImageState.Unload;

            if (LoadingCover != null)
            {
                LoadingCover.SetActive(true);
            }
            if (this.gameObject.activeInHierarchy && !string.IsNullOrEmpty(ImageUrl))
            {
                LoadTexture(ImageUrl);
            }
        }

        void OnEnable()
        {
            if(this._image == null)
            {
                _image = this.gameObject.GetComponent<Image>();
                _imageDefaultSprite = _image.sprite;
            }
            if (_isInited && _httpImageState == UIHttpImageState.Unload && !string.IsNullOrEmpty(ImageUrl))
            {
                LoadTexture(ImageUrl);
            }
        }

        void OnDisable()
        {
            if (_isInited && _httpImageState == UIHttpImageState.Loading)
            {
                StopAllCoroutines();
                _httpImageState = UIHttpImageState.Unload;
                if (LoadingCover != null)
                {
                    LoadingCover.SetActive(true);
                }
            }
        }


        public void SetImageUrl(string url)
        {
            if (string.IsNullOrEmpty(url) || (!IsForceSetImageUrl && string.Equals(url, ImageUrl)))
            {
                return;
            }

            ImageUrl = url;

            if (_image != null)
            {
                _image.sprite=_imageDefaultSprite;
            }

            if (this.gameObject.activeInHierarchy && _httpImageState == UIHttpImageState.Loading)
            {
                StopAllCoroutines();
            }

            _httpImageState = UIHttpImageState.Unload;

            if (LoadingCover != null)
            {
                LoadingCover.SetActive(true);
            }

            if (this.gameObject.activeInHierarchy)
            {
               LoadTexture(ImageUrl);
            }
        }

        public void SetSprite(Sprite ss)
        {
            if (_image != null)
            {
                _image.sprite = ss;
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
            if (_httpImageState == UIHttpImageState.Loaded)
            {
                return;
            }

            if (IsCacheTexture)
            {
                Texture2D texture2D;
                if (CachedNetAssetWidth != 0 && CachedNetAssetHeight!=0)
                {
                    texture2D = NetAssetService.GetInstance().GetCachedNetImage(url, CachedTextureValidDays,CachedNetAssetWidth,CachedNetAssetHeight);
                }
                else
                {
                    texture2D = NetAssetService.GetInstance().GetCachedNetImage(url, CachedTextureValidDays);
                }
                //
                if (texture2D != null)
                {
                    if (_image != null)
                    {
                        _image.sprite=Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

                        if (IsSetNativeSize)
                        {
                            SetNativeSize();
                        }

                        _httpImageState = UIHttpImageState.Loaded;

                        if (LoadingCover != null)
                        {
                            LoadingCover.SetActive(false);
                        }
                    }
                }
                else
                {
                    StartCoroutine(DownloadImage(url));
                }
            }
            else
            {
                StartCoroutine(DownloadImage(url));
            }
        }


        private IEnumerator DownloadImage(string url)
        {
            _httpImageState = UIHttpImageState.Loading;

            float startTime = Time.realtimeSinceStartup;

            WWW www = new WWW(url);
            yield return www;

            _httpImageState = UIHttpImageState.Loaded;

            if (LoadingCover != null)
            {
                LoadingCover.SetActive(false);
            }

            if (string.IsNullOrEmpty(www.error))
            {
                string contentType = null;
                www.responseHeaders.TryGetValue("CONTENT-TYPE", out contentType);

                if (contentType != null)
                {
                    contentType = contentType.ToLower();
                }

                if (string.IsNullOrEmpty(contentType) || !contentType.Contains("image/"))
                {
                    JW.Common.Log.LogE("UIHttpImage Download Image Content Error:" + ImageUrl);
                }
                else
                {
                    bool isGif = string.Equals(contentType, "image/gif");

                    Texture2D texture2D = null;

                    if (isGif)
                    {
                        JW.Common.Log.LogE("ToDO Support Gif");
                    }
                    else
                    {
                        texture2D = www.texture;
                    }

                    if (texture2D != null)
                    {
                        if (_image != null)
                        {
                            _image.sprite=Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

                            if (IsSetNativeSize)
                            {
                                SetNativeSize();
                            }
                        }

                        if (IsCacheTexture)
                        {
                            NetAssetService.GetInstance().AddCachedNetImage(url, texture2D.width, texture2D.height,www.bytes);
                        }
                    }
                }                
            }
            else
            {
                JW.Common.Log.LogE("UIHttpImage Download Image Error:" + ImageUrl);
            }
        }
    };
};