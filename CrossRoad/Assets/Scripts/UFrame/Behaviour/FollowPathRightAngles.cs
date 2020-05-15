///*******************************************************************
//* FileName:     FollowPathRightAngles.cs
//* Author:       Fan Zheng Yong
//* Date:         2019-11-18
//* Description:  
//* other:    
//********************************************************************/


//using UnityEngine;
//using UFrame.EntityFloat;
//using UFrame.Logger;
//using System.Collections.Generic;
////using Game.Path.StraightLine;

//namespace UFrame.BehaviourFloat
//{
//    public class RightAnglesControllNode
//    {
//        public RightAnglesControllNode()
//        {

//        }

//        public RightAnglesControllNode(RightAnglesControllNode node)
//        {
//            this.forwardPos = node.forwardPos;
//            this.backPos = node.backPos;
//            this.turnOrg = node.turnOrg;
//            this.turnSign = node.turnSign;
//        }
//        /// <summary>
//        /// 前点
//        /// </summary>
//        public Vector3 forwardPos;

//        /// <summary>
//        /// 后点
//        /// </summary>
//        public Vector3 backPos;

//        /// <summary>
//        /// 旋转中心点(旋转原点)
//        /// </summary>
//        public Vector3 turnOrg;

//        /// <summary>
//        /// 旋转方向(+1, -1)
//        /// </summary>
//        public short turnSign;
//    }

//    public class FollowPathRightAngles : BehaviourBase
//    {
//        public List<Vector3> pathPosList = new List<Vector3>();
//        public int nextPosIdx = 0;
//        public Vector3 nextPos = Vector3.zero;
//        public bool isArrivedEnd = false;

//        protected Quaternion orgRotation;

//        /// <summary>
//        /// 执行转弯的点和目标点的偏移量
//        /// 转弯实现是还没到目标点就要开始执行转向
//        /// </summary>
//        public float turnOffset = 3;

//        /// <summary>
//        /// 转弯速度 角速度
//        /// </summary>
//        protected float turnSpeed;

//        /// <summary>
//        /// 控制点列表
//        /// </summary>
//        public List<RightAnglesControllNode> ctrList;

//        /// <summary>
//        /// 路过的控制点索引
//        /// </summary>
//        public int idxCtr;

//        /// <summary>
//        /// 转弯CD
//        /// </summary>
//        protected IntCD turnCD = new IntCD(0);

//        /// <summary>
//        /// 转弯CD值
//        /// </summary>
//        protected int turnCDVal = 0;

//        /// <summary>
//        /// 弯道角度90
//        /// </summary>
//        protected float cornerAngle = Const.RightAngles;

//        /// <summary>
//        /// 是否是往前运动，如果是倒车这种就是false
//        /// </summary>
//        public bool isForward = true;

//        public string pathName;

//        public int groupID;
//        public int idx;

//        public override void Tick(int deltaTimeMS)
//        {
//            if (!this.CouldRun())
//            {
//                return;
//            }

//            Follow(deltaTimeMS);
//            TickTurn(deltaTimeMS);
//        }

//        public virtual void TickTurn(int deltaTimeMS)
//        {
//            if (!turnCD.IsRunning() || turnCD.IsFinish())
//            {
//                return;
//            }

//            turnCD.Tick(deltaTimeMS);
//            var ctr = this.ctrList[idxCtr];

//            int left = turnCD.org - Mathf.Max(0, turnCD.cd);

//            int logicDelta = deltaTimeMS;
//            if (turnCD.cd < 0)
//            {
//                logicDelta += turnCD.cd;
//            }
//            //旋转
//            this.ownerEntity.Rotate(new Vector3(0, ctr.turnSign * this.turnSpeed * logicDelta, 0), Space.Self);
//            //位移
//            var ctrForward = ctr.backPos - ctr.turnOrg;
//            var turnDir = Quaternion.Euler(0, ctr.turnSign * this.turnSpeed * left, 0) * ctrForward;
//            this.ownerEntity.position = ctr.turnOrg + turnDir;

