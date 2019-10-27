using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JW.Framework.UGUI
{
    public class UIDepthCtr : UIComponent
    {
        [Header("UI 模式")]
        public bool IsUI = true;

        [Header("排序偏移")]
        public int SortOffset = 0;

        //初始化
        public override void Initialize(UIForm form)
        {
            if (_isInited)
            {
                //return;
            }
            base.Initialize(form);
            _isInited = true;
        }

        //关闭
        public override void OnClose()
        {

        }

        //隐藏
        public override void OnHide()
        {

        }

        //显示
        public override void OnAppear()
        {

        }

        public void OnEnable()
        {
            if (BelongedForm != null)
            {
                SetSortingOrder(BelongedForm.GetSortingOrder());
            }
        }

        //设置SortingOrder
        public override void SetSortingOrder(int sortingOrder)
        {
            if (IsUI)
            {
                Canvas canvas = GetComponent<Canvas>();
                if (canvas == null)
                {
                    canvas = gameObject.AddComponent<Canvas>();
                }
                if (canvas != null)
                {
                    canvas.overrideSorting = true;
                    canvas.sortingOrder = sortingOrder + SortOffset;
                }
                GraphicRaycaster gr = GetComponent<GraphicRaycaster>();
                if (gr == null)
                {
                    gr = gameObject.AddComponent<GraphicRaycaster>();
                }
            }
            else
            {
                Renderer[] renders = GetComponentsInChildren<Renderer>();
                foreach (Renderer render in renders)
                {
                    render.sortingOrder = sortingOrder + SortOffset;
                }
            }
        }

    }
}
