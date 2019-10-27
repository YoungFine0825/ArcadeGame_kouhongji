#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class JWResFileUtilWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(JW.Res.FileUtil);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 35, 2, 2);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "IsFileExist", _m_IsFileExist_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsDirectoryExist", _m_IsDirectoryExist_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsExistInIFSExtraFolder", _m_IsExistInIFSExtraFolder_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsFileExistInStreamingAssets", _m_IsFileExistInStreamingAssets_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CreateDirectory", _m_CreateDirectory_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DeleteDirectory", _m_DeleteDirectory_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetFileLength", _m_GetFileLength_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadFile", _m_ReadFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadFileInStreamingAssets", _m_ReadFileInStreamingAssets_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "WriteFile", _m_WriteFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DeleteFile", _m_DeleteFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CopyFile", _m_CopyFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "MoveFile", _m_MoveFile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CopyDirectory", _m_CopyDirectory_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetFileMd5", _m_GetFileMd5_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetFileMd5ByFileStream", _m_GetFileMd5ByFileStream_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetMd5", _m_GetMd5_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ClearDirectory", _m_ClearDirectory_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CombinePath", _m_CombinePath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CombinePaths", _m_CombinePaths_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetStreamingAssetsPath", _m_GetStreamingAssetsPath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetStreamingAssetsPathWithHeader", _m_GetStreamingAssetsPathWithHeader_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetCachePath", _m_GetCachePath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetExeRootPath", _m_GetExeRootPath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetCachePathWithHeader", _m_GetCachePathWithHeader_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetIFSExtractPath", _m_GetIFSExtractPath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetIFSExtractPathWithHeader", _m_GetIFSExtractPathWithHeader_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetFullName", _m_GetFullName_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "EraseFirstDirectory", _m_EraseFirstDirectory_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "EraseExtension", _m_EraseExtension_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetExtension", _m_GetExtension_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetExtensionWithoutPoint", _m_GetExtensionWithoutPoint_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetFullDirectory", _m_GetFullDirectory_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetRelativePath", _m_GetRelativePath_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "OnOperateFileFail", _g_get_OnOperateFileFail);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "IFSExtractFolder", _g_get_IFSExtractFolder);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "OnOperateFileFail", _s_set_OnOperateFileFail);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "IFSExtractFolder", _s_set_IFSExtractFolder);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "JW.Res.FileUtil does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsFileExist_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string filePath = LuaAPI.lua_tostring(L, 1);
                    
                        bool __cl_gen_ret = JW.Res.FileUtil.IsFileExist( filePath );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsDirectoryExist_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string directory = LuaAPI.lua_tostring(L, 1);
                    
                        bool __cl_gen_ret = JW.Res.FileUtil.IsDirectoryExist( directory );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsExistInIFSExtraFolder_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string path = LuaAPI.lua_tostring(L, 1);
                    
                        bool __cl_gen_ret = JW.Res.FileUtil.IsExistInIFSExtraFolder( path );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsFileExistInStreamingAssets_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string fileName = LuaAPI.lua_tostring(L, 1);
                    
                        bool __cl_gen_ret = JW.Res.FileUtil.IsFileExistInStreamingAssets( fileName );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateDirectory_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string directory = LuaAPI.lua_tostring(L, 1);
                    
                        bool __cl_gen_ret = JW.Res.FileUtil.CreateDirectory( directory );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DeleteDirectory_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string directory = LuaAPI.lua_tostring(L, 1);
                    
                        bool __cl_gen_ret = JW.Res.FileUtil.DeleteDirectory( directory );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFileLength_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string filePath = LuaAPI.lua_tostring(L, 1);
                    
                        int __cl_gen_ret = JW.Res.FileUtil.GetFileLength( filePath );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string filePath = LuaAPI.lua_tostring(L, 1);
                    
                        byte[] __cl_gen_ret = JW.Res.FileUtil.ReadFile( filePath );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadFileInStreamingAssets_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string fileName = LuaAPI.lua_tostring(L, 1);
                    
                        byte[] __cl_gen_ret = JW.Res.FileUtil.ReadFileInStreamingAssets( fileName );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WriteFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    string filePath = LuaAPI.lua_tostring(L, 1);
                    byte[] data = LuaAPI.lua_tobytes(L, 2);
                    bool createDirectory = LuaAPI.lua_toboolean(L, 3);
                    
                        bool __cl_gen_ret = JW.Res.FileUtil.WriteFile( filePath, data, createDirectory );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string filePath = LuaAPI.lua_tostring(L, 1);
                    byte[] data = LuaAPI.lua_tobytes(L, 2);
                    
                        bool __cl_gen_ret = JW.Res.FileUtil.WriteFile( filePath, data );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    string filePath = LuaAPI.lua_tostring(L, 1);
                    byte[] data = LuaAPI.lua_tobytes(L, 2);
                    int offset = LuaAPI.xlua_tointeger(L, 3);
                    int length = LuaAPI.xlua_tointeger(L, 4);
                    
                        bool __cl_gen_ret = JW.Res.FileUtil.WriteFile( filePath, data, offset, length );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Res.FileUtil.WriteFile!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DeleteFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string filePath = LuaAPI.lua_tostring(L, 1);
                    
                        bool __cl_gen_ret = JW.Res.FileUtil.DeleteFile( filePath );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CopyFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    string srcFile = LuaAPI.lua_tostring(L, 1);
                    string dstFile = LuaAPI.lua_tostring(L, 2);
                    bool createDirectory = LuaAPI.lua_toboolean(L, 3);
                    
                    JW.Res.FileUtil.CopyFile( srcFile, dstFile, createDirectory );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string srcFile = LuaAPI.lua_tostring(L, 1);
                    string dstFile = LuaAPI.lua_tostring(L, 2);
                    
                    JW.Res.FileUtil.CopyFile( srcFile, dstFile );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Res.FileUtil.CopyFile!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MoveFile_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    string srcFile = LuaAPI.lua_tostring(L, 1);
                    string dstFile = LuaAPI.lua_tostring(L, 2);
                    bool createDirectory = LuaAPI.lua_toboolean(L, 3);
                    
                    JW.Res.FileUtil.MoveFile( srcFile, dstFile, createDirectory );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string srcFile = LuaAPI.lua_tostring(L, 1);
                    string dstFile = LuaAPI.lua_tostring(L, 2);
                    
                    JW.Res.FileUtil.MoveFile( srcFile, dstFile );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Res.FileUtil.MoveFile!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CopyDirectory_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string sourceDirName = LuaAPI.lua_tostring(L, 1);
                    string destDirName = LuaAPI.lua_tostring(L, 2);
                    bool copySubDirs = LuaAPI.lua_toboolean(L, 3);
                    
                    JW.Res.FileUtil.CopyDirectory( sourceDirName, destDirName, copySubDirs );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFileMd5_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string filePath = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = JW.Res.FileUtil.GetFileMd5( filePath );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFileMd5ByFileStream_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string filePath = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = JW.Res.FileUtil.GetFileMd5ByFileStream( filePath );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetMd5_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    byte[] data = LuaAPI.lua_tobytes(L, 1);
                    
                        string __cl_gen_ret = JW.Res.FileUtil.GetMd5( data );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string str = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = JW.Res.FileUtil.GetMd5( str );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Res.FileUtil.GetMd5!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearDirectory_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string fullPath = LuaAPI.lua_tostring(L, 1);
                    
                        bool __cl_gen_ret = JW.Res.FileUtil.ClearDirectory( fullPath );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<string[]>(L, 2)&& translator.Assignable<string[]>(L, 3)) 
                {
                    string fullPath = LuaAPI.lua_tostring(L, 1);
                    string[] fileExtensionFilter = (string[])translator.GetObject(L, 2, typeof(string[]));
                    string[] folderFilter = (string[])translator.GetObject(L, 3, typeof(string[]));
                    
                        bool __cl_gen_ret = JW.Res.FileUtil.ClearDirectory( fullPath, fileExtensionFilter, folderFilter );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Res.FileUtil.ClearDirectory!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CombinePath_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string path1 = LuaAPI.lua_tostring(L, 1);
                    string path2 = LuaAPI.lua_tostring(L, 2);
                    
                        string __cl_gen_ret = JW.Res.FileUtil.CombinePath( path1, path2 );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CombinePaths_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string[] values = translator.GetParams<string>(L, 1);
                    
                        string __cl_gen_ret = JW.Res.FileUtil.CombinePaths( values );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetStreamingAssetsPath_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string fileName = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = JW.Res.FileUtil.GetStreamingAssetsPath( fileName );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 0) 
                {
                    
                        string __cl_gen_ret = JW.Res.FileUtil.GetStreamingAssetsPath(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Res.FileUtil.GetStreamingAssetsPath!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetStreamingAssetsPathWithHeader_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string fileName = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = JW.Res.FileUtil.GetStreamingAssetsPathWithHeader( fileName );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCachePath_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 0) 
                {
                    
                        string __cl_gen_ret = JW.Res.FileUtil.GetCachePath(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string fileName = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = JW.Res.FileUtil.GetCachePath( fileName );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to JW.Res.FileUtil.GetCachePath!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetExeRootPath_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string __cl_gen_ret = JW.Res.FileUtil.GetExeRootPath(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCachePathWithHeader_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string fileName = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = JW.Res.FileUtil.GetCachePathWithHeader( fileName );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetIFSExtractPath_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string __cl_gen_ret = JW.Res.FileUtil.GetIFSExtractPath(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetIFSExtractPathWithHeader_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string __cl_gen_ret = JW.Res.FileUtil.GetIFSExtractPathWithHeader(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFullName_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string fullPath = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = JW.Res.FileUtil.GetFullName( fullPath );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EraseFirstDirectory_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string fullPath = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = JW.Res.FileUtil.EraseFirstDirectory( fullPath );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EraseExtension_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string fullName = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = JW.Res.FileUtil.EraseExtension( fullName );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetExtension_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string fullName = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = JW.Res.FileUtil.GetExtension( fullName );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetExtensionWithoutPoint_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string fullName = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = JW.Res.FileUtil.GetExtensionWithoutPoint( fullName );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFullDirectory_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string fullPath = LuaAPI.lua_tostring(L, 1);
                    
                        string __cl_gen_ret = JW.Res.FileUtil.GetFullDirectory( fullPath );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetRelativePath_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string fullPath = LuaAPI.lua_tostring(L, 1);
                    string parentDir = LuaAPI.lua_tostring(L, 2);
                    
                        string __cl_gen_ret = JW.Res.FileUtil.GetRelativePath( fullPath, parentDir );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnOperateFileFail(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, JW.Res.FileUtil.OnOperateFileFail);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IFSExtractFolder(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, JW.Res.FileUtil.IFSExtractFolder);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnOperateFileFail(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    JW.Res.FileUtil.OnOperateFileFail = translator.GetDelegate<JW.Res.FileUtil.OnOperateFileFailDelegate>(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IFSExtractFolder(RealStatePtr L)
        {
		    try {
                
			    JW.Res.FileUtil.IFSExtractFolder = LuaAPI.lua_tostring(L, 1);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
