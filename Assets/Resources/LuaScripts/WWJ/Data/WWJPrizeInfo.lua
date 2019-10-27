--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-06
*		描述： 娃娃机配置的礼品信息
*********************************************************************--]]
local WWJPrizeInfoClass = DeclareClass("WWJPrizeInfoClass")
--
function WWJPrizeInfoClass:ctor()
    
   --礼品Id
   self.PrizeId=0
   --礼品名称
   self.PrizeName=""
   --保夹次数
   self.GuaranteeValue=0
    
end

function WWJPrizeInfoClass:InitData(data)
   self.PrizeId = data.prizeId;
   self.GuaranteeValue = data.bingoValue;
   --分割出礼品名称和单次兑换的数量
   local str = self:SplitStr(data.prizeName,"*");
   self.PrizeName = str[1];
end

function WWJPrizeInfoClass:SplitStr(str,sep)
   if(not sep) then
      sep = "%s";
   end
   local rt = {};
   for s in string.gmatch(str,"([^"..sep.."]+)") do
      rt[#rt + 1] = s;
   end
   return rt;
end





