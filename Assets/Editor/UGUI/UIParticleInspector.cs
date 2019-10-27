using UnityEngine;
using System.Collections;
using UnityEditor;
using JW.Framework.UGUI;

[CustomEditor(typeof(UIParticle))]
public class CUIParticleEditor : Editor
{
    private UIParticle m_uiParticleScript;

    protected virtual void OnEnable()
    {
        m_uiParticleScript = target as UIParticle;
    }

    void OnDisable()
    {
        m_uiParticleScript = null;
    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (EditorApplication.isPlayingOrWillChangePlaymode)
        {
            return;
        }
        UIUtility.SetGameObjectLayer(m_uiParticleScript.gameObject, UIUtility.c_uiLayer);
    }
};
