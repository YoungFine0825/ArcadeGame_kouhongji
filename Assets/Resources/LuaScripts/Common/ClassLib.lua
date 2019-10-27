--[[********************************************************************
*		作者： jordenwu
*		时间： 2018-07-04
*		描述： 全局类元库定义
*********************************************************************--]]
--全局
ClassLib = {}

local function Construct(cls, obj, ...)
    if cls.super then
        Construct(cls.super, obj, ...)
    end

    if cls.ctor then
        cls.ctor(obj, ...)
    end
end

function DeclareClass(name, super)
    if not name then
        return nil
    end
    assert(name, "DeclareClass no name")
    assert(not ClassLib[name], "Duplicate DeclareClass name: "..name)

    local class_type = {}
    class_type.__classname = name
    class_type.ctor = false
    class_type.super = super
    class_type.__super = super and super.vtbl

    local vtbl = {}
    class_type.vtbl = vtbl

    class_type.meta =
    {
        __index = vtbl,
        __newindex = function(tb, k, v)
            local value = v or "nil"
            LogE("DeclareClass : forbid add field -- setting " .. class_type.__classname .. "." .. k .. " to " .. tostring(value))
        end
    }

    class_type.new = function(...)
        local obj = {}
        obj.__classname = class_type.__classname
        Construct(class_type, obj, ...)
        setmetatable(obj, class_type.meta)
        return obj
    end

    setmetatable(class_type,
        {
            __newindex = function(t, k, v)
                if type(v) ~= "function" then
                    LogE("DeclareClass : not a function")
                    return
                end
                t.vtbl[k] = v
            end
        }
    )

    if super then
        setmetatable(vtbl,
            {
                __index = function(_, k)
                    local ret = super.vtbl[k]
                    vtbl[k] = ret
                    return ret
                end
            }
        )
    end

    ClassLib[name] = class_type
    return class_type
end