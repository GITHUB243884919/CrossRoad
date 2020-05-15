/*******************************************************************
* FileName:     FollowPath.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-6
* Description:  
* other:    
********************************************************************/


using UnityEngine;
using UFrame.EntityFloat;
using System.Collections.Generic;

namespace UFrame.BehaviourFloat
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FollowPath : BehaviourBase
    {
        public List<Vector3> pathPosList;
        public int nextPosIdx = 0;
        public Vector3 nextPos = Vector3.zero;
        public bool isArrivedEnd = false;
        protected bool isLoop = false;

        protected Vector3 forwardNormal = Vector3.zero;
        protected Vector3 cross = Vector3.zero;
        protected Vector3 rotateValue = Vector3.zero;
        protected Quaternion orgRotation;

        public override void Tick(int deltaTimeMS)
        {
            if (!this.CouldRun())
            {
                return;
            }

            this.Follow(deltaTimeMS);
        }

        public void Init(EntityMovable ownerEntity, List<Vector3> pathPosList, Vector3 orgPos, int nextPosIdx, float speed, bool isLoop)
        {
            if (this.IsRunning())
            {
                string e = string.Format("{0}, {1}, {2} FollowPath非法初始化", ownerEntity.entityID, ownerEntity.mainGameObject.name,
                    ownerEntity.mainGameObject.GetInstanceID());
                throw new System.Exception(e);
            }

            this.ownerEntity = ownerEntity;
            this.pathPosList = pathPosList;
            this.orgPos = orgPos;
            this.nextPosIdx = nextPosIdx;
            this.nextPos = this.pathPosList[this.nextPosIdx];
            this.speed = speed;
            this.isLoop = isLoop;
            this.orgRotation = this.ownerEntity.mainGameObject.transform.rotation;
            this.isArrivedEnd = false;
        }

        public override void Release()
        {
            pathPosList = null;
            base.Release();
        }

        protected void Follow(int deltaTimeMS)
        {
            if (!Math_F.Approximate3D(this.ownerEntity.position, this.nextPos) && !this.IsPassed(this.nextPos))
            {
                this.UnArrived(deltaTimeMS);
                return;
            }

            this.Arrived();
        }

        protected void TurnTo(Vector3 target)
        {
            this.ownerEntity.LookAt(target);
        }

        protected void UnArrived(int deltaTimeMS)
        {

            //向目的地走
            this.tickDir = Math_F.TwoPositionDir(this.ownerEntity.position, this.nextPos);

            //活动移动量
            this.tickSpeed = this.speed * deltaTimeMS * 0.001f;
            this.tickMove = this.tickDir * this.tickSpeed;

            //新位置
            this.ownerEntity.position += this.tickMove;
        }

        protected void Arrived()
        {
            MessageArrived msg = null;
            //到达，获取下一个点
            if (this.nextPosIdx == this.pathPosList.Count - 1)
            {
                this.isArrivedEnd = true;
                if (!this.isLoop)
                {
                    this.Stop();
                    msg = MessageArrived.Send(this);
                    //this.ownerEntity.rotation = this.orgRotation;
                    DebugFile.GetInstance().WriteKeyFile(ownerEntity.entityID, "{0} send {1}", ownerEntity.entityID, msg);
                    DebugFile.GetInstance().WriteKeyFile(ownerEntity.mainGameObject.GetInstanceID(), "{0} send {1}", ownerEntity.mainGameObject.GetInstanceID(), msg);

                    return;
                }

                //循环模式，下一个点是第二个点
                this.nextPosIdx = 1;
                //this.ownerEntity.mainSprite3D.transform.position = this.pathPosList[0].clone();
                //this.ownerEntity.mainSprite3D.transform.rotation = this.orgRotation;
                //this.ownerEntity.position = this.pathPosList[0];
                //this.ownerEntity.rotation = this.orgRotation;
            }
            else
            {
                this.nextPosIdx++;
            }

            this.nextPos = this.pathPosList[this.nextPosIdx];
            this.TurnTo(nextPos);

            //msg = MessageArrived.Send(this);
            DebugFile.GetInstance().WriteKeyFile(ownerEntity.entityID, "{0} send {1}", ownerEntity.entityID, msg);
            DebugFile.GetInstance().WriteKeyFile(ownerEntity.mainGameObject.GetInstanceID(), "{0} send {1}", ownerEntity.mainGameObject.GetInstanceID(), msg);

        }

        public void ResetRoation()
        {
            this.ownerEntity.rotation = this.orgRotation;
        }

        public void ModifyPath(Vector3 offset)
        {
            for (int i = 0; i < pathPosList.Count; i++)
            {
                pathPosList[i] += offset;
            }

            this.nextPos += offset;
        }

        public void ModifyPath(System.Action<FollowPath, Vector3> onModifyPath, Vector3 offset)
        {
            onModifyPath?.Invoke(this, offset);
        }
    }
}

