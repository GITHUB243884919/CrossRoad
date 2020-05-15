/*******************************************************************
* FileName:     EntityManager.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-7
* Description:  
* other:    
********************************************************************/


using Game.MiniGame;
using System.Collections.Generic;
using UFrame;
using UFrame.Common;
using UFrame.EntityFloat;
using UFrame.MessageCenter;
using Game.GlobalData;
using UFrame.Logger;

namespace Game
{
    public class EntityManagerTick : TickBase
    {
        public override void Tick(int deltaTimeMS)
        {
            if (!this.CouldRun())
            {
                return;
            }

            entityManager.BeforeTickMoveables();
            //LogWarp.LogErrorFormat("========begin tick {0}==========", GameManager.GetInstance().tickCount);
            foreach (var val in this.entityManager.entityMovables.Values)
            {
                //LogWarp.LogErrorFormat("{0}, {1}", val.entityID, val.mainGameObject.GetInstanceID());
                val.Tick(deltaTimeMS);
            }
            //LogWarp.LogErrorFormat("--------end tick{0}---------", GameManager.GetInstance().tickCount);

            entityManager.AfterTickMoveables();
        }


        public EntityManager entityManager;
    }

    public class EntityManager : Singleton<EntityManager>, ISingleton
    {
        EntityManagerTick tickObj;
        private static int s_entityID = 0;

        public Dictionary<int, EntityMovable> entityMovables;
        protected List<EntityMovable> entityMovableAdds;
        protected List<EntityMovable> entityMovableRemoves;

        public void Init()
        {
            this.entityMovables = new Dictionary<int, EntityMovable>();
            this.entityMovableAdds = new List<EntityMovable>();
            this.entityMovableRemoves = new List<EntityMovable>();

            this.tickObj = new EntityManagerTick();
            this.tickObj.entityManager = this;

            this.Run();
        }

        public void Release()
        {
            foreach(var val in entityMovables.Values)
            {
                Release(val);
            }
            entityMovables.Clear();

            foreach (var val in entityMovableAdds)
            {
                Release(val);
            }
            entityMovableAdds.Clear();

            foreach (var val in entityMovableRemoves)
            {
                Release(val);
            }
            entityMovableRemoves.Clear();

            //EntityVisitorCar.pool.RecoveryAll();
            //EntityVisitor.pool.RecoveryAll();
            //EntityAnimalWander.pool.RecoveryAll();
            //EntityShuttle.pool.RecoveryAll();
            //EntityShip.pool.RecoveryAll();
            //EntityGroundParkingCar.pool.RecoveryAll();
            //CrossRoadGame.EntityCrossRoadCar.pool.RecoveryAll();
        }

        public void Release(EntityMovable entity)
        {
            //清go
            var goPool = PoolManager.GetInstance().IsBelongGameObjectPool(entity.entityResType);
            goPool.OnRecovery(entity.mainGameObject);

            //清entity
            EntityFuncType eEntityFuncType = (EntityFuncType)entity.entityFuncType;
            switch (eEntityFuncType)
            {
                case EntityFuncType.CrossRoadCar:
                    CrossRoadGame.EntityCrossRoadCar.pool.Delete(entity as CrossRoadGame.EntityCrossRoadCar);
                    break;
                default:
                    string e = string.Format("没有这种 EntityFuncType {0}", eEntityFuncType);
                    throw new System.Exception(e);
            }
        }

        public static int GetNewEntityID()
        {
            if (s_entityID < int.MaxValue)
            {
                return s_entityID++;
            }

            s_entityID = 0;
            return s_entityID;
        }

        ///// <summary>
        ///// 根据策划资源表的restype随机生成
        ///// </summary>
        ///// <param name="resType"></param>
        ///// <param name="entityFuncType"></param>
        ///// <returns></returns>
        //public EntityGameObject GetRandomEntity(ResType resType, EntityFuncType entityFuncType)
        //{
        //    EntityGameObject entity = null;

        //    List<ResourceKeyCell> cellList = GlobalDataManager.GetInstance().logicTableResource.GetResListByResType((int)resType);
        //    if (cellList == null || cellList.Count <= 0)
        //    {
        //        string e = string.Format("cellList 异常 {0}", (int)resType);
        //        throw new System.Exception(e);
        //    }

        //    int idx = UnityEngine.Random.Range(0, cellList.Count);
        //    var cell = cellList[idx];

        //    entity = GenEntityGameObject(cell.key, entityFuncType);
        //    return entity;
        //}

