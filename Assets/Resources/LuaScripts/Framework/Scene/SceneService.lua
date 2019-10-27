--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-08-01
*		描述： 游戏场景管理 服务 支持加载Unity 场景  todo 支持加载打包的 unity场景
*********************************************************************--]]
local SceneServiceClass = DeclareClass("SceneServiceClass")
--
function SceneServiceClass:ctor()
    --
    self._csService=Interaction.SceneService
    
end

--[[
    @desc: 异步加载unity场景
    --@name:场景名称
	--@mode:模式 CS LoadSceneMode  0 单独 1附加
	--@handler: func(progress,isDone)
    @return:无
]]
function SceneServiceClass:LoadUnitySceneAsync(name,mode,handler)
    -- body
    if not self._csService then
        return
    end

    self._csService:LoadUnitySceneAsyncLua(name,mode,handler)

end

SceneService=SceneServiceClass.new()