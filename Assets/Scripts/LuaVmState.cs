using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JW.Common;
using JW.Framework.MVC;
using JW.Lua;

public class LuaVmState : IState
{

    public void InitializeState()
    {

    }

    public string Name()
    {
        return "LuaVM";
    }

    public void OnStateEnter(object usrData = null)
    {
        JW.Common.Log.LogD("Enter Login GameState");
        LuaService.GetInstance();
        LuaService.GetInstance().LoadMainLua(OnMainLuaLoaded);
    }

    private void OnMainLuaLoaded(int st)
    {
        if (st == LuaLoader.LoadStateSuccess)
        {
            LuaService.GetInstance().InitFramework();
            LuaService.GetInstance().InitLogic();
            LuaService.GetInstance().StartGame();
        }
    }

    public void OnStateLeave()
    {
        JW.Common.Log.LogD("Leave LuaVM GameState");
        LuaService.DestroyInstance();
        //
        JW.Framework.Audio.AudioService.GetInstance().StopAll();
        JW.Framework.Asset.AssetService.GetInstance().UnloadAllUsingAssets();

    }

    public void OnStateOverride()
    {

    }

    public void OnStateResume()
    {

    }

    public void UninitializeState()
    {

    }
}
