/********************************************************************
	created:	2018-7-26
	author:		jordenwu
	
	purpose:	UI事件监听
*********************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using JW.Framework.Audio;

namespace JW.Framework.UGUI
{
    //事件
    public class UIListenerEvent:UnityEvent<PointerEventData>{

    }

    public class UIEventListener : UIComponent, IPointerDownHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerUpHandler, IPointerEnterHandler,IPointerExitHandler
    {
        [Header("拖动传递目标")]
        public UIEventListener DragNext = null;
        [Header("按下音效")]
        public string OnDownAudio = string.Empty;
        [Header("点击音效")]
        public string OnClickedAudio = string.Empty;
        [Header("使用点击动画")]
        public bool ApplyClickTween = false;

        /// 事件
        [HideInInspector]
        public UIListenerEvent onDown=new UIListenerEvent();
        [HideInInspector]
        public UIListenerEvent onUp=new UIListenerEvent();
        [HideInInspector]
        public UnityEvent onClick=new UnityEvent();
        [HideInInspector]
        public UIListenerEvent onHoldStart=new UIListenerEvent();
        [HideInInspector]
        public UIListenerEvent onHold=new UIListenerEvent();
        [HideInInspector]
        public UIListenerEvent onHoldEnd=new UIListenerEvent();
        [HideInInspector]
        public UIListenerEvent onDragStart=new UIListenerEvent();
        [HideInInspector]
        public UIListenerEvent onDrag=new UIListenerEvent();
        [HideInInspector]
        public UIListenerEvent onDragEnd=new UIListenerEvent();
        [HideInInspector]
        public UIListenerEvent onDrop=new UIListenerEvent();
        [HideInInspector]
        public UIListenerEvent onPointerEnter = new UIListenerEvent();
        [HideInInspector]
        public UIListenerEvent onPointerExit = new UIListenerEvent();

        //操作标志
        private bool _isDown = false;
        private bool _isHold = false;
        private bool _canClick = false;

        //相关伐值及参数
        private const float _holdTimeValue = 0.35f;
        private const float _clickAreaValue = 40f;
        private float _downTimestamp = 0;
        private Vector2 _downPosition;

        //hold需要用到的Pointer事件参数
        private PointerEventData _holdPointerEventData;
        //是否需要clear输入状态
        private bool _needClearInputStatus = false;

        /// 初始化
        public override void Initialize(UIForm formScript)
        {
            if (_isInited)
            {
                return;
            }
            base.Initialize(formScript);
        }

        private bool IsPointerOnUI()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
#else
             if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))  
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
#endif
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            _isDown = true;
            _isHold = false;
            _canClick = true;
            _downTimestamp = Time.realtimeSinceStartup;
            _downPosition = eventData.position;
            _holdPointerEventData = eventData;

            _needClearInputStatus = false;
            //派发事件
            if (onDown!=null)
            {
                onDown.Invoke(eventData);
            }

            if (DragNext != null)
            {
                DragNext.OnPointerDown(eventData);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //处理holdend
            if (_isHold && _holdPointerEventData != null)
            {
                if (onHoldEnd!=null)
                {
                    onHoldEnd.Invoke(_holdPointerEventData);
                }
            }

            //派发事件
            if (onUp != null)
            {
                onUp.Invoke(eventData);
            }
            //clear输入状态
            ClearInputStatus();
            //
            if (DragNext != null)
            {
                DragNext.OnPointerUp(eventData);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_canClick)
            {
                //如果是作为List元素，执行List选中操作
                if (BelongedListView != null && IndexInListView >= 0)
                {
                    BelongedListView.SelectElement(IndexInListView);
                }

                if (onClick != null)
                {
                    onClick.Invoke();
                }
                if (!string.IsNullOrEmpty(OnClickedAudio))
                {
                    AudioService.GetInstance().Play((uint)AudioService.AudioChannelType.ACT_EF, OnClickedAudio, false);
                }
                //动画
                if (ApplyClickTween)
                {
                    UICurveTween tt = gameObject.GetComponent<UICurveTween>();
                    if (tt == null)
                    {
                        tt = gameObject.AddComponent<UICurveTween>();
                    }
                    tt.Play();
                }
                //
            }
            //clear输入状态
            ClearInputStatus();
        }

        //mouse over
        public void OnPointerEnter(PointerEventData eventData)
        {
          //派发事件
            if (onPointerEnter!=null)
            {
                onPointerEnter.Invoke(eventData);
            }

            if (DragNext != null)
            {
                DragNext.OnPointerEnter(eventData);
            }
        }

        //mouse leave
        public void OnPointerExit(PointerEventData eventData)
        {
             //派发事件
            if (onPointerExit!=null)
            {
                onPointerExit.Invoke(eventData);
            }

            if (DragNext != null)
            {
                DragNext.OnPointerExit(eventData);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //Drag值超出伐值，不能再响应click
            if (_canClick && BelongedForm != null && BelongedForm.ChangeScreenValueToForm(Vector2.Distance(eventData.position, _downPosition)) > _clickAreaValue)
            {
                _canClick = false;
            }
            //Drag值超出伐值，不能再响应click
            if (Vector2.SqrMagnitude(eventData.position - _downPosition) > 1200)
            {
                _canClick = false;
            }

            //派发事件
            if (onDragStart != null)
            {
                onDragStart.Invoke(eventData);
            }

            //冒泡
            if (BelongedListView != null && BelongedListView.m_scrollRect != null)
            {
                BelongedListView.m_scrollRect.OnBeginDrag(eventData);
            }
            if (DragNext != null)
            {
                DragNext.OnBeginDrag(eventData);
            }

        }

        public void OnDrag(PointerEventData eventData)
        {
            //Drag值超出伐值，不能再响应click
            if (_canClick && BelongedForm != null && BelongedForm.ChangeScreenValueToForm(Vector2.Distance(eventData.position, _downPosition)) > _clickAreaValue)
            {
                _canClick = false;
            }
            //Drag值超出伐值，不能再响应click
            if (Vector2.SqrMagnitude(eventData.position - _downPosition) > 1200)
            {
                _canClick = false;
            }

            //派发事件
            if (onDrag != null)
            {
                onDrag.Invoke(eventData);
            }
            //冒泡
            if (BelongedListView != null && BelongedListView.m_scrollRect != null)
            {
                BelongedListView.m_scrollRect.OnDrag(eventData);
            }
            //
            if (DragNext != null)
            {
                DragNext.OnDrag(eventData);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //Drag值超出伐值，不能再响应click
            if (_canClick && BelongedForm != null && BelongedForm.ChangeScreenValueToForm(Vector2.Distance(eventData.position, _downPosition)) > _clickAreaValue)
            {
                _canClick = false;
            }

            //派发事件
            if (onDragEnd!=null)
            {
                onDragEnd.Invoke(eventData);
            }
            //冒泡
            if (BelongedListView != null && BelongedListView.m_scrollRect != null)
            {
                BelongedListView.m_scrollRect.OnEndDrag(eventData);
            }
            if (DragNext != null)
            {
                DragNext.OnEndDrag(eventData);
            }
            //clear输入状态
            ClearInputStatus();
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (onDrop != null)
            {
                onDrop.Invoke(eventData);
            }
        }

        /// Clear输入状态
        public bool ClearInputStatus()
        {
            _needClearInputStatus = true;
            return _isDown;
        }

        /// 执行Clear输入状态
        public void ExecuteInputStatus()
        {
            _isDown = false;
            _isHold = false;
            _canClick = false;
            _downTimestamp = 0;
            _downPosition = Vector2.zero;
            _holdPointerEventData = null;
        }


        void Update()
        {
            if (_needClearInputStatus)
            {
                return;
            }

            if (_isDown)
            {
                if (!_isHold)
                {
                    if (Time.realtimeSinceStartup - _downTimestamp >= _holdTimeValue)
                    {
                        _isHold = true;
                        _canClick = false;
                        if (onHoldStart!=null)
                        {
                            onHoldStart.Invoke(_holdPointerEventData);
                        }
                    }
                }
                else
                {
                    if (onHold!=null)
                    {
                        onHold.Invoke(_holdPointerEventData);
                    }
                }
            }
        }

        void LateUpdate()
        {
            if (_needClearInputStatus)
            {
                ExecuteInputStatus();
                _needClearInputStatus = false;
            }
        }

    }
}
