using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JW.Common;
using JW.Framework.Asset;

namespace JW.Framework.MVC
{
    //外部禁止随意调用
    public static class UIFormHelper
    {
        public static T CreateFormClass<T>(bool pool, UIMediator mediator, int customID = 0, object parameter = null) where T : UIFormClass, new()
        {
            T prefabClass = (T)AssetService.GetInstance().LoadFormAsset<T>(pool ? LifeType.UIState : LifeType.Immediate);
            if (prefabClass == null)
            {
                return null;
            }
            prefabClass.Create(mediator, customID, parameter);
            return prefabClass;
        }

        public static void DisposeFormClass<T>(ref T prefabClass) where T : UIFormClass
        {
            DisposeFormClass(prefabClass);
            prefabClass = null;
        }

        public static void DisposeFormClass(UIFormClass prefabClass)
        {
            if (null == prefabClass)
            {
                return;
            }
            prefabClass.Destroy();
        }

        public static T CreateResidentFormClass<T>( ) where T : UIFormClass, new()
        {
            T prefabClass = (T)AssetService.GetInstance().LoadFormAsset<T>(LifeType.Resident);
            if (prefabClass == null)
            {
                return null;
            }
            prefabClass.Create(null, 0, null);
            return prefabClass;
        }


    }
}
