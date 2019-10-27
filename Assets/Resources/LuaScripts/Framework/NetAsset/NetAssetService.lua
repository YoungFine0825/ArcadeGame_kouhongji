--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-22
*		描述： 网络图片服务
*********************************************************************--]]
local NetAssetServiceClass = DeclareClass("NetAssetServiceClass")

function NetAssetServiceClass:ctor()
	
	self.csS=Interaction.NetAssetService
	
end

--[[
    @desc: 准备下载网络资源
	--@finishhandler: 完成回调 （float progress）
    @return:
]]
function NetAssetServiceClass:PrepareNetAssets(urls,finishhandler)
    
    if not self.csS then
        LogE("NetImageService Error Cs")
        return false
    end
    --
    self.csS:PrepareNetAssets(urls,finishhandler)
    --
    return true
end

NetAssetService=NetAssetServiceClass.new()