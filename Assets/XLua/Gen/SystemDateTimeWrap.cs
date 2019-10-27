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
    public class SystemDateTimeWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(System.DateTime);
			Utils.BeginObjectRegister(type, L, translator, 5, 27, 13, 0);
			Utils.RegisterFunc(L, Utils.OBJ_META_IDX, "__add", __AddMeta);
            Utils.RegisterFunc(L, Utils.OBJ_META_IDX, "__eq", __EqMeta);
            Utils.RegisterFunc(L, Utils.OBJ_META_IDX, "__lt", __LTMeta);
            Utils.RegisterFunc(L, Utils.OBJ_META_IDX, "__le", __LEMeta);
            Utils.RegisterFunc(L, Utils.OBJ_META_IDX, "__sub", __SubMeta);
            
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Add", _m_Add);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddDays", _m_AddDays);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddTicks", _m_AddTicks);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddHours", _m_AddHours);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddMilliseconds", _m_AddMilliseconds);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddMinutes", _m_AddMinutes);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddMonths", _m_AddMonths);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddSeconds", _m_AddSeconds);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddYears", _m_AddYears);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CompareTo", _m_CompareTo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsDaylightSavingTime", _m_IsDaylightSavingTime);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Equals", _m_Equals);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToBinary", _m_ToBinary);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetDateTimeFormats", _m_GetDateTimeFormats);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetHashCode", _m_GetHashCode);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetTypeCode", _m_GetTypeCode);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Subtract", _m_Subtract);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToFileTime", _m_ToFileTime);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToFileTimeUtc", _m_ToFileTimeUtc);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToLongDateString", _m_ToLongDateString);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToLongTimeString", _m_ToLongTimeString);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToOADate", _m_ToOADate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToShortDateString", _m_ToShortDateString);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToShortTimeString", _m_ToShortTimeString);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToString", _m_ToString);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToLocalTime", _m_ToLocalTime);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToUniversalTime", _m_ToUniversalTime);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Date", _g_get_Date);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Month", _g_get_Month);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Day", _g_get_Day);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "DayOfWeek", _g_get_DayOfWeek);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "DayOfYear", _g_get_DayOfYear);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "TimeOfDay", _g_get_TimeOfDay);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Hour", _g_get_Hour);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Minute", _g_get_Minute);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Second", _g_get_Second);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Millisecond", _g_get_Millisecond);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Ticks", _g_get_Ticks);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Year", _g_get_Year);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Kind", _g_get_Kind);
            
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 16, 3, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Compare", _m_Compare_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FromBinary", _m_FromBinary_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SpecifyKind", _m_SpecifyKind_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DaysInMonth", _m_DaysInMonth_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Equals", _m_Equals_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FromFileTime", _m_FromFileTime_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FromFileTimeUtc", _m_FromFileTimeUtc_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FromOADate", _m_FromOADate_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsLeapYear", _m_IsLeapYear_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Parse", _m_Parse_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ParseExact", _m_ParseExact_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "TryParse", _m_TryParse_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "TryParseExact", _m_TryParseExact_xlua_st_);
            
			
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "MaxValue", System.DateTime.MaxValue);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "MinValue", System.DateTime.MinValue);
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Now", _g_get_Now);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Today", _g_get_Today);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "UtcNow", _g_get_UtcNow);
            
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 2 && (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) || LuaAPI.lua_isint64(L, 2)))
				{
					long ticks = LuaAPI.lua_toint64(L, 2);
					
					System.DateTime __cl_gen_ret = new System.DateTime(ticks);
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 4 && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4))
				{
					int year = LuaAPI.xlua_tointeger(L, 2);
					int month = LuaAPI.xlua_tointeger(L, 3);
					int day = LuaAPI.xlua_tointeger(L, 4);
					
					System.DateTime __cl_gen_ret = new System.DateTime(year, month, day);
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 7 && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7))
				{
					int year = LuaAPI.xlua_tointeger(L, 2);
					int month = LuaAPI.xlua_tointeger(L, 3);
					int day = LuaAPI.xlua_tointeger(L, 4);
					int hour = LuaAPI.xlua_tointeger(L, 5);
					int minute = LuaAPI.xlua_tointeger(L, 6);
					int second = LuaAPI.xlua_tointeger(L, 7);
					
					System.DateTime __cl_gen_ret = new System.DateTime(year, month, day, hour, minute, second);
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 8 && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 8))
				{
					int year = LuaAPI.xlua_tointeger(L, 2);
					int month = LuaAPI.xlua_tointeger(L, 3);
					int day = LuaAPI.xlua_tointeger(L, 4);
					int hour = LuaAPI.xlua_tointeger(L, 5);
					int minute = LuaAPI.xlua_tointeger(L, 6);
					int second = LuaAPI.xlua_tointeger(L, 7);
					int millisecond = LuaAPI.xlua_tointeger(L, 8);
					
					System.DateTime __cl_gen_ret = new System.DateTime(year, month, day, hour, minute, second, millisecond);
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 5 && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) && translator.Assignable<System.Globalization.Calendar>(L, 5))
				{
					int year = LuaAPI.xlua_tointeger(L, 2);
					int month = LuaAPI.xlua_tointeger(L, 3);
					int day = LuaAPI.xlua_tointeger(L, 4);
					System.Globalization.Calendar calendar = (System.Globalization.Calendar)translator.GetObject(L, 5, typeof(System.Globalization.Calendar));
					
					System.DateTime __cl_gen_ret = new System.DateTime(year, month, day, calendar);
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 8 && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7) && translator.Assignable<System.Globalization.Calendar>(L, 8))
				{
					int year = LuaAPI.xlua_tointeger(L, 2);
					int month = LuaAPI.xlua_tointeger(L, 3);
					int day = LuaAPI.xlua_tointeger(L, 4);
					int hour = LuaAPI.xlua_tointeger(L, 5);
					int minute = LuaAPI.xlua_tointeger(L, 6);
					int second = LuaAPI.xlua_tointeger(L, 7);
					System.Globalization.Calendar calendar = (System.Globalization.Calendar)translator.GetObject(L, 8, typeof(System.Globalization.Calendar));
					
					System.DateTime __cl_gen_ret = new System.DateTime(year, month, day, hour, minute, second, calendar);
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 9 && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 8) && translator.Assignable<System.Globalization.Calendar>(L, 9))
				{
					int year = LuaAPI.xlua_tointeger(L, 2);
					int month = LuaAPI.xlua_tointeger(L, 3);
					int day = LuaAPI.xlua_tointeger(L, 4);
					int hour = LuaAPI.xlua_tointeger(L, 5);
					int minute = LuaAPI.xlua_tointeger(L, 6);
					int second = LuaAPI.xlua_tointeger(L, 7);
					int millisecond = LuaAPI.xlua_tointeger(L, 8);
					System.Globalization.Calendar calendar = (System.Globalization.Calendar)translator.GetObject(L, 9, typeof(System.Globalization.Calendar));
					
					System.DateTime __cl_gen_ret = new System.DateTime(year, month, day, hour, minute, second, millisecond, calendar);
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 3 && (LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) || LuaAPI.lua_isint64(L, 2)) && translator.Assignable<System.DateTimeKind>(L, 3))
				{
					long ticks = LuaAPI.lua_toint64(L, 2);
					System.DateTimeKind kind;translator.Get(L, 3, out kind);
					
					System.DateTime __cl_gen_ret = new System.DateTime(ticks, kind);
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 8 && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7) && translator.Assignable<System.DateTimeKind>(L, 8))
				{
					int year = LuaAPI.xlua_tointeger(L, 2);
					int month = LuaAPI.xlua_tointeger(L, 3);
					int day = LuaAPI.xlua_tointeger(L, 4);
					int hour = LuaAPI.xlua_tointeger(L, 5);
					int minute = LuaAPI.xlua_tointeger(L, 6);
					int second = LuaAPI.xlua_tointeger(L, 7);
					System.DateTimeKind kind;translator.Get(L, 8, out kind);
					
					System.DateTime __cl_gen_ret = new System.DateTime(year, month, day, hour, minute, second, kind);
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 9 && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 8) && translator.Assignable<System.DateTimeKind>(L, 9))
				{
					int year = LuaAPI.xlua_tointeger(L, 2);
					int month = LuaAPI.xlua_tointeger(L, 3);
					int day = LuaAPI.xlua_tointeger(L, 4);
					int hour = LuaAPI.xlua_tointeger(L, 5);
					int minute = LuaAPI.xlua_tointeger(L, 6);
					int second = LuaAPI.xlua_tointeger(L, 7);
					int millisecond = LuaAPI.xlua_tointeger(L, 8);
					System.DateTimeKind kind;translator.Get(L, 9, out kind);
					
					System.DateTime __cl_gen_ret = new System.DateTime(year, month, day, hour, minute, second, millisecond, kind);
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 10 && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 7) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 8) && translator.Assignable<System.Globalization.Calendar>(L, 9) && translator.Assignable<System.DateTimeKind>(L, 10))
				{
					int year = LuaAPI.xlua_tointeger(L, 2);
					int month = LuaAPI.xlua_tointeger(L, 3);
					int day = LuaAPI.xlua_tointeger(L, 4);
					int hour = LuaAPI.xlua_tointeger(L, 5);
					int minute = LuaAPI.xlua_tointeger(L, 6);
					int second = LuaAPI.xlua_tointeger(L, 7);
					int millisecond = LuaAPI.xlua_tointeger(L, 8);
					System.Globalization.Calendar calendar = (System.Globalization.Calendar)translator.GetObject(L, 9, typeof(System.Globalization.Calendar));
					System.DateTimeKind kind;translator.Get(L, 10, out kind);
					
					System.DateTime __cl_gen_ret = new System.DateTime(year, month, day, hour, minute, second, millisecond, calendar, kind);
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
				if (LuaAPI.lua_gettop(L) == 1)
				{
				    translator.Push(L, default(System.DateTime));
			        return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to System.DateTime constructor!");
            
        }
        
		
        
		
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __AddMeta(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
			
				if (translator.Assignable<System.DateTime>(L, 1) && translator.Assignable<System.TimeSpan>(L, 2))
				{
					System.DateTime leftside;translator.Get(L, 1, out leftside);
					System.TimeSpan rightside;translator.Get(L, 2, out rightside);
					
					translator.Push(L, leftside + rightside);
					
					return 1;
				}
            
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to right hand of + operator, need System.DateTime!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __EqMeta(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
			
				if (translator.Assignable<System.DateTime>(L, 1) && translator.Assignable<System.DateTime>(L, 2))
				{
					System.DateTime leftside;translator.Get(L, 1, out leftside);
					System.DateTime rightside;translator.Get(L, 2, out rightside);
					
					LuaAPI.lua_pushboolean(L, leftside == rightside);
					
					return 1;
				}
            
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to right hand of == operator, need System.DateTime!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __LTMeta(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
			
				if (translator.Assignable<System.DateTime>(L, 1) && translator.Assignable<System.DateTime>(L, 2))
				{
					System.DateTime leftside;translator.Get(L, 1, out leftside);
					System.DateTime rightside;translator.Get(L, 2, out rightside);
					
					LuaAPI.lua_pushboolean(L, leftside < rightside);
					
					return 1;
				}
            
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to right hand of < operator, need System.DateTime!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __LEMeta(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
			
				if (translator.Assignable<System.DateTime>(L, 1) && translator.Assignable<System.DateTime>(L, 2))
				{
					System.DateTime leftside;translator.Get(L, 1, out leftside);
					System.DateTime rightside;translator.Get(L, 2, out rightside);
					
					LuaAPI.lua_pushboolean(L, leftside <= rightside);
					
					return 1;
				}
            
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to right hand of <= operator, need System.DateTime!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __SubMeta(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
			
				if (translator.Assignable<System.DateTime>(L, 1) && translator.Assignable<System.DateTime>(L, 2))
				{
					System.DateTime leftside;translator.Get(L, 1, out leftside);
					System.DateTime rightside;translator.Get(L, 2, out rightside);
					
					translator.Push(L, leftside - rightside);
					
					return 1;
				}
            
			
				if (translator.Assignable<System.DateTime>(L, 1) && translator.Assignable<System.TimeSpan>(L, 2))
				{
					System.DateTime leftside;translator.Get(L, 1, out leftside);
					System.TimeSpan rightside;translator.Get(L, 2, out rightside);
					
					translator.Push(L, leftside - rightside);
					
					return 1;
				}
            
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to right hand of - operator, need System.DateTime!");
            
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Add(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    System.TimeSpan value;translator.Get(L, 2, out value);
                    
                        System.DateTime __cl_gen_ret = __cl_gen_to_be_invoked.Add( value );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddDays(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    double value = LuaAPI.lua_tonumber(L, 2);
                    
                        System.DateTime __cl_gen_ret = __cl_gen_to_be_invoked.AddDays( value );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddTicks(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    long value = LuaAPI.lua_toint64(L, 2);
                    
                        System.DateTime __cl_gen_ret = __cl_gen_to_be_invoked.AddTicks( value );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddHours(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    double value = LuaAPI.lua_tonumber(L, 2);
                    
                        System.DateTime __cl_gen_ret = __cl_gen_to_be_invoked.AddHours( value );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddMilliseconds(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    double value = LuaAPI.lua_tonumber(L, 2);
                    
                        System.DateTime __cl_gen_ret = __cl_gen_to_be_invoked.AddMilliseconds( value );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddMinutes(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    double value = LuaAPI.lua_tonumber(L, 2);
                    
                        System.DateTime __cl_gen_ret = __cl_gen_to_be_invoked.AddMinutes( value );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddMonths(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    int months = LuaAPI.xlua_tointeger(L, 2);
                    
                        System.DateTime __cl_gen_ret = __cl_gen_to_be_invoked.AddMonths( months );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddSeconds(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    double value = LuaAPI.lua_tonumber(L, 2);
                    
                        System.DateTime __cl_gen_ret = __cl_gen_to_be_invoked.AddSeconds( value );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddYears(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    int value = LuaAPI.xlua_tointeger(L, 2);
                    
                        System.DateTime __cl_gen_ret = __cl_gen_to_be_invoked.AddYears( value );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Compare_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.DateTime t1;translator.Get(L, 1, out t1);
                    System.DateTime t2;translator.Get(L, 2, out t2);
                    
                        int __cl_gen_ret = System.DateTime.Compare( t1, t2 );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CompareTo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& translator.Assignable<object>(L, 2)) 
                {
                    object value = translator.GetObject(L, 2, typeof(object));
                    
                        int __cl_gen_ret = __cl_gen_to_be_invoked.CompareTo( value );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<System.DateTime>(L, 2)) 
                {
                    System.DateTime value;translator.Get(L, 2, out value);
                    
                        int __cl_gen_ret = __cl_gen_to_be_invoked.CompareTo( value );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.DateTime.CompareTo!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsDaylightSavingTime(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.IsDaylightSavingTime(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Equals(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& translator.Assignable<System.DateTime>(L, 2)) 
                {
                    System.DateTime value;translator.Get(L, 2, out value);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.Equals( value );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<object>(L, 2)) 
                {
                    object value = translator.GetObject(L, 2, typeof(object));
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.Equals( value );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.DateTime.Equals!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToBinary(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                        long __cl_gen_ret = __cl_gen_to_be_invoked.ToBinary(  );
                        LuaAPI.lua_pushint64(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FromBinary_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    long dateData = LuaAPI.lua_toint64(L, 1);
                    
                        System.DateTime __cl_gen_ret = System.DateTime.FromBinary( dateData );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SpecifyKind_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.DateTime value;translator.Get(L, 1, out value);
                    System.DateTimeKind kind;translator.Get(L, 2, out kind);
                    
                        System.DateTime __cl_gen_ret = System.DateTime.SpecifyKind( value, kind );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DaysInMonth_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    int year = LuaAPI.xlua_tointeger(L, 1);
                    int month = LuaAPI.xlua_tointeger(L, 2);
                    
                        int __cl_gen_ret = System.DateTime.DaysInMonth( year, month );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Equals_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.DateTime t1;translator.Get(L, 1, out t1);
                    System.DateTime t2;translator.Get(L, 2, out t2);
                    
                        bool __cl_gen_ret = System.DateTime.Equals( t1, t2 );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FromFileTime_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    long fileTime = LuaAPI.lua_toint64(L, 1);
                    
                        System.DateTime __cl_gen_ret = System.DateTime.FromFileTime( fileTime );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FromFileTimeUtc_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    long fileTime = LuaAPI.lua_toint64(L, 1);
                    
                        System.DateTime __cl_gen_ret = System.DateTime.FromFileTimeUtc( fileTime );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FromOADate_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    double d = LuaAPI.lua_tonumber(L, 1);
                    
                        System.DateTime __cl_gen_ret = System.DateTime.FromOADate( d );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetDateTimeFormats(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1) 
                {
                    
                        string[] __cl_gen_ret = __cl_gen_to_be_invoked.GetDateTimeFormats(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    char format = (char)LuaAPI.xlua_tointeger(L, 2);
                    
                        string[] __cl_gen_ret = __cl_gen_to_be_invoked.GetDateTimeFormats( format );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<System.IFormatProvider>(L, 2)) 
                {
                    System.IFormatProvider provider = (System.IFormatProvider)translator.GetObject(L, 2, typeof(System.IFormatProvider));
                    
                        string[] __cl_gen_ret = __cl_gen_to_be_invoked.GetDateTimeFormats( provider );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.IFormatProvider>(L, 3)) 
                {
                    char format = (char)LuaAPI.xlua_tointeger(L, 2);
                    System.IFormatProvider provider = (System.IFormatProvider)translator.GetObject(L, 3, typeof(System.IFormatProvider));
                    
                        string[] __cl_gen_ret = __cl_gen_to_be_invoked.GetDateTimeFormats( format, provider );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.DateTime.GetDateTimeFormats!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetHashCode(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                        int __cl_gen_ret = __cl_gen_to_be_invoked.GetHashCode(  );
                        LuaAPI.xlua_pushinteger(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTypeCode(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                        System.TypeCode __cl_gen_ret = __cl_gen_to_be_invoked.GetTypeCode(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsLeapYear_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    int year = LuaAPI.xlua_tointeger(L, 1);
                    
                        bool __cl_gen_ret = System.DateTime.IsLeapYear( year );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Parse_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string s = LuaAPI.lua_tostring(L, 1);
                    
                        System.DateTime __cl_gen_ret = System.DateTime.Parse( s );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.IFormatProvider>(L, 2)) 
                {
                    string s = LuaAPI.lua_tostring(L, 1);
                    System.IFormatProvider provider = (System.IFormatProvider)translator.GetObject(L, 2, typeof(System.IFormatProvider));
                    
                        System.DateTime __cl_gen_ret = System.DateTime.Parse( s, provider );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.IFormatProvider>(L, 2)&& translator.Assignable<System.Globalization.DateTimeStyles>(L, 3)) 
                {
                    string s = LuaAPI.lua_tostring(L, 1);
                    System.IFormatProvider provider = (System.IFormatProvider)translator.GetObject(L, 2, typeof(System.IFormatProvider));
                    System.Globalization.DateTimeStyles styles;translator.Get(L, 3, out styles);
                    
                        System.DateTime __cl_gen_ret = System.DateTime.Parse( s, provider, styles );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.DateTime.Parse!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ParseExact_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.IFormatProvider>(L, 3)) 
                {
                    string s = LuaAPI.lua_tostring(L, 1);
                    string format = LuaAPI.lua_tostring(L, 2);
                    System.IFormatProvider provider = (System.IFormatProvider)translator.GetObject(L, 3, typeof(System.IFormatProvider));
                    
                        System.DateTime __cl_gen_ret = System.DateTime.ParseExact( s, format, provider );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.IFormatProvider>(L, 3)&& translator.Assignable<System.Globalization.DateTimeStyles>(L, 4)) 
                {
                    string s = LuaAPI.lua_tostring(L, 1);
                    string format = LuaAPI.lua_tostring(L, 2);
                    System.IFormatProvider provider = (System.IFormatProvider)translator.GetObject(L, 3, typeof(System.IFormatProvider));
                    System.Globalization.DateTimeStyles style;translator.Get(L, 4, out style);
                    
                        System.DateTime __cl_gen_ret = System.DateTime.ParseExact( s, format, provider, style );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<string[]>(L, 2)&& translator.Assignable<System.IFormatProvider>(L, 3)&& translator.Assignable<System.Globalization.DateTimeStyles>(L, 4)) 
                {
                    string s = LuaAPI.lua_tostring(L, 1);
                    string[] formats = (string[])translator.GetObject(L, 2, typeof(string[]));
                    System.IFormatProvider provider = (System.IFormatProvider)translator.GetObject(L, 3, typeof(System.IFormatProvider));
                    System.Globalization.DateTimeStyles style;translator.Get(L, 4, out style);
                    
                        System.DateTime __cl_gen_ret = System.DateTime.ParseExact( s, formats, provider, style );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.DateTime.ParseExact!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TryParse_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string s = LuaAPI.lua_tostring(L, 1);
                    System.DateTime result;
                    
                        bool __cl_gen_ret = System.DateTime.TryParse( s, out result );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    translator.Push(L, result);
                        
                    
                    
                    
                    return 2;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.IFormatProvider>(L, 2)&& translator.Assignable<System.Globalization.DateTimeStyles>(L, 3)) 
                {
                    string s = LuaAPI.lua_tostring(L, 1);
                    System.IFormatProvider provider = (System.IFormatProvider)translator.GetObject(L, 2, typeof(System.IFormatProvider));
                    System.Globalization.DateTimeStyles styles;translator.Get(L, 3, out styles);
                    System.DateTime result;
                    
                        bool __cl_gen_ret = System.DateTime.TryParse( s, provider, styles, out result );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    translator.Push(L, result);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.DateTime.TryParse!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TryParseExact_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.IFormatProvider>(L, 3)&& translator.Assignable<System.Globalization.DateTimeStyles>(L, 4)) 
                {
                    string s = LuaAPI.lua_tostring(L, 1);
                    string format = LuaAPI.lua_tostring(L, 2);
                    System.IFormatProvider provider = (System.IFormatProvider)translator.GetObject(L, 3, typeof(System.IFormatProvider));
                    System.Globalization.DateTimeStyles style;translator.Get(L, 4, out style);
                    System.DateTime result;
                    
                        bool __cl_gen_ret = System.DateTime.TryParseExact( s, format, provider, style, out result );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    translator.Push(L, result);
                        
                    
                    
                    
                    return 2;
                }
                if(__gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<string[]>(L, 2)&& translator.Assignable<System.IFormatProvider>(L, 3)&& translator.Assignable<System.Globalization.DateTimeStyles>(L, 4)) 
                {
                    string s = LuaAPI.lua_tostring(L, 1);
                    string[] formats = (string[])translator.GetObject(L, 2, typeof(string[]));
                    System.IFormatProvider provider = (System.IFormatProvider)translator.GetObject(L, 3, typeof(System.IFormatProvider));
                    System.Globalization.DateTimeStyles style;translator.Get(L, 4, out style);
                    System.DateTime result;
                    
                        bool __cl_gen_ret = System.DateTime.TryParseExact( s, formats, provider, style, out result );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    translator.Push(L, result);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.DateTime.TryParseExact!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Subtract(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& translator.Assignable<System.DateTime>(L, 2)) 
                {
                    System.DateTime value;translator.Get(L, 2, out value);
                    
                        System.TimeSpan __cl_gen_ret = __cl_gen_to_be_invoked.Subtract( value );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<System.TimeSpan>(L, 2)) 
                {
                    System.TimeSpan value;translator.Get(L, 2, out value);
                    
                        System.DateTime __cl_gen_ret = __cl_gen_to_be_invoked.Subtract( value );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.DateTime.Subtract!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToFileTime(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                        long __cl_gen_ret = __cl_gen_to_be_invoked.ToFileTime(  );
                        LuaAPI.lua_pushint64(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToFileTimeUtc(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                        long __cl_gen_ret = __cl_gen_to_be_invoked.ToFileTimeUtc(  );
                        LuaAPI.lua_pushint64(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToLongDateString(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                        string __cl_gen_ret = __cl_gen_to_be_invoked.ToLongDateString(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToLongTimeString(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                        string __cl_gen_ret = __cl_gen_to_be_invoked.ToLongTimeString(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToOADate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                        double __cl_gen_ret = __cl_gen_to_be_invoked.ToOADate(  );
                        LuaAPI.lua_pushnumber(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToShortDateString(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                        string __cl_gen_ret = __cl_gen_to_be_invoked.ToShortDateString(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToShortTimeString(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                        string __cl_gen_ret = __cl_gen_to_be_invoked.ToShortTimeString(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToString(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1) 
                {
                    
                        string __cl_gen_ret = __cl_gen_to_be_invoked.ToString(  );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& translator.Assignable<System.IFormatProvider>(L, 2)) 
                {
                    System.IFormatProvider provider = (System.IFormatProvider)translator.GetObject(L, 2, typeof(System.IFormatProvider));
                    
                        string __cl_gen_ret = __cl_gen_to_be_invoked.ToString( provider );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string format = LuaAPI.lua_tostring(L, 2);
                    
                        string __cl_gen_ret = __cl_gen_to_be_invoked.ToString( format );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                if(__gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.IFormatProvider>(L, 3)) 
                {
                    string format = LuaAPI.lua_tostring(L, 2);
                    System.IFormatProvider provider = (System.IFormatProvider)translator.GetObject(L, 3, typeof(System.IFormatProvider));
                    
                        string __cl_gen_ret = __cl_gen_to_be_invoked.ToString( format, provider );
                        LuaAPI.lua_pushstring(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.DateTime.ToString!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToLocalTime(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                        System.DateTime __cl_gen_ret = __cl_gen_to_be_invoked.ToLocalTime(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToUniversalTime(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
            
            
                
                {
                    
                        System.DateTime __cl_gen_ret = __cl_gen_to_be_invoked.ToUniversalTime(  );
                        translator.Push(L, __cl_gen_ret);
                    
                    
                        translator.Update(L, 1, __cl_gen_to_be_invoked);
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Date(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                translator.Push(L, __cl_gen_to_be_invoked.Date);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Month(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.Month);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Day(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.Day);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DayOfWeek(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                translator.Push(L, __cl_gen_to_be_invoked.DayOfWeek);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DayOfYear(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.DayOfYear);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_TimeOfDay(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                translator.Push(L, __cl_gen_to_be_invoked.TimeOfDay);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Hour(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.Hour);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Minute(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.Minute);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Second(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.Second);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Millisecond(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.Millisecond);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Now(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, System.DateTime.Now);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Ticks(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                LuaAPI.lua_pushint64(L, __cl_gen_to_be_invoked.Ticks);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Today(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, System.DateTime.Today);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UtcNow(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, System.DateTime.UtcNow);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Year(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.Year);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Kind(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.DateTime __cl_gen_to_be_invoked;translator.Get(L, 1, out __cl_gen_to_be_invoked);
                translator.Push(L, __cl_gen_to_be_invoked.Kind);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
