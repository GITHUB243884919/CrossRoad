///*******************************************************************
//* FileName:     FingerCamera.cs
//* Author:       Fan Zheng Yong
//* Date:         2019-8-6
//* Description:  
//* other:    
//********************************************************************/


//using UnityEngine;
//using UFrame.Common;
//using UFrame.Logger;
//using UnityEngine.EventSystems;
//using System.Collections.Generic;
//using UnityEngine.UI;

//namespace UFrame.OrthographicCamera
//{
//    public enum DragType
//    {
//        Normal,
//        NormalPlus,
//    }

//    /// <summary>
//    /// 基于FingerGestures实现相机移动控制,相机是正交相机
//    /// </summary>
//    public class FingerCamera : SingletonMono<FingerCamera>
//    {
//        /// <summary>
//        /// 滑动类型
//        /// </summary>
//        public DragType dragType = DragType.Normal;

//        /// <summary>
//        /// 滑动强度
//        /// </summary>
//        public float dragSensitivity = 0.02f;

//        /// <summary>
//        /// 滑动缓动速度
//        /// </summary>
//        public float dragSmoothSpeed = 1;

//        /// <summary>
//        /// 滑动缓动时长
//        /// </summary>
//        public int dragSmoothDurationMS = 100;

//        /// <summary>
//        /// 滑动缓动方向
//        /// </summary>
//        protected Vector2 dragSmoothDir = Vector2.zero;

//        /// <summary>
//        /// 滑动缓动CD
//        /// </summary>
//        IntCD DragSmoothCD;

//        /// <summary>
//        /// 碰到边界后 矫正后方向的强度
//        /// </summary>
//        public float crossSensitivity = 5f;

//        public Transform crossPoint;

//        public Transform tryPoint;

//        public Transform moveToPoint;

//        /// <summary>
//        /// 进入时相位置
//        /// </summary>
//        public Vector3 editorInitPos = new Vector3(-4.63f, 0, -8.44f);

//        public Vector3 editorCamLeftUpPos;
//        public Vector3 editorCamRightUpPos;
//        public Vector3 editorCamLeftDownPos;
//        public Vector3 editorCamRightDownPos;

//        DragGesture dragGesture;
//        public Vector3 dragMoveTo = Vector3.zero;
//        protected Vector2 dragMoveTo2D = Vector2.zero;
//        //拉伸
//        protected bool pinching = false;
//        public float pinchSensitivity = 0.6f;
//        public float maxOrthographicSize = 65f;
//        public float minOrthographicSize = 10f;
//        public Transform cacheTrans;
//        public Camera cacheCam;

//        public override void Awake()
//        {
//            base.Awake();
//            cacheTrans = this.transform;
//            dragMoveTo = cacheTrans.position;
//            cacheCam = GetComponent<Camera>();
//            crossPoint = GameObject.Find("camer_range/cross_point").transform;
//            tryPoint = GameObject.Find("camer_range/try_point").transform;
//            moveToPoint = GameObject.Find("camer_range/moveto_point").transform;
//        }

//        void Start()
//        {
//            DragSmoothCD = new IntCD(dragSmoothDurationMS);
//            DragSmoothCD.Stop();
//            Init();
//        }

//        public virtual void Update()
//        {
//            OnUpdate_NormalPlus();
//        }

//        void LateUpdate()
//        {
//        }

//        void FixedUpdate()
//        {
//        }

//        public virtual void Init()
//        {
//            PointAtScreenCenter(new Vector3(-4.63f, 0, -8.44f));
//        }

//        void OnDrag(DragGesture gesture)
//        {
//            if (!this.enabled)
//            {
//                return;
//            }

//            switch(dragType)
//            {
//                case DragType.Normal:
//                    OnDrag_Normal(gesture);
//                    break;
//                case DragType.NormalPlus:
//                    OnDrag_NormalPlus(gesture);
//                    break;
//                default:
//                    string e = string.Format("不支持滑动类型{0}", dragType);
//                    throw new System.Exception(e);
//            }
//        }


//        void OnDrag_Normal(DragGesture gesture)
//        {
//            if (gesture.State == GestureRecognitionState.InProgress && CouldDrag())
//            {
//                dragGesture = gesture;
//                if (dragGesture.DeltaMove.SqrMagnitude() > 0)
//                {
//                    Vector2 screenSpaceMove = dragSensitivity * dragGesture.DeltaMove;
//                    Vector3 worldSpaceMove = screenSpaceMove.x * cacheTrans.right + screenSpaceMove.y * cacheTrans.up;

