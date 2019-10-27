--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-07-04
*		描述： 游戏状态管理 注意这里为LuaVM 自己的游戏状态管理 跟c#端无关系
*********************************************************************--]]
local GameStateServiceClass = DeclareClass("GameStateServiceClass")
function GameStateServiceClass:ctor()

	self._curState=false
	self._registedStates={}

end

function GameStateServiceClass:Initialize() 
    
    
end

--反初始化
function GameStateServiceClass:Uninitialize()

	if self._registedStates then
		for k ,v in pairs(self._registedStates) do
       		v:vUninitializeState()
            self._registedStates[k] = nil
    	end
    	self._registedStates=false
	end
	self._curState=false

end


--获取当前State名称
function GameStateServiceClass:GetCurStateName()

	if not self._curState then
		return ""
	end
	return self._curState:vGetName()
end

function GameStateServiceClass:GetCurStateIsCanKill( )
	if not self._curState then
		return true
	end
	return self._curState:vGetIsCanKill()
end

--注册游戏状态
function GameStateServiceClass:RegisterState(name,state)

	if self._registedStates[name] then
		LogE("Already Registered State "..name)
		return
	end
    if state then
       state:vInitializeState()
    end
	self._registedStates[name]=state
end

--取消注册游戏状态
function GameStateServiceClass:UnRegisterState(name)
    
    local ss=self._registedStates[name]
    if ss then
       ss:vUninitializeState() 
    end
	self._registedStates[name]=nil
end


--[[
    @desc: 切换游戏状态
    --@name:名字
	--@param: 参数
    @return:
]]
function GameStateServiceClass:ChangeState(name,param)

	if not self._registedStates[name] then
		LogE("No Registered State "..name)
	end

	local old=""

	if self._curState then
		
		old=self._curState:vGetName()
		if self._curState:vGetName()==name then
            LogE("------------------Aleary In GameState----------------")
            return
        else
            self._curState:vOnStateLeave(param)
            --通知到模块
            if MVCService then
               MVCService:OnChangeGameState(MVCService.LeaveGameStateOp, old,name,param)
            end
        end
	end
    ---
	self._curState=self._registedStates[name]
	--
	self._curState:vOnStateEnter(param,old)
	--
    --通知到模块
    if MVCService then
       MVCService:OnChangeGameState(MVCService.EnterGameStateOp, old,name,param)
    end
    --
    return self._curState
    
end

GameStateService=GameStateServiceClass.new()