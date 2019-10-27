--[[********************************************************************
*      作者： jordenwu
*      时间： 2018-07-25
*      描述： 本地服务类 
*********************************************************************--]]
NetType={
    --未连接网络
    UnConnect = -1,
    --未知网络
    UnKnow = 0,
    Wifi = 1,
    Net2G = 2,
    Net3G = 3,
    Net4G = 4,
    Net5G = 5,
    --蜂窝网络统称
    NetWWAN = 6, 
}

local NativeServiceClass = DeclareClass("NativeServiceClass")

function NativeServiceClass:ctor()
	
	local csS=Interaction.NativeService
	self._csService = csS
	self._curNetType = NetType.UnKnow

end

function NativeServiceClass:GetNetType()
	
	if not self._csService then
		return NetType.UnKnow
	end
	return self._csService:GetNetType()

end

function NativeServiceClass:GetNetIsWifi()
	
	if self:GetNetType()==NetType.Wifi then
		return true
	end
	return false

end

function NativeServiceClass:GetIsNetEnable()
	if not self._csService then
		return false
	end
	return self._csService:GetIsNetEnable()
end

-- 获取设备唯一id
-- @return 无
function NativeServiceClass:GetDeviceUUID()
	
	if not self._csService then
		return ""
	end
	return self._csService:GetDeviceUUID()
end

-- 获取ip
-- @return 无
function NativeServiceClass:GetIPAddress()
	
	if not self._csService then
		return ""
	end
	return self._csService:GetIPAddress()
end

-- 获取设备信息
-- @return 无
function NativeServiceClass:GetDeviceInfo()
	
	if not self._csService then
		return {}
	end
	local info = self._csService:GetDeviceInfo()
	local infoT = RapidJson.Decode(info)
	return infoT	
	
end

--[[
    @desc: 获取屏幕尺寸
    @return:
]]
function NativeServiceClass:GetScreenSize()
	if not self._csService then
		return 0,0
	end
	local vv2=self._csService:GetScreenSize()
	return vv2.x,vv2.y

end

function NativeServiceClass:RebootDevice()
	if not self._csService then
		return 
	end
	self._csService:RebootDevice()
end


NativeService = NativeServiceClass.new() 