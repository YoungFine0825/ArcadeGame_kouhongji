--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-07-04
*		描述： 资产加载回掉
*********************************************************************--]]
local AssetLoadCallbackClass = DeclareClass("AssetLoadCallbackClass")
local null = {}

function AssetLoadCallbackClass:ctor()
    self._data = {}
end

function AssetLoadCallbackClass:Add(name, tbl)
    if not name or not tbl then
        LogE("AssetLoadCallbackClass.Add : invalid parameter")
        return
    end
    
    for i = 1, #self._data do
        local data = self._data[i]
        if data ~= null and data.name == name and data.tbl == tbl then
            LogE("AssetLoadCallbackClass.Add : duplicate add")
            return
        end
    end
    
    local data = Pool:CreateTable()
    data.name = name
    data.tbl = tbl
    self._data[#self._data+1] = data
end

function AssetLoadCallbackClass:Remove(tbl, assetName)
    if not tbl then
        LogE("AssetLoadCallbackClass.Remove : invalid parameter")
        return
    end

    for i = 1, #self._data do
        local data = self._data[i]
        
        if data ~= null and data.tbl == tbl and (not assetName or data.name == assetName) then
            self._data[i] = null
            Pool:DestroyTable(data)
        end
    end
end

function AssetLoadCallbackClass:OnLoadAssetCompleted(name, result, resource)
    if not name then
        LogE("AssetLoadCallbackClass.OnLoadAssetCompleted : invalid parameter")
        return
    end

    local i = 1
    while true do
        if i > #self._data then
            break
        end
        
        local data = self._data[i]
        
        if data == null then
            table.remove(self._data, i)
        elseif data.name == name then
            local tbl = data.tbl
            self._data[i] = null
            Pool:DestroyTable(data)
            
            i = i + 1
            
            if tbl and tbl.OnLoadAssetCompleted then
                tbl:OnLoadAssetCompleted(name, result, resource)
            end
        else
            i = i + 1
        end
    end
end