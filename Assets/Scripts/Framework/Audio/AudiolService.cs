/********************************************************************
	created:	14:8:2017   9:36
	author:		jordenwu
	purpose:	游戏播放背景音乐 和音效服务
*********************************************************************/
using UnityEngine;
using JW.Common;
//using JW.Framework.UI;

namespace JW.Framework.Audio
{
    public class AudioService : MonoSingleton<AudioService>
    {
        /// <summary>
        /// 声音通道类型定义
        /// </summary>
        public enum AudioChannelType
        {
            ACT_BGM=1,            //背景音乐
            ACT_UI=2,             //UI音效
            ACT_EF=3,             //特效通道 各种效果
            ACT_VOICE=4           //对话语音
        }

        protected AudioChannel m_kChannel_BGM;
        protected AudioChannel m_kChannel_UI;
        protected AudioChannel m_kChannel_EF;
        protected AudioChannel m_kChannel_VOICE;

        public float m_fCurBGMVolume = 0.7f;
        public float m_fCurUIVolume = 1f;
        public float m_fCurEFVolume = 1f;
        public float m_fCurVoiceVolume = 1f;

        public string m_kCurBGMName;
        public float m_fLastBGMVolume = 1.0f;

        public float m_fLastUIVolume = 1.0f;
        public float m_fLastEFVolume = 1.0f;
        public float m_fLastVoiceVolume = 0.6f; 
        //
        private bool m_bLockCloseAll = false;
        //
        private Transform _rootTf;
        public Transform RootTf
        {
            get {
                return _rootTf;
            }
        }

        
        public override bool Initialize()
        {
            //
            _rootTf = new GameObject("AudioService").transform;
            _rootTf.gameObject.ExtAddComponent<AudioListener>(true);
            _rootTf.gameObject.ExtDontDestroyOnLoad();
            //
            m_kChannel_BGM = new AudioChannel(5, true);
            m_kChannel_EF = new AudioChannel(8);
            m_kChannel_UI = new AudioChannel(10);
            m_kChannel_VOICE = new AudioChannel(5);
            //
            if (PlayerPrefs.HasKey(System.Enum.GetName(typeof(AudioService.AudioChannelType), AudioService.AudioChannelType.ACT_BGM)))
            {
                float volume = PlayerPrefs.GetFloat(System.Enum.GetName(typeof(AudioService.AudioChannelType), AudioService.AudioChannelType.ACT_BGM));
                m_fCurBGMVolume = volume;
            }

            if (PlayerPrefs.HasKey(System.Enum.GetName(typeof(AudioService.AudioChannelType), AudioService.AudioChannelType.ACT_UI)))
            {
                float volume = PlayerPrefs.GetFloat(System.Enum.GetName(typeof(AudioService.AudioChannelType), AudioService.AudioChannelType.ACT_UI));
                m_fCurUIVolume = volume;
            }

            if (PlayerPrefs.HasKey(System.Enum.GetName(typeof(AudioService.AudioChannelType), AudioService.AudioChannelType.ACT_EF)))
            {
                float volume = PlayerPrefs.GetFloat(System.Enum.GetName(typeof(AudioService.AudioChannelType), AudioService.AudioChannelType.ACT_EF));
                m_fCurEFVolume = volume;
            }

            if (PlayerPrefs.HasKey(System.Enum.GetName(typeof(AudioService.AudioChannelType), AudioService.AudioChannelType.ACT_VOICE)))
            {
                float volume = PlayerPrefs.GetFloat(System.Enum.GetName(typeof(AudioService.AudioChannelType), AudioService.AudioChannelType.ACT_VOICE));
                m_fCurVoiceVolume = volume;
            }

            m_fLastBGMVolume = m_fCurBGMVolume;
            m_fLastUIVolume = m_fCurUIVolume;
            m_fLastEFVolume = m_fCurEFVolume;
            m_fLastVoiceVolume = m_fCurVoiceVolume;
            //挂接全局
            JW.Framework.UGUI.UIButton.PlayAudioHandler = PlayUIAudio;
            return true;
        }

