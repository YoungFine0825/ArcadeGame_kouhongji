/********************************************************************
	created:	2018-05-31   
	filename: 	UGUIRoot
	author:		jordenwu
	
	purpose:	UGUI 根
*********************************************************************/
using System;
using UnityEngine;
using JW.Common;
using System.Collections.Generic;
using JW.Framework.Asset;
using UnityEngine.EventSystems;
using JW.Res;
using UnityEngine.UI;

namespace JW.Framework.UGUI
{
    /// <summary>
    /// UI窗口层次切换管理
    /// </summary>
    public class UGUIRoot : Singleton<UGUIRoot>
    {
        private static string sFormCameraName = "Camera_Form";
        //LayerMask.NameToLayer("UI");
        private const int Const_FormCameraMaskLayer = 5;    
        private const int Const_FormCameraDepth = 10;

        //Form列表
        private JWObjList<UIForm> _forms;

        //Form打开顺序号
        private int _formOpenOrder;

        //Form序列号，可以作为每个Form的唯一标识
        private int _formSequence;

        //UI元素 Root
        private GameObject _uiRoot;

        public delegate void OnFormSortedDelegate(JWObjList<UIForm> inForms);
        public OnFormSortedDelegate OnFormSortedHandler = null;

        //UI系统渲染帧数
        public static int sUGUIFrameCount;

        //UI输入事件系统
        private EventSystem _uiInputEventSystem;

        //Camera
        private Camera _formCamera;
        public Camera FormCamera
        {
            get
            {
                return _formCamera;
            }
        }

        //Forms相关操作
        private bool _needSortForms = false;
        private bool _needUpdateRaycasterAndHide = false;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns>初始化成功/失败</returns>
        public override bool Initialize()
        {
            _forms = new JWObjList<UIForm>();
            _formOpenOrder = 1;
            _formSequence = 0;
            sUGUIFrameCount = 0;
            //创建UIRoot
            CreateUIRoot();
            //创建EventSystem
            CreateEventSystem();
            //创建Camera
            CreateCamera();
            return true;
        }

        private void CreateUIRoot()
        {
            _uiRoot = new GameObject("UGUIRoot");
            GameObject.DontDestroyOnLoad(_uiRoot);
        }

        private void CreateEventSystem()
        {
            //街机不需要
            //_uiInputEventSystem = UnityEngine.Object.FindObjectOfType<EventSystem>();
            //if (_uiInputEventSystem == null)
            //{
            //    GameObject eventSystem = new GameObject("EventSystem");
            //    _uiInputEventSystem = eventSystem.AddComponent<EventSystem>();
            //    eventSystem.AddComponent<StandaloneInputModule>();
            //}
            //_uiInputEventSystem.gameObject.transform.parent = _uiRoot.transform;
        }

        private void CreateCamera()
        {
            GameObject cameraObject = new GameObject(sFormCameraName);
            cameraObject.transform.SetParent(_uiRoot.transform, true);
            cameraObject.transform.localPosition = Vector3.zero;
            cameraObject.transform.localRotation = Quaternion.identity;
            cameraObject.transform.localScale = Vector3.one;
            //
            Camera camera = cameraObject.AddComponent<Camera>();
            camera.orthographic = true;
            camera.orthographicSize = 50;
            camera.clearFlags = CameraClearFlags.Depth;

            camera.cullingMask = 1 << Const_FormCameraMaskLayer;
            camera.depth = Const_FormCameraDepth;
            camera.useOcclusionCulling = false;
            //camera.allowDynamicResolution = false;
            camera.allowMSAA = false;
            camera.allowHDR = false;
            _formCamera = camera;
        }


        /// <summary>
        /// 反初始化
        /// </summary>
        public override void Uninitialize()
        {
            if (_forms != null)
            {
                //清理
                for (int i = 0; i < _forms.Count;i++)
                {
                    UIForm cur = _forms[i];
                    if (cur != null)
                    {
                        //转为关闭状态
                        if (cur.TurnToClosed(true))
                        {
                            cur.OnRemove();
                            _forms[i] = null;
                        }
                    }
                }
                _forms.Clear();
                _forms = null;
            }

            _formOpenOrder = 1;
            _formSequence = 0;
            sUGUIFrameCount = 0;
            if (_uiRoot != null)
            {
                GameObject.Destroy(_uiRoot);
                _uiRoot = null;
            }
            _formCamera = null;
            _uiInputEventSystem = null;
        }

