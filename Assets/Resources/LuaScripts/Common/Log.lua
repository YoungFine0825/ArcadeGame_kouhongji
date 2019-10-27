--[[********************************************************************
*      作者： jordenwu
*      时间： 2018-07-02
*      描述： Log对接
*********************************************************************--]]
local _interaction = CS.JW.Lua.LuaInteraction
local _isDebug=Global.IsDebug

function LogD(content, ...)
    
    if not _isDebug then
        return
    end
    if not content then
       print("LogD Arg Error") 
       return
    end
    if select('#', ...) > 0 then
        content = string.format(content, select(1, ...))
    end
    local info=debug.getinfo(2,"Sl") 
    local path=info.short_src
    local row=info.currentline  
    _interaction.Log(0,"[[:"..path..":"..row..':]]'..content)
end


function LogW(content, ...)
   
   if not _isDebug then
        return
    end
    
   if select('#', ...) > 0 then
        content = string.format(content, select(1, ...))
    end
    local info=debug.getinfo(2,"S") 
    local path=info.short_src
    local row=info.lastlinedefined 
    _interaction.Log(1,"[[:"..path..":"..row..':]]'..content)
end

function LogE(content, ...)
    
    if select('#', ...) > 0 then
        content = string.format(content, select(1, ...))
    end
    local info=debug.getinfo(2,"S") 
    local path=info.short_src
    local row=info.lastlinedefined 
    _interaction.Log(2,"[[:"..path..":"..row..':]]'..content..debug.traceback())
    --_interaction.Log(2,"[[:"..path..":"..row..':]]'..content)
    
end

function LogDump(value, desciption, nesting)
    
    if not _isDebug then
        return
    end
    
    if type(nesting) ~= "number" then nesting = 10 end
    local lookupTable = {}
    local result = {}

    local function _v(v)
        if type(v) == "string" then
            v = "\"" .. v .. "\""
        end
        return tostring(v)
    end

    LogD("<color=yellow>Lua Dump Begin</color>")

    local function _dump(value, desciption, indent, nest, keylen)
        desciption = desciption or "<var>"
        local spc = ""
        if type(keylen) == "number" then
            spc = string.rep(" ", keylen - string.len(_v(desciption)))
        end
        if type(value) ~= "table" then
            result[#result +1 ] = string.format("%s%s%s = %s", indent, _v(desciption), spc, _v(value))
        elseif lookupTable[value] then
            result[#result +1 ] = string.format("%s%s%s = *REF*", indent, desciption, spc)
        else
            lookupTable[value] = true
            if nest > nesting then
                result[#result +1 ] = string.format("%s%s = *MAX NESTING*", indent, desciption)
            else
                result[#result +1 ] = string.format("%s%s = {", indent, _v(desciption))
                local indent2 = indent.."    "
                local keys = {}
                local keylen = 0
                local values = {}
                for k, v in pairs(value) do
                    keys[#keys + 1] = k
                    local vk = _v(k)
                    local vkl = string.len(vk)
                    if vkl > keylen then keylen = vkl end
                    values[k] = v
                end
                table.sort(keys, function(a, b)
                    if type(a) == "number" and type(b) == "number" then
                        return a < b
                    else
                        return tostring(a) < tostring(b)
                    end
                end)
                for i, k in ipairs(keys) do
                    _dump(values[k], k, indent2, nest + 1, keylen)
                end
                result[#result +1] = string.format("%s}", indent)
            end
        end
    end
    _dump(value, desciption, "- ", 1)

    LogD(table.concat(result,"\n"))
    LogD("<color=yellow>Lua Dump End</color>")
end

function Assert(check,other)
    if not check then
        if other then
            LogE(other)
        end
        LogE(string.format("Assert error Stack:%s", debug.traceback()))
        return false
    end
    return true
end


local serpent= require "Common.serpent"
function LogBlock(value)
    
    if not _isDebug then
        return
    end
    LogD(serpent.block(value))

end

