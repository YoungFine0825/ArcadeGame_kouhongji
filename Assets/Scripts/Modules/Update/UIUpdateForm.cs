/********************************************************************
	created:	2018-06-05   
	filename: 	UIUpdateForm
	author:		jordenwu
	
	purpose:	更新状态UI窗口
*********************************************************************/
using JW.Framework.MVC;
using JW.Framework.UGUI;
using UnityEngine.UI;

public class UIUpdateForm : UIFormClass
{
    public override string GetPath()
    {
        return "Fixed/Update/UIUpdateForm";
    }


    private const int _uiProgressSliderSliderIndex = 0;
    private const int _uiTipTextTextIndex = 1;

    private Slider _uiProgressSliderSlider;
    private Text _uiTipTextText;


    /// <summary>
    /// 资源加载后回调
    /// </summary>
    protected override void OnResourceLoaded()
    {
        _uiProgressSliderSlider = GetComponent(_uiProgressSliderSliderIndex) as Slider;
        _uiTipTextText = GetComponent(_uiTipTextTextIndex) as Text;

    }

    /// <summary>
    /// 资源卸载后回调
    /// </summary>
    protected override void OnResourceUnLoaded()
    {
        _uiProgressSliderSlider = null;
        _uiTipTextText = null;

    }

    /// <summary>
    /// 逻辑初始化
    /// </summary>
    /// <param name="parameter">初始化参数</param>
    protected override void OnInitialize(object parameter)
    {
    }

    /// <summary>
    /// 逻辑反初始化
    /// </summary>
    protected override void OnUninitialize()
    {
    }

    /// <summary>
    /// 更新UI
    /// </summary>
    /// <param name="id">更新ID标识</param>
    /// <param name="param">更新参数</param>
    /// <returns>是否阻断向下传递刷新</returns>
    protected override bool OnUpdateUI(string id, object param)
    {
        if(id== "OnUpdateStateInfo")
        {
            EventDeclare.UpdateStateChangeEventArg realArg = (EventDeclare.UpdateStateChangeEventArg)param;

            if (_uiProgressSliderSlider != null)
            {
                _uiProgressSliderSlider.value = realArg.Progress;
            }
            if (_uiTipTextText != null)
            {
                _uiTipTextText.text = realArg.StateInfo;
            }
            return true;
        }
        return true;
    }

}
