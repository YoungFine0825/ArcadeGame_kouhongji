/********************************************************************
	created:	2018-06-14
	filename: 	UIComponent
	author:		jordenwu
	
	purpose:	UI组件定义
*********************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JW.Common;

namespace JW.Framework.UGUI
{
    public class UIComponent : MonoBehaviour
    {
        //所属窗口
        [HideInInspector]
        public UIForm BelongedForm;
        //所属滚动视图
        [HideInInspector]
        public UIListView BelongedListView;
        [HideInInspector]
        public int IndexInListView;

        //标记
        protected bool  _isInited= false;

        //初始化
        public virtual void Initialize(UIForm form)
        {
            if (_isInited)
            {
                return;
            }
            BelongedForm = form;
            //初始化sortingOrder
            SetSortingOrder(form.GetSortingOrder());
            _isInited = true;
        }

        void OnDestroy()
        {
            BelongedForm = null;
        }

        //关闭
        public virtual void OnClose()
        {

        }

        //隐藏
        public virtual void OnHide()
        {

        }

        //显示
        public virtual void OnAppear()
        {

        }

        //设置SortingOrder
        public virtual void SetSortingOrder(int sortingOrder)
        {

        }

        /// 设置所属ListView控件脚本及索引
        public void SetBelongedList(UIListView belongedListScript, int index)
        {
            BelongedListView = belongedListScript;
            IndexInListView = index;
            //
            SetSortingOrder(BelongedListView.BelongedForm.GetSortingOrder());
        }


        /// 遍历初始化UI组件
        protected void InitializeComponent(GameObject root)
        {
            UIComponent[] uiComponents = root.GetComponents<UIComponent>();

            if (uiComponents != null && uiComponents.Length > 0)
            {
                for (int i = 0; i < uiComponents.Length; i++)
                {
                    //JW.Common.Log.LogD("-------------" + uiComponents[i].gameObject.name);
                    uiComponents[i].Initialize(BelongedForm);
                }
            }

            for (int i = 0; i < root.transform.childCount; i++)
            {
                InitializeComponent(root.transform.GetChild(i).gameObject);
            }
        }

    }
}
