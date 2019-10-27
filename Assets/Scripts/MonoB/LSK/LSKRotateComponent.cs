/* ============================================================================== 
* 功能描述：口红机旋转组件 
* 创 建 者：XH 
* 创建日期：2018/11/6 9:40:32 
* ==============================================================================*/

using System.Collections.Generic;
using UnityEngine;
using XLua;
using DG.Tweening;

namespace XH.Pin
{
    public class LSKRotateComponent : MonoBehaviour
    {
        public struct NumberArea
        {
            public float Min;
            public float Max;
        }

        public enum WheelState
        {
            None = -1,
            Rotate = 0,
            Trick = 1,
            Stop = 2,
        }
        public enum TrickMode
        {
            None = -1,
            Back = 1,
            Pass = 2,
        }
        public System.Action OnTrickOver;
        [SerializeField] private float _speed;

        private WheelState _state = WheelState.None;
        private float _accSpeed;// 加速度
        private float _changeDuration = 2f;
        private List<NumberArea> _initSpeedCfg = new List<NumberArea>();
        private List<NumberArea> _changeSpeedCfg = new List<NumberArea>();
        private List<NumberArea> _durationCfg = new List<NumberArea>();

        private float _radius = 0f;

        private TrickMode _trickMode = TrickMode.None;
        private float _trickMin = 0;
        private float _trickMax = 0;
        private float _trickSpeed = 0;
        private bool _tricked = false;
        private bool _trickIn = false;
        private float _trickElapsedTime = 0;
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            CapsuleCollider collider = transform.GetComponent<CapsuleCollider>();
            if(collider != null)
            {
                _radius = collider.radius;
            }
        }
        public void StartRotate()
        {
            ReCaculateSpeed();
            _state = WheelState.Rotate;
        }

        public float GetRadius()
        {
            return _radius;
        }
        public void StopRotate()
        {
            _state = WheelState.Stop;
        }

        private void PrintList(string name,List<NumberArea> list)
        {
            JW.Common.Log.LogD("---------Print " + name + " Begin----------------");
            foreach (var area in list)
            {
                JW.Common.Log.LogD(string.Format("Min = {0},Max = {1}", area.Min, area.Max));
            }
        }
        public void InitData(LuaTable table)
        {
            _initSpeedCfg = table.Get<List<NumberArea>>("InitSpeed");
            _changeSpeedCfg = table.Get<List<NumberArea>>("ChangeSpeed");
            _durationCfg = table.Get<List<NumberArea>>("ChangeDuration");

            int initCnt = _initSpeedCfg.Count;
            if (initCnt > 0)
            {
                NumberArea area = _initSpeedCfg[0];
                if(initCnt > 1)
                {
                    int idx = Random.Range(0, initCnt - 1);
                    area = _initSpeedCfg[idx];
                }
                _speed = Random.Range(area.Min, area.Max);
            }
        }


        private void FixedUpdate()
        {
            switch(_state)
            {
                case WheelState.Rotate:
                    {
                        RotateUpdate();
                        break;
                    }
                case WheelState.Trick:
                    {
                        TrickUpdate();
                        break;
                    }
            }
        }

        private void RotateUpdate()
        {
            if (_changeDuration <= 0)
            {
                ReCaculateSpeed();
            }
            if (_state == WheelState.Rotate)
            {
                _speed = _speed + (_accSpeed * Time.fixedDeltaTime);
                transform.Rotate(0, 0, _speed * Time.fixedDeltaTime);
            }
            _changeDuration -= Time.fixedDeltaTime;
        }

        public void ReCaculateSpeed()
        {
            int changeDurationCnt = _durationCfg.Count;
            if (changeDurationCnt > 0)
            {
                NumberArea area = _durationCfg[0];
                if (changeDurationCnt > 1)
                {
                    int idx = Random.Range(0, changeDurationCnt);
                    area = _durationCfg[idx];
                }
                _changeDuration = Random.Range(area.Min, area.Max);
            }
            int changeSpeedCnt = _changeSpeedCfg.Count;
            if (changeSpeedCnt > 0)
            {
                NumberArea area = _changeSpeedCfg[0];
                if (changeSpeedCnt > 1)
                {
                    int idx = Random.Range(0, changeSpeedCnt);
                    
                    area = _changeSpeedCfg[idx];

                }
                float endSpeed = Random.Range(area.Min, area.Max);
                _accSpeed = (endSpeed - _speed) / _changeDuration;
            }
        }

        public float GetSpeed()
        {
            return _speed;
        }