//            if (turnCD.IsFinish())
//            {
//                turnCD.Stop();

//                //去除tick旋转和位移的误差：位移到前点，看向下一个后点
//                ownerEntity.position = ctr.forwardPos;
//                idxCtr++;
//                if (idxCtr < ctrList.Count)
//                {
//                    this.ownerEntity.LookAt(this.ctrList[idxCtr].backPos);
//                    if (!isForward)
//                    {
//                        ownerEntity.Rotate(new Vector3(0, 180f, 0), Space.Self);
//                    }
//                }
//            }
//        }

//        public virtual void Init(EntityMovable ownerEntity, string pathName, List<Vector3> pathPosList, Vector3 orgPos, int nextPosIdx, float speed)
//        {
//            if (this.IsRunning())
//            {
//                string e = string.Format("{0} {1} FollowPathRightAngles非法初始化", ownerEntity.entityID, ownerEntity.mainGameObject.GetInstanceID());
//                throw new System.Exception(e);
//            }

//            this.ownerEntity = ownerEntity;
//            this.pathPosList = pathPosList;
//            this.pathName = pathName;
//            this.orgPos = orgPos;
//            this.nextPosIdx = nextPosIdx;
//            this.nextPos = this.pathPosList[this.nextPosIdx];
//            this.speed = speed * 0.001f;
//            this.orgRotation = this.ownerEntity.mainGameObject.transform.rotation;
//            this.isArrivedEnd = false;

//            idxCtr = 0;
//            //停止旋转CD
//            turnCD.Stop();
//            this.turnSpeed = CalcTurnSpeed(this.speed);
//            //因为知道角度是90度所以cd是旋转cd恒定的
//            turnCDVal = Math_F.FloatToInt(cornerAngle / this.turnSpeed);

//            ctrList = CalcPathManager.GetInstance().GetRac(pathName);
//            if (ctrList == null)
//            {
//                ctrList = GenControllNodeList(pathPosList, this.turnOffset);
//                CalcPathManager.GetInstance().AddRac(pathName, ctrList);
//            }

//            isForward = true;
//            //先把朝向转到第一个后点
//            ownerEntity.LookAt(ctrList[0].backPos);
//        }

//        public virtual void Init(EntityMovable ownerEntity, int groupID, int idx, List<Vector3> pathPosList, Vector3 orgPos, int nextPosIdx, float speed)
//        {
//            if (this.IsRunning())
//            {
//                string e = string.Format("{0} {1} FollowPathRightAngles非法初始化", ownerEntity.entityID, ownerEntity.mainGameObject.GetInstanceID());
//                throw new System.Exception(e);
//            }

//            this.ownerEntity = ownerEntity;
//            this.pathPosList = pathPosList;
//            this.orgPos = orgPos;
//            this.nextPosIdx = nextPosIdx;
//            this.nextPos = this.pathPosList[this.nextPosIdx];
//            this.speed = speed * 0.001f;
//            this.orgRotation = this.ownerEntity.mainGameObject.transform.rotation;
//            this.isArrivedEnd = false;

//            idxCtr = 0;
//            //停止旋转CD
//            turnCD.Stop();
//            this.turnSpeed = CalcTurnSpeed(this.speed);
//            //因为知道角度是90度所以cd是旋转cd恒定的
//            turnCDVal = Math_F.FloatToInt(cornerAngle / this.turnSpeed);

//            var pathUnit = GroundParingSpacePathManager.GetInstance().GetPathUnit(groupID, idx);
//            if (!GroundParingSpacePathManager.IsExist(pathUnit.inPathRac))
//            {
//                ctrList = GroundParingSpacePathManager.GenRAC(pathPosList, this.turnOffset);
//                GroundParingSpacePathManager.GetInstance().AddPath(
//                    GroundParingSpacePathType.InPathRac, pathUnit, null, ctrList);
//            }
//            ctrList = pathUnit.inPathRac;
//            this.groupID = groupID;
//            this.idx = idx;

//            isForward = true;
//            //先把朝向转到第一个后点
//            ownerEntity.LookAt(ctrList[0].backPos);
//        }

