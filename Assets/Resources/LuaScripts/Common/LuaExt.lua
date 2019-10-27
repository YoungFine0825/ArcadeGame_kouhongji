--[[********************************************************************
*      作者： jordenwu
*      时间： 07/17/17 15:31:13
*      描述： 辅助
*********************************************************************--]]
local null={}

-- 清空表
-- @param tbl 表
-- @return 无
function table.clear(tbl)
    for k in pairs(tbl) do
        tbl[k] = nil
    end
end

--tbl必须是数组才行
function table.removeItem(tbl, item)
    for k,v in pairs(tbl) do
        if v == item then
            tbl[k] = nil
            table.remove(tbl, k)
            break
        end
    end
end


function table.clone(a,b)
    b = {}
    for i=1,#a do
        table.insert(b, a[i])
    end
end

function table.removeNullFromArray(tbl)
    for i = #tbl, 1, -1 do
        if tbl[i] == null then
            table.remove(tbl, i)
        end
    end
end


function table.merge(dest, src)
    for k, v in pairs(src) do
        dest[k] = v
    end
end

function table.val_to_str ( v )
  if "string" == type( v ) then
    v = string.gsub( v, "\n", "\\n" )
    if string.match( string.gsub(v,"[^'\"]",""), '^"+$' ) then
      return "'" .. v .. "'"
    end
    return '"' .. string.gsub(v,'"', '\\"' ) .. '"'
  else
    return "table" == type( v ) and table.tostring( v ) or
      tostring( v )
  end
end

function table.key_to_str ( k )
  if "string" == type( k ) and string.match( k, "^[_%a][_%a%d]*$" ) then
    return k
  else
    return "[" .. table.val_to_str( k ) .. "]"
  end
end

function table.tostring( tbl )
  local result, done = {}, {}
  for k, v in ipairs( tbl ) do
    table.insert( result, table.val_to_str( v ) )
    done[ k ] = true
  end
  for k, v in pairs( tbl ) do
    if not done[ k ] then
      table.insert( result,
        table.key_to_str( k ) .. "=" .. table.val_to_str( v ) )
    end
  end
  return "{" .. table.concat( result, "," ) .. "}"
end

-- 获取 kv 表中的元素个数
function table.kvtable_length(t)
  if t == nil then return 0 end
  local len = 0
  for k, v in pairs(t) do len=len+1 end
  return len
end


function table.deepcopy(object)
    local lookup_table = {}
    local function _copy(object)
        if type(object) ~= "table" then
            return object
        elseif lookup_table[object] then
            return lookup_table[object]
        end  -- if
        local new_table = {}
        lookup_table[object] = new_table
        for index, value in pairs(object) do
            new_table[_copy(index)] = _copy(value)
        end  -- for
        return setmetatable(new_table, getmetatable(object))
    end  -- function _copy
    return _copy(object)
end  -- function deepcopy


----------------------------------------------math---------------------------
function math.GetPreciseDecimal(nNum, n)
    if type(nNum) ~= "number" then
        return nNum;
    end

    n = n or 0;
    n = math.floor(n)
    local fmt = '%.' .. n .. 'f'
    local nRet = tonumber(string.format(fmt, nNum))

    return nRet;
end

function math.clamp01(num)
    if type(num) ~= "number" then
        return num;
    end
    
    if num < 0 then
        return 0
    elseif num > 1 then
        return 1
    else
        return num
    end
end


function math.Clamp(num,min,max)
    if type(num) ~= "number" then
        return num;
    end
    if num < min then
        return min
    elseif num > max then
        return max
    else
        return num
    end
end


function math.Lerp(from, to, ratio)
    ratio = ratio < 0 and 0 or ratio
    ratio = ratio > 1 and 1 or ratio
    return from + (to - from) * ratio
end

function math.EqualFloat(a, b,customV)
    local c = a - b
    c = math.abs( c )
    if not customV then
        if c < 0.00000001 then
            return true
        else
            return false
        end
    else
        if c < customV then
            return true
        else
            return false
        end
    end
end

