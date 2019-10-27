using UnityEngine;
using System.Collections.Generic;
using JW.PLink;

namespace XH.SlotScroll
{
    // 滚动的方向
    public enum SlotScrollMoveDirection
    {
        None        = 0,// 无
        UpToBottom  = 1,// 从上到下
        BottomToUp  = 2,// 从下到上
        LeftToRight = 3,// 从左到右
        RightToLeft = 4,// 从右到左
    }
    public class SlotScroll : MonoBehaviour
    {
        public enum MoveStep
        {
            None = 0,
            Initial = 1, // 开始匀速转动
            SpeedUp = 2, // (反向)匀加速
            Idle = 3,    // 找时机阶段
            Slowdown = 4,// 逐步停下来
        }

        [SerializeField] private GameObject _prefab;//格子
        [SerializeField] private float _gridWidth = 0;
        [SerializeField] private float _gridHeight = 0;
        [SerializeField] private float _uniformDuration = 2f;//匀速运动时间
        [SerializeField] private float _initSpeed = -1000;
        [SerializeField] [Range(0.5f,10f)]private float _idleDuration = 1;
        [SerializeField] private int CircleCount = 2;//最后idle转的圈数

        #region property
        private int _count = 10;
        private int _curIndex = 0;
        private float _topClamp;
        private float _bottomClamp;

        private float _speed = -1500;//速度
        private float _idleSpeed = -200;//idle的速度
        private int _circleCnt = 0;//idle状态下当前圈数
        private float _speedUpDuration = 2f;//变速时间
        private float _stepElapsedTime = 0f;//消逝的时间
        private float _accSpeed = 0f;//加速度
        private int _targetIndex = 0;//目标index

        private List<SlotScrollGrid> _gridList = new List<SlotScrollGrid>();
        private MoveStep _step = MoveStep.None;
        private SlotScrollMoveDirection _moveDirection = SlotScrollMoveDirection.None;
        #endregion
        #region callback
        public System.Action OnRollEnd;
        public System.Action<int, PrefabLink> OnEnableGrid;
        #endregion
        //创建格子
        private void CreateGrids(int count)
        {
            if(_gridList.Count > 0)
            {
                DestroyGrids();
            }
            _count = count;

            _prefab.SetActive(true);
            for (int i = 0; i < count; i++)
            {
                GameObject gridGo = Instantiate<GameObject>(_prefab);
                SlotScrollGrid grid = gridGo.GetComponent<SlotScrollGrid>();
                grid.Index = i;
                if (grid!=null)
                {
                    _gridList.Add(grid);
                }
                gridGo.transform.SetParent(transform);
                gridGo.transform.name = i.ToString();
                gridGo.transform.localScale = Vector3.one;
                gridGo.transform.localPosition = Vector3.zero;
                if(OnEnableGrid!=null)
                {
                    OnEnableGrid.Invoke(grid.Index, grid.GetPrefabLink());
                }
            }
            _prefab.SetActive(false);

            ResetPosition();
        }

        //销毁所有的格子
        private void DestroyGrids()
        {
            if(_gridList == null || _gridList.Count == 0)
            {
                return;
            }
            for(int i = 0; i < _gridList.Count; i++)
            {
                _gridList[i].Clear();
                Destroy(_gridList[i].gameObject);
            }
            _gridList.Clear();
            _count = 0;
        }
        //设置组件格子总个数并初始化
        public void SetData(int count)
        {
            if(count<=0)
            {
                return;
            }
            _count = count;
            _moveDirection = SlotScrollMoveDirection.UpToBottom;
            _bottomClamp = -(_count-1) / 2 * _gridHeight;
            _accSpeed = 0;
            _curIndex = 0;
            CreateGrids(count);

            _idleSpeed = -1*(_count*_gridHeight* CircleCount) /(_idleDuration);
        }

        //重置位置
        private void ResetPosition()
        {
            int bottomIdx = ((_count-1)/2+_curIndex)%_count;
            int topIdx = (bottomIdx + 1) % _count;
            float startPosY = _count / 2 * _gridHeight;
            for (int i = 0; i < _count; i++)
            {
                int relativeIdx = (i + _count - topIdx) % _count;
                float y = startPosY - relativeIdx*_gridHeight;
                _gridList[i].Root.localPosition = new Vector3(0, y, 0);
            }
        }

