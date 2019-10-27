/********************************************************************
	created:	2018-11-1
	author:		jordenwu
	
	purpose:	注册街机设备 窗口
*********************************************************************/
using UnityEngine;
using JW.Framework.MVC;
using System;
using System.Text;
using UnityEngine.UI;
using JW.Framework.ArcadeInput;
using DG.Tweening;
public class UIRegisterForm : UIFormClass
{
    public override string GetPath()
    {
        return "Fixed/Register/UIRegisterForm";
    }

    private const int _uiMIDInputRectTransformIndex = 0;
    private const int _uiMIDShowTextIndex = 1;
    private const int _uiMIDCursorRectTransformIndex = 2;
    private const int _uiTipTextTextIndex = 3;

    private RectTransform _uiMIDInputRectTransform;
    private Text _uiMIDShowText;
    private RectTransform _uiMIDCursorRectTransform;
    private Text _uiTipTextText;


    private enum InputState
    {
        None,
        Ready,
        Waiting,
        Stop,
    }

    private const float NumWidth = 56;
    private int _cursorPos;
    private StringBuilder _numSB;
    private InputState _state;

    /// <summary>
    /// 资源加载后回调
    /// </summary>
    protected override void OnResourceLoaded()
    {
        _uiMIDInputRectTransform = GetComponent(_uiMIDInputRectTransformIndex) as RectTransform;
        _uiMIDShowText = GetComponent(_uiMIDShowTextIndex) as Text;
        _uiMIDCursorRectTransform = GetComponent(_uiMIDCursorRectTransformIndex) as RectTransform;
        _uiTipTextText = GetComponent(_uiTipTextTextIndex) as Text;

    }

    /// <summary>
    /// 资源卸载后回调
    /// </summary>
    protected override void OnResourceUnLoaded()
    {
        _uiMIDInputRectTransform = null;
        _uiMIDShowText = null;
        _uiMIDCursorRectTransform = null;
        _uiTipTextText = null;
    }

    //*
    /// <summary>
    /// 逻辑初始化
    /// </summary>
    /// <param name="parameter">初始化参数</param>
    protected override void OnInitialize(object parameter)
    {
        //监听输入
        ArcadeInputService.GetInstance().RockerHandler += DealRockerInput;
        ArcadeInputService.GetInstance().PressHandler += DealPressInput;
        //
        _cursorPos = 0;
        _numSB = new StringBuilder();
        _numSB.Append("00000000000");
        _uiMIDShowText.text = _numSB.ToString();
        _state = InputState.Ready;
        //
        _uiMIDCursorRectTransform.anchoredPosition = new Vector2(_cursorPos * NumWidth, -55);
        _uiTipTextText.text = "";
        //
        Tweener tw = _uiMIDCursorRectTransform.DOShakeScale(0.15f);
        tw.SetLoops(-1);

    }

    /// <summary>
    /// 逻辑反初始化
    /// </summary>
    protected override void OnUninitialize()
    {
        ArcadeInputService.GetInstance().RockerHandler -= DealRockerInput;
        ArcadeInputService.GetInstance().PressHandler -= DealPressInput;
    }

    /// <summary>
    /// 更新UI
    /// </summary>
    /// <param name="id">更新ID标识</param>
    /// <param name="param">更新参数</param>
    /// <returns>是否阻断向下传递刷新</returns>
    protected override bool OnUpdateUI(string id, object param)
    {
        if (id.Equals("ShowTip"))
        {
            this._uiTipTextText.text = param as string;
        }
        return true;
    }


    //处理输入------------------
    private string NumberReplace(StringBuilder sb, int position, int add)
    {
        if (sb == null || sb.Length < position + 1)
        {
            JW.Common.Log.LogE("MID Replace Error");
            return "00000000000";
        }
        int n = Convert.ToInt32(sb[position]) + add;
        if (n > '0' + 9)
        {
            n -= 10;
        }
        if (n < '0')
        {
            n += 10;
        }
        sb = sb.Replace(sb[position], Convert.ToChar(n), position, 1);
        return sb.ToString();
    }

    private void DealRockerInput(int st)
    {
        if (_state == InputState.Stop)
        {
            return;
        }
        //上
        if ((RockerState)st == RockerState.RockerMoveForward)
        {
            if (_state == InputState.Ready)
            {
                _uiMIDShowText.text = NumberReplace(_numSB, _cursorPos, 1);
                _state = InputState.Waiting;
            }
            return;
        }

        //下
        if ((RockerState)st == RockerState.RockerMoveBack)
        {
            if (_state == InputState.Ready)
            {
                _uiMIDShowText.text = NumberReplace(_numSB, _cursorPos, -1);
                _state = InputState.Waiting;
            }
            return;
        }

        //右
        if ((RockerState)st == RockerState.RockerMoveRight)
        {
            if (_state == InputState.Ready)
            {
                if (_cursorPos < 10)
                {
                    _cursorPos++;
                }
                _uiMIDCursorRectTransform.DOAnchorPosX(_cursorPos * NumWidth, 0.2f);
                _state = InputState.Waiting;
            }
            return;
        }
        //左
        if ((RockerState)st == RockerState.RockerMoveLeft)
        {
            if (_state == InputState.Ready)
            {
                if (_cursorPos > 0)
                {
                    _cursorPos--;
                }
                _uiMIDCursorRectTransform.DOAnchorPosX(_cursorPos * NumWidth, 0.2f);
                _state = InputState.Waiting;
            }
            return;
        }
        //回位
        if ((RockerState)st == RockerState.RockerMoveMiddle)
        {
            _state = InputState.Ready;
        }

    }

    //确定按键
    private void DealPressInput(int pe)
    {
        if ((PressEvent)pe == PressEvent.PressClick)
        {
            Action("DoRegisterMachine", _numSB.ToString());
        }
    }

}