----------------------------------------------------string-------------------------------
function string.toSBytes(str)
    local bytes = string.toBytes(str)
    for i=1, #bytes do
        if bytes[i] > 127 then
            bytes[i] = bytes[i] - 256
        end
    end
    return bytes
end

function string.toBytes(str)
    local bytes = {}
    local i = 1
    while true do
        local c = string.sub(str,i,i)
        local b = string.byte(c)
        if b > 128 then
            local rst = string.sub(str,i,i+2)
            for k=1,#rst do
                local rstchar = string.sub(rst,k,k)
                local rstByte = string.byte(rstchar)
                table.insert(bytes, rstByte)
            end
            i = i + 3
        else
            if b == 32 then
                table.insert(bytes, string.byte(' '))
            else
                table.insert(bytes, string.byte(c))
            end
            i = i + 1
        end

        if i > #str then
            break
        end
    end
    -- 加结束符
    table.insert(bytes, 0)
    return bytes
end

function string.fromBytes(bytes)
    if not bytes or #bytes <= 0 then
        return ""
    end

    local rst = string.char(table.unpack(bytes))

    -- 根据结束符截断
    local endIdx = string.find(rst, "\0")

    if endIdx and string.len(rst) > 1 then
        rst = string.sub(rst, 1, endIdx-1)
    end

    return rst
end

function string.fromSBytes(sbytes)
    for i=1, #sbytes do
        if sbytes[i] < 0 then
            sbytes[i] = sbytes[i] + 256
        end
    end

    return string.fromBytes(sbytes)
end

function string.formatCSharp(s, t)
    return string.gsub(s, "{(%d)}", function(p)
            local index = math.tointeger(p)
            if index and index + 1 <= #t then
                return t[index + 1]
            else
                return p
            end
        end)
end

-- 返回以实际汉字，字符，英文字符的个数
function string.GetWordLen(str)
    local count = 0
    if str then
        local length = string.len(str)
        local index = 1
        while index < length do
            local curByte = string.byte(str, index, index)
            local totalCount = 0
            if curByte > 0 and curByte <= 127 then
                totalCount = 1
            elseif curByte >= 192 and curByte <= 223 then
                totalCount = 2
            elseif curByte >= 224 and curByte <= 239 then
                totalCount = 3
            elseif curByte >= 240 and curByte <= 247 then
                totalCount = 4
            end
            index = index + totalCount
            count = count + 1
        end
    end
    return count
end

