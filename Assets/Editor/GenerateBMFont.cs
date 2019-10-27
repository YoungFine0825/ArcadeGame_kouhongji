
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Xml;
using System.Runtime.InteropServices;

public class GenerateBMFont : EditorWindow {

    [DllImport("User32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern int MessageBox(System.IntPtr handler,string message,string title,int type);
    

    [MenuItem("BMFont/ImportFontInfo",false,1)]
    public static void GeneateFont()
    {
        EditorWindow.GetWindow(typeof(GenerateBMFont));
    }

    Font mCustomFont = null;
    TextAsset mTextAsset = null;
    Texture mTexture = null;

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.Space(10);
        GUI.skin.label.fontSize = 24;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("导入字体布局到CustomFont");

        GUI.skin.label.fontSize = 12;
        GUI.skin.label.alignment = TextAnchor.MiddleLeft;
        GUILayout.Space(20);
        GUILayout.Label("在BMFont导出设置中设置字体布局文件为XML格式，先修改文件后缀为.txt，再导入Unity！");
        GUILayout.Space(20);
        mCustomFont = EditorGUILayout.ObjectField("Custom Font 文件：", mCustomFont,typeof(Font)) as Font;
        GUILayout.Space(10);
        mTextAsset = EditorGUILayout.ObjectField("字体布局文件：", mTextAsset, typeof(TextAsset)) as TextAsset;
        
        GUILayout.Space(10);
        if (GUILayout.Button("导入"))
        {
            Import();
        }
        GUILayout.EndVertical();
        
    }

    private void Import()
    {
        if (!mCustomFont) { ShowMsgBox("缺少CustomFont文件！", "错误！"); return; }
        if (mCustomFont.material)
        {
            if (!mCustomFont.material.mainTexture)
            {
                ShowMsgBox("CustomFont文件的材质主纹理不能为空！", "错误！");
                return;
            }
            else
            {
                mTexture = mCustomFont.material.mainTexture;
            }
        }
        else
        {
            ShowMsgBox("请检查CustomFont文件的材质是否正确！", "错误！");
            return;
        }

        if (mTextAsset)
        {

            XmlNodeList fontinfolist = ReadXmlNode(AssetDatabase.GetAssetPath(mTextAsset), "font/chars");
            XmlElement elt = null;
            int x = 0;
            int y = 0;
            int width = 0;
            int height = 0;
            int id = 0;
            int advance = 0;
            List<CharacterInfo> cilist = new List<CharacterInfo>();
            foreach (XmlNode node in fontinfolist)
            {
                elt = (XmlElement)node;
                id = int.Parse(elt.GetAttribute("id"));
                x = int.Parse(elt.GetAttribute("x"));
                y = int.Parse(elt.GetAttribute("y"));
                width = int.Parse(elt.GetAttribute("width"));
                height = int.Parse(elt.GetAttribute("height"));
                advance = int.Parse(elt.GetAttribute("xadvance"));

                CharacterInfo ci = new CharacterInfo();
                ci.glyphWidth = mTexture.width;
                ci.glyphHeight = mTexture.height;
                ci.index = id;
                ci.advance = advance;

                ci.uvTopLeft = new Vector2((float)x / mTexture.width, 1 - (float)y / mTexture.height);
                ci.uvTopRight = new Vector2((float)(x + width) / mTexture.width, 1 - (float)y / mTexture.height);

                ci.uvBottomLeft = new Vector2((float)x / mTexture.width, 1 - (float)(y + height) / mTexture.height);
                ci.uvBottomRight = new Vector2((float)(x + width) / mTexture.width, 1 - (float)(y + height) / mTexture.height);

                ci.minX = 0;
                ci.maxX = width;
                ci.maxY = 0;
                ci.minY = -height;

                cilist.Add(ci);

            }

            mCustomFont.characterInfo = cilist.ToArray();

            AssetDatabase.Refresh();

            ShowMsgBox("导入字体布局成功！！", "成功！");
        }
        else
        {
            ShowMsgBox("缺少字体布局文件！", "错误！");
            return;
        }
    }

    private XmlNodeList ReadXmlNode(string filePath,string nodePath)
    {
        XmlDocument xml = new XmlDocument();
        xml.Load(filePath);
        XmlNode node = xml.SelectSingleNode(nodePath);
        return node.ChildNodes;
    }

    private void ShowMsgBox(string msg, string title)
    {
        MessageBox(System.IntPtr.Zero, msg, title, 0);
    }
}