        /// <summary>
        /// 根据策划资源表的主键和实体单位功能类型(与表无关)
        /// </summary>
        /// <param name="entityResType">对应策划资源表的主键</param>
        /// <param name="entityFuncType"></param>
        /// <returns></returns>
        public EntityGameObject GenEntityGameObject(int entityResType, EntityFuncType entityFuncType)
        {
            EntityGameObject entity = null;

            int ientityFuncType = (int)entityFuncType;

            switch (entityFuncType)
            {
                case EntityFuncType.CrossRoadCar:
                    entity = CrossRoadGame.EntityCrossRoadCar.pool.New();
                    break;
                case EntityFuncType.Animal_LittleGame:
                    entity = CrossRoadGame.EntityCrossRoadAnimal.pool.New();
                    break;
                default:
                    string e = string.Format("没有这种 EntityFuncType {0}", entityFuncType);
                    throw new System.Exception(e);
            }

            entity.entityFuncType = ientityFuncType;
            int newEntityID = GetNewEntityID();
#if UNITY_EDITOR
            if (entity.mainGameObject != null)
            {
                DebugFile.GetInstance().WriteKeyFile(newEntityID, "{0} get from pool oldEntityID={1}, mainGameObject={2} ",
                    newEntityID, entity.entityID, entity.mainGameObject.GetInstanceID());
                DebugFile.GetInstance().WriteKeyFile(entity.mainGameObject.GetInstanceID(), "{0} get from pool oldEntityID={1}, newEntityID={2} ",
                    entity.mainGameObject.GetInstanceID(), entity.entityID, newEntityID);
            }
#endif
            entity.entityID = newEntityID;


            DebugFile.GetInstance().WriteKeyFile(entity.entityID, "{0} GetNewEntityID", entity.entityID);
            if (entity.mainGameObject != null && entity.entityResType == entityResType)
            {
#if UNITY_EDITOR
                if (entityMovables.ContainsKey(entity.entityID))
                {
                    string e = string.Format("{0} 还在移动列表", entity.entityID);
                    throw new System.Exception(e);
                }
#endif
                //什么都不用干，用原来的mainGameObject
                DebugFile.GetInstance().WriteKeyFile(entity.entityID, "{0} old {1}", entity.entityID, entity.mainGameObject.GetInstanceID());
                DebugFile.GetInstance().WriteKeyFile(entity.mainGameObject.GetInstanceID(), "{0} old {1}", entity.mainGameObject.GetInstanceID(), entity.entityID);
                return entity;
            }

            GameObjectPool pool = null;
            if (entity.mainGameObject != null && entity.entityResType != entityResType)
            {
                //回收mainGameObject
                pool = PoolManager.GetInstance().IsBelongGameObjectPool(entity.entityResType);
                if (pool == null)
                {
                    string e = string.Format("异常,找不到回收的GameObjectPool {0}", entity.entityResType);
                    throw new System.Exception(e);
                }
                DebugFile.GetInstance().WriteKeyFile(entity.entityID, "{0} change {1}, pos {2}", entity.entityID, entity.mainGameObject.GetInstanceID(), entity.mainGameObject.transform.position);
                DebugFile.GetInstance().WriteKeyFile(entity.mainGameObject.GetInstanceID(), "{0} change {1}, pos {2}", entity.mainGameObject.GetInstanceID(), entity.entityID, entity.mainGameObject.transform.position);

                pool.Delete(entity.mainGameObject);
                entity.ClearCatchTrans();
            }

            entity.entityResType = (int)entityResType;
            pool = PoolManager.GetInstance().GetGameObjectPool(entity.entityResType);
            entity.mainGameObject = pool.New();
            DebugFile.GetInstance().WriteKeyFile(entity.entityID, "{0} new {1}", entity.entityID, entity.mainGameObject.GetInstanceID());
            DebugFile.GetInstance().WriteKeyFile(entity.mainGameObject.GetInstanceID(), "{0} new {1}", entity.mainGameObject.GetInstanceID(), entity.entityID);

            return entity;
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

        public void Run()
        {
            tickObj.Run();
        }

        public void Stop()
        {
            tickObj.Stop();
        }

        public void Pause()
        {
            tickObj.Pause();
        }

        public void Tick(int deltaTimeMS)
        {
            tickObj.Tick(deltaTimeMS);
        }

        public void BeforeTickMoveables()
        {
            foreach(var v in this.entityMovableAdds)
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
                switch(eEntityFuncType)
                {
                    case EntityFuncType.Animal_LittleGame:
                        CrossRoadGame.EntityCrossRoadAnimal.pool.Delete(v as CrossRoadGame.EntityCrossRoadAnimal);
                        break;
                    case EntityFuncType.CrossRoadCar:
                        CrossRoadGame.EntityCrossRoadCar.pool.Delete(v as CrossRoadGame.EntityCrossRoadCar);
                        break;
                    default:
                        string e = string.Format("没有这种 EntityFuncType {0}", eEntityFuncType);
                        throw new System.Exception(e);
                }
            }
            entityMovableRemoves.Clear();
        }

        public EntityMovable GetEntityMovable(int entityID)
        {
            EntityMovable entity;

            this.entityMovables.TryGetValue(entityID, out entity);

            return entity;
        }

    }
}
