using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JW.IFS
{
    [System.Serializable]
    public class UFileInfo
    {   
        //带扩展名的文件名称 此路径都是相对于 更新目录的路径
        [SerializeField]
        public string FileName;
        //SVN 对应文件版本号
        [SerializeField]
        public string SvnVerNum;
        //文件对应的MD5 
        [SerializeField]
        public string MD5Num;
        //文件大小 字节单位 
        [SerializeField]
        public int FileSize;

    }
}



