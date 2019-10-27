--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-05
*		描述： 娃娃机数据
*********************************************************************--]]
--
local WWJDataClass = DeclareClass("WWJDataClass")
--
function WWJDataClass:ctor()
    self.PlayerInfo = false;
    self.MachineId = false;
    self.MachineName = false;
    self.PromoterId = false;--代理商ID
    self.WeChatLink = false;--微信登陆链接
    self.PayLink = false;--支付链接
    --
    self._mPrizesList = false;--奖品列表
end

function WWJDataClass:Initialize()
    self._mPrizesList = {};
end

function WWJDataClass:UnInitialize()
    self.PlayerInfo = false;
    self._mPrizesList = false;

    self.MachineId = false;
    self.MachineName = false;
    self.PromoterId = false;
    self.WeChatLink = false;
    self.PayLink = false;
end

--[[
    @初始化服务端数据
    @param 'data' : json字符串
    {
	"flag":true,
	"initData":{
				"machineId":"M00000000002",
				"machineName":"成都测试00000000002",
				"streetPromoterId":"888888",
				"payLink":"http://ghtest.scbczx.com/pay?machineId=M00000000002",
				"advertisement":[],
				"machinePrizeList":[{
										"prizeId":"11",
										"bingoValue":4,
										"prizeName":"布朗熊*1"
									}],
				"userName":"none",
				"nickName":"none",
				"photoURL":"none",
				"playCount":0,
				"awardNum":0,
				"dollResult":"none",
				"weChatLink":"http://weixin.qq.com/r/OS1lfarErffSre4e93gr"
				}
	}
--]]
function WWJDataClass:InitWithSvrData(data)
    local initData = data.initData;
    --初始化玩家信息
    self.PlayerInfo = ClassLib.WWJPlayerInfoClass.new();
    self.PlayerInfo:InitData(initData);
    --初始化礼品列表
    for k, v in ipairs(initData.machinePrizeList) do
        local prize = ClassLib.WWJPrizeInfoClass.new();
        prize:InitData(v);
        self._mPrizesList[#self._mPrizesList + 1] = prize;
    end
    --初始化娃娃机基本信息
    self.MachineId = data.machineId;
    self.MachineName = data.machineName;
    self.PromoterId = data.promoterId;
    self.WeChatLink = data.weChatLink;
    self.PayLink = data.payLink;
end

--获取礼品列表
function WWJDataClass:GetPrizesList()
    return self._mPrizesList;
end