//        public static List<RightAnglesControllNode> GenControllNodeList(List<Vector3> pathPosList, float turnOrgOffset)
//        {
//            List<RightAnglesControllNode> ctrList = new List<RightAnglesControllNode>();
//            int posLen = pathPosList.Count;
//            if (posLen < 2)
//            {
//                return ctrList;
//            }

//            for (int i = 1; i < posLen - 1; i++)
//            {
//                //前点
//                var forwardDir = (pathPosList[i + 1] - pathPosList[i]).normalized;
//                var forwardPos = pathPosList[i] + forwardDir * turnOrgOffset;
//#if UNITY_EDITOR
//                var forwardGo = GameObject.CreatePrimitive(PrimitiveType.Sphere);
//                forwardGo.transform.position = forwardPos + new Vector3(0, 8, 0);
//                forwardGo.name = string.Format("ctr_forward_{0}", i);
//#endif
//                //后点
//                var backDir = (pathPosList[i] - pathPosList[i - 1]).normalized;
//                var backPos = pathPosList[i] - backDir * turnOrgOffset;
//#if UNITY_EDITOR
//                var backGo = GameObject.CreatePrimitive(PrimitiveType.Capsule);
//                backGo.transform.position = backPos + new Vector3(0, 8, 0);
//                backGo.name = string.Format("ctr_back_{0}", i);
//#endif
//                //旋转原点
//                var cross = Vector3.Cross(forwardDir, backDir);
//                short turnSign = 1;
//                if (cross.y > 0)
//                {
//                    turnSign = -1;
//                }
//                var turnOrgDir = Quaternion.Euler(0, UFrame.Const.RightAngles * turnSign, 0) * backDir;
//                var turnOrgPos = backPos + turnOrgDir * turnOrgOffset;
//#if UNITY_EDITOR
//                var turnOrgGo = GameObject.CreatePrimitive(PrimitiveType.Cube);
//                turnOrgGo.transform.position = turnOrgPos + new Vector3(0, 8, 0);
//                turnOrgGo.name = string.Format("ctr_turnOrg{0}", i);
//#endif
//                var ctr = new RightAnglesControllNode();
//                ctr.forwardPos = forwardPos;
//                ctr.backPos = backPos;
//                ctr.turnOrg = turnOrgPos;
//                ctr.turnSign = turnSign;
//                ctrList.Add(ctr);

//                ////旋转点
//                //var ctrForward = backNode - ctrNode;
//                //float turnAngle = 30f;
//                //turnSignList[idxCtr - 1] = 1;
//                //if (cross.y > 0)
//                //{
//                //    turnAngle = -turnAngle;
//                //    turnSignList[idxCtr - 1] = -1;
//                //}
//                //var turnDir = Quaternion.Euler(0, turnAngle, 0) * ctrForward;
//                //var turnNode = ctrNode + turnDir;
//                //var turnGo = GameObject.CreatePrimitive(PrimitiveType.Cube);
//                //turnGo.transform.position = turnNode + new Vector3(0, 8, 0);
//                //turnGo.name = string.Format("turn_{0}", i);
//            }

//            return ctrList;
//        }

//        /// <summary>
//        /// 根据速度计算旋转角速度。
//        /// 速度可以看成是以控制点为中心，半径为specialOffset的圆上的上两个点的距离。
//        /// 这样可以根据余弦定理可以求角
//        /// speed最好带入毫秒单位，否则Acos会出现NaN值
//        /// 准确的说，应该确保2 * this.specialOffset * this.specialOffset > speed * speed
//        /// 以毫秒单位的speed不能超过圆的直径，否则构不成三角形
//        /// 也就是cos为[0, 1]
//        /// </summary>
//        /// <param name="speed"></param>
//        /// <returns></returns>
//        float CalcTurnSpeed(float speed)
//        {
//            //cos(A) = (b^b + c^c - a^a) / 2bc
//            float cos = (2 * this.turnOffset * this.turnOffset - speed * speed) / (2 * this.turnOffset * this.turnOffset);
//            float rad = Mathf.Acos(cos);
//            return turnSpeed = Math_F.RadianToAngle(rad);
//        }

