/********************************************************************
	created:	14:8:2017   9:35
	author:		jordenwu
	purpose:	声音通道；一个通道管理几个 声音源
*********************************************************************/
using UnityEngine;
using JW.Common;
using JW.Framework.NetAsset;

namespace JW.Framework.Audio
{
    public class AudioChannel
    {
        private int m_iCapacity = 1;
        //背景音乐模式
        private bool m_bIsMusic = false;

        private JWObjList<Audio> m_kAudioList = new JWObjList<Audio>();
        private JWObjList<Audio> m_kAudioTempList = new JWObjList<Audio>();

        //构建
        public AudioChannel(int capacity,bool isMusic=false)
        {
            m_iCapacity = Mathf.Max(1, capacity);
            //需要淡出淡入的BGM
            m_bIsMusic = isMusic;
        }

        //反初始化
        public void UnInit()
        {
            if (m_kAudioList != null)
            {
                for (int i = 0; i < m_kAudioList.Count; i++)
                {
                    Audio audio = m_kAudioList[i];
                    audio.UnInit();
                }
                m_kAudioList.Clear();
                m_kAudioList = null;
            }
        }
        
        //同步
        public void Update(float fCurrentTime)
        {
            m_kAudioTempList.Clear();

            for (int i = m_kAudioList.Count - 1; i >= 0; i--)
            {
                Audio audio = m_kAudioList[i];
                if (audio.IsFadeOutEnd)
                {
                    audio.Stop();
                    m_kAudioList.RemoveAt(i);
                    continue;
                }
                else
                {
                    if ((!audio.IsLoop) && (audio.EndTime < fCurrentTime))
                    {
                        audio.Stop();
                        m_kAudioList.RemoveAt(i);
                        continue;
                    }
                    else
                    {
                        m_kAudioTempList.Add(audio);
                    }
                }
            }

            m_kAudioList.Clear();
            //
            for (int i = 0; i < m_kAudioTempList.Count; i++)
            {
                Audio audio2 = m_kAudioTempList[i];
                m_kAudioList.Add(audio2);
            }

            for (int i = 0; i < m_kAudioList.Count; i++)
            {
                Audio audio3 = m_kAudioList[i];
                audio3.Update(fCurrentTime);
            }
        }

        //播放
        public void Play(string audioName, float volume, bool loop,bool isNetAsset=false)
        {
            if (string.IsNullOrEmpty(audioName)) return;
            //音效播放
            if (!m_bIsMusic && m_kAudioList.Count < m_iCapacity)
            {
                _createAudio(audioName, volume, loop,isNetAsset);
                return;
            }

            //音乐模式
            if (m_bIsMusic && m_kAudioList.Count < m_iCapacity)
            {
                bool isHave = false;
                //其他的背景淡出 
                for (int i = 0; i < m_kAudioList.Count; i++)
                {
                    Audio audio = m_kAudioList[i];
                    if (audioName == audio.AudioResName)
                    {
                        isHave = true;
                        audio.FadeIn();
                        audio.Continue();
                    }
                    else
                    {
                        audio.FadeOut();
                    }
                }
                //
                if (isHave == false)
                {
                    _createAudio(audioName, volume, loop, isNetAsset);
                }
            }
        }

        public void Stop()
        {
            for (int i = 0; i < m_kAudioList.Count; i++)
            {
                Audio audio = m_kAudioList[i];
                audio.Stop();
            }
            m_kAudioList.Clear();
        }

        public void SetVolume(float in_value)
        {
            if (in_value < 0)
                in_value = 0;
            if (in_value > 1.0f)
                in_value = 1.0f;

            if (m_kAudioList != null && m_kAudioList.Count > 0)
            {
                for (int i = 0; i < m_kAudioList.Count; i++)
                {
                    Audio audio = m_kAudioList[i];
                    if (audio != null)
                    {
                        audio.SetVolume(in_value);
                    }
                }
            }
        }

        public void ChangeVolume(float in_value)
        {
            if (in_value < 0)
                in_value = 0;
            if (in_value > 1.0f)
                in_value = 1.0f;

            if (m_kAudioList != null && m_kAudioList.Count > 0)
            {
                for (int i = 0; i < m_kAudioList.Count; i++)
                {
                    if (m_bIsMusic)
                    {
                        Audio audio = m_kAudioList[i];
                        if (audio != null && (!audio.IsFadeOuting))
                        {
                            audio.ChangeVolume(in_value);
                        }
                    }
                    else
                    {
                        Audio audio = m_kAudioList[i];
                        if (audio != null)
                        {
                            audio.ChangeVolume(in_value);
                        }
                    }
                }
            }
        }

        public void FadeIn()
        {
            for (int i = 0; i < m_kAudioList.Count; i++)
            {
                Audio audio = m_kAudioList[i];
                if (audio.Source.isPlaying)
                {
                    audio.FadeIn();
                }
            }
        }

        public void FadeIn(string audioResName)
        {
            for (int i = 0; i < m_kAudioList.Count; i++)
            {
                Audio audio = m_kAudioList[i];
                if (!audio.Source.isPlaying && audioResName == m_kAudioList[i].AudioResName)
                {
                    audio.FadeIn();
                }
            }
        }

        public void FadeOut()
        {
            for (int i = 0; i < m_kAudioList.Count; i++)
            {
                Audio audio = m_kAudioList[i];
                if (audio.Source.isPlaying)
                {
                    audio.FadeOut();
                }
            }
        }

        public void FadeOut(string audioResName)
        {
            for (int i = 0; i < m_kAudioList.Count; i++)
            {
                Audio audio = m_kAudioList[i];
                if (audio.Source.isPlaying && audioResName == m_kAudioList[i].AudioResName)
                {
                    audio.FadeOut();
                }
            }
        }

        public void FadeToTargetVolume(float myTargetVolume)
        {
            for (int i = 0; i < m_kAudioList.Count; i++)
            {
                Audio audio = m_kAudioList[i];
                if (audio.Source.isPlaying)
                {
                    audio.FadeToTargetVolume(myTargetVolume);
                }
            }
        }

        public void ReplayBGM(string audioName, float volume, bool loop)
        {
            if (m_bIsMusic == false)
            {
                return;
            }
            for (int i = 0; i < m_kAudioList.Count; i++)
            {
                if (audioName == m_kAudioList[i].AudioResName)
                {
                    m_kAudioList[i].Stop();
                    m_kAudioList.RemoveAt(i);
                }
            }
            Play(audioName, volume, loop);
        }

        private void _createAudio(string audioName, float volume, bool loop,bool isNetAsset=false)
        {
            Audio audio;
            if (m_bIsMusic == false)
            {
                audio = new Audio(audioName, volume, loop, 0.005f, isNetAsset);
            }
            else
            {
                //BGM
                audio = new Audio(audioName, volume, loop, 0.005f,isNetAsset);
            }
            m_kAudioList.Add(audio);

            //背景淡入
            if (m_bIsMusic == true)
            {
                audio.Play(Time.fixedTime);
                audio.FadeIn();
            }
            else
            {
                //音效直接播放
                audio.Play(Time.fixedTime);
            }
        }
    }
}