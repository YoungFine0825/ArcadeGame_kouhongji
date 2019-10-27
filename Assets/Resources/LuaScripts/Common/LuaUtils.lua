--[[********************************************************************
*      作者： jordenwu
*      时间： 10/10/17 11:07:46
*      描述： 基础工具
*********************************************************************--]]
LuaUtils = {
}

-- similar with python's repr
function LuaUtils:repr(val)
    if type(val) == 'string' then
        return string.format('"%s"', val)
    else
        return tostring(val)
    end
end

-- string.xxx extention

function LuaUtils:string_concat(split , ... )
    local noneNilArray = {}
    for i,v in ipairs(...) do
        if v then
            table.insert(noneNilArray, v)
        end
    end
    return table.concat(noneNilArray, split)
end

function LuaUtils:QuickStringAddString(stringArray, str)
    for v in str:gmatch"." do
        stringArray[#stringArray+1] = tostring(v)
    end
end

function LuaUtils:QuickStringAddNumber(stringArray, number)
    stringArray[#stringArray+1] = tostring(number)
end


function LuaUtils:string_split(str, sep)
    local r = {}
    for i in string.gmatch(str, string.format('[^%s]+', sep)) do
        table.insert(r, i)
    end
    return r
end

function LuaUtils:string_combine(r, sep)
    local str = ""
    for k,v in ipairs(r) do
        if (k == #r) then
            str = str .. v
        else
            str = str .. v .. sep
        end
    end
    return str
end

function LuaUtils:string_startswith(str, prefix)
    return string.find(str, prefix) == 1
end

function LuaUtils:string_endswith(str, suffix)
    return self:string_startswith(string.reverse(str), string.reverse(suffix))
end

function LuaUtils:string_trim(s)
    local first = string.find(s, '%S')
    if not first then return '' end
    local last = string.find(string.reverse(s), '%S')
    return string.sub(s, first, #s + 1 - last)
end

function LuaUtils:string_compare(l, r)
    return string.lower(l) == string.lower(r)
end

-- table.xxx extention
-- unrecursive version
function LuaUtils:table_merge(dest, src)
    for k, v in pairs(src) do
        dest[k] = v
    end
    return dest
end

function LuaUtils:array_equal(t, t2)
    for i, v in ipairs(t) do
        if v ~= t2[i] then return false end
    end
    return true
end

function LuaUtils:array_unique(array)
    local r = {}
    local s = {}
    for _, v in ipairs(array) do
        if not s[v] then
            s[v] = true
            table.insert(r, v)
        end
    end
    return r
end

function LuaUtils:array_new(len, val)
    local r = {}
    for i = 1, len do 
        table.insert(r, val)
    end
    return r
end

function LuaUtils:array_find(t, val)
    for i, v in ipairs(t) do
        if val == v then return i end
    end
    return -1
end

-- 数组移除特定值的一个元素
-- @param t 
-- @param val 
function LuaUtils:array_remove_one(t, val)
    for i=#t, 1,-1 do
        if val == t[i] then 
            table.remove(t,i)
            return true
        end
    end
    return false
end


-- 数组移除特定值的所有元素 返回是否有对应的值
-- @param t 
-- @param val 
-- 移除对应数值 的最大个数
function LuaUtils:array_remove(t, val,cnt)
    local removedCnt=0
    for i=#t, 1,-1 do
        if (val == t[i]) and (removedCnt<cnt) then 
            removedCnt=removedCnt+1
            table.remove(t,i)
        end
    end
end


-- 数组删除最后的
-- @param t 表
-- @param cnt 删除个数
function LuaUtils:array_remove_last(t,cnt)
    if #t<cnt then
        return 
    end
    local times=0
    for i=#t, 1,-1 do
        if times<cnt then
            table.remove(t,i)
            times=times+1
        else
           break 
        end
    end
end


function LuaUtils:table_size(t)
    local r = 0
    for _, _ in pairs(t) do r = r + 1 end
    return r
end

function LuaUtils:table_empty(t)
    return not next(t)
end

function LuaUtils:table_equal(t, t2)
    local n = 0
    for k, v in pairs(t) do
        if v ~= t2[k] then return false end
        n = n + 1
    end
    return n == self:table_size(t2)
end

function LuaUtils:table_copy(t)
    return self:table_merge({}, t)
end

function LuaUtils:table_keys(t)
    local r = {}
    for k, _ in pairs(t) do
        table.insert(r, k)
    end
    return r
end

function LuaUtils:table_values(t)
    local r = {}
    for _, v in pairs(t) do
        table.insert(r, v)
    end
    return r
end

function LuaUtils:table_filter(t, func)
    local r = {}
    for k, v in pairs(t) do
        if func(k, v) then r[k] = v end
    end
    return r
end

function LuaUtils:table_map(t, func)
    local r = {}
    for k, v in pairs(t) do
        local nk, nv = func(k, v)
        r[nk] = nv
    end
    return r
end

function LuaUtils:table_length( t )
    local count = 0
    for _ in pairs(t) do count = count + 1 end
    return count
end

-- table.xxx extention
-- recursive version
function LuaUtils:table_requal(t, t2)
    local n = 0
    for k, v in pairs(t) do
        if type(v) == 'table' then
            local v2 = t2[k]
            if type(v2) ~= 'table' then return false end
            if not self:table_requal(v, v2) then return false end
        else
            if v ~= t2[k] then return false end
        end
        n = n + 1
    end
    return n == self:table_size(t2)
end

function LuaUtils:table_rcopy(t)
    local r = {}
    for k, v in pairs(t) do
        if type(v) == 'table' then
            r[k] = self:table_rcopy(v)
        else
            r[k] = v
        end
    end
    return r
end

function LuaUtils:eval(str)
    if type(str) == "string" then
        return loadstring("return " .. str)()
    elseif type(str) == "number" then
        return loadstring("return " .. tostring(str))()
    else
        error("is not a string")
    end
end

function LuaUtils:FindFirstNumber(str)
    local r = string.match(str, '[%d]+')
    if (r) then
        return tonumber(r)
    else
        return 0
    end
end

function LuaUtils:GetIntPart(x)
    if x <= 0 then
       return math.ceil(x);
    end
    if math.ceil(x) == x then
       x = math.ceil(x);
    else
       x = math.ceil(x) - 1;
    end
    return x;
end

function LuaUtils:Basen(n,b)
    local floor,insert = math.floor, table.insert

    n = floor(n)
    if not b or b == 10 then return tostring(n) end

    local digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"
    local t = {}
    local sign = ""

    if n < 0 then
        sign = "-"
    n = -n
    end
    
    repeat
        local d = (n % b) + 1
        n = floor(n / b)
        insert(t, 1, digits:sub(d,d))
    until n == 0
    
    return sign .. table.concat(t,"")
end

function LuaUtils:ColorWithDigitalExclude( digital )
    digital = tonumber(digital)
    local tmp = nil
    tmp = bit:_rshift(digital, 16)  
    local red = bit:_and(tmp, 0xFF)

    tmp = bit:_rshift(digital, 8)
    local green = bit:_and(tmp, 0xFF)
    
    local blue = bit:_and(digital, 0xFF)

    print(digital, red, green, blue)
    local colorString = self:Basen(red, 16) .. self:Basen(green, 16) .. self:Basen(blue, 16)
    print(colorString)
    return colorString
end

function LuaUtils:Shallowcopy(orig)
    local orig_type = type(orig)
    local copy
    if orig_type == 'table' then
        copy = {}
        for orig_key, orig_value in pairs(orig) do
            copy[orig_key] = orig_value
        end
    else
        copy = orig
    end
    return copy
end

function LuaUtils:Deepcopy(orig)
    local orig_type = type(orig)
    local copy
    if orig_type == 'table' then
        copy = {}
        for orig_key, orig_value in next, orig, nil do
            copy[self:Deepcopy(orig_key)] = self:Deepcopy(orig_value)
        end
        setmetatable(copy, self:Deepcopy(getmetatable(orig)))
    else
        copy = orig
    end
    return copy
end

function LuaUtils:UpperFirstChar( str )
    return string.upper(string.sub(str,0,1))..string.sub(str,2)
end


function LuaUtils.FormatNick(nick, num)
    return LuaUtils.subStr(tostring(nick), num or 10)
end

-- 判断utf8字符byte长度
-- 0xxxxxxx - 1 byte
-- 110yxxxx - 192, 2 byte
-- 1110yyyy - 225, 3 byte
-- 11110zzz - 240, 4 byte
local function chsize(char)
    if not char then
        return 1
    elseif char >= 240 then
        return 4
    elseif char >= 225 then
        return 3
    elseif char >= 192 then
        return 2
    else
        return 1
    end
end

-- 计算utf8字符串字符数, 各种字符都按一个字符计算
-- 例如utf8len("1你好") => 3
function LuaUtils.utf8len(str)
    local len = 0
    local currentIndex = 1
    while currentIndex <= #str do
        local char = string.byte(str, currentIndex)
        currentIndex = currentIndex + chsize(char)
        len = len +1
    end
    return len
end


-- 截取字符串指定长度
function LuaUtils.subStr(str, num, avoidSuffix)
    local len = #str
    local numChars = LuaUtils.utf8len(str)
    local suffix = ""
    local result = ""
    local startIndex = 1
    if not avoidSuffix and len > num then
        suffix = ".."
    end
    local currentIndex = startIndex
    while currentIndex - startIndex < num do
        local char = string.byte(str, currentIndex)
        local temp = chsize(char)
        result = result .. string.sub(str, currentIndex, currentIndex + temp - 1)
        currentIndex = currentIndex + temp
    end
    return result .. suffix
end

--按字数截取 多余的用...代替
function LuaUtils.subStrWithPoint(str,num)
	local rtn = ""
	if LuaUtils.utf8len(str) > num then
		rtn = LuaUtils.subUtf8Str(str,1,num).."..."
	else
		rtn = str
	end
	return rtn
end
--按字数截取字串
function LuaUtils.subUtf8Str(str,startIndex,count)
	local num = #str
    local result = ""
	local charNum = 0
	local addNum = 0
    local startBtye = 1
    local currentIndex = startBtye
	count = count+1
    while currentIndex - startBtye < num and (addNum <=count)do
		charNum = charNum + 1
        local char = string.byte(str, currentIndex)
        local temp = chsize(char)
		if charNum>= startIndex then
			addNum = 1 + addNum
			result = result .. string.sub(str, currentIndex, currentIndex + temp - 1)
		end
        currentIndex = currentIndex + temp
    end
    return result
end
--把字串中间用...代替 totalNum字符总数 endNum省略号后面数量  12345678 LuaUtils.subStrMid("12345678",6,2) = "1234...78"
function LuaUtils.subStrMid(str,totalNum,endNum)
	return LuaUtils.subUtf8Str(str,1,totalNum - endNum).."..."..LuaUtils.subUtf8Str(str,LuaUtils.utf8len(str) - endNum+1,endNum)
end

--验证手机号码是否合法
function LuaUtils.IsValidMobileNumber(str)
    if not str then
        return false
    end
    
    return string.match(str, '[1][3,4,5,6,7,8,9]%d%d%d%d%d%d%d%d%d') == str
end

--验证邮箱地址
function LuaUtils.IsValidEmailAdress(str)
    if not str then
        return false
    end
 
    if (string.match("[A-Za-z0-9%.%%%+%-]+@[A-Za-z0-9%.%%%+%-]+%.%w%w%w?%w?")) then
        return true
    else
        return false
    end
end

--将'0#100','2.0'格式的字符串转换为一个number类型的值
function LuaUtils.CountTotalNumberBySpecialChar(str, char)
    local ret = 0
    if not str then
        return ret
    end
    if char == nil then
        char = '#'
    end
    if string.find(str,char) then
        local array = string.split(str, char)
        ret = tonumber(array[1]) + tonumber(array[2])
    else
        ret = math.floor(tonumber(str))
    end
    return ret
end

--将格式字符串，转化为 id - count 对应的 key,value 列表
--arguments
--@idStr : id字符串，eg : "1,2,3,4" or "1.0" or "1" 
--@countStr : id字符串，eg : "1,2,3,4" or "1" 
--@return 
--@{{id,count},{id,count}...},error return nil
function LuaUtils.FormatToIdCountList(idStr, countStr)
    if not idStr or not countStr then
        return nil
    end
    if type(idStr) ~= 'string' or type(countStr) ~= 'string' then
        LogE("Call Function : LuaUtils.FormatToIdCountList(idstr,countstr) Bad Arguments : (string, string) Needs .")
        return
    end
    --开始解析具体格式
    local retTb = {}
    if string.find(idStr, ',') then
        local ids = string.split(idStr, ',')
        local counts = string.split(countStr, ',')
        for i = 1, #ids do 
            local id = math.floor(tonumber(ids[i]))
            local count = LuaUtils.CountTotalNumberBySpecialChar(counts[i])
            table.insert(retTb, {id=id,count=count})
        end
    else
        local id = math.floor(TryGetNumber(idStr))
        local count = LuaUtils.CountTotalNumberBySpecialChar(countStr)
        table.insert(retTb, {id=id,count=count}) 
    end
    return retTb
end

--获取当日日期格式 
--@return 年月日 eg:'20180110'
function LuaUtils.GetCurDayDateStr()
    return os.date('%Y%m%d')
end