//        public override void Release()
//        {
//            pathPosList = null;
//            ctrList = null;
//            base.Release();
//        }

//        protected override bool IsPassed(Vector3 pos)
//        {
//            var localPos = this.ownerEntity.InverseTransformPoint(pos);
//            if (isForward)
//            {
//                return localPos.z <= 0;
//            }

//            return localPos.z > 0;
//        }

//        protected virtual void Follow(int deltaTimeMS)
//        {
//            //是否到后点
//            //到达后点, 开始启动旋转，位移转按角速度求出的位置
//            //未到达后点往后点继续前进
//            if (turnCD.IsRunning() || !turnCD.IsFinish())
//            {
//                return;
//            }

//            //还没到最后一个转弯点
//#if UNITY_EDITOR
//            if (ctrList == null)
//            {
//                string e = string.Format("{0} 路径异常", this.ownerEntity.entityID);
//                throw new System.Exception(e);
//            }
//#endif
//            if (idxCtr < ctrList.Count)
//            {
//                var ctr = ctrList[idxCtr];
//                if (!IsArrivedTarget(ctr.backPos))
//                {
//                    UnArrived(deltaTimeMS, ctr.backPos);
//                    return;
//                }

//                //重新设置旋转CD
//                turnCD.ResetOrg(turnCDVal);
//                turnCD.Run();
//                return;
//            }

//            //到这里已经转过最后一个弯了
//            var lastPos = pathPosList[pathPosList.Count - 1];
//            if (!IsArrivedTarget(lastPos))
//            {
//                UnArrived(deltaTimeMS, lastPos);
//                return;
//            }

//            //走到这里已经到达路径最后一个点，走完全部path
//            WhenArrivedEndPos();

//            this.isArrivedEnd = true;
//        }

//        public virtual void WhenArrivedEndPos()
//        {
//            MessageArrivedEx.Send((int)UFrameBuildinMessage.ArrivedRightAngle, this);
//            this.Stop();
//        }

//        public bool IsArrivedTarget(Vector3 target)
//        {
//            if (Math_F.Approximate3D(this.ownerEntity.position, target) || this.IsPassed(target))
//            {
//                return true;
//            }
//            return false;
//        }

//        /// <summary>
//        /// 直接转，相当于LookAt基本不用。
//        /// </summary>
//        /// <param name="target"></param>
//        protected void TurnTo(Vector3 target)
//        {
//            var dirWorld = target - this.ownerEntity.position;
//            float angle = Math_F.TwoDirYAngle(this.ownerEntity.forward, dirWorld.normalized);
//            if (Vector3.Cross(dirWorld, this.ownerEntity.forward).y >= 0)
//            {
//                this.ownerEntity.Rotate(new Vector3(0, -angle, 0), Space.Self);
//                return;
//            }

//            this.ownerEntity.Rotate(new Vector3(0, angle, 0), Space.Self);
//        }
        
//        protected virtual void UnArrived(int deltaTimeMS, Vector3 target)
//        {
//            //向目的地走
//            this.tickDir = Math_F.TwoPositionDir(this.ownerEntity.position, target);
//            //活动移动量
//            this.tickSpeed = this.speed * deltaTimeMS;            
//            this.tickMove = this.tickDir * this.tickSpeed;
//            //新位置
//            this.ownerEntity.position += this.tickMove;
//        }

//        public void ResetRoation()
//        {
//            this.ownerEntity.rotation = this.orgRotation;
//        }

//        public void ModifyPath(Vector3 offset)
//        {
//            for (int i = 0; i < pathPosList.Count; i++)
//            {
//                pathPosList[i] += offset;
//            }

//            this.nextPos += offset;
//        }

//        public void ModifyPath(System.Action<BehaviourBase, Vector3> onModifyPath, Vector3 offset)
//        {
//            onModifyPath?.Invoke(this, offset);
//        }
//    }
//}

