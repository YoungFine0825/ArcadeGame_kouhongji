/********************************************************************
	created:	2018-07-02
	filename: 	LuaLoader
	author:		jordenwu
	
	purpose:	Lua加载器
*********************************************************************/
using System.Collections;
using UnityEngine;
using JW.Res;
using System;
using JW.Common;
using XLua;
using System.IO;
using JW.IFS;

namespace JW.Lua
{
    public class LuaLoader
    {
        public const int LoadStateReady = 0;
        public const int LoadStateLoading = 1;
        public const int LoadStateSuccess = 2;
        public const int LoadStateFail = 3;

        private IFSFile _curLuaZip = null;

#if !USE_LUA_PACK
        //加载Main所有Lua
        public IEnumerator LoadMainLua(LuaEnv lua, System.Action<int> callback = null)
        {
            if (callback != null)
            {
                callback(LoadStateLoading);
            }

#if UNITY_EDITOR && !USE_PACK_RES
            string luaPath = Application.dataPath + "/Resources/LuaScripts/?.lua;";
#else
            string luaPath = string.Format("{0}/{1}/{2};",
                     JW.Res.FileUtil.GetIFSExtractPath(),
                     "",
                      "LuaScripts/?.lua");
#endif

            string script = string.Format("package.path = package.path .. ';{0}'", luaPath);
            lua.DoString(script);
            lua.AddLoader(CustomLuaLoader);
            lua.DoString("require('Boot')");
            //收集脚本
            LuaFunction collectLua = lua.Global.GetInPath<LuaFunction>("CollectScripts");
            if (collectLua != null)
            {
                string[] scripts = collectLua.Func<bool, string[]>(true);
                if (scripts == null || scripts.Length == 0)
                {
                    JW.Common.Log.LogE("Collect lua scripts failed.");
                }
                else
                {
                    for (int i = 0; i < scripts.Length; i++)
                    {
                        lua.DoString(string.Format("require('{0}')", scripts[i]));
                        if ((i + 1) % 10 == 0)
                        {
                            yield return null;
                        }
                    }
                }
            }
            collectLua = null;
            if (callback != null)
            {
                callback(LoadStateSuccess);
            }
        }

        public byte[] CustomLuaLoader(ref string filepath)
        {
            filepath = filepath.Replace('.', '/');
            filepath = "LuaScripts/" + filepath + ".lua";
            //编辑器 强制走
#if UNITY_EDITOR && !USE_PACK_RES
            string fullPath = JW.Res.FileUtil.CombinePath(Application.dataPath, "Resources/"+filepath);
            return JW.Res.FileUtil.ReadFile(fullPath);
#else
            ResObj res = ResService.GetResource(filepath);
            if (res != null && res.Content != null)
            {
                BinaryObject binaryObject = res.Content as BinaryObject;
                byte[] bb = binaryObject.m_data;
                ResService.UnloadResource(res, false);
                return bb;
            }
            else
            {
                // 有脚本加载失
                JW.Common.Log.LogE("Load lua script failed, path:{0}.", filepath);
                return null;
            }
#endif
        }

#else

        //----------------------lua文件包读取方式----------------
        public IEnumerator LoadMainLua(LuaEnv lua, System.Action<int> callback = null)
        {
            if (callback != null)
            {
                callback(LoadStateLoading);
            }
            //
            string filePath = FileUtil.CombinePath(JW.Res.FileUtil.GetIFSExtractPath(), "Main_Lua.bytes");
            if (!JW.Res.FileUtil.IsFileExist(filePath))
            {
                JW.Common.Log.LogE("Get Main_Lua.bytes pack File Failed");
                if (callback != null)
                {
                    callback(LoadStateFail);
                }
            }
            else
            {
                _curLuaZip = new IFSFile();
                if (_curLuaZip.InitWithFile(filePath) == false)
                {
                    JW.Common.Log.LogE("Get Main_Lua Init Failed");
                    if (callback != null)
                    {
                        callback(LoadStateFail);
                    }
                }
                else
                {

                    Log.LogD("<color=yellow>Init Main_Lua Done!</color>");
                    //
                    lua.AddLoader(CustomLuaLoader);
                    //启动
                    lua.DoString("require('Boot')");
                    //收集脚本
                    LuaFunction collectLua = lua.Global.GetInPath<LuaFunction>("CollectScripts");
                    if (collectLua != null)
                    {
                        string[] scripts = collectLua.Func<bool, string[]>(true);
                        if (scripts == null || scripts.Length == 0)
                        {
                            JW.Common.Log.LogE("Collect lua scripts failed.");
                        }
                        else
                        {
                            for (int i = 0; i < scripts.Length; i++)
                            {
                                lua.DoString(string.Format("require('{0}')", scripts[i]));
                                if ((i + 1) % 10 == 0)
                                {
                                    yield return null;
                                }
                            }
                        }
                    }
                    collectLua = null;
                    if (callback != null)
                    {
                        callback(LoadStateSuccess);
                    }
                    yield return null;
                    Log.LogD("Load main_lua All Done!");
                    _curLuaZip.Close();
                    _curLuaZip = null;

                }
            }
        }


        public byte[] CustomLuaLoader(ref string filepath)
        {
            if (_curLuaZip == null)
            {
                JW.Common.Log.LogE("CustomLuaLoader No Init LuaZip filePath: {0}", filepath);
                return null;
            }
            filepath = filepath.Replace('.', '/');
            //编辑器强制走调试用
#if UNITY_EDITOR 
            filepath = "LuaScripts/" + filepath + ".lua";
            string fullPath = JW.Res.FileUtil.CombinePath(Application.dataPath, "Resources/" + filepath);
            return JW.Res.FileUtil.ReadFile(fullPath);
#else
            string entryName = filepath + ".lua";
            byte[] fileData = _curLuaZip.GetEntryData(entryName);
            if (fileData == null)
            {
                Log.LogE("LuaLoader Load Lua From LuaZip Failed:{0}", entryName);
                return null;
            }
            else
            {
                return fileData;
            }
#endif

        }
#endif

        public bool LoadLuaZip(string luaZipFileName)
        {
            if (string.IsNullOrEmpty(luaZipFileName))
            {
                Log.LogE("LoadLuaZip Error Arg!");
                return false;
            }
            string filePath = FileUtil.CombinePath(JW.Res.FileUtil.GetIFSExtractPath(), luaZipFileName + ".bytes");
            if (!JW.Res.FileUtil.IsFileExist(filePath))
            {
                JW.Common.Log.LogE("LoadLuaZip Failed Not Exist File");
                return false;
            }
            if (_curLuaZip != null)
            {
                _curLuaZip.Close();
                _curLuaZip = null;
            }
            _curLuaZip = new IFSFile();
            if (_curLuaZip.InitWithFile(filePath) == false)
            {
                JW.Common.Log.LogE("LuaZip Init Failed");
                return false;
            }
            else
            {
                Log.LogD("<color=yellow>{0} LuaZip Init Successed!</color>", luaZipFileName);
                return true;
            }
        }

        public void UnLoadLuaZip()
        {
            if (_curLuaZip != null)
            {
                _curLuaZip.Close();
                _curLuaZip = null;
            }
        }

        public void UnInit()
        {
            if (_curLuaZip != null)
            {
                _curLuaZip.Close();
                _curLuaZip = null;
            }
        }
    }
}
