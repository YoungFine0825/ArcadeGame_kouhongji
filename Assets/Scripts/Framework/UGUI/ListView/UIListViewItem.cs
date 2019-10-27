/********************************************************************
	created:	2018-7-31
	author:		jordenwu
	
	purpose:	列表Item
*********************************************************************/
using UnityEngine;
using UnityEngine.UI;
using JW.Common;
using UnityEngine.EventSystems;
using JW.PLink;

namespace JW.Framework.UGUI
{
    //自定义rect
    public struct stRect
    {
        //宽高
        public int m_width;
        public int m_height;
        //坐标
        public int m_top;
        public int m_bottom;
        public int m_left;
        public int m_right;
        public Vector2 m_center;
    };

    [RequireComponent(typeof(JW.PLink.PrefabLink))]
    public class UIListViewItem : UIComponent
    {
        [Header("选中显示对象")]
        public GameObject m_selectFrontObj;

        [Header("选中显示精灵")]
        public Sprite m_selectedSprite;

        [Header("使用点击选中动画")]
        public bool ApplyClickSelTween;

        //原始背景Sprite
        [HideInInspector]
        public Sprite m_defaultSprite;

        //原始背景的color值
        [HideInInspector]
        public Color m_defaultColor;

        //原始的文本color值
        [HideInInspector]
        public Color m_defaultTextColor;

        //文本对象
        [Header("选中显示文本")]
        public Text m_textObj;
        //选中后的文本color值
        [Header("选中显示文本颜色")]
        public Color m_selectTextColor = new Color(1, 1, 1, 1);

        //默认尺寸
        [HideInInspector]
        public Vector2 m_defaultSize;

        //索引
        [HideInInspector]
        public int m_index;

        //用于显示的Image
        private Image m_image;

        //在Content上面的区域
        public stRect m_rect;

        //使用SetActive()还是CanvasGroup来显示或隐藏list element
        public bool m_useSetActiveForDisplay = false;

        //是否自动加上UIEventListenerScript
        public bool m_autoAddUIEventScript = true;

        //Canvas Group
        private CanvasGroup m_canvasGroup;
        //
        public UIListView m_belongedListScript;

        //
        public PrefabLink PLink;

        /// 初始化
        public override void Initialize(UIForm formScript)
        {
            if (_isInited)
            {
                return;
            }

            base.Initialize(formScript);
            //
            PLink = gameObject.GetComponent<PrefabLink>();
            //
            m_image = gameObject.GetComponent<Image>();
            if (m_image != null)
            {
                m_defaultSprite = m_image.sprite;
                m_defaultColor = m_image.color;
            }

            //如果Element不包含 增加一个以便于接收选中事件
            if (m_autoAddUIEventScript)
            {
                UIEventListener eventScript = gameObject.GetComponent<UIEventListener>();
                if (eventScript == null)
                {
                    eventScript = gameObject.AddComponent<UIEventListener>();
                    eventScript.Initialize(formScript);
                }
                if (ApplyClickSelTween)
                {
                    eventScript.ApplyClickTween = true;
                }
            }

            //如果Element不包含CanvasGroup，增加一个以便于隐藏/显示
            if (!m_useSetActiveForDisplay)
            {
                m_canvasGroup = gameObject.GetComponent<CanvasGroup>();
                if (m_canvasGroup == null)
                {
                    m_canvasGroup = gameObject.AddComponent<CanvasGroup>();
                }
            }
            //
            m_defaultSize = GetDefaultSize();

            //初始化RectTransform
            InitRectTransform();
            //
            if(m_textObj != null)
            {
                m_defaultTextColor = m_textObj.color;
            }
        }

        /// 获取默认尺寸
        protected virtual Vector2 GetDefaultSize()
        {
            return (new Vector2(((RectTransform)this.gameObject.transform).rect.width, ((RectTransform)this.gameObject.transform).rect.height));
        }

        /// Enable元素
        public void Enable(UIListView belongedList, int index, string name, ref stRect rect, bool selected)
        {
            m_belongedListScript = belongedList;
            m_index = index;
            //
            gameObject.name = name + "_" + index.ToString();

            if (m_useSetActiveForDisplay)
            {
                gameObject.ExtSetActive(true);
            }
            else
            {
                m_canvasGroup.alpha = 1f;
                m_canvasGroup.blocksRaycasts = true;
            }
            //递归设置从属List
            SetComponentBelongedList(gameObject);
            //设置位置属性
            SetRect(ref rect);
            //设置选中/非选中外观
            ChangeDisplay(selected);
        }

        /// 禁用元素
        public void Disable()
        {
            if (m_useSetActiveForDisplay)
            {
                gameObject.ExtSetActive(false);
            }
            else
            {
                m_canvasGroup.alpha = 0f;
                m_canvasGroup.blocksRaycasts = false;
            }
        }

        /// 被选中时回调
        /// 此函数会被注册给element及其所有子元素
        public void OnSelected(BaseEventData baseEventData)
        {
            m_belongedListScript.SelectElement(m_index);
        }

        /// 改变显示(选中/非选中)
        public virtual void ChangeDisplay(bool selected)
        {
            //处理背景选择图案
            if (m_image != null && m_selectedSprite != null)
            {
                if (selected)
                {
                    m_image.sprite = m_selectedSprite;
                    m_image.color = new Color(m_defaultColor.r, m_defaultColor.g, m_defaultColor.b, 255.0f);
                }
                else
                {
                    m_image.sprite = m_defaultSprite;
                    m_image.color = m_defaultColor;
                }
            }

            if (m_selectFrontObj != null)
            {
                m_selectFrontObj.ExtSetActive(selected);
            }

            if (m_textObj != null)
            {
                m_textObj.color = selected ? m_selectTextColor : m_defaultTextColor;
            }
        }

        /// 遍历并设置从属List
        /// @gameObject
        //--------------------------------------------------
        public void SetComponentBelongedList(GameObject gameObject)
        {
            //为UIComponent设置所属List
            UIComponent[] componentScripts = gameObject.GetComponents<UIComponent>();
            if (componentScripts != null && componentScripts.Length > 0)
            {
                for (int i = 0; i < componentScripts.Length; i++)
                {
                    componentScripts[i].SetBelongedList(m_belongedListScript, m_index);
                }
            }

            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                SetComponentBelongedList(gameObject.transform.GetChild(i).gameObject);
            }
        }

        /// 设置在Content上的位置
        public void SetRect(ref stRect rect)
        {
            m_rect = rect;
            RectTransform rectTransform = gameObject.transform as RectTransform;
            rectTransform.sizeDelta = new Vector2(m_rect.m_width, m_rect.m_height);
            rectTransform.anchoredPosition = new Vector2(rect.m_left, rect.m_top);
            rectTransform.anchoredPosition3D = new Vector3(rect.m_left, rect.m_top, 0);
        }

        /// 初始化Rect
        /// @index
        private void InitRectTransform()
        {
            //设置锚点和枢轴点均为Top-Left
            RectTransform rectTransform = gameObject.transform as RectTransform;
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(0, 1);
            rectTransform.pivot = new Vector2(0, 1);
            rectTransform.sizeDelta = m_defaultSize;

            rectTransform.localRotation = Quaternion.identity;
            rectTransform.localScale = new Vector3(1, 1, 1);
        }
    };
};