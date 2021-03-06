/********************************************************************
	created:	10:8:2017   10:28
	author:		jordenwu
	purpose:	一个播放的声音源
*********************************************************************/
using UnityEngine;
using JW.Framework.Asset;
using JW.Common;
using JW.Framework.NetAsset;
using System;

namespace JW.Framework.Audio
{
    public class Audio
    {
        private string m_kAudioResName = string.Empty;
        private AudioSource m_kSource = null;
        private AudioClip m_kClip = null;

        private float m_fStartTime = 0;
        private float m_fEndTime = 0;
        private bool m_bIsLoop = false;
        private float m_iInitVolume;

        private bool m_bIsFadeIn = false;
        private bool m_bIsFadeOut = false;
        private bool m_bIsFadeOutEnd = false;
        //淡入淡出音量间隔
        private float m_fFadeInterval = 0.01f;

        private bool m_bIsFadeToTargetVolume = false;
        private float m_fFadeToTargetVolume;

        //声音资产
        private AudioAsset _audioAsset;

        //
        private bool _isNetAudioAsset;
        private WWW _audioWWW;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="audioName"></param>
        /// <param name="volume"></param>
        /// <param name="loop"></param>
        public Audio(string audioName, float volume, bool loop,float fadeInterval=0.005f,bool isNetAsset=false)
        {
            m_kAudioResName = audioName;
            m_bIsLoop = loop;
            m_fFadeInterval = fadeInterval;
            //todo 池子
            GameObject gameObject = new GameObject(audioName);
            gameObject.transform.parent = AudioService.GetInstance().RootTf;
            m_kSource = gameObject.AddComponent<AudioSource>();
            if (m_kSource != null)
            {
                m_kSource.volume = Mathf.Max(0, Mathf.Min(1, volume));
                m_kSource.loop = m_bIsLoop;
            }
            m_iInitVolume = Mathf.Max(0, Mathf.Min(1, volume));
            //
            _isNetAudioAsset = isNetAsset;
        }


        /// <summary>
        /// 销毁
        /// </summary>
        public void UnInit()
        {
            if (m_kSource != null)
            {
                m_kSource.Stop();
                m_kSource.clip = null;
                UnityEngine.Object.DestroyImmediate(m_kSource.gameObject);
                m_kSource = null;
            }

            if (m_kClip != null)
            {
                if (null != _audioAsset)
                {
                    AssetService.GetInstance().Unload(_audioAsset);
                    _audioAsset = null;
                }
                if (_isNetAudioAsset)
                {
                    UnityEngine.Object.Destroy(m_kClip);
                }
                m_kClip = null;
            }
       
        }

        
        /// <summary>
        /// 统一时序
        /// </summary>
        /// <param name="fCurrentTime"></param>
        public void Update(float fCurrentTime)
        {
            if (_isNetAudioAsset && _audioWWW != null)
            {
                if (_audioWWW.isDone)
                {
                    m_kClip = _audioWWW.GetAudioClip(false,true,AudioType.OGGVORBIS);
                    _audioWWW.Dispose();
                    _audioWWW = null;
                }
                if (m_kClip != null)
                {
                    m_fStartTime = fCurrentTime;
                    m_fEndTime = fCurrentTime;
                    if (m_kClip.length < 0.2f)
                    {
                        m_fEndTime = m_fStartTime + 0.2f;
                    }
                    else
                    {
                        m_fEndTime = m_fStartTime + m_kClip.length;
                    }
                    if (m_kSource != null)
                    {
                        m_kSource.clip = m_kClip;
                        m_kSource.Play();
                    }
                }
                return;
            }
            _fadeIn();
            _fadeOut();
            _fadeToTargetVolume();
        }

        //播放
        public void Play(float currentTime)
        {
            m_fStartTime = currentTime;
            m_fEndTime = currentTime;
            //
            if (_audioAsset != null)
            {
                AssetService.GetInstance().Unload(_audioAsset);
                _audioAsset = null;
            }
            //
            if (_isNetAudioAsset)
            {
                NetAssetInfo info = NetAssetService.GetInstance().GetCachedNetAssetInfoByUrl(m_kAudioResName);
                if (info != null)
                {
                    string filePath = info.GetCachedFilePath();
                    string url = string.Format("file://{0}", filePath);
                    _audioWWW = new WWW(url);
                    //给1秒钟去加载
                    m_fEndTime = m_fStartTime + 1.0f;
                }
            }
            else
            {
                _audioAsset = AssetService.GetInstance().LoadAudioAsset(m_kAudioResName);
                if (null != _audioAsset)
                {
                    m_kClip = _audioAsset.Clip;
                }
                else
                {
                    JW.Common.Log.LogE("Load Audio File Error:" + m_kAudioResName);
                    m_kClip = null;
                }
            }
            //
            if (m_kClip != null)
            {
                if (m_kClip.length < 0.2f)
                {
                    m_fEndTime = m_fStartTime + 0.2f;
                }
                else
                {
                    m_fEndTime = m_fStartTime + m_kClip.length;
                }
                if (m_kSource != null)
                {
                    m_kSource.clip = m_kClip;
                    m_kSource.Play();
                }
            }
        }

