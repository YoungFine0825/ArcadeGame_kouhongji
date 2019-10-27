#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class UltimateRopeWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UltimateRope);
			Utils.BeginObjectRegister(type, L, translator, 0, 25, 76, 76);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DeleteRope", _m_DeleteRope);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DeleteRopeLinks", _m_DeleteRopeLinks);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Regenerate", _m_Regenerate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsLastStatusError", _m_IsLastStatusError);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ChangeRopeDiameter", _m_ChangeRopeDiameter);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "MeshCreate", _m_MeshCreate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ChangeRopeSegmentSides", _m_ChangeRopeSegmentSides);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetupRopeMaterials", _m_SetupRopeMaterials);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetupRopeLinks", _m_SetupRopeLinks);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetupRopeJoints", _m_SetupRopeJoints);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CheckNeedsStartExitLockZ", _m_CheckNeedsStartExitLockZ);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FillLinkMeshIndicesRope", _m_FillLinkMeshIndicesRope);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FillLinkMeshIndicesSections", _m_FillLinkMeshIndicesSections);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "HasDynamicSegmentNodes", _m_HasDynamicSegmentNodes);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "BeforeImportedBonesObjectRespawn", _m_BeforeImportedBonesObjectRespawn);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AfterImportedBonesObjectRespawn", _m_AfterImportedBonesObjectRespawn);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DoExtendRopeLinear", _m_DoExtendRopeLinear);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ExtendRope", _m_ExtendRope);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RecomputeCoil", _m_RecomputeCoil);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "BuildStaticMeshObject", _m_BuildStaticMeshObject);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "MoveNodeUp", _m_MoveNodeUp);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "MoveNodeDown", _m_MoveNodeDown);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateNewNode", _m_CreateNewNode);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveNode", _m_RemoveNode);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FirstNodeIsCoil", _m_FirstNodeIsCoil);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Status", _g_get_Status);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "RopeType", _g_get_RopeType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "RopeStart", _g_get_RopeStart);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "RopeNodes", _g_get_RopeNodes);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "RopeLayer", _g_get_RopeLayer);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "RopePhysicsMaterial", _g_get_RopePhysicsMaterial);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "RopeDiameter", _g_get_RopeDiameter);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "RopeDiameterScaleX", _g_get_RopeDiameterScaleX);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "RopeDiameterScaleY", _g_get_RopeDiameterScaleY);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "RopeSegmentSides", _g_get_RopeSegmentSides);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "RopeMaterial", _g_get_RopeMaterial);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "RopeTextureTileMeters", _g_get_RopeTextureTileMeters);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "RopeSectionMaterial", _g_get_RopeSectionMaterial);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "RopeTextureSectionTileMeters", _g_get_RopeTextureSectionTileMeters);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsExtensible", _g_get_IsExtensible);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ExtensibleLength", _g_get_ExtensibleLength);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "HasACoil", _g_get_HasACoil);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CoilObject", _g_get_CoilObject);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CoilAxisRight", _g_get_CoilAxisRight);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CoilAxisUp", _g_get_CoilAxisUp);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CoilWidth", _g_get_CoilWidth);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CoilDiameter", _g_get_CoilDiameter);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CoilNumBones", _g_get_CoilNumBones);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LinkObject", _g_get_LinkObject);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LinkAxis", _g_get_LinkAxis);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LinkOffsetObject", _g_get_LinkOffsetObject);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LinkTwistAngleStart", _g_get_LinkTwistAngleStart);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LinkTwistAngleIncrement", _g_get_LinkTwistAngleIncrement);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "BoneFirst", _g_get_BoneFirst);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "BoneLast", _g_get_BoneLast);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "BoneListNamesStatic", _g_get_BoneListNamesStatic);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "BoneListNamesNoColliders", _g_get_BoneListNamesNoColliders);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "BoneAxis", _g_get_BoneAxis);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "BoneColliderType", _g_get_BoneColliderType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "BoneColliderDiameter", _g_get_BoneColliderDiameter);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "BoneColliderSkip", _g_get_BoneColliderSkip);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "BoneColliderLength", _g_get_BoneColliderLength);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "BoneColliderOffset", _g_get_BoneColliderOffset);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LinkMass", _g_get_LinkMass);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LinkSolverIterationCount", _g_get_LinkSolverIterationCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LinkJointAngularXLimit", _g_get_LinkJointAngularXLimit);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LinkJointAngularYLimit", _g_get_LinkJointAngularYLimit);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LinkJointAngularZLimit", _g_get_LinkJointAngularZLimit);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LinkJointSpringValue", _g_get_LinkJointSpringValue);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LinkJointDamperValue", _g_get_LinkJointDamperValue);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LinkJointMaxForceValue", _g_get_LinkJointMaxForceValue);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LinkJointBreakForce", _g_get_LinkJointBreakForce);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LinkJointBreakTorque", _g_get_LinkJointBreakTorque);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LockStartEndInZAxis", _g_get_LockStartEndInZAxis);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "SendEvents", _g_get_SendEvents);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "EventsObjectReceiver", _g_get_EventsObjectReceiver);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnBreakMethodName", _g_get_OnBreakMethodName);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PersistAfterPlayMode", _g_get_PersistAfterPlayMode);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "EnablePrefabUsage", _g_get_EnablePrefabUsage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "AutoRegenerate", _g_get_AutoRegenerate);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Deleted", _g_get_Deleted);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LinkLengths", _g_get_LinkLengths);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "TotalLinks", _g_get_TotalLinks);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "TotalRopeLength", _g_get_TotalRopeLength);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_bRopeStartInitialOrientationInitialized", _g_get_m_bRopeStartInitialOrientationInitialized);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_v3InitialRopeStartLocalPos", _g_get_m_v3InitialRopeStartLocalPos);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_qInitialRopeStartLocalRot", _g_get_m_qInitialRopeStartLocalRot);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_v3InitialRopeStartLocalScale", _g_get_m_v3InitialRopeStartLocalScale);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_nFirstNonCoilNode", _g_get_m_nFirstNonCoilNode);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_afCoilBoneRadiuses", _g_get_m_afCoilBoneRadiuses);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_afCoilBoneAngles", _g_get_m_afCoilBoneAngles);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_afCoilBoneX", _g_get_m_afCoilBoneX);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_fCurrentCoilRopeRadius", _g_get_m_fCurrentCoilRopeRadius);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_fCurrentCoilTurnsLeft", _g_get_m_fCurrentCoilTurnsLeft);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_fCurrentCoilLength", _g_get_m_fCurrentCoilLength);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ImportedBones", _g_get_ImportedBones);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_bBonesAreImported", _g_get_m_bBonesAreImported);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_strStatus", _g_get_m_strStatus);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_bLastStatusIsError", _g_get_m_bLastStatusIsError);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_strAssetFile", _g_get_m_strAssetFile);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_fCurrentExtension", _g_get_m_fCurrentExtension);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "Status", _s_set_Status);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "RopeType", _s_set_RopeType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "RopeStart", _s_set_RopeStart);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "RopeNodes", _s_set_RopeNodes);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "RopeLayer", _s_set_RopeLayer);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "RopePhysicsMaterial", _s_set_RopePhysicsMaterial);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "RopeDiameter", _s_set_RopeDiameter);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "RopeDiameterScaleX", _s_set_RopeDiameterScaleX);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "RopeDiameterScaleY", _s_set_RopeDiameterScaleY);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "RopeSegmentSides", _s_set_RopeSegmentSides);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "RopeMaterial", _s_set_RopeMaterial);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "RopeTextureTileMeters", _s_set_RopeTextureTileMeters);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "RopeSectionMaterial", _s_set_RopeSectionMaterial);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "RopeTextureSectionTileMeters", _s_set_RopeTextureSectionTileMeters);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "IsExtensible", _s_set_IsExtensible);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ExtensibleLength", _s_set_ExtensibleLength);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "HasACoil", _s_set_HasACoil);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "CoilObject", _s_set_CoilObject);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "CoilAxisRight", _s_set_CoilAxisRight);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "CoilAxisUp", _s_set_CoilAxisUp);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "CoilWidth", _s_set_CoilWidth);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "CoilDiameter", _s_set_CoilDiameter);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "CoilNumBones", _s_set_CoilNumBones);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LinkObject", _s_set_LinkObject);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LinkAxis", _s_set_LinkAxis);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LinkOffsetObject", _s_set_LinkOffsetObject);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LinkTwistAngleStart", _s_set_LinkTwistAngleStart);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LinkTwistAngleIncrement", _s_set_LinkTwistAngleIncrement);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "BoneFirst", _s_set_BoneFirst);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "BoneLast", _s_set_BoneLast);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "BoneListNamesStatic", _s_set_BoneListNamesStatic);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "BoneListNamesNoColliders", _s_set_BoneListNamesNoColliders);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "BoneAxis", _s_set_BoneAxis);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "BoneColliderType", _s_set_BoneColliderType);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "BoneColliderDiameter", _s_set_BoneColliderDiameter);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "BoneColliderSkip", _s_set_BoneColliderSkip);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "BoneColliderLength", _s_set_BoneColliderLength);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "BoneColliderOffset", _s_set_BoneColliderOffset);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LinkMass", _s_set_LinkMass);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LinkSolverIterationCount", _s_set_LinkSolverIterationCount);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LinkJointAngularXLimit", _s_set_LinkJointAngularXLimit);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LinkJointAngularYLimit", _s_set_LinkJointAngularYLimit);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LinkJointAngularZLimit", _s_set_LinkJointAngularZLimit);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LinkJointSpringValue", _s_set_LinkJointSpringValue);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LinkJointDamperValue", _s_set_LinkJointDamperValue);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LinkJointMaxForceValue", _s_set_LinkJointMaxForceValue);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LinkJointBreakForce", _s_set_LinkJointBreakForce);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LinkJointBreakTorque", _s_set_LinkJointBreakTorque);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LockStartEndInZAxis", _s_set_LockStartEndInZAxis);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "SendEvents", _s_set_SendEvents);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "EventsObjectReceiver", _s_set_EventsObjectReceiver);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnBreakMethodName", _s_set_OnBreakMethodName);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "PersistAfterPlayMode", _s_set_PersistAfterPlayMode);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "EnablePrefabUsage", _s_set_EnablePrefabUsage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "AutoRegenerate", _s_set_AutoRegenerate);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Deleted", _s_set_Deleted);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LinkLengths", _s_set_LinkLengths);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "TotalLinks", _s_set_TotalLinks);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "TotalRopeLength", _s_set_TotalRopeLength);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_bRopeStartInitialOrientationInitialized", _s_set_m_bRopeStartInitialOrientationInitialized);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_v3InitialRopeStartLocalPos", _s_set_m_v3InitialRopeStartLocalPos);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_qInitialRopeStartLocalRot", _s_set_m_qInitialRopeStartLocalRot);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_v3InitialRopeStartLocalScale", _s_set_m_v3InitialRopeStartLocalScale);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_nFirstNonCoilNode", _s_set_m_nFirstNonCoilNode);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_afCoilBoneRadiuses", _s_set_m_afCoilBoneRadiuses);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_afCoilBoneAngles", _s_set_m_afCoilBoneAngles);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_afCoilBoneX", _s_set_m_afCoilBoneX);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_fCurrentCoilRopeRadius", _s_set_m_fCurrentCoilRopeRadius);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_fCurrentCoilTurnsLeft", _s_set_m_fCurrentCoilTurnsLeft);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_fCurrentCoilLength", _s_set_m_fCurrentCoilLength);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "ImportedBones", _s_set_ImportedBones);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_bBonesAreImported", _s_set_m_bBonesAreImported);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_strStatus", _s_set_m_strStatus);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_bLastStatusIsError", _s_set_m_bLastStatusIsError);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_strAssetFile", _s_set_m_strAssetFile);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_fCurrentExtension", _s_set_m_fCurrentExtension);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					UltimateRope __cl_gen_ret = new UltimateRope();
					translator.Push(L, __cl_gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception __gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to UltimateRope constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DeleteRope(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 3&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    bool bResetNodePositions = LuaAPI.lua_toboolean(L, 2);
                    bool bDestroySkin = LuaAPI.lua_toboolean(L, 3);
                    
                    __cl_gen_to_be_invoked.DeleteRope( bResetNodePositions, bDestroySkin );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool bResetNodePositions = LuaAPI.lua_toboolean(L, 2);
                    
                    __cl_gen_to_be_invoked.DeleteRope( bResetNodePositions );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 1) 
                {
                    
                    __cl_gen_to_be_invoked.DeleteRope(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UltimateRope.DeleteRope!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DeleteRopeLinks(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.DeleteRopeLinks(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Regenerate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool bResetNodePositions = LuaAPI.lua_toboolean(L, 2);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.Regenerate( bResetNodePositions );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 1) 
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.Regenerate(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UltimateRope.Regenerate!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsLastStatusError(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.IsLastStatusError(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ChangeRopeDiameter(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float fNewDiameter = (float)LuaAPI.lua_tonumber(L, 2);
                    float fNewScaleX = (float)LuaAPI.lua_tonumber(L, 3);
                    float fNewScaleY = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.ChangeRopeDiameter( fNewDiameter, fNewScaleX, fNewScaleY );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MeshCreate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.MeshCreate(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ChangeRopeSegmentSides(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int nNewSegmentSides = LuaAPI.xlua_tointeger(L, 2);
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.ChangeRopeSegmentSides( nNewSegmentSides );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetupRopeMaterials(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.SetupRopeMaterials(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetupRopeLinks(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.SetupRopeLinks(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetupRopeJoints(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.SetupRopeJoints(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CheckNeedsStartExitLockZ(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.CheckNeedsStartExitLockZ(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FillLinkMeshIndicesRope(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 6&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<int[]>(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)) 
                {
                    int nLinearLinkIndex = LuaAPI.xlua_tointeger(L, 2);
                    int nTotalLinks = LuaAPI.xlua_tointeger(L, 3);
                    int[] indices = (int[])translator.GetObject(L, 4, typeof(int[]));
                    bool bBreakable = LuaAPI.lua_toboolean(L, 5);
                    bool bBrokenLink = LuaAPI.lua_toboolean(L, 6);
                    
                    __cl_gen_to_be_invoked.FillLinkMeshIndicesRope( nLinearLinkIndex, nTotalLinks, ref indices, bBreakable, bBrokenLink );
                    translator.Push(L, indices);
                        
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<int[]>(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)) 
                {
                    int nLinearLinkIndex = LuaAPI.xlua_tointeger(L, 2);
                    int nTotalLinks = LuaAPI.xlua_tointeger(L, 3);
                    int[] indices = (int[])translator.GetObject(L, 4, typeof(int[]));
                    bool bBreakable = LuaAPI.lua_toboolean(L, 5);
                    
                    __cl_gen_to_be_invoked.FillLinkMeshIndicesRope( nLinearLinkIndex, nTotalLinks, ref indices, bBreakable );
                    translator.Push(L, indices);
                        
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UltimateRope.FillLinkMeshIndicesRope!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FillLinkMeshIndicesSections(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 6&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<int[]>(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)) 
                {
                    int nLinearLinkIndex = LuaAPI.xlua_tointeger(L, 2);
                    int nTotalLinks = LuaAPI.xlua_tointeger(L, 3);
                    int[] indices = (int[])translator.GetObject(L, 4, typeof(int[]));
                    bool bBreakable = LuaAPI.lua_toboolean(L, 5);
                    bool bBrokenLink = LuaAPI.lua_toboolean(L, 6);
                    
                    __cl_gen_to_be_invoked.FillLinkMeshIndicesSections( nLinearLinkIndex, nTotalLinks, ref indices, bBreakable, bBrokenLink );
                    translator.Push(L, indices);
                        
                    
                    
                    
                    return 1;
                }
                if(__gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<int[]>(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)) 
                {
                    int nLinearLinkIndex = LuaAPI.xlua_tointeger(L, 2);
                    int nTotalLinks = LuaAPI.xlua_tointeger(L, 3);
                    int[] indices = (int[])translator.GetObject(L, 4, typeof(int[]));
                    bool bBreakable = LuaAPI.lua_toboolean(L, 5);
                    
                    __cl_gen_to_be_invoked.FillLinkMeshIndicesSections( nLinearLinkIndex, nTotalLinks, ref indices, bBreakable );
                    translator.Push(L, indices);
                        
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UltimateRope.FillLinkMeshIndicesSections!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HasDynamicSegmentNodes(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.HasDynamicSegmentNodes(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BeforeImportedBonesObjectRespawn(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.BeforeImportedBonesObjectRespawn(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AfterImportedBonesObjectRespawn(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.AfterImportedBonesObjectRespawn(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DoExtendRopeLinear(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float fIncrement = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        float __cl_gen_ret = __cl_gen_to_be_invoked.DoExtendRopeLinear( fIncrement );
                        LuaAPI.lua_pushnumber(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ExtendRope(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UltimateRope.ERopeExtensionMode eRopeExtensionMode;translator.Get(L, 2, out eRopeExtensionMode);
                    float fIncrement = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    __cl_gen_to_be_invoked.ExtendRope( eRopeExtensionMode, fIncrement );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RecomputeCoil(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    __cl_gen_to_be_invoked.RecomputeCoil(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BuildStaticMeshObject(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string strStatusMessage;
                    
                        UnityEngine.GameObject __cl_gen_ret = __cl_gen_to_be_invoked.BuildStaticMeshObject( out strStatusMessage );
                        translator.Push(L, __cl_gen_ret);
                    LuaAPI.lua_pushstring(L, strStatusMessage);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MoveNodeUp(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int nNode = LuaAPI.xlua_tointeger(L, 2);
                    
                    __cl_gen_to_be_invoked.MoveNodeUp( nNode );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MoveNodeDown(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int nNode = LuaAPI.xlua_tointeger(L, 2);
                    
                    __cl_gen_to_be_invoked.MoveNodeDown( nNode );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateNewNode(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int nNode = LuaAPI.xlua_tointeger(L, 2);
                    
                    __cl_gen_to_be_invoked.CreateNewNode( nNode );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveNode(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int nNode = LuaAPI.xlua_tointeger(L, 2);
                    
                    __cl_gen_to_be_invoked.RemoveNode( nNode );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FirstNodeIsCoil(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        bool __cl_gen_ret = __cl_gen_to_be_invoked.FirstNodeIsCoil(  );
                        LuaAPI.lua_pushboolean(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Status(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.Status);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RopeType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.RopeType);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RopeStart(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.RopeStart);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RopeNodes(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.RopeNodes);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RopeLayer(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.RopeLayer);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RopePhysicsMaterial(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.RopePhysicsMaterial);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RopeDiameter(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.RopeDiameter);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RopeDiameterScaleX(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.RopeDiameterScaleX);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RopeDiameterScaleY(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.RopeDiameterScaleY);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RopeSegmentSides(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.RopeSegmentSides);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RopeMaterial(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.RopeMaterial);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RopeTextureTileMeters(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.RopeTextureTileMeters);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RopeSectionMaterial(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.RopeSectionMaterial);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_RopeTextureSectionTileMeters(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.RopeTextureSectionTileMeters);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsExtensible(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.IsExtensible);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ExtensibleLength(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.ExtensibleLength);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_HasACoil(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.HasACoil);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CoilObject(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.CoilObject);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CoilAxisRight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.CoilAxisRight);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CoilAxisUp(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.CoilAxisUp);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CoilWidth(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.CoilWidth);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CoilDiameter(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.CoilDiameter);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CoilNumBones(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.CoilNumBones);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LinkObject(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.LinkObject);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LinkAxis(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.LinkAxis);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LinkOffsetObject(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.LinkOffsetObject);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LinkTwistAngleStart(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.LinkTwistAngleStart);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LinkTwistAngleIncrement(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.LinkTwistAngleIncrement);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BoneFirst(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.BoneFirst);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BoneLast(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.BoneLast);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BoneListNamesStatic(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.BoneListNamesStatic);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BoneListNamesNoColliders(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.BoneListNamesNoColliders);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BoneAxis(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.BoneAxis);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BoneColliderType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.BoneColliderType);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BoneColliderDiameter(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.BoneColliderDiameter);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BoneColliderSkip(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.BoneColliderSkip);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BoneColliderLength(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.BoneColliderLength);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BoneColliderOffset(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.BoneColliderOffset);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LinkMass(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.LinkMass);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LinkSolverIterationCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.LinkSolverIterationCount);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LinkJointAngularXLimit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.LinkJointAngularXLimit);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LinkJointAngularYLimit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.LinkJointAngularYLimit);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LinkJointAngularZLimit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.LinkJointAngularZLimit);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LinkJointSpringValue(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.LinkJointSpringValue);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LinkJointDamperValue(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.LinkJointDamperValue);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LinkJointMaxForceValue(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.LinkJointMaxForceValue);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LinkJointBreakForce(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.LinkJointBreakForce);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LinkJointBreakTorque(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.LinkJointBreakTorque);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LockStartEndInZAxis(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.LockStartEndInZAxis);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SendEvents(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.SendEvents);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_EventsObjectReceiver(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.EventsObjectReceiver);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnBreakMethodName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.OnBreakMethodName);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PersistAfterPlayMode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.PersistAfterPlayMode);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_EnablePrefabUsage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.EnablePrefabUsage);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AutoRegenerate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.AutoRegenerate);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Deleted(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.Deleted);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LinkLengths(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.LinkLengths);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_TotalLinks(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.TotalLinks);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_TotalRopeLength(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.TotalRopeLength);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_bRopeStartInitialOrientationInitialized(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.m_bRopeStartInitialOrientationInitialized);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_v3InitialRopeStartLocalPos(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector3(L, __cl_gen_to_be_invoked.m_v3InitialRopeStartLocalPos);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_qInitialRopeStartLocalRot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineQuaternion(L, __cl_gen_to_be_invoked.m_qInitialRopeStartLocalRot);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_v3InitialRopeStartLocalScale(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector3(L, __cl_gen_to_be_invoked.m_v3InitialRopeStartLocalScale);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_nFirstNonCoilNode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, __cl_gen_to_be_invoked.m_nFirstNonCoilNode);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_afCoilBoneRadiuses(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.m_afCoilBoneRadiuses);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_afCoilBoneAngles(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.m_afCoilBoneAngles);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_afCoilBoneX(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.m_afCoilBoneX);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_fCurrentCoilRopeRadius(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.m_fCurrentCoilRopeRadius);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_fCurrentCoilTurnsLeft(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.m_fCurrentCoilTurnsLeft);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_fCurrentCoilLength(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.m_fCurrentCoilLength);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ImportedBones(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                translator.Push(L, __cl_gen_to_be_invoked.ImportedBones);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_bBonesAreImported(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.m_bBonesAreImported);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_strStatus(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.m_strStatus);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_bLastStatusIsError(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, __cl_gen_to_be_invoked.m_bLastStatusIsError);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_strAssetFile(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, __cl_gen_to_be_invoked.m_strAssetFile);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_fCurrentExtension(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, __cl_gen_to_be_invoked.m_fCurrentExtension);
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Status(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.Status = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RopeType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                UltimateRope.ERopeType __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.RopeType = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RopeStart(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.RopeStart = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RopeNodes(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.RopeNodes = (System.Collections.Generic.List<UltimateRope.RopeNode>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<UltimateRope.RopeNode>));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RopeLayer(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.RopeLayer = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RopePhysicsMaterial(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.RopePhysicsMaterial = (UnityEngine.PhysicMaterial)translator.GetObject(L, 2, typeof(UnityEngine.PhysicMaterial));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RopeDiameter(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.RopeDiameter = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RopeDiameterScaleX(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.RopeDiameterScaleX = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RopeDiameterScaleY(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.RopeDiameterScaleY = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RopeSegmentSides(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.RopeSegmentSides = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RopeMaterial(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.RopeMaterial = (UnityEngine.Material)translator.GetObject(L, 2, typeof(UnityEngine.Material));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RopeTextureTileMeters(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.RopeTextureTileMeters = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RopeSectionMaterial(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.RopeSectionMaterial = (UnityEngine.Material)translator.GetObject(L, 2, typeof(UnityEngine.Material));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_RopeTextureSectionTileMeters(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.RopeTextureSectionTileMeters = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsExtensible(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.IsExtensible = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ExtensibleLength(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.ExtensibleLength = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_HasACoil(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.HasACoil = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CoilObject(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.CoilObject = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CoilAxisRight(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                UltimateRope.EAxis __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.CoilAxisRight = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CoilAxisUp(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                UltimateRope.EAxis __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.CoilAxisUp = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CoilWidth(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.CoilWidth = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CoilDiameter(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.CoilDiameter = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CoilNumBones(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.CoilNumBones = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LinkObject(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.LinkObject = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LinkAxis(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                UltimateRope.EAxis __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.LinkAxis = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LinkOffsetObject(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.LinkOffsetObject = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LinkTwistAngleStart(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.LinkTwistAngleStart = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LinkTwistAngleIncrement(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.LinkTwistAngleIncrement = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_BoneFirst(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.BoneFirst = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_BoneLast(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.BoneLast = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_BoneListNamesStatic(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.BoneListNamesStatic = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_BoneListNamesNoColliders(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.BoneListNamesNoColliders = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_BoneAxis(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                UltimateRope.EAxis __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.BoneAxis = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_BoneColliderType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                UltimateRope.EColliderType __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.BoneColliderType = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_BoneColliderDiameter(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.BoneColliderDiameter = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_BoneColliderSkip(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.BoneColliderSkip = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_BoneColliderLength(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.BoneColliderLength = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_BoneColliderOffset(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.BoneColliderOffset = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LinkMass(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.LinkMass = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LinkSolverIterationCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.LinkSolverIterationCount = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LinkJointAngularXLimit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.LinkJointAngularXLimit = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LinkJointAngularYLimit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.LinkJointAngularYLimit = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LinkJointAngularZLimit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.LinkJointAngularZLimit = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LinkJointSpringValue(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.LinkJointSpringValue = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LinkJointDamperValue(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.LinkJointDamperValue = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LinkJointMaxForceValue(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.LinkJointMaxForceValue = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LinkJointBreakForce(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.LinkJointBreakForce = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LinkJointBreakTorque(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.LinkJointBreakTorque = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LockStartEndInZAxis(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.LockStartEndInZAxis = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_SendEvents(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.SendEvents = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_EventsObjectReceiver(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.EventsObjectReceiver = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnBreakMethodName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.OnBreakMethodName = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_PersistAfterPlayMode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.PersistAfterPlayMode = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_EnablePrefabUsage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.EnablePrefabUsage = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_AutoRegenerate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.AutoRegenerate = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Deleted(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.Deleted = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LinkLengths(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.LinkLengths = (float[])translator.GetObject(L, 2, typeof(float[]));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_TotalLinks(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.TotalLinks = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_TotalRopeLength(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.TotalRopeLength = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_bRopeStartInitialOrientationInitialized(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_bRopeStartInitialOrientationInitialized = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_v3InitialRopeStartLocalPos(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector3 __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.m_v3InitialRopeStartLocalPos = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_qInitialRopeStartLocalRot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                UnityEngine.Quaternion __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.m_qInitialRopeStartLocalRot = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_v3InitialRopeStartLocalScale(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector3 __cl_gen_value;translator.Get(L, 2, out __cl_gen_value);
				__cl_gen_to_be_invoked.m_v3InitialRopeStartLocalScale = __cl_gen_value;
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_nFirstNonCoilNode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_nFirstNonCoilNode = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_afCoilBoneRadiuses(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_afCoilBoneRadiuses = (float[])translator.GetObject(L, 2, typeof(float[]));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_afCoilBoneAngles(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_afCoilBoneAngles = (float[])translator.GetObject(L, 2, typeof(float[]));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_afCoilBoneX(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_afCoilBoneX = (float[])translator.GetObject(L, 2, typeof(float[]));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_fCurrentCoilRopeRadius(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_fCurrentCoilRopeRadius = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_fCurrentCoilTurnsLeft(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_fCurrentCoilTurnsLeft = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_fCurrentCoilLength(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_fCurrentCoilLength = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_ImportedBones(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.ImportedBones = (UltimateRope.RopeBone[])translator.GetObject(L, 2, typeof(UltimateRope.RopeBone[]));
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_bBonesAreImported(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_bBonesAreImported = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_strStatus(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_strStatus = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_bLastStatusIsError(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_bLastStatusIsError = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_strAssetFile(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_strAssetFile = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_fCurrentExtension(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UltimateRope __cl_gen_to_be_invoked = (UltimateRope)translator.FastGetCSObj(L, 1);
                __cl_gen_to_be_invoked.m_fCurrentExtension = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
