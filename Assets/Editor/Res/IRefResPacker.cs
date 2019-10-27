/********************************************************************
	created:	2018/04/08
	created:	8:4:2018   14:32
	author:		wuyouquan
	
	purpose:	引用资源打包接口
*********************************************************************/
using System.Collections.Generic;
using UnityEngine;

namespace JW.Editor.Res
{
    [System.Serializable]
    public class RefResMemberData
    {
        public List<string> resourcePaths;

        public RefResMemberData()
        {
            this.resourcePaths = new List<string>();
        }
        public RefResMemberData(string path)
        {
            this.resourcePaths = new List<string> { path };
        }
    }

    public interface IRefResPacker
    {
        List<RefResMemberData> GetMemberData(Object obj, EPackHandlerParam halderparam);
    }
}
