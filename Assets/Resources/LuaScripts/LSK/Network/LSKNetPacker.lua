--[[********************************************************************
*      作者： jordenwu
*      时间： 11/21/17 15:14:58
*      描述： 网络打包器 负责根据comamnd 和参数来打包 负责解析收到的数据返回对应pb 定义的结构
*********************************************************************--]]
local Pb=require "pb"
--包头定义
local SIZE_LEN = 4
local PACKET_MAX_LEN = 8192
--协议定义
local Protocol=LSKNet.Protocol
--
local LSKNetPackerClass = DeclareClass("LSKNetPackerClass")

function LSKNetPackerClass:ctor()
    --缓冲区
    self._netDataBuff={}
    self.Uid = 0
end

function LSKNetPackerClass:UnInit()
   
    self._netDataBuff=nil
    self.Uid = 0
end


-- 重置
-- @return 无
function LSKNetPackerClass:Reset()
    
    if self._netDataBuff then
        for i=#self._netDataBuff,1, -1 do
            table.remove(self._netDataBuff,i)
        end
    end
end


--注册Pb协议定义到Pb
function LSKNetPackerClass:RegistePbFile(buffer)
    Pb.load(buffer)
end

-- 组装数据包
-- @param cid  命令ID 定义在Net.Command
-- @param params 请求参数 定义在具体的pb file
-- @return 数据包
function LSKNetPackerClass:CreatePacket(cid,params)
    
    params=params or {}

    --统一的协议包结构
    local PBCSMsg={
        uid=self.Uid,
        cli_ver=100,
    }
    --获取对应comand 定义的请求message
    local pbName=Protocol.Req[cid]
    if not pbName then
        LogE("LSKNetwork CreatePacket Error No Req PbFile Message Define Cid:"..tostring(cid))
        return false
    end
    --oneof
    PBCSMsg[pbName]=params
    --内容
    local pbData=Pb.encode("PBCSMsg",PBCSMsg) or ""
    --头
    local writePackLen=SIZE_LEN+(#pbData)
    local sendData=string.pack(">I4",writePackLen)
    --拼
    local sendPacket=sendData..pbData
    --
    return sendPacket,pbName
end


-- 解析数据
-- @param date 
-- @return 无
function LSKNetPackerClass:ParsePackets(bytes)
    
    if (not bytes) or(#bytes==0) then
        return false
    end

    --加入buff
    for i=1,#bytes do
        self._netDataBuff[#self._netDataBuff+1]=string.byte(bytes,i)
    end
    --
    local rspMsgs=Pool:CreateTable()
    --包头4个字节
    local preLen=SIZE_LEN
    --
    while #self._netDataBuff>=preLen do

        local curBuffLen=#self._netDataBuff
        local header=string.char(table.unpack(self._netDataBuff,1,preLen))
        local totalLen=string.unpack(">I4",header)
        --数据长度
        local bodyLen=totalLen-4
        if (curBuffLen-SIZE_LEN)<bodyLen then
            --数据不够
            break
        else
            --协议数据限制
            if bodyLen<=PACKET_MAX_LEN then
                --一个完整的回包
                local protoData=string.char(table.unpack(self._netDataBuff,5,bodyLen+4))
                --PBCSMsg
                local pbcsMsg=Pb.decode("PBCSMsg",protoData)
                if pbcsMsg then
                    rspMsgs[#rspMsgs+1]=pbcsMsg
                else
                    LogE("ParsePackets Error")
                end
            end
            --清理 
            for i=totalLen,1, -1 do
                table.remove(self._netDataBuff,i)
            end
        end
 
    end
    return rspMsgs

end


function LSKNetPackerClass:GetPBCSMsgLogicMsgKey(cmd)

    local msgKey =Protocol.Rsp[cmd]
    if not msgKey then
        LogD("<color=yellow>Error</color>------>LSK Net Rsp No Define Cmd %d", cmd)
        return nil
    end
    return msgKey
end


