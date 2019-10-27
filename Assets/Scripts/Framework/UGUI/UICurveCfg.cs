/********************************************************************
	created:	2018-8-15
	author:		jordenwu
	
	purpose:	UI动画曲线 配置
*********************************************************************/
using System;
using UnityEngine;

namespace JW.Framework.UGUI
{
    public class UICurveCfg : MonoBehaviour
    {
        [Serializable]
        public class CurveData
        {
            public string Name;
            public AnimationCurve Curve;
            //public AnimationCurve HyperbolaCurve;
        }

        public CurveData[] CurveDatas;

        public AnimationCurve GetCurve(string curveName)
        {
            if (string.IsNullOrEmpty(curveName))
            {
                return null;
            }

            for (int i = 0; i < CurveDatas.Length; i++)
            {
                if (string.Compare(CurveDatas[i].Name, curveName, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return CurveDatas[i].Curve;
                }
            }

            return null;
        }

        //public AnimationCurve GetHyperbolaCurve(string curveName)
        //{
        //    if (string.IsNullOrEmpty(curveName))
        //    {
        //        return null;
        //    }

        //    for (int i = 0; i < CurveDatas.Length; i++)
        //    {
        //        if (string.Compare(CurveDatas[i].Name, curveName, StringComparison.OrdinalIgnoreCase) == 0)
        //        {
        //            return CurveDatas[i].HyperbolaCurve;
        //        }
        //    }

        //    return null;
        //}
    }
}