        public override void Uninitialize()
        {
            if (null != _rootTf)
            {
                _rootTf.gameObject.ExtDestroy();
                _rootTf = null;
            }
            if (null != m_kChannel_BGM)
            {
                m_kChannel_BGM.UnInit();
                m_kChannel_BGM = null;
            }
            if (null != m_kChannel_EF)
            {
                m_kChannel_EF.UnInit();
                m_kChannel_EF = null;
            }
            if (null != m_kChannel_UI)
            {
                m_kChannel_UI.UnInit();
                m_kChannel_UI = null;
            }
            if (null != m_kChannel_VOICE)
            {
                m_kChannel_VOICE.UnInit();
                m_kChannel_VOICE = null;
            }
            JW.Framework.UGUI.UIButton.PlayAudioHandler = null;
            //保存记录
            PlayerPrefs.SetFloat(System.Enum.GetName(typeof(AudioService.AudioChannelType), AudioService.AudioChannelType.ACT_BGM),m_fCurBGMVolume);
            PlayerPrefs.SetFloat(System.Enum.GetName(typeof(AudioService.AudioChannelType), AudioService.AudioChannelType.ACT_EF), m_fCurEFVolume);
            PlayerPrefs.SetFloat(System.Enum.GetName(typeof(AudioService.AudioChannelType), AudioService.AudioChannelType.ACT_UI), m_fCurUIVolume);
            PlayerPrefs.SetFloat(System.Enum.GetName(typeof(AudioService.AudioChannelType), AudioService.AudioChannelType.ACT_VOICE), m_fCurVoiceVolume);

        }

        /// <summary>
        /// 固定频率更新
        /// </summary>
        protected void FixedUpdate()
        {
            if (m_kChannel_BGM == null)
                return;
            float ff = Time.fixedTime;
            m_kChannel_BGM.Update(ff);
            m_kChannel_UI.Update(ff);
            m_kChannel_EF.Update(ff);
            m_kChannel_VOICE.Update(ff);
        }

        /// <summary>
        /// 全局播放UI 音效
        /// </summary>
        /// <param name="bankName"></param>
        /// <param name="eventName"></param>
        private void PlayUIAudio(string resName)
        {
            if (string.IsNullOrEmpty(resName))
            {
                return;
            }
            Play((uint)AudioChannelType.ACT_UI, resName, 1.0f, false);
        }

        //播放
        public void Play(uint eChannelType, string resName, bool loop,bool isNetAsset=false)
        {
            if (isNetAsset)
            {
                NetAsset.NetAssetInfo info = NetAsset.NetAssetService.GetInstance().GetCachedNetAssetInfoByUrl(resName);
                if (info == null)
                {
                    Log.LogE("Play NetAsset Audio Error No Cached:" + resName);
                    return;
                }
            }
            AudioChannel kAudioChannel = GetChannelByType(eChannelType);
            float channelVolume = GetVolumeByChannel(eChannelType);
            if (kAudioChannel != null)
            {
                kAudioChannel.Play(resName, channelVolume, loop,isNetAsset);
            }
        }

        ///播放
        public void Play(uint eChannelType, string resName, float volumeScale, bool loop=false)
        {
            AudioChannel kAudioChannel = GetChannelByType(eChannelType);

            float channelVolume = GetVolumeByChannel(eChannelType);

            if (kAudioChannel != null)
            {
                kAudioChannel.Play(resName, volumeScale * channelVolume, loop);
            }
        }

        ///从头重新播放
        public void Replay(uint eChannelType, string resName, float volume, bool loop)
        {
            AudioChannel kAudioChannel = GetChannelByType(eChannelType);

            float channelVolume = GetVolumeByChannel(eChannelType);

            if (kAudioChannel != null)
            {
                kAudioChannel.ReplayBGM(resName, volume * channelVolume, loop);
            }
        }

        //停止单个通道
        public void Stop(uint eChannelType)
        {
            AudioChannel kAudioChannel = GetChannelByType(eChannelType);
            if (kAudioChannel != null)
            {
                kAudioChannel.Stop();
            }
        }

        //停止所有
        public void StopAll()
        {
            m_kChannel_BGM.Stop();
            m_kChannel_UI.Stop();
            m_kChannel_EF.Stop();
            m_kChannel_VOICE.Stop();
        }

        //关闭所有声音
        public void CloseAll()
        {
            if (m_bLockCloseAll == false)
            {
                ChangeVolume((uint)AudioChannelType.ACT_BGM, 0f);
                m_fLastBGMVolume = m_fCurBGMVolume;
                m_fLastVoiceVolume = m_fCurVoiceVolume;
                m_fLastEFVolume = m_fCurEFVolume;
                m_fLastUIVolume = m_fCurUIVolume;
                m_fCurBGMVolume = 0;
                m_fCurUIVolume = 0;
                m_fCurEFVolume = 0;
                m_fCurVoiceVolume = 0;
                //
                m_bLockCloseAll = true;
            }
        }

        //打开所有
        public void OpenAll()
        {
            m_fCurBGMVolume = m_fLastBGMVolume;
            m_fCurUIVolume = m_fLastUIVolume;
            m_fCurEFVolume = m_fLastEFVolume;
            m_fCurVoiceVolume = m_fLastVoiceVolume;
            ChangeVolume((uint)AudioChannelType.ACT_BGM, m_fCurBGMVolume);
            m_bLockCloseAll = false;
        }

