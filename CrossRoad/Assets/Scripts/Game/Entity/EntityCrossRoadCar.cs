
using UFrame;
using UFrame.EntityFloat;
using UnityEngine;

namespace CrossRoadGame
{
    public class EntityCrossRoadCar : EntityMovable
    {
        public Vector3 startPos;
        public Vector3 endPos;

        //移动方向
        Vector3 moveDir;

        public static ObjectPool<EntityCrossRoadCar> pool = new ObjectPool<EntityCrossRoadCar>();

        public override void Active()
        {
            base.Active();

        }

        public override void Deactive()
        {
            this.position = Const.Invisible_Postion;
            base.Deactive();
        }

        public override void Tick(int deltaTimeMS)
        {
            if (!this.CouldActive())
            {
                return;
            }
            if (Math_F.Approximate3D(position, endPos) ||
                this.IsPassed(endPos)
            )
            {
                //到达终点
                //重要：回收前先把Collider设置成false，避免在pool中触发碰撞
                GetTrans().Find("Collider").gameObject.SetActive(false);
                CrossRoadModelManager.GetInstance().entityModel.RemoveFromEntityMovables(this);

                return;
            }

            Tick_Move(deltaTimeMS);
        }

        public override void OnDeathToPool()
        {
            this.Deactive();
            base.OnDeathToPool();
        }

        public override void OnRecovery()
        {
            this.Deactive();
            base.OnRecovery();
        }

        public void Init(Vector3 startPos, Vector3 endPos, float speed)
        {
            this.startPos = startPos;
            this.endPos = endPos;
            moveDir = (endPos - startPos).normalized;
            this.moveSpeed = speed * 0.001f;
        }

        void Tick_Move(int deltaTimeMS)
        {
            this.position += (float)(this.moveSpeed * deltaTimeMS) * moveDir;
        }

        protected virtual bool IsPassed(Vector3 pos)
        {
            var localPos = GetTrans().InverseTransformPoint(pos);
            return localPos.z <= 0;
        }
    }
}