        //统一驱动
        public void CustomUpdate()
        {
            for (int i = 0; i < _forms.Count;)
            {
                UIForm cur = _forms[i];
                cur.CustomUpdate();
                //
                if (cur.IsNeedClose())
                {
                    //转为关闭状态成功
                    if (cur.TurnToClosed(false))
                    {
                        //真正关闭
                        cur.OnRemove();
                        _forms.RemoveAt(i);
                        _needSortForms = true;
                        continue;
                    }
                }
                else if (cur.IsClosed())
                {
                    //FadeOut结束后移除
                    if (!cur.IsInFadeOut())
                    {
                        //移除
                        cur.OnRemove();
                        _forms.RemoveAt(i);
                        _needSortForms = true;
                        continue;
                    }
                }
                //直到
                i++;
            }

            //有form被关闭等sortingOrder发生改变的情况发生，需要对form进行排序，如果关闭form的情况，还需要重置sequence并刷新raycaster
            if (_needSortForms)
            {
                ProcessFormList(true, true);
            }
            else if (_needUpdateRaycasterAndHide)
            {
                ProcessFormList(false, true);
            }
            _needSortForms = false;
            _needUpdateRaycasterAndHide = false;
        }

        /// LateUpdate
        public void CustomLateUpdate()
        {
            for (int i = 0; i < _forms.Count; i++)
            {
                if (_forms[i] != null)
                {
                    _forms[i].CustomLateUpdate();
                }
            }
            //计数
            sUGUIFrameCount++;
        }


        #region 输入控制

        //返回EventSystem
        public EventSystem GetEventSystem()
        {
            return _uiInputEventSystem;
        }

        public void DisableInput()
        {
            if (_uiInputEventSystem != null)
            {
                _uiInputEventSystem.gameObject.ExtSetActive(false);
            }
        }

        public void EnableInput()
        {
            if (_uiInputEventSystem != null)
            {
                _uiInputEventSystem.gameObject.ExtSetActive(true);
            }
        }
        #endregion

        #region 打开关闭控制
        
        //处理Form优先级 和 显示改变 
        private void OnUIFormEvent(UIFormEventType eType)
        {
            if (eType == UIFormEventType.VisibleChanged)
            {
                _needUpdateRaycasterAndHide = true;
            }
            if (eType == UIFormEventType.PriorityChanged)
            {
                _needSortForms = true;
            }
        }
        
        //获取目前还未处于关闭状态的指定页面
        private UIForm GetUnClosedForm(string formPath)
        {
            for (int i = 0; i < _forms.Count; i++)
            {
                if (_forms[i] != null)
                {
                    if (_forms[i].FormPath.Equals(formPath) && !_forms[i].IsClosed())
                    {
                        return _forms[i];
                    }
                }
            }
            return null;
        }

        /// 关闭同组Form
        public void CloseGroupForm(int group)
        {
            if (group == 0)
            {
                return;
            }

            for (int i = 0; i < _forms.Count; i++)
            {
                if (_forms[i].GroupId == group)
                {
                    _forms[i].Close();
                }
            }
        }

        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <param name="formCpt">窗口组件</param>
        /// <returns></returns>
        public UIForm OpenForm(UIForm formCpt,bool useCamera=true)
        {
            if (formCpt == null)
            {
                JW.Common.Log.LogE("Open Form Error Null Arg");
                return null;
            }
            UIForm old;
            //检查同名Form是否存在
            old = GetUnClosedForm(formCpt.FormPath);
            //只有一个 重新打开
            if (old != null && old.IsSingleton)
            {
                //更新sequence
                old.Open(_formSequence, _formOpenOrder, true);
                _formSequence++;
                _formOpenOrder++;
                _needSortForms = true;
                //
                return old;
            }
            //
            GameObject formGo = formCpt.gameObject;
            if (formGo == null)
            {
                JW.Common.Log.LogE("Form " + formCpt.FormPath + " Open Fail!!!");
                return null;
            }

            //确保form为active
            if (!formGo.activeSelf)
            {
                formGo.ExtSetActive(true);
            }
            //
            //挂接
            if (formGo.transform.parent != _uiRoot.transform)
            {
                formGo.transform.SetParent(_uiRoot.transform);
            }

            //设置参数
            if (formCpt != null)
            {
                formCpt.Open(useCamera ? _formCamera : null, _formSequence, _formOpenOrder, false);
                //close 同组Form
                if (formCpt.GroupId > 0)
                {
                    CloseGroupForm(formCpt.GroupId);
                }
                //监听
                formCpt.EventHandler += this.OnUIFormEvent;
                _forms.Add(formCpt);
            }
            _formSequence++;
            _formOpenOrder++;
            _needSortForms = true;
            return formCpt;
        }

        /// 关闭窗口
        public void CloseForm(UIForm form)
        {
            if (form == null)
            {
                return;
            }
            if (_forms == null)
            {
                return;
            }
            for (int i = 0; i < _forms.Count; i++)
            {
                if (_forms[i] == form)
                {
                    _forms[i].Close();
                }
            }
        }