        public void Trick(int trickMode, float minAngle, float maxAngle)
        {
            if(_state == WheelState.Trick)
            {
                return;
            }
            _state = WheelState.Trick;
            _trickMode = (TrickMode)trickMode;
            _trickMin = minAngle;
            _trickMax = maxAngle;
            _tricked = false;
            _trickIn = false;

            switch (_trickMode)
            {
                case TrickMode.Back:
                    {
                        float sign = _speed > 0 ? 1 : -1;
                        float offset = 10;
                        minAngle -= offset;
                        maxAngle -= offset;
                        _speed = Mathf.Max(150, Mathf.Abs(_speed))*sign;
                        float duration = Random.Range(0.15f, 0.8f);
                        _accSpeed = (0 - _speed) /duration;
                        _trickIn = true;
                        //Debug.Log("Trick Back Begin AccSpeed = " + _accSpeed.ToString() + ", _speed = " + _speed.ToString());
                        break;
                    }

                case TrickMode.Pass:
                    {
                        float sign = (_speed >= 0 ? 1 : -1);
                        _accSpeed = 0;
                        // 保证此刻速度足够
                        if (Mathf.Abs(_speed)<=100)
                        {
                            _speed = 150 * sign;
                        }
                        //逆时针旋转
                        // 计算快速通过时间
                        float passDuration = 0.15f;//150ms
                        _trickSpeed = sign*Mathf.Abs(_trickMax - _trickMin) / passDuration;
                        _trickSpeed = sign * Mathf.Max(100, Mathf.Abs(_trickSpeed));
                        Debug.Log("Trick Pass Begin : Speed = " + _trickSpeed.ToString());
                        break;
                    }
            }
        }

        private void TrickUpdate()
        {
            if(_trickMode == TrickMode.None)
            {
                return;
            }
            float angleZ = transform.localEulerAngles.z;
            bool isClockwise = _speed < 0;
            bool inArea = _trickMax > 360 ? ((angleZ >= _trickMin && angleZ <= 360)||(angleZ >= 0 && angleZ <= (_trickMax-360))) :
                                            (angleZ >= _trickMin && angleZ <= _trickMax);
            if (_trickMode == TrickMode.Back)
            {
                if(_trickIn)
                {
                    //_speed *= 0.85f;
                }
                else
                {
                    _speed = _speed + _accSpeed * Time.fixedDeltaTime;

                }
                if (inArea)
                {
                    if(_trickIn)
                    {//开始反向
                        float sign = _speed > 0 ? 1 : -1;
                        sign *= -1;
                        _speed = 0;
                        _trickSpeed = Random.Range(100, 200) * sign;
                        float duration = Random.Range(0.35f, 0.8f);
                        _accSpeed = (_trickSpeed - _speed)/duration;
                        _trickIn = false;
                        _tricked = true;
                        // Debug.Log("Trick Back Back  _speed = " + _speed.ToString());
                    }
                }
                if(_tricked)
                {
                    if(Mathf.Abs(_speed) >= Mathf.Abs(_trickSpeed))
                    {
                        _state = WheelState.Rotate;
                        // Debug.Log("Trick Back End  _speed = " + _speed.ToString());
                        ReCaculateSpeed();
                        _tricked = false;
                        // Debug.Log("Trick B to Normal  _speed = " + _speed.ToString());
                        if(OnTrickOver != null)
                        {
                            OnTrickOver.Invoke();
                        }
                    }
                }
            }
            else
            {   //快速通过
                if (inArea)
                {
                    _speed = _trickSpeed;
                    _tricked = true;
                }
                else
                {
                    if (_tricked)
                    {
                        Debug.Log("Trick Pass End  _speed = " + _speed.ToString());
                        _state = WheelState.Rotate;
                        _tricked = false;
                        float sign = _speed > 0 ? 1 : -1;
                        int changeSpeedCnt = _changeSpeedCfg.Count;
                        if (changeSpeedCnt > 0)
                        {
                            NumberArea area = _changeSpeedCfg[0];
                            if (changeSpeedCnt > 1)
                            {
                                int idx = Random.Range(0, changeSpeedCnt);
                                
                                area = _changeSpeedCfg[idx];

                            }
                            _speed = sign*Mathf.Abs(Random.Range(area.Min, area.Max));
                        }
                        ReCaculateSpeed();

                        Debug.Log("Trick Pass To Normal  _speed = " + _speed.ToString());
                        if (OnTrickOver != null)
                        {
                            OnTrickOver.Invoke();
                        }
                    }
                }
            }
            transform.Rotate(0, 0, _speed * Time.fixedDeltaTime);
        }
    }
}