        // 滚动到指定的index
        public void Scroll(int index)
        {
            if(_count<=0)
            {
                return;
            }
            if(index >= _count)
            {
                return;
            }
            _curIndex = 0;
            _speed = _initSpeed;
            _accSpeed = 0;
            _targetIndex = index;
            _stepElapsedTime = 0;
            _circleCnt = -1;
            ResetPosition();
            ChangeStepState(MoveStep.Initial);
        }

        private void FixedUpdate()
        {
            if(_step == MoveStep.None)
            {
                return;
            }
            float deltaTime = Time.fixedDeltaTime;
            _stepElapsedTime += deltaTime;
            switch (_step)
            {
                case MoveStep.Initial:
                    {
                        if (_stepElapsedTime >= _uniformDuration)
                        {
                            ChangeStepState(MoveStep.SpeedUp);
                        }
                        break;
                    }
                case MoveStep.SpeedUp:
                    {
                        if(_speed >= _idleSpeed)
                        {
                            ChangeStepState(MoveStep.Idle);
                        }
                        break;
                    }
                case MoveStep.Idle:
                    {
                        //判断该物体是否已经在Top位置
                        if (GetRelativeCenterIdx(-3) == _targetIndex)
                        {
                            if(++_circleCnt>CircleCount)
                            {
                                ChangeStepState(MoveStep.Slowdown);
                            }
                        }
                        break;
                    }
                case MoveStep.Slowdown:
                    {
                        float y = _gridList[_targetIndex].Root.localPosition.y;
                        if(y <= 0.0f)
                        {
                            ChangeStepState(MoveStep.None);
                            if (OnRollEnd!=null)
                            {
                                OnRollEnd.Invoke();
                            }
                            ResetPosition();
                        }
                        break;
                    }
            }
            UpToBottomMove(deltaTime);
        }

        private void ChangeStepState(MoveStep step)
        {
            if(_step == step)
            {
                return;
            }
            _step = step;
            switch (step)
            {
                case MoveStep.None:
                    {
                        _speed = 0;
                        _accSpeed = 0;
                        _curIndex = _targetIndex;
                        _circleCnt = 0;
                        break;
                    }
                case MoveStep.SpeedUp:
                    {
                        //开始匀减速运动 -- 降到指定的速度
                        float lastSpeed = _idleSpeed;
                        _accSpeed = (lastSpeed - _speed) / _speedUpDuration;
                        _stepElapsedTime = 0;
                        break;
                    }
                case MoveStep.Idle:
                    {
                        _accSpeed = 0;
                        _stepElapsedTime = 0;
                        break;
                    }
                case MoveStep.Slowdown:
                    {
                        //开始计算加速度
                        // t = 2*s/(Vt+V0)
                        // a = (V1 - V0)/t
                        float y = _gridList[_targetIndex].Root.localPosition.y;
                        //float duration = (2 * y) / Mathf.Abs((0 + _speed));
                        float duration = 4;
                        _accSpeed = (0 - _speed) / duration;

                        break;
                    }
            }
        }

        private void UpToBottomMove(float deltaTime)
        {
            _speed = _speed + _accSpeed*deltaTime;
            if(_speed>=0)
            {
                _speed = -60;
                _accSpeed = 0;
            }
            Vector3 deltaMove = new Vector3(0,_speed*deltaTime,0);
            for (int i = 0; i < _count; i++)
            {
                _gridList[i].Root.localPosition += deltaMove;
                float y = _gridList[i].Root.localPosition.y;
                if(y >= -0.5*_gridHeight && y <= 0.5*_gridHeight)
                {
                    _curIndex = i;
                }
            }
            for(int i = 0; i < _count; i++)
            {
                float y = _gridList[i].Root.localPosition.y;
                if (y <= _bottomClamp)
                {
                    int bottomIdx = ((_count - 1) / 2 + _curIndex) % _count;
                    int topIdx = (bottomIdx + 1) % _count;
                    float startPosY = _count / 2 * _gridHeight;
                    _gridList[i].Root.localPosition = _gridList[topIdx].Root.localPosition + new Vector3(0, _gridHeight, 0);
                }
            }
        }
        //
        private int GetRelativeCenterIdx(int offset)
        {
            return (_curIndex + _count + offset) % _count;
        }

        public void Clear()
        {
            ChangeStepState(MoveStep.None);
            DestroyGrids();
            _speed = 0;
            _accSpeed = 0;
            _curIndex = 0;
            _circleCnt = 0;
        }

    }
}

