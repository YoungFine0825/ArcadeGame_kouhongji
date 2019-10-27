using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace XH.Pin
{
    [ExecuteInEditMode]
    public class LSKMoveComponent : MonoBehaviour
    {
        public enum MoveState
        {
            None = -1,
            Move = 0,
            Stop = 1,
            ParabolaMove = 2,
            Rotate = 3,
        }
        #region 移动
        private Transform _rootTf;
        private Transform _rotateTf;
        private Vector3 _velocity=new Vector3(0,10,0);
        [SerializeField]private float _speed = 50;
        [SerializeField]private Collider _collider;
        private int _id;
        private MoveState _moveState = MoveState.None;
        public System.Action<float> OnHitRotateTarget;

        public System.Action<int> OnHitMoveTarget;

        public float TopY = 0;
        public int ID { get { return _id; }
                        set { _id = value;
                            transform.name = "Lipstick_"+_id.ToString();
                        }
         }
        public float _centerOffsetY = 0f;
        private Vector3 _half_size;

        private RaycastHit _hit;
        private bool _detect_hit;

        [SerializeField]private BoxCollider _caculateCollider;
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            _rotateTf = transform.GetChild(0);
            //_caculateCollider = _rotateTf.GetComponent<BoxCollider>();
            //_collider = transform.GetComponent<BoxCollider>();

            _rootTf = transform;
            _centerOffsetY = _rotateTf.GetChild(0).localPosition.y;
            _rotateTf.rotation = Quaternion.identity;
            _half_size = _caculateCollider.size;
            _half_size.y /= 2;
            SetColliderEnabled(false);
        }

        public Vector3 Velocity
        {
            get { return _velocity; }
        }

        public void SetSpeed(float x, float y, float z)
        {
            _velocity = new Vector3(x, y, z);
            _moveState = MoveState.None;
        }


        private void FixedUpdate()
        {
            switch(_moveState)
            {
                case MoveState.Move:
                    {
                        // Move
                        Move();
                        RaycastHitDetection();
                        break;
                    }
                case MoveState.ParabolaMove:
                    {
                        ParabolaMove();
                        Rotate();
                        break;
                    }
                case MoveState.Rotate:
                    {
                        break;
                    }
                case MoveState.Stop:
                    {
                        Rotate();
                        break;
                    }
            }
        }
        private void Move()
        {
             _rootTf.position += _velocity * Time.fixedDeltaTime;
        }
        public void Shoot()
        {
            // if (_moveState == MoveState.None)
            // {
                SetColliderEnabled(false);
                _moveState = MoveState.Move;
            // }
        }
        public void Stop()
        {
            _moveState = MoveState.Stop;
            SetColliderEnabled(true);
        }
        // private void OnDrawGizmos()
        // {
        //     // if(_moveState != MoveState.Move)
        //     // {
        //     //     return;
        //     // }
        //     Color origin = Gizmos.color;
        //     Gizmos.color = Color.blue;

        //     if(_detect_hit)
        //     {
        //         Gizmos.color = Color.red;
        //         Gizmos.DrawRay(_rootTf.position, Velocity * _hit.distance);
        //         Gizmos.DrawWireCube(_rootTf.position + Velocity.normalized * _hit.distance, _half_size);
        //     }
        //     else
        //     {
        //         Gizmos.DrawRay(_rootTf.position, Velocity.normalized);
        //         Gizmos.DrawWireCube(_rootTf.position, _half_size);
        //     }
 
            
        //     Gizmos.color = origin;
        // }
        /// <summary>
        /// 射线检测
        /// </summary>
        /// <param name="type">传入参数</param>
        /// <returns>null</returns>
        private void RaycastHitDetection()
        {
            _detect_hit = Physics.BoxCast(_rootTf.position, _half_size, _velocity.normalized, out _hit, Quaternion.identity, 0.5f);
            if(_detect_hit)
            {
                HitTarget();
            }
            else
            {
                if(_rootTf.position.y >= TopY)
                {
                    Stop();
                    if (OnHitRotateTarget != null)
                    {
                        OnHitRotateTarget.Invoke(-1);
                    }
                }
            }
        }
        

        private void HitTarget()
        {
            if(_hit.collider == null)
            {
                return;
            }
            GameObject hitGo = _hit.collider.gameObject;
            Debug.Log("Hit " + _hit.collider.name);
            if (hitGo && hitGo.name == "WheelNode")
            {
                Stop();
                if (OnHitRotateTarget != null)
                {
                    OnHitRotateTarget.Invoke(hitGo.transform.eulerAngles.z);
                }
            }
            else if (hitGo && hitGo.name.Contains("Lipstick"))
            {
                LSKMoveComponent moveCtrl = hitGo.GetComponent<LSKMoveComponent>();
                if (moveCtrl == null)
                {
                    return;
                }
                Stop();
               
                    
                
                if (OnHitMoveTarget != null)
                {
                    OnHitMoveTarget.Invoke(moveCtrl.ID);
                }

            }
            else
            {
                Stop();
                if (OnHitMoveTarget != null)
                {
                    OnHitMoveTarget.Invoke(-1);
                }
                JW.Common.Log.LogE("Hit Error");
            }
        }
        public void AttachToWheel(Transform target, float radius)
        {
            _rootTf.SetParent(target);
            Vector3 wheelNodeAngles = target.localEulerAngles;
            Vector3 originPos = new Vector3(0, -1* (_centerOffsetY + radius), 0);
            Vector3 localPos = Quaternion.Euler(0, 0, -1 * wheelNodeAngles.z) * originPos;
            _rootTf.localPosition = localPos;
            _rootTf.localEulerAngles = wheelNodeAngles * -1f;
            SetColliderEnabled(true);
        }
        private void InitData(int id, float speed)
        {
            _id = id;
            _velocity = new Vector3(0, speed, 0);
        }

        public void SetColliderEnabled(bool isEnable)
        {
            _collider.enabled = isEnable;
        }
        private void Clear()
        {
            OnHitMoveTarget = null;
            OnHitRotateTarget = null;
        }
        #endregion
        #region 抛物线移动代码
        [SerializeField]private float _parabola_duration = 0.25f;
        [SerializeField]private float _gravity = -50;//重力加速度
        [SerializeField] private Vector3 _high_pos_min = new Vector3(-1,-1,-1);//最高点的偏移位置
        [SerializeField] private Vector3 _high_pos_max = new Vector3(1, 1, 1);//最高点的偏移位置
        [SerializeField] private Vector3 _min_angle = new Vector3(-1, -1, -1);//旋转角度最小值
        [SerializeField] private Vector3 _max_angle = new Vector3(1, 1, 1);//旋转角度最大值
        private Vector3 _parabola_speed;//初速度向量
        private Vector3 _gravity_vec;//重力向量
        private float _bottom_y = -20;
        private float _eclapsed_time = 0;
        private Vector3 _rotate_speed;
        /// <summary>
        /// 抛物线运动
        /// </summary>
        private void ParabolaMove()
        {
            _gravity_vec.y = _gravity * (_eclapsed_time += Time.fixedDeltaTime);//v=at
            transform.Translate(_parabola_speed * Time.fixedDeltaTime, Space.World);
            transform.Translate(_gravity_vec * Time.fixedDeltaTime, Space.World);
            if(transform.position.y <= _bottom_y)
            {
                _moveState = MoveState.None;
            }
        }
        /// <summary>
        /// 旋转
        /// </summary>
        private void Rotate()
        {
            _rotateTf.Rotate(_rotate_speed * Time.fixedDeltaTime);
        }

        /// <summary>
        /// 开始做抛物线运动
        /// </summary>
        /// <param name="type">传入参数</param>
        /// <returns>返回参数</returns>
        public void DoParabolaMove(bool isForward)
        {
            
            float duration = 0.05f;
            // 运动方向
            Vector3 moveDir = isForward ? new Vector3(0,-1,0) : (Quaternion.Euler(transform.eulerAngles)*new Vector3(0,-1,0)).normalized*10;
            // 随机偏移
            Vector3 random_offset = new Vector3(Random.Range(_high_pos_min.x, _high_pos_max.x),
                                         Random.Range(_high_pos_min.y, _high_pos_max.y),
                                         Random.Range(_high_pos_min.z, _high_pos_max.z));
            // 最高点相对位置
            Vector3 end_position = moveDir*duration + random_offset;
            
            // 旋转速度
            _rotate_speed = new Vector3(Random.Range(_min_angle.x, _max_angle.x),
                                        Random.Range(_min_angle.y, _max_angle.y),
                                        Random.Range(_min_angle.z, _max_angle.z));
            // 抛物线初速度
            _parabola_speed = new Vector3(end_position.x/ duration,
                                          end_position.y/ duration, 
                                          end_position.z/ duration);

            // 重力初始速度为0
            _gravity_vec = Vector3.zero;
            _eclapsed_time = 0f;
            _rotateTf.rotation = Quaternion.identity;
            _moveState = MoveState.ParabolaMove;
        }

        /// <summary>
        /// 设置重力
        /// </summary>
        /// <param name="gravity">重力加速度</param>
        public void SetPBMGravity(float gravity)
        {
            _gravity = gravity;
        }

        /// <summary>
        /// 设置旋转角度区间
        /// </summary>
        /// <param name="min">最小旋转速度</param>
        /// <param name="max">最大旋转速度</param>
        public void SetPBMRotateRange(Vector3 min, Vector3 max)
        {
            _min_angle = min;
            _max_angle = max;
        }

        /// <summary>
        /// 设置初速度区间
        /// </summary>
        /// <param name="min">最小初速度</param>
        /// <param name="max">最大初速度</param>
        public void SetPBMInitSpeedRange(Vector3 min, Vector3 max)
        {
            _high_pos_min = min;
            _high_pos_max = max;
        }

        /// <summary>
        /// 设置旋转角度--Editor调用
        /// </summary>
        /// <param name="rotation">旋转四元数</param>
        public void SetRotation(Quaternion rotation)
        {
            _rotateTf.rotation = rotation;
        }

        /// <summary>
        /// 重置抛物线--Editor调用
        /// </summary>
        public void ResetParabola()
        {
            _moveState = MoveState.None;
            transform.localPosition = Vector3.zero;
            _rotateTf.rotation = Quaternion.identity;
        }
        /// <summary>
        /// 开始旋转--Editor调用
        /// </summary>
        /// <param name="type">传入参数</param>
        /// <returns>返回参数</returns>
        public void DORotate()
        {
            _rotate_speed = new Vector3(Random.Range(_min_angle.x, _max_angle.x),
                Random.Range(_min_angle.y, _max_angle.y),
                Random.Range(_min_angle.z, _max_angle.z));
            _moveState = MoveState.Rotate;
        }
        #endregion
    }
}