/*******************************************************************
* FileName:     TraceCamera.cs
* Author:       Fan Zheng Yong
* Date:         2019-10-28
* Description:  
* other:    
********************************************************************/


using UnityEngine;
using UFrame.Common;
using UFrame.Logger;
using System.Collections.Generic;

namespace UFrame.OrthographicCamera
{
    public class TraceCamera : SingletonMono<TraceCamera>
    {
        protected Transform cachedTrans;
        protected Camera cacheCam;

        protected Transform tracedTrans = null;
        private bool isTrace = false;
        protected List<MonoBehaviour> otherCtrs = new List<MonoBehaviour>();
        protected Vector3 beginTraceOffset;
        protected bool isModifiedOtherCtr = false;
        public override void Awake()
        {
            base.Awake();
            cachedTrans = this.transform;
            cacheCam = GetComponent<Camera>();
        }

        public void LateUpdate()
        {
            if (!isTrace)
            {
                return;
            }
            if (tracedTrans != null)
            {
                if (tracedTrans.position == UFrame.Const.Invisible_Postion)
                {
                    if (!isModifiedOtherCtr)
                    {
                        isModifiedOtherCtr = true;
                        FinishTrace();
                    }
                    return;
                }
                this.cachedTrans.position = tracedTrans.position - beginTraceOffset;
            }
        }

        public void Init(MonoBehaviour [] otherCtrs)
        {
            this.otherCtrs.Clear();
            MonoBehaviour ctr = null;
            for (int i = 0; i < otherCtrs.Length; i++)
            {
                ctr = otherCtrs[i];
                if (ctr.enabled)
                {
                    ctr.enabled = false;
                    this.otherCtrs.Add(ctr);
                }
            }
        }

        public void BeginTrace(Transform traceTrans)
        {
            if (traceTrans == null)
            {
                return;
            }
            PointAtScreenCenter(traceTrans.position);

            this.tracedTrans = traceTrans;
            isTrace = true;
            beginTraceOffset = this.tracedTrans.position - this.cachedTrans.position;
        }

        public void FinishTrace()
        {
            //for (int i = 0; i < otherCtrs.Count; i++)
            //{
            //    otherCtrs[i].enabled = true;
            //}
            LogWarp.Log("相机跟随结束");
            isTrace = false;
        }

        public void PointAtScreenCenter(Vector3 point)
        {
            Vector2 screenCenter = Vector2.zero;
            screenCenter.x = Screen.width / 2;
            screenCenter.y = Screen.height / 2;

            Ray ray = cacheCam.ScreenPointToRay(screenCenter);

            //屏幕中心点，投影到地面的坐标
            Vector3 groundPoint = UFrame.Math_F.GetIntersectWithLineAndGround(ray.origin, ray.direction);

            //地面的坐标的差异就是相机偏移
            cachedTrans.position -= (groundPoint - point);
        }
    }
}

