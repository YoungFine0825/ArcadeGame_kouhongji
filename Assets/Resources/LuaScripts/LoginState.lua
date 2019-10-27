--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-11-01
*		描述： 机器登录状态
*********************************************************************--]]
local LoginStateClass = DeclareClass("LoginStateClass")

function LoginStateClass:ctor()
   
end

function LoginStateClass:vInitializeState()
    

end

function LoginStateClass:vUninitializeState()


end

function LoginStateClass:vGetName()

    return "LoginGameState"
end

function LoginStateClass:vGetIsCanKill()

    return true
    
end


function LoginStateClass:vOnStateEnter(param,oldSt)

    LogD("Enter Login Machine State")
    --切换场景
    SceneService:LoadUnitySceneAsync(
        "Empty",
        0,
        function(pro, isDone)
            
            if isDone then
                MVCService:ChangeUIState("UIState_LoginMachine")
            end
        end
    )
    
end


function LoginStateClass:vOnStateLeave(param)

    LogD("Leave Login Machine State")
    GCService:CollectGarbage(true)
    
end



