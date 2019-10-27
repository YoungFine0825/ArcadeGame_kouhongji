/********************************************************************
	created:	2018-8-15
	author:		jordenwu
	
	purpose:	Ui 音效配置
*********************************************************************/
using System;
using UnityEngine;
namespace JW.Framework.UGUI
{
    public class UIAudioCfg : MonoBehaviour
    {
        [Serializable]
        public class AudioData
        {
            public string Name;
            public string ResName;
        }

        public AudioData[] AudioDatas;

        /// <summary>
        /// 根据名称获取对应资源文件名称
        /// </summary>
        /// <param name="audioName"></param>
        /// <param name="resName"></param>
        /// <returns></returns>
        public bool GetAudio(string audioName, out string resName)
        {
            resName = null;

            if (string.IsNullOrEmpty(audioName))
            {
                return false;
            }

            for (int i = 0; i < AudioDatas.Length; ++i)
            {
                if (string.Compare(AudioDatas[i].Name, audioName, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    resName = AudioDatas[i].ResName;
                    return true;
                }
            }

            return false;
        }
    }
}