-- 返回以实际汉字，字符，英文字符的数组
function string.SpriteWord(str)
    local array = {}
    if str then
        local length = string.len(str)
        local index = 1
        while index < length do
            local curByte = string.byte(str, index)
            local totalCount = 0
            if curByte > 0 and curByte <= 127 then
                totalCount = 1
            elseif curByte >= 192 and curByte <= 223 then
                totalCount = 2
            elseif curByte >= 224 and curByte <= 239 then
                totalCount = 3
            elseif curByte >= 240 and curByte <= 247 then
                totalCount = 4
            end
            array[#array + 1] = string.sub(str, index, index + totalCount - 1)
            index = index + totalCount
        end
    end
    return array
end

--此方法只适用于lua5.1版本
-- function string.split(str, delimiter)
-- 	if str==nil or str=='' or delimiter==nil then
-- 		return nil
-- 	end
	
--     local result = {}
--     for match in (str..delimiter):gmatch("(.-)"..delimiter) do
--         table.insert(result, match)
--     end
--     return result
-- end

--此方法适用于lua5.3
function string.split(str, pattern)
    if pattern then
        pattern = string.format("[^%s]+", pattern)
    end
    pattern = pattern or "[^%s]+"
    if string.len(pattern) == 0 then
       pattern = "[^%s]+"
    end
    local parts = {__index = table.insert}
    setmetatable(parts, parts)
    str:gsub(pattern, parts)
    setmetatable(parts, nil)
    parts.__index = nil
    return parts
 end

function string.IsNullOrEmpty(str)
    
    if not str then
        return true
    end
    if type(str)~= "string" then
        return true
    end
    if #str==0 then
        return true
    end
    return false
end


--将number类型的num转换为货币格式的string，"1,000" '5万'
function string.GetFormatMoneyStr(num,deperator)
    if not num or type(num) ~= 'number' then
        return
    end
    --转为int
    num = math.floor(num)
    local limit = 10000
    --是否需要加'万'字
    local tail = ''
    if num >= 100000 then
        num = math.floor(num/limit)
        tail = '万'
    end
    --返回字符串
    local retStr =''
    local numStr = tostring(num)
    local strLen = string.len(numStr)
    --千分位分隔符
    deperator = deperator or ','
    for i=1,strLen do
        retStr = string.char(string.byte(numStr,strLen+1 - i)) .. retStr
        if i%3 == 0 then
            --下一个数 还有就加
            if strLen - i ~= 0 then
                retStr = deperator..retStr
            end
        end
    end
    return retStr..tail
end

--截取名字，并自动补上"..."
--参数 @str：要截取的字符串
--参数 @len: 包含中文字符串时的长度
--参数 @enCharLen：如果是纯英文字符串，截取长度
function string.GetNickName(str, len, enCharLen)
    len = len or 7
    if string.OnlyContainsEnglishCharacters(str) then
      len = enCharLen or 12
    end
    local realLen = string.SubStringGetTotalIndex(str)
    local dots = realLen > len and "..." or ""
    if realLen > len then
        len = len - 1--算上省略号
    end
    return string.SubStringUTF8(str, 1, len) .. dots
end
  
function string.GetTrimUTF8String(str, startIndex, endIndex)
    startIndex = startIndex or 1
    local realLen = string.SubStringGetTotalIndex(str)
    local dots = realLen > endIndex and "..." or ""
    return string.SubStringUTF8(str, startIndex, endIndex) .. dots
end
  
function string.SubStringUTF8(str, startIndex, endIndex)
    if startIndex < 0 then
        startIndex = string.SubStringGetTotalIndex(str) + startIndex + 1;
    end

    if endIndex ~= nil and endIndex < 0 then
        endIndex = string.SubStringGetTotalIndex(str) + endIndex + 1;
    end

    if endIndex == nil then 
        return string.sub(str, string.SubStringGetTrueIndex(str, startIndex));
    else
        return string.sub(str, string.SubStringGetTrueIndex(str, startIndex), string.SubStringGetTrueIndex(str, endIndex + 1) - 1);
    end
end
  
--获取中英混合UTF8字符串的真实字符数量
function string.SubStringGetTotalIndex(str)
    local curIndex = 0;
    local i = 1;
    local lastCount = 1;
    repeat 
        lastCount = string.SubStringGetByteCount(str, i)
        i = i + lastCount;
        curIndex = curIndex + 1;
    until(lastCount == 0);
    return curIndex - 1;
end

function string.SubStringGetTrueIndex(str, index)
    local curIndex = 0;
    local i = 1;
    local lastCount = 1;
    repeat 
        lastCount = string.SubStringGetByteCount(str, i)
        i = i + lastCount;
        curIndex = curIndex + 1;
    until(curIndex >= index);
    return i - lastCount;
end

--返回当前字符实际占用的字符数
function string.SubStringGetByteCount(str, index)
    local curByte = string.byte(str, index)
    local byteCount = 1;
    if curByte == nil then
        byteCount = 0
    elseif curByte > 0 and curByte <= 127 then
        byteCount = 1
    elseif curByte>=192 and curByte<=223 then
        byteCount = 2
    elseif curByte>=224 and curByte<=239 then
        byteCount = 3
    elseif curByte>=240 and curByte<=247 then
        byteCount = 4
    end
    return byteCount
end

--判断是否只包含英文字符
function string.OnlyContainsEnglishCharacters(str)
    for i=1, #str do
        local curByte = string.byte(str, i)
        if curByte > 127 then
            return false
        end
    end
    return true
end

--得到一个number类型
function TryGetNumber(val)
    if type(val) == 'string' then
        return tonumber(val)
    end
    return val
end