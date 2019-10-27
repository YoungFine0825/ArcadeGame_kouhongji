/********************************************************************
	created:	2018-11-12
	author:		jordenwu
	
	purpose:	物理关节同步显示组件
*********************************************************************/
using UnityEngine;

public class PhyJointSync : MonoBehaviour {

    /// <summary>
    /// 同步目标模式
    /// </summary>
    public enum SyncTargetMode
    {
        SyncPos,
        SyncRotateAll,
        SyncRotateX,
        SyncRotateY,
        SyncRotateZ,
    }

    /// <summary>
    /// 映射到自己控制模式
    /// </summary>
    public enum LocalMapMode
    {
        MapPosAll,
        MapPosX,
        MapPosY,
        MapPosZ,
        MapRotateAll,
        MapRotateX,
        MapRotateY,
        MapRotateZ,
    }

    //同步目标
    public Transform SyncTargetTrans;
    //同步模式
    public SyncTargetMode SyncMode=SyncTargetMode.SyncRotateX;
    //映射模式
    public LocalMapMode MapMode= LocalMapMode.MapRotateZ;
    //是否反转
    public bool IsReversal = true;



    private Transform _cachedTrans;

	void Start ()
    {
        _cachedTrans = transform;
        
	}
	
	
	//void Update ()
 //   {
		
	//}

    void LateUpdate()
    {
        if (SyncTargetTrans == null)
        {
            return;
        }
        //同步位置
        if (SyncMode == SyncTargetMode.SyncPos)
        {
            Vector3 vv = SyncTargetTrans.localPosition;
            if (MapMode == LocalMapMode.MapPosAll)
            {
                _cachedTrans.localPosition = vv;
            }
            if (MapMode == LocalMapMode.MapPosX)
            {
                Vector3 ss = _cachedTrans.localPosition;
                ss.x = vv.x;
                _cachedTrans.localPosition =ss;
            }
            if (MapMode == LocalMapMode.MapPosY)
            {
                Vector3 ss = _cachedTrans.localPosition;
                ss.y = vv.y;
                _cachedTrans.localPosition = ss;
            }
            if (MapMode == LocalMapMode.MapPosZ)
            {
                Vector3 ss = _cachedTrans.localPosition;
                ss.z = vv.z;
                _cachedTrans.localPosition = ss;
            }
            return;
        }
        //同步旋转
        if (SyncMode == SyncTargetMode.SyncRotateX || SyncMode == SyncTargetMode.SyncRotateY ||
            SyncMode==SyncTargetMode.SyncRotateZ || SyncMode==SyncTargetMode.SyncRotateAll)
        {

            Vector3 vv = SyncTargetTrans.localEulerAngles;
            //
            if (MapMode == LocalMapMode.MapRotateZ)
            {
                if (SyncMode == SyncTargetMode.SyncRotateX)
                {
                    float tValue = vv.x;
                    if (IsReversal)
                    {
                        tValue = -tValue;
                    }
                    Vector3 ll = _cachedTrans.localEulerAngles;
                    ll.z = tValue;
                    _cachedTrans.localEulerAngles = ll;
                }
                return;
            }
        }
    }


}