        /// 关闭页面
        public void CloseForm(int formSequence)
        {
            for (int i = 0; i < _forms.Count; i++)
            {
                if (_forms[i].GetSequence() == formSequence)
                {
                    _forms[i].Close();
                }
            }
        }

        /// 关闭所有窗口
        /// @exceptFormNames 排除列表
        /// @closeImmediately 是否立即执行close
        public void CloseAllForm(string[] exceptFormNames = null, bool closeImmediately = true)
        {
            for (int i = 0; i < _forms.Count; i++)
            {
                bool close = true;
                if (exceptFormNames != null)
                {
                    for (int j = 0; j < exceptFormNames.Length; j++)
                    {
                        if (string.Equals(_forms[i].FormPath, exceptFormNames[j]))
                        {
                            close = false;
                            break;
                        }
                    }
                }
                if (close)
                {
                    _forms[i].Close();
                }
            }
            //立即模式
            if (closeImmediately)
            {
                for (int i = 0; i < _forms.Count;)
                {
                    if (_forms[i].IsNeedClose() || _forms[i].IsClosed())
                    {
                        //转为关闭状态并回收(忽略fadeout)
                        if (_forms[i].IsNeedClose())
                        {
                            _forms[i].TurnToClosed(true);
                        }
                        _forms.RemoveAt(i);
                        continue;
                    }
                    i++;
                }
                if (exceptFormNames != null)
                {
                    ProcessFormList(true, true);
                }
            }
        }

        /// 处理Forms次序相关
        /// @sort : 是否排序(按显示顺序升序排列)
        /// @handleInputAndHide : 是否处理输入屏蔽及隐藏相关
        private void ProcessFormList(bool sort, bool handleInputAndHide)
        {
            if (sort)
            {
                _forms.Sort();

                if (true)   //(m_formOpenOrder > 10)
                {
                    for (int i = 0; i < _forms.Count; i++)
                    {
                        _forms[i].SetDisplayOrder(i + 1);
                    }
                    _formOpenOrder = _forms.Count + 1;
                }
            }

            if (handleInputAndHide)
            {
                UpdateFormHided();
                UpdateFormRaycaster();
            }

            if (OnFormSortedHandler != null)
            {
                OnFormSortedHandler(_forms);
            }
        }

        /// Update Form 隐藏情况
        private void UpdateFormHided()
        {
            bool needHide = false;

            for (int i = _forms.Count - 1; i >= 0; i--)
            {
                if (needHide)
                {
                    _forms[i].Hide(UIFormHideFlag.HideByOtherForm, false);
                }
                else
                {
                    _forms[i].Appear(UIFormHideFlag.HideByOtherForm, false);
                }

                if (!needHide && !_forms[i].IsHided() && _forms[i].IsHideUnderForms)
                {
                    needHide = true;
                }
            }
        }

        /// Update Form 输入响应
        private void UpdateFormRaycaster()
        {
            bool respondInput = true;
            //从后开始
            for (int i = _forms.Count - 1; i >= 0; i--)
            {
                if (_forms[i].IsDisableInput || _forms[i].IsHided())
                {
                    continue;
                }

                GraphicRaycaster graphicRaycaster = _forms[i].GetGraphicRaycaster();
                if (graphicRaycaster != null)
                {
                    graphicRaycaster.enabled = respondInput;
                }

                if (_forms[i].IsModal && respondInput)
                {
                    respondInput = false;
                }
            }
        }

        #endregion

        #region 辅助
        /// 是否存在UI页面
        public bool HasForm()
        {
            return (_forms.Count > 0);
        }

        /// 获取UI页面
        public UIForm GetForm(string formPath)
        {
            for (int i = 0; i < _forms.Count; i++)
            {
                if (_forms[i].FormPath.Equals(formPath) && !_forms[i].IsNeedClose() && !_forms[i].IsClosed())
                {
                    return _forms[i];
                }
            }
            return null;
        }

        /// 获取UI页面
        public UIForm GetForm(int formSequence)
        {
            for (int i = 0; i < _forms.Count; i++)
            {
                if (_forms[i].GetSequence() == formSequence && !_forms[i].IsNeedClose() && !_forms[i].IsClosed())
                {
                    return _forms[i];
                }
            }
            return null;
        }

        /// 返回位于最顶层的Form
        public UIForm GetTopForm()
        {
            UIForm topFormScript = null;
            for (int i = 0; i < _forms.Count; i++)
            {
                if (_forms[i] == null)
                {
                    continue;
                }

                if (topFormScript == null)
                {
                    topFormScript = _forms[i];
                }
                else if (_forms[i].GetSortingOrder() > topFormScript.GetSortingOrder())
                {
                    topFormScript = _forms[i];
                }
            }
            return topFormScript;
        }

        public JWObjList<UIForm> GetForms()
        {
            return _forms;
        }
        #endregion
    }
}

