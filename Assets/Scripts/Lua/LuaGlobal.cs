/********************************************************************
	created:	2018-07-04
	filename: 	LuaGlobal
	author:		jordenwu
	
	purpose:	Lua全局对接
*********************************************************************/
namespace JW.Lua
{
    public static class LuaGlobal
    {
        public static bool IsDebug
        {
            get
            {
#if JW_DEBUG
             return true;
#else
            return false;
#endif
            }
        }

        public static bool IsEditor
        {
            get
            {
#if UNITY_EDITOR
                return true;
#else
                return false;
#endif

            }
        }

        public static bool IsUsePackRes
        {
            get
            {
#if USE_PACK_RES
                return true;
#else
                return false;
#endif
            }
        }

        public static bool IsWin
        {
            get
            {
#if UNITY_STANDALONE_WIN
            return true;
#else
            return false;
#endif
            }
        }

        public static bool IsUseLuaPack
        {
            get
            {
#if USE_LUA_PACK
                return true;
#else
                return false;
#endif
            }
        }


        public static string GetAppCodeVersion()
        {
            return VersionInfo.GetCodeVersion();
        }
    }
}
