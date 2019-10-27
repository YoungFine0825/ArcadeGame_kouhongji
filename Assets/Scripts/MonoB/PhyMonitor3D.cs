/********************************************************************
	created:	2018-11-14
	author:		jordenwu
	
	purpose:	物理监视器
*********************************************************************/
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PhyMonitor3D : MonoBehaviour
{

    //回调类型
    private enum PhyMonitor3DCallType
    {
        None = 0,
        JustEnter = 1,
        JustExist = 2,
        JustEnterAndExist = 3,
        All = 3,
    }

    //物理监视委托
    public delegate void PhyMonitor3DDelegate(int eventType, GameObject go);
    //
    //处理
    private PhyMonitor3DCallType _callType = PhyMonitor3DCallType.None;
    private PhyMonitor3DDelegate _handler;

    //标记取进入的刚体 GameObject 
    public bool RigidBodyTriggerMode = true;
    //当前触发器进入的对象列表
    private List<GameObject> _curEnterGos = new List<GameObject>(3);

    private string _goName;

    // Use this for initialization
    void Start()
    {
        _goName = gameObject.name;
        _curEnterGos.Clear();
    }

    private void OnDestroy()
    {
        _handler = null;
        if (_curEnterGos != null)
        {
            _curEnterGos.Clear();
            _curEnterGos = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_callType == PhyMonitor3DCallType.None)
        {
            return;
        }
        if (_handler != null && (_callType == PhyMonitor3DCallType.All || _callType == PhyMonitor3DCallType.JustEnter
            || _callType == PhyMonitor3DCallType.JustEnterAndExist))
        {
            _handler(1, collision.gameObject);
        }
        JW.Common.Log.LogD(_goName + ":OnCollisionEnter:" + collision.gameObject.name);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (_callType == PhyMonitor3DCallType.None)
        {
            return;
        }
        if (_handler != null && _callType == PhyMonitor3DCallType.All)
        {
            _handler(2, collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (_callType == PhyMonitor3DCallType.None)
        {
            return;
        }
        if (_handler != null && (_callType == PhyMonitor3DCallType.All || _callType == PhyMonitor3DCallType.JustExist
            || _callType == PhyMonitor3DCallType.JustEnterAndExist))
        {
            _handler(3, collision.gameObject);
        }
        JW.Common.Log.LogD(_goName + ":OnCollisionExit:" + collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject realGo = FilterTriggerInCollider(other);
        if (realGo == null)
        {
            return;
        }
        //判断是否添加过 
        if (_curEnterGos.Contains(realGo) == false)
        {
            _curEnterGos.Add(realGo);
            JW.Common.Log.LogD(_goName + ":OnTriggerEnter:" + realGo.name);

            if (_handler != null && (_callType == PhyMonitor3DCallType.All || _callType == PhyMonitor3DCallType.JustEnter
          || _callType == PhyMonitor3DCallType.JustEnterAndExist))
            {
                _handler(4, realGo);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_callType != PhyMonitor3DCallType.All)
        {
            return;
        }
        GameObject realGo = FilterTriggerInCollider(other);
        if (realGo == null)
        {
            return;
        }
        if (_curEnterGos.IndexOf(realGo) == -1)
        {
            JW.Common.Log.LogE(_goName + ":PhyMonitor Error No Enter But Stay ?");
        }
        if (_handler != null && _callType == PhyMonitor3DCallType.All)
        {
            _handler(5, realGo);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject realGo = FilterTriggerInCollider(other);
        if (realGo == null)
        {
            return;
        }
        //清理 
        if (_curEnterGos != null)
        {
            //过滤
            if (_curEnterGos.IndexOf(realGo) == -1)
            {
                //JW.Common.Log.LogD(_goName + ":OnTriggerExit:" + realGo.name);
            }
            else
            {
                JW.Common.Log.LogD(_goName + ":OnTriggerExit:" + realGo.name);
                _curEnterGos.Remove(realGo);
                //
                if (_handler != null && (_callType == PhyMonitor3DCallType.All || _callType == PhyMonitor3DCallType.JustExist
                    || _callType == PhyMonitor3DCallType.JustEnterAndExist))
                {
                    _handler(6, realGo);
                }
            }
        }
    }


    private GameObject FilterTriggerInCollider(Collider other)
    {
        if (RigidBodyTriggerMode == false)
        {
            return other.gameObject;
        }
        if (other.attachedRigidbody != null)
        {
            return other.attachedRigidbody.gameObject;
        }
        //从父亲找
        Transform pp = other.transform.parent;
        while (pp != null)
        {
            Rigidbody bb = pp.gameObject.GetComponent<Rigidbody>();
            if (bb != null)
            {
                return pp.gameObject;
            }
            else
            {
                pp = pp.parent;
            }
        }
        //
        return null;
    }

    //获取当前进入的对象个数
    public int GetEnteredGameObjectCnt()
    {
        if (_curEnterGos != null)
        {
            return _curEnterGos.Count;
        }
        return 0;
    }

    //获取当前进入的第一个对象
    public GameObject GetEnteredFirstGameObject()
    {
        if (_curEnterGos != null && _curEnterGos.Count > 0)
        {
            return _curEnterGos[0];
        }
        return null;
    }

    public void InitHandler(int callType, PhyMonitor3DDelegate handler)
    {
        _callType = (PhyMonitor3DCallType)callType;
        _handler = handler;

    }
    public void CleanHandler()
    {
        _handler = null;
    }

    public void Reset()
    {
        JW.Common.Log.LogD(_goName + "Reset:");
        if (_curEnterGos != null)
        {
            _curEnterGos.Clear();
        }
    }
}
