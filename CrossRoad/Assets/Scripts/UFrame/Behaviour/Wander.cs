using UFrame.Logger;
using System.Collections;
using System.Collections.Generic;
using UFrame.EntityFloat;
using UnityEngine;

namespace UFrame.BehaviourFloat
{
    public class Wander : BehaviourBase
    {
        protected float radius = 0f;

        public override void Tick(int deltaTimeMS)
        {
            if (!CouldRun())
            {
                return;
            }
            

            if (Math_F.Approximate3D(this.ownerEntity.position, this.tarPos) || IsPassed(this.tarPos))
            {
                //this.tarPos = Math_F.RandomInsideCircle(this.orgPos, this.radius);
                WanderArrived.Send(this.ownerEntity.entityID);
                return;
            }
            
            this.tickDir = Math_F.TwoPositionDir(this.ownerEntity.position, this.tarPos);

            ////活动移动量
            this.tickSpeed = this.speed * deltaTimeMS * 0.001f;
            //活动移动量
            this.tickMove = this.tickDir * this.tickSpeed;

            ////新位置
            this.ownerEntity.position += tickMove;
        }


        public void Init(EntityMovable ownerEntity, Vector3 orgPos, float radius, float speed)
        {
            this.ownerEntity = ownerEntity;
            this.orgPos = orgPos;
            this.radius = radius;
            this.speed = speed;
            this.tarPos = Math_F.RandomInsideCircle(this.orgPos, this.radius);
        }

        public override void Run()
        {
            base.Run();
            this.tarPos = Math_F.RandomInsideCircle(this.orgPos, this.radius);
            this.ownerEntity.LookAt(this.tarPos);
            //LogWarp.LogFormat("Run tarPos {0}", tarPos);
        }
    }
}
