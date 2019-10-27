--[[********************************************************************
*      作者： jordenwu
*      时间： 10/10/17 10:11:42
*      描述： RapidJson 全局工具
*********************************************************************--]]
local rapidJson=require "rapidjson"
RapidJson={
        
    
}

function RapidJson.Decode(strs)
    
    if not strs then
        LogE("RapidJson Decode Arg Is NIL")
        return false
    end
    if not rapidJson then
        return false
    end
    return rapidJson.decode(strs)
    
end

function RapidJson.Encode(arg)
    
    if not  arg then
        LogE("RapidJson Encode Arg Is NIL")
        return false
    end
    if not rapidJson then
        return false
    end
    return rapidJson.encode(arg)
    
end

----示例
--local t = RapidJson.Decode('{"a":123}')
--print(t.a)
--t.a = 456
--local s = RapidJson.Encode(t)
--print('json:%s', s)

