/********************************************************************
	created:	18:5:2018   
	filename: 	ResInfo
	author:		jordenwu
	
	purpose:	资源信息定义 代表resources目录下一个资源的信息
*********************************************************************/
using UnityEngine;

namespace JW.Res
{
    [System.Serializable]
    public class ResInfo
    {
        //资源在Resources目录下的完整路径(不以“/”开头, 不带扩展名)
        [SerializeField]
        public string Path;

        //资源扩展名(带".")
        [SerializeField]
        public string Ext;

        //是否在磁盘空间 
        [SerializeField]
        public bool Outside;

#if UNITY_EDITOR
        // 工程目录中的相对路径
        [SerializeField]
        public string RelativePath;
        //不从Resources目录删除
        [SerializeField]
        public bool Keep;           
#endif
    };
}