//                    dragMoveTo.x -= worldSpaceMove.x;
//                    dragMoveTo.z -= worldSpaceMove.z;

//                    dragMoveTo2D.x = dragMoveTo.x;
//                    dragMoveTo2D.y = dragMoveTo.z;
//                    if (CouldMoveTo(dragMoveTo2D))
//                    {
//                        cacheTrans.position = dragMoveTo;
//                    }
//                    else
//                    {
//                        WhenCannotMoveTo(dragMoveTo2D);
//                    }
//                }
//            }
//        }

//        void OnDrag_NormalPlus(DragGesture gesture)
//        {
//            if (gesture.State == GestureRecognitionState.InProgress && CouldDrag())
//            {
//                //LogWarp.LogErrorFormat("{0} OnDrag_NormalPlus", Time.realtimeSinceStartup);
//                //MessageString.Send((int)UFrameBuildinMessage.CameraDebug, Time.realtimeSinceStartup + " OnDrag_NormalPlus");
//                if (gesture.DeltaMove.SqrMagnitude() > 0)
//                {
//                    Vector2 screenSpaceMove = dragSensitivity * gesture.DeltaMove;
//                    Vector3 worldSpaceMove = screenSpaceMove.x * cacheTrans.right + screenSpaceMove.y * cacheTrans.up;

//                    dragMoveTo.x -= worldSpaceMove.x;
//                    dragMoveTo.z -= worldSpaceMove.z;

//                    dragSmoothDir.x = -worldSpaceMove.x;
//                    dragSmoothDir.y = -worldSpaceMove.z;

//                    dragMoveTo2D.x = dragMoveTo.x;
//                    dragMoveTo2D.y = dragMoveTo.z;
//                    if (CouldMoveTo(dragMoveTo2D))
//                    {
//                        cacheTrans.position = dragMoveTo;
//#if UNITY_EDITOR
//                        moveToPoint.position = dragMoveTo;
//                        //LogWarp.LogErrorFormat("{0} CouldMoveTo moveToPoint {1}", Time.realtimeSinceStartup, dragMoveTo);
//#endif
//                    }
//                    else
//                    {
//                        WhenCannotMoveTo(dragMoveTo2D);
//                    }

//                    DragSmoothCD.Stop();
//                }
//            }
//            else if (gesture.State == GestureRecognitionState.Started && CouldDrag())
//            {
//                DragSmoothCD.Stop();
//            }
//            else if (gesture.State == GestureRecognitionState.Ended && CouldDrag())
//            {
//                DragSmoothCD.ResetOrg(dragSmoothDurationMS);
//                DragSmoothCD.Run();
//            }
//        }

//        //拉伸
//        public virtual void OnPinch(PinchGesture gesture)
//        {
//            if (gesture.Phase == ContinuousGesturePhase.Started)
//            {
//                //MessageString.Send((int)UFrameBuildinMessage.CameraDebug, Time.realtimeSinceStartup + " OnPinch true");
//                pinching = true;
//            }
//            else if (gesture.Phase == ContinuousGesturePhase.Updated)
//            {
//                if (pinching)
//                {
//                    if (cacheCam.orthographicSize - gesture.Delta.Centimeters() * pinchSensitivity < this.minOrthographicSize)
//                    {
//                        return;
//                    }
//                    if (cacheCam.orthographicSize - gesture.Delta.Centimeters() * pinchSensitivity > this.maxOrthographicSize)
//                    {
//                        return;
//                    }
//                    cacheCam.orthographicSize -= gesture.Delta.Centimeters() * pinchSensitivity;

//                }
//            }
//            else if (gesture.Phase == ContinuousGesturePhase.Ended)
//            {
//                //if (pinching)
//                {
//                    pinching = false;
//                }
//                //LogWarp.LogErrorFormat("{0} OnPinch false", Time.realtimeSinceStartup);
//                //MessageString.Send((int)UFrameBuildinMessage.CameraDebug, Time.realtimeSinceStartup + " OnPinch false");

//            }
//        }