        //获取通道
        private AudioChannel GetChannelByType(uint eChannelType)
        {
            AudioChannelType tt = (AudioChannelType)eChannelType;
            switch (tt)
            {
                case AudioChannelType.ACT_BGM:
                    return m_kChannel_BGM;
                case AudioChannelType.ACT_UI:
                    return m_kChannel_UI;
                case AudioChannelType.ACT_VOICE:
                    return m_kChannel_VOICE;
                case AudioChannelType.ACT_EF:
                    return m_kChannel_EF;
                default:
                    return null;
            }
        }

        //获取对应通道音量
        public float GetVolumeByChannel(uint eChannelType)
        {
            AudioChannelType tt = (AudioChannelType)eChannelType;
            switch (tt)
            {
                case AudioChannelType.ACT_BGM:
                    return m_fCurBGMVolume;
                case AudioChannelType.ACT_UI:
                    return m_fCurUIVolume;
                case AudioChannelType.ACT_VOICE:
                    return m_fCurVoiceVolume;
                case AudioChannelType.ACT_EF:
                    return m_fCurEFVolume;
                default:
                    return m_fCurUIVolume;
            }
        }

        //获取背景音量
        public float GetBGMVolume()
        {
            return m_fCurBGMVolume;
        }

        //设置音量
        public void SetChannelVolume(uint eChannelType, float in_value)
        {
            if (in_value < 0)
                in_value = 0;
            if (in_value > 1.0f)
                in_value = 1.0f;
            AudioChannel kAudioChannel = GetChannelByType(eChannelType);
            if (kAudioChannel != null)
            {
                kAudioChannel.SetVolume(in_value);
            }
            //记录
            AudioChannelType tt = (AudioChannelType)eChannelType;
            if (tt == AudioChannelType.ACT_BGM)
            {
                m_fCurBGMVolume = in_value;
                m_fLastBGMVolume = in_value;
                PlayerPrefs.SetFloat(System.Enum.GetName(typeof(AudioService.AudioChannelType), AudioService.AudioChannelType.ACT_BGM), m_fCurBGMVolume);
            }
            else if (tt == AudioChannelType.ACT_EF)
            {
                m_fCurEFVolume = in_value;
                m_fLastEFVolume = in_value;
                PlayerPrefs.SetFloat(System.Enum.GetName(typeof(AudioService.AudioChannelType), AudioService.AudioChannelType.ACT_EF), m_fCurEFVolume);
            }
            else if (tt == AudioChannelType.ACT_UI)
            {
                m_fCurUIVolume = in_value;
                m_fLastUIVolume = in_value;
                PlayerPrefs.SetFloat(System.Enum.GetName(typeof(AudioService.AudioChannelType), AudioService.AudioChannelType.ACT_UI), m_fCurUIVolume);
            }
            else if (tt == AudioChannelType.ACT_VOICE)
            {
                m_fCurVoiceVolume = in_value;
                m_fLastVoiceVolume = in_value;
                PlayerPrefs.SetFloat(System.Enum.GetName(typeof(AudioService.AudioChannelType), AudioService.AudioChannelType.ACT_VOICE), m_fCurVoiceVolume);
            }
        }

        //改变音量
        public void ChangeVolume(uint eChannelType, float in_value)
        {
            if (in_value < 0)
                in_value = 0;
            if (in_value > 1.0f)
                in_value = 1.0f;
            AudioChannel kAudioChannel = GetChannelByType(eChannelType);
            if (kAudioChannel != null)
            {
                kAudioChannel.ChangeVolume(in_value);
            }
        }

        //淡入
        public void FadeIn(uint eChannelType)
        {
            AudioChannel kAudioChannel = GetChannelByType(eChannelType);
            if (kAudioChannel != null)
            {
                kAudioChannel.FadeIn();
            }
        }

        //淡入声音
        public void FadeIn(uint eChannelType, string audioResName)
        {
            AudioChannel kAudioChannel = GetChannelByType(eChannelType);
            if (kAudioChannel != null)
            {
                kAudioChannel.FadeIn(audioResName);
            }
        }

        //淡出
        public void FadeOut(uint eChannelType)
        {
            AudioChannel kAudioChannel = GetChannelByType(eChannelType);
            if (kAudioChannel != null)
            {
                kAudioChannel.FadeOut();
            }
        }

        //淡出
        public void FadeOut(uint eChannelType, string audioResName)
        {
            AudioChannel kAudioChannel = GetChannelByType(eChannelType);
            if (kAudioChannel != null)
            {
                kAudioChannel.FadeOut(audioResName);
            }
        }

        //转到音量
        public void FadeToTargetVolume(uint eChannelType, float myTargetVolume)
        {
            AudioChannel kAudioChannel = GetChannelByType(eChannelType);
            if (kAudioChannel != null)
            {
                kAudioChannel.FadeToTargetVolume(myTargetVolume);
            }
        }
    }
}
