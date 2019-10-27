/********************************************************************
	created:	2018-06-05   
	filename: 	UIUpdateMediator
	author:		jordenwu
	
	purpose:	更新模块UI中间件
*********************************************************************/
using JW.Framework.MVC;

public class UIUpdateMediator : UIMediator {

    private UIUpdateForm _updateForm;
    protected override string[] GetBelongsUIStateName()
    {
        return new string[] { "UIUpdate" };
    }

    /// <summary>
    /// 当初始化时调用
    /// </summary>
    protected override void OnInitialize()
    {
    }

    /// <summary>
    /// 当销毁时调用调用
    /// </summary>
    protected override void OnUninitialize()
    {
        if (_updateForm != null)
        {
            DisposeFormClass(ref _updateForm);
            _updateForm = null;
        }
    }

    /// <summary>
    /// 当UIState切入时调用
    /// </summary>
    /// <param name="changeType">切换类型</param>
    /// <param name="oldStateName">老状态名，可能为空和null</param>
    /// <param name="newStateName">新状态名，可能为空和null</param>
    /// <param name="stateParam">状态参数</param>
    protected override void OnUIStateIn(int changeType, string oldStateName, string newStateName, object stateParam)
    {
        if (newStateName.Equals("UIUpdate"))
        {
            if (_updateForm == null)
            {
                _updateForm = CreateFormClass<UIUpdateForm>(false);
            }
        }
    }

    /// <summary>
    /// 当UIState切出时调用
    /// <param name="changeType">切换类型</param>
    /// <param name="oldStateName">老状态名，可能为空和null</param>
    /// <param name="newStateName">新状态名，可能为空和null</param>
    /// </summary>
    protected override void OnUIStateOut(int changeType, string oldStateName, string newStateName)
    {
        
    }

    /// <summary>
    /// 状态刷新时调用
    /// <param name="changeType">切换类型</param>
    /// <param name="oldStateName">老状态名，可能为空和null</param>
    /// <param name="newStateName">新状态名，可能为空和null</param>
    /// <param name="stateParam">状态参数</param>
    /// </summary>
    protected override void OnUIStateStay(int changeType, string oldStateName, string newStateName, object stateParam)
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
        if (id.Equals("DoDestroyForm"))
        {
            this.DestroyForm();
        }
        return false;
    }

    /// <summary>
    /// 操作
    /// </summary>
    /// <param name="id">操作ID标识</param>
    /// <param name="param">操作参数</param>
    /// <returns>是否阻断向上传递操作</returns>
    protected override bool OnAction(string id, object param)
    {
        return false;
    }

    private void DestroyForm()
    {
        if (_updateForm != null)
        {
            DisposeFormClass(ref _updateForm);
            _updateForm = null;
        }
    }
}