        //淡入
        public void FadeIn()
        {
            if (m_kSource != null)
            {
                m_kSource.volume = 0f;
            }
            m_bIsFadeIn = true;
            m_bIsFadeOut = false;
        }

        //淡出
        public void FadeOut()
        {
            if (m_kSource != null)
            {
                m_kSource.volume = m_iInitVolume;
            }
            m_bIsFadeOut = true;
            m_bIsFadeOutEnd = false;
            m_bIsFadeIn = false;
        }

        //到目标音量
        public void FadeToTargetVolume(float myTargetVolume)
        {
            m_fFadeToTargetVolume  = myTargetVolume;
            m_bIsFadeToTargetVolume = true;
        }

        //暂停
        public void Pause()
        {
            if (m_kSource != null)
            {
                m_kSource.Pause();
            }
        }

        //继续
        public void Continue()
        {
            if (m_kSource != null)
            {
                m_kSource.Play();
            }
        }

        //停止
        public void Stop()
        {
            if (m_kClip != null)
            {
                if (null!=_audioAsset)
                {
                    AssetService.GetInstance().Unload(_audioAsset);
                    _audioAsset = null;
                }
                if (_isNetAudioAsset)
                {
                    UnityEngine.Object.Destroy(m_kClip);
                }
                m_kClip = null;
            }

            if (m_kSource != null)
            {
                m_kSource.Stop();
                m_kSource.clip = null;
                UnityEngine.Object.DestroyImmediate(m_kSource.gameObject);
                m_kSource = null;
            }
        }

        //设置音量
        public void SetVolume(float in_value)
        {
            if (in_value < 0)
                in_value = 0;
            if (in_value > 1.0f)
                in_value = 1.0f;

            if (m_kSource != null)
            {
                m_kSource.volume = in_value;
            }
            m_iInitVolume = in_value;
        }

        //改变音量
        public void ChangeVolume(float in_value)
        {
            m_bIsFadeIn = false;
            m_bIsFadeOut = false;

            if (in_value < 0)
                in_value = 0;
            if (in_value > 1.0f)
                in_value = 1.0f;

            if (m_kSource != null)
            {
                m_kSource.volume = in_value;
            }
        }

        //获取正在播放声音名称
        public string GetPlayingAudioName()
        {
            if (m_kSource.isPlaying)
            {
                return AudioResName;
            }
            else
            {
                return "";
            }
        }

        //淡入
        private void _fadeIn()
        {
            if (m_bIsFadeIn)
            {
                if (m_kSource.volume < m_iInitVolume)
                {
                    m_kSource.volume += m_fFadeInterval;
                }
                else
                {
                    m_kSource.volume = m_iInitVolume;
                    m_bIsFadeIn = false;
                }
            }
        }

        //淡出
        private void _fadeOut()
        {
            if (m_bIsFadeOut)
            {
                if (m_kSource.volume > 0)
                {
                    m_kSource.volume -= m_fFadeInterval;
                }
                else
                {
                    m_kSource.volume = 0;
                    m_bIsFadeOut = false;
                    Pause();
                    m_bIsFadeOutEnd = true;
                }
            }
        }

        //淡出到目标音量
        private void _fadeToTargetVolume()
        {
            if (m_bIsFadeToTargetVolume)
            {
                if (m_fFadeToTargetVolume >= m_kSource.volume)
                {
                    m_kSource.volume += m_fFadeInterval;
                    if (m_kSource.volume >= m_fFadeToTargetVolume)
                    {
                        m_kSource.volume = m_fFadeToTargetVolume;
                        m_bIsFadeToTargetVolume = false;
                    }
                }
                else
                {
                    m_kSource.volume -= m_fFadeInterval;
                    if (m_kSource.volume <= m_fFadeToTargetVolume)
                    {
                        m_kSource.volume = m_fFadeToTargetVolume;
                        m_bIsFadeToTargetVolume = false;
                    }
                }
            }
        }

        public string AudioResName
        {
            get { return m_kAudioResName; }
        }

        public AudioSource Source
        {
            get { return m_kSource; }
        }

        public float EndTime
        {
            get { return m_fEndTime; }
        }

        public bool IsLoop
        {
            get { return m_bIsLoop; }
        }

        public bool IsFadeOutEnd
        {
            get { return m_bIsFadeOutEnd; }
        }

        public bool IsFadeOuting
        {
            get { return m_bIsFadeOut; }
        }
    }
}