//        public virtual void OnUpdate_NormalPlus()
//        {
//            if (dragType != DragType.NormalPlus || !CouldDrag())
//            {
//                return;
//            }

//            int deltaMS = Math_F.FloatToInt1000(Time.deltaTime);
//            DragSmoothCD.Tick(deltaMS);
//            if (!DragSmoothCD.IsFinish() && DragSmoothCD.IsRunning())
//            {
//                //LogWarp.LogErrorFormat("{0} OnUpdate_NormalPlus", Time.realtimeSinceStartup);
//                //MessageString.Send((int)UFrameBuildinMessage.CameraDebug, Time.realtimeSinceStartup + " OnUpdate_NormalPlus");
//                //滑动减速
//                var deltaMove2D = dragSmoothDir * (float)DragSmoothCD.cd / (float)DragSmoothCD.org;
//                dragMoveTo2D.x = cacheTrans.position.x + deltaMove2D.x;
//                dragMoveTo2D.y = cacheTrans.position.z + deltaMove2D.y;
//                if (CouldMoveTo(dragMoveTo2D))
//                {
//                    dragMoveTo.x = cacheTrans.position.x + deltaMove2D.x;
//                    dragMoveTo.z = cacheTrans.position.z + deltaMove2D.y;
//                    cacheTrans.position = dragMoveTo;
//                }
//            }
//        }

//        public virtual bool CouldMoveTo(Vector2 moveTo)
//        {
//            return true;
//        }

//        public virtual void WhenCannotMoveTo(Vector2 moveTo)
//        {

//        }

//        public virtual bool CouldDrag()
//        {
//            return !pinching && !IsPointOnUI();
//        }

//        public virtual void SetRange()
//        {
//            GameObject editorPosRange = GameObject.Find("camer_range/editor_pos_range");
//            SetRangPos(editorPosRange, editorCamLeftDownPos, editorCamLeftUpPos, editorCamRightUpPos, editorCamRightDownPos);
//        }

//        public virtual bool IsPointOnUI()
//        {
//            return false;
//        }

//        /// <summary>
//        /// 使地面上某一点处于屏幕中间
//        /// </summary>
//        /// <param name="point"></param>
//        public void PointAtScreenCenter(Vector3 point)
//        {
//            Vector2 screenCenter = Vector2.zero;
//            screenCenter.x = Screen.width / 2;
//            screenCenter.y = Screen.height / 2;

//            Ray ray = cacheCam.ScreenPointToRay(screenCenter);

//            //屏幕中心点，投影到地面的坐标
//            Vector3 groundPoint = UFrame.Math_F.GetIntersectWithLineAndGround(ray.origin, ray.direction);

//            //地面的坐标的差异就是相机偏移
//            transform.position -= (groundPoint - point);

//            //防止LateUpdate的拖动
//            dragMoveTo = transform.position;

//            LogWarp.LogFormat("Camera Pos {0}", transform.position);
//        }

//        protected void SetRangPos(GameObject rangObj, Vector3 leftDown, Vector3 leftUp, Vector3 rightUp, Vector3 rightDown)
//        {
//            rangObj.transform.Find("leftdown").position = leftDown;
//            rangObj.transform.Find("leftup").position = leftUp;
//            rangObj.transform.Find("rightup").position = rightUp;
//            rangObj.transform.Find("rightdown").position = rightDown;
//        }

//        /// <summary>
//        /// 使场景上某一点处于上半部屏幕中间
//        /// </summary>
//        /// <param name="point"></param>
//        public void PointAtScreenUpCenter(Vector3 point)
//        {
//            Vector2 screenCenter = Vector2.zero;
//            screenCenter.x = Screen.width / 2;
//            screenCenter.y = Screen.height * Config.globalConfig.getInstace().BuildingViewRatioY;

//            Ray ray = cacheCam.ScreenPointToRay(screenCenter);

//            //屏幕中心点，投影到地面的坐标
//            Vector3 groundPoint = UFrame.Math_F.GetIntersectWithLineAndGround(ray.origin, ray.direction);

//            //地面的坐标的差异就是相机偏移
//            transform.position -= (groundPoint - point);

//            //防止LateUpdate的拖动
//            dragMoveTo = transform.position;

//            LogWarp.LogFormat("Camera Pos {0}", transform.position);
//        }
//    }

//}
