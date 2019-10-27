--[[********************************************************************
*      作者： jordenwu
*      时间： 10/23/17 15:11:49
*      描述： 全局宏
*********************************************************************--]]
local global=CS.JW.Lua.LuaGlobal
Global={
}
local isDebug=global.IsDebug
local isEditor = global.IsEditor
local isUsePackRes = global.IsUsePackRes
local isWin = global.IsWin
local isUseLuaPack=global.IsUseLuaPack

--
Global.IsDebug=isDebug
Global.IsEditor=isEditor
Global.IsUsePackRes=isUsePackRes
Global.IsWin=isWin
Global.IsUseLuaPack=isUseLuaPack

--游戏代码版本
local appCodeVersion=global.GetAppCodeVersion()
Global.AppCodeVersion=appCodeVersion






