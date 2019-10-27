/********************************************************************
	created:	2018/04/08
	created:	8:4:2018   14:35
	author:		wuyouquan
	
	purpose:	资源打包接口
*********************************************************************/
using System;
using System.Collections.Generic;

namespace JW.Editor.Res
{
    public interface IResPacker
    {
        void SetPath(string path);

        void SetHandlerParam(EPackHandlerParam packHandlerParam);
        string GetPath();

        /// <summary>
        /// 此处返回的文件，一旦改变就需要重新构建此资源
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetRelatedFiles();
        /// <summary>
        /// 此处返回的文件列表，只有是跟之前列表发生改变时才重新构建此资源
        /// </summary>
        /// <returns></returns>
        string[] GetRelatedFileList();

        void Preprocess(List<string> excludeList = null);

        UnityEngine.Object GetAimAssetObj();

        void Clear();

        List<Type> GetReplaceRefTypeList();
    }
}

