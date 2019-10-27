/********************************************************************
	created:	2018/04/08
	created:	8:4:2018   14:31
	author:		wuyouquan
	
	purpose:	基础资源打包
*********************************************************************/
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JW.Editor.Res
{ 
    class ComResPacker : IResPacker
    {
        private string _path;
        private EPackHandlerParam _packHandlerParam;

        public void SetHandlerParam(EPackHandlerParam packHandlerParam)
        {
            _packHandlerParam = packHandlerParam;
        }

        public EPackHandlerParam GetPackHandlerParam()
        {
            return _packHandlerParam;
        }

        public void SetPath(string path)
        {
            this._path = path;
        }

        public string GetPath()
        {
            return _path;
        } 

        public IEnumerable<string> GetRelatedFiles()
        {
            return new string[] { _path, _path + ".meta" };
        }

        public string[] GetRelatedFileList()
        {
            return ResPackHelper.GetDependencies( _path );
        }

        public void Preprocess(List<string> excludeList = null)
        {
        }

        public Object GetAimAssetObj()
        {
            Object obj = AssetDatabase.LoadMainAssetAtPath(_path);
            return obj;
        }

        public void Clear()
        {
        }

        public virtual List<System.Type> GetReplaceRefTypeList()
        {
            return null;
        }
    }
}
