--[[********************************************************************
*      作者： jordenwu
*      时间： 07/17/17 15:39:01
*      描述： Unity3d PlayerPrefs包装
*********************************************************************--]]
local PlayerPrefsClass = DeclareClass("PlayerPrefsClass")

function PlayerPrefsClass:ctor()
    self._playerPrefs = CS.UnityEngine.PlayerPrefs
end

-- 设置整型存储
-- @param key 键
-- @param value 值
-- @return 无
function PlayerPrefsClass:SetInt(key, value)
    if not key or not value then
        LogE("PlayerPrefsClass.SetInt : invalid Parameter")
        return
    end
    
    self._playerPrefs.SetInt(key, value)
end

-- 获取整型存储
-- @param key 键
-- @return 值
function PlayerPrefsClass:GetInt(key)
    if not key then
        LogE("PlayerPrefsClass.GetInt : invalid Parameter")
        return
    end
    
    return self._playerPrefs.GetInt(key,0)
end

-- 设置浮点存储
-- @param key 键
-- @param value 值
-- @return 无
function PlayerPrefsClass:SetFloat(key, value)
    if not key or not value then
        LogE("PlayerPrefsClass.SetFloat : invalid Parameter")
        return
    end
    
    self._playerPrefs.SetFloat(key, value)
end

-- 获取浮点存储
-- @param key 键
-- @return 值
function PlayerPrefsClass:GetFloat(key)
    if not key then
        LogE("PlayerPrefsClass.GetFloat : invalid Parameter")
        return
    end
    
    return self._playerPrefs.GetFloat(key,0.0)
end

-- 设置字符串存储
-- @param key 键
-- @param value 值
-- @return 无
function PlayerPrefsClass:SetString(key, value)
    if not key or not value then
        LogE("PlayerPrefsClass.SetString : invalid Parameter")
        return
    end
    
    self._playerPrefs.SetString(key, value)
end

-- 获取字符串存储
-- @param key 键
-- @return 值
function PlayerPrefsClass:GetString(key)
    if not key then
        LogE("PlayerPrefsClass.GetString : invalid Parameter")
        return
    end
    
    return self._playerPrefs.GetString(key,'')
end

-- 是否存在存储
-- @param key 键
-- @return 是否存在
function PlayerPrefsClass:HasKey(key)
    if not key then
        LogE("PlayerPrefsClass.HasKey : invalid Parameter")
        return
    end
    
    return self._playerPrefs.HasKey(key)
end

-- 删除存储
-- @param key 键
-- @return 无
function PlayerPrefsClass:DeleteKey(key)
    if not key then
        LogE("PlayerPrefsClass.DeleteKey : invalid Parameter")
        return
    end
    
    self._playerPrefs.DeleteKey(key)
end

-- 删除所有存储
-- @return 无
function PlayerPrefsClass:DeleteAll() 
    self._playerPrefs.DeleteAll()
end

-- 保存存储
-- @return 无
function PlayerPrefsClass:Save() 
    self._playerPrefs.Save()
end

PlayerPrefs = PlayerPrefsClass.new()
