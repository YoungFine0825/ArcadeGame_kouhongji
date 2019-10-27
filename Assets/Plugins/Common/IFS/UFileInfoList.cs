using System.Collections.Generic;
using UnityEngine;

namespace JW.IFS
{
    public enum UFileInfoListType
    {
        //主游戏资源列表
        MainGame=0,
        //子游戏资源列表
        SubGame=1,
    }

    [System.Serializable]
    public class UFileInfoList
    {
        //文件信息列表
        [SerializeField]
        public List<UFileInfo> FileList = new List<UFileInfo>();
        //类型
        [SerializeField]
        public UFileInfoListType ListType = UFileInfoListType.MainGame;
        //游戏版本
        [SerializeField]
        public string GameVersion;
        //游戏资源版本
        [SerializeField]
        public string ResVersion;
        //开发SVN号
        [SerializeField]
        public string DevSvnVersion;

        //添加文件
        public void AddUFile(ref UFileInfo file)
        {
            if (!FileList.Contains(file))
            {
                FileList.Add(file);
            }
        }
         
        //获取总文件大小
        public int GetAllFileSize()
        {
            if (FileList == null)
            {
                return 0;
            }
            if (FileList.Count == 0)
            {
                return 0;
            }
            int ret = 0;
            for(int i = 0; i < FileList.Count; i++)
            {
                ret = ret + FileList[i].FileSize;
            }
            return ret;
        }

        /// <summary>
        /// 差异输出 自己有 别人有 比较md5 不同就加入 自己有 别人没有 不管 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public UFileInfoList DiffWithOther(ref UFileInfoList other)
        {
            UFileInfoList ret = new UFileInfoList();

            if (FileList.Count == 0)
            {
                ret = null;
                return other;
            }
            if (other == null||other.FileList.Count==0)
            {
                return null;
            }
            for (int i=0; i < other.FileList.Count; i++)
            {
                UFileInfo uf = other.FileList[i];
                bool isDiff = false;
                bool isFind = false;
                for(int j = 0; j < FileList.Count; j++)
                {
                    UFileInfo sf = FileList[j];
                    if (sf.FileName.Equals(uf.FileName))
                    {
                        isFind = true;
                        if (!sf.MD5Num.Equals(uf.MD5Num))
                        {
                            isDiff = true;
                            break;
                        }
                        break;
                    }
                }
                //
                if (isDiff||(isFind==false))
                {
                    ret.AddUFile(ref uf);
                }
            }
            return ret;
        }

    }
}
