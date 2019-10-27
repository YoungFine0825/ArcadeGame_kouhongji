--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-22
*		描述： 礼品系统 提供获取礼品相关信息 和资源
*********************************************************************--]]
local PrizeSystemClass = DeclareClass("PrizeSystemClass")
function PrizeSystemClass:ctor()

    self._cachedPrizeInfos=false

    self.GetUrl=""
    
    self.SessionHandler=false
    --每一个小时重新更新一次奖品信息
    self._reupdateDelay = 3600
end

function PrizeSystemClass:Initialize(  )
    
    self.GetUrl=MachineUrlRoot.."/getPrizeInfo"
    self._cachedPrizeInfos = {}
    --定时更新奖品信息
    ScheduleService:AddTimer(self,self.DoUpdatePrizesInfo,self._reupdateDelay,true)
end

function PrizeSystemClass:UnInitialize(  )
    -- body
    self._cachedPrizeInfos=false
    ScheduleService:RemoveTimer(self)
end

--[[
    @desc: 根据礼品id 获取礼品信息
    --@pid: 礼品id
    @return:礼品信息
]]
function PrizeSystemClass:GetPrizeInfo(pid)
    if type(pid) == 'number' then
        pid = tostring(pid)
    end
    return self._cachedPrizeInfos[pid]

end

--[[
    @desc: 根据礼品id 准备礼品信息
    --@pids:礼品id列表
	--@finishedHandler: 完成回调 (isSuccess)
    @return:
]]
function PrizeSystemClass:PreparePrizeInfos(pids,obj,finishedHandler)
    
    if not pids or #pids == 0 then
        LogE("PreparePrizeInfos Error Arg")
        if finishedHandler then
            finishedHandler(obj,true,0)
        end
        return
    end
    --
    local postData=RapidJson.Encode({idList=pids})
    --先获取基础信息
    HttpService:AsyncGetTextByPostUrl(self.GetUrl,postData,function (ret)
        -- body
        if ret=="error" then

            LogE("PreparePrizeInfos Get Config Net Error ")
            if finishedHandler then
                finishedHandler(obj,true,0)
            end

        else
            
            LogD("GetPrizeInfo:%s",ret)
            local cfgs=RapidJson.Decode(ret)
            if not cfgs then
                LogE("PreparePrizeInfos Get Config Error Back")
                if finishedHandler then
                    finishedHandler(obj,true,0)
                end
            else
                if cfgs.errCode then
                    LogE("PreparePrizeInfos Get Config Error Code :"..cfgs.errCode)
                    if finishedHandler then
                        finishedHandler(obj,true,0)
                    end
                    return
                end
                --保存
                local cnt=0
                for k,v in pairs(cfgs) do
                    local newInfo=ClassLib.PrizeInfoClass.new()
                    newInfo:InitInfo(k,v)
                    self._cachedPrizeInfos[k]=newInfo
                    cnt=cnt+1
                end

                if cnt>0 then
                    --
                    if finishedHandler then
                        finishedHandler(obj,false,0.1)
                    end

                    self.SessionHandler = Pool:CreateTable()
                    self.SessionHandler.obj = obj
                    self.SessionHandler.func = finishedHandler
                    self:DoPreparePrizeRes()
                    --
                else
                    if finishedHandler then
                        finishedHandler(obj,true,0)
                    end
                end

            end
        end
    end)
end

--[[
    @desc: 准备礼品配置相关资源
    @return:
]]
function PrizeSystemClass:DoPreparePrizeRes()
    
    local prizeIconUrls={}
    for k,v in pairs(self._cachedPrizeInfos) do
        
        for k1,v1 in ipairs(v.IconUrls) do
            if not string.IsNullOrEmpty(v1) then
                prizeIconUrls[#prizeIconUrls+1]=v1
            end
        end
    end
    --
    if #prizeIconUrls==0 then
        if self.SessionHandler then
            self.SessionHandler.func(self.SessionHandler.obj,true,0)
            Pool:DestroyTable(self.SessionHandler)
            self.SessionHandler=false
        end
        return
    end
    --
    NetAssetService:PrepareNetAssets(prizeIconUrls,function (progress)

        if self.SessionHandler then

            self.SessionHandler.func(self.SessionHandler.obj,false,progress)
            if progress>=1 then
                Pool:DestroyTable(self.SessionHandler)
                self.SessionHandler=false
                --定时更新奖品信息
                ScheduleService:RemoveTimer(self)
                ScheduleService:AddTimer(self,self.DoUpdatePrizesInfo,self._reupdateDelay,true)
            end
            --
        end

    end)

end

--[[
    重新拉去奖品信息
--]]
function PrizeSystemClass:DoUpdatePrizesInfo()
    --提取奖品Id
    if not self._cachedPrizeInfos then
        return
    end
    local prizeIds = {}
    for k,v in pairs(self._cachedPrizeInfos) do
        if v then
            prizeIds[#prizeIds + 1] = v.Id
        end
    end
    if #prizeIds <= 0 then
        return
    end
    --
    LogD("-------- Update Prizes Information --------")
    local postData=RapidJson.Encode({idList=prizeIds})
    --先获取基础信息
    HttpService:AsyncGetTextByPostUrl(self.GetUrl,postData,function (ret)
        if ret=="error" then
            LogE("Update PrizeInfos Get Config Net Error ")
        else
            LogD("GetPrizeInfo:%s",ret)
            local cfgs=RapidJson.Decode(ret)
            if not cfgs then
                LogE("Update PrizeInfos Get Config Error Back")
            else
                if cfgs.errCode then
                    LogE("Update PrizeInfos Get Config Error Code :"..cfgs.errCode)
                else
                    --保存
                    for k,v in pairs(cfgs) do
                        if self._cachedPrizeInfos[k] then
                            self._cachedPrizeInfos[k]:InitInfo(k,v)
                        else
                            local newInfo=ClassLib.PrizeInfoClass.new()
                            newInfo:InitInfo(k,v)
                            self._cachedPrizeInfos[k]=newInfo
                        end
                    end
                end
            end
        end
    end)
end

PrizeSystem=ClassLib.PrizeSystemClass.new()
