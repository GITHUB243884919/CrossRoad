using Game;
using Game.MiniGame;
using System.Collections;
using System.Collections.Generic;
using UFrame;
using UFrame.EntityFloat;
using UnityEngine;
namespace CrossRoadGame
{

    public class AnimalEntityModel 
    {
        /// <summary>
        /// 移动的实体
        /// </summary>
        public Dictionary<int, EntityMovable> entityMovables = new Dictionary<int, EntityMovable>();
        protected List<EntityMovable> entityMovableAdds = new List<EntityMovable>();
        protected List<EntityMovable> entityMovableRemoves = new List<EntityMovable>();

        public AnimalEntityModel()
        {
            Init();
        }
        void Init()
        {
        }
        public void Tick(int deltaTimeMS)
        {
            BeforeTickMoveables();
            //LogWarp.LogErrorFormat("========begin tick {0}==========", GameManager.GetInstance().tickCount);
            foreach (var val in entityMovables.Values)
            {
                //LogWarp.LogErrorFormat("{0}, {1}", val.entityID, val.mainGameObject.GetInstanceID());
                val.Tick(deltaTimeMS);
            }
            //LogWarp.LogErrorFormat("--------end tick{0}---------", GameManager.GetInstance().tickCount);

            AfterTickMoveables();
        }

        public void Release()
        {
            foreach (var val in entityMovables.Values)
            {
                ReleaseEntity(val);
            }
            entityMovables.Clear();

            foreach (var val in entityMovableAdds)
            {
                ReleaseEntity(val);
            }
            entityMovableAdds.Clear();

            foreach (var val in entityMovableRemoves)
            {
                ReleaseEntity(val);
            }
            entityMovableRemoves.Clear();

            EntityCrossRoadAnimal.pool.RecoveryAll();
        }

        public void AddToEntityMovables(EntityMovable entity)
        {
            DebugFile.GetInstance().WriteKeyFile(entity.entityID, "{0} entityMovableAdds.add {1}",
                entity.entityID, entity.mainGameObject.GetInstanceID());
            DebugFile.GetInstance().WriteKeyFile(entity.mainGameObject.GetInstanceID(), "{0} entityMovableAdds.add {1}",
                entity.mainGameObject.GetInstanceID(), entity.entityID);
            this.entityMovableAdds.Add(entity);
        }

        public void RemoveFromEntityMovables(EntityMovable entity)
        {
            DebugFile.GetInstance().WriteKeyFile(entity.entityID, "{0} entityMovableRemoves.add {1}",
                entity.entityID, entity.mainGameObject.GetInstanceID());
            DebugFile.GetInstance().WriteKeyFile(entity.mainGameObject.GetInstanceID(), "{0} entityMovableRemoves.add {1}",
                entity.mainGameObject.GetInstanceID(), entity.entityID);
            this.entityMovableRemoves.Add(entity);
        }

        public void BeforeTickMoveables()
        {
            foreach (var v in this.entityMovableAdds)
            {
                entityMovables.Add(v.entityID, v);

                DebugFile.GetInstance().WriteKeyFile(v.entityID, "{0} BeforeTickMoveables {1}",
                    v.entityID, v.mainGameObject.GetInstanceID());
                DebugFile.GetInstance().WriteKeyFile(v.mainGameObject.GetInstanceID(), "{0} BeforeTickMoveables {1}",
                    v.mainGameObject.GetInstanceID(), v.entityID);
            }

            entityMovableAdds.Clear();
        }

        public void AfterTickMoveables()
        {

            foreach (var v in this.entityMovableRemoves)
            {

                bool retCode = entityMovables.Remove(v.entityID);
#if UNITY_EDITOR
                if (!retCode)
                {
                    string e = string.Format("{0} AfterTickMoveables 异常{1}", v.entityID, v.mainGameObject.name);
                    throw new System.Exception(e);
                }
#endif
                DebugFile.GetInstance().WriteKeyFile(v.entityID, "{0} AfterTickMoveables {1}", v.entityID, v.mainGameObject.GetInstanceID());
                DebugFile.GetInstance().WriteKeyFile(v.mainGameObject.GetInstanceID(), "{0} AfterTickMoveables {1}", v.mainGameObject.GetInstanceID(), v.entityID);


                EntityFuncType eEntityFuncType = (EntityFuncType)v.entityFuncType;
                switch (eEntityFuncType)
                {
                    case EntityFuncType.Animal_LittleGame:
                        CrossRoadGame.EntityCrossRoadAnimal.pool.Delete(v as CrossRoadGame.EntityCrossRoadAnimal);
                        break;
                    default:
                        string e = string.Format("没有这种 EntityFuncType {0}", eEntityFuncType);
                        throw new System.Exception(e);
                }
            }
            entityMovableRemoves.Clear();
        }

        public void ReleaseEntity(EntityMovable entity)
        {
            //清go
            var goPool = PoolManager.GetInstance().IsBelongGameObjectPool(entity.entityResType);
            goPool.OnRecovery(entity.mainGameObject);

            //清entity
            EntityFuncType eEntityFuncType = (EntityFuncType)entity.entityFuncType;
            switch (eEntityFuncType)
            {
                case EntityFuncType.Animal_LittleGame:
                    CrossRoadGame.EntityCrossRoadAnimal.pool.Delete(entity as CrossRoadGame.EntityCrossRoadAnimal);
                    break;
                default:
                    string e = string.Format("没有这种 EntityFuncType {0}", eEntityFuncType);
                    throw new System.Exception(e);
            }
        }
    }
}
