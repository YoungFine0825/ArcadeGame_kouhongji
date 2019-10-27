/********************************************************************
*		作者： XH
*		时间： 2018-08-03
*		描述： DropDown扩展
*********************************************************************/
using System;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JW.Framework.UGUI;
public class UIDropDown: UIComponent {
    private Dropdown _dropDown = null;
    public Action<int, string> mChangeCallBack = null;
    //初始化
    private void Awake() {
        _dropDown = this.transform.GetComponent<Dropdown>();
        _dropDown.onValueChanged.AddListener (OnDropChange);
    }

    //添加下拉成员
    public void SetOptions(string content, char parseChar=',')
    {
        if(_dropDown == null) 
            return;
        string[] options = content.Split(parseChar);
        List<string> list = new List<string>(options);
        _dropDown.ClearOptions();
        _dropDown.AddOptions(list);
    }
    //清除
    public void ClearOptions()
    {
        if(_dropDown == null)
            return;
        _dropDown.ClearOptions();
    }
    //选中某个下拉item
    public void OnDropChange(int index)
	{
		if(_dropDown == null)
            return;
		if (null != mChangeCallBack)
			mChangeCallBack (index,_dropDown.options [index].text);
	} 
    //设置默认选中
    public void SetDefaultIndex(int index)
    {
        if(index < 0 || index >= _dropDown.options.Count)
			return;
		_dropDown.value = index;
		_dropDown.captionText.text = _dropDown.options[index].text;
    }
    //获取当前选中的文本内容
    public string GetSelectedCaptionText()
    {
        if(_dropDown == null)
            return string.Empty;
        return _dropDown.captionText.text;
    }
}