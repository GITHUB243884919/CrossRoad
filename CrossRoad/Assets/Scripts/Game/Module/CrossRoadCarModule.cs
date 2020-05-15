using Game;
using System.Collections;
using System.Collections.Generic;
using UFrame;
using UnityEngine;

namespace CrossRoadGame
{
    public class SpawnCarParam : IVoidParam
    {
        public Vector3 startPos;
        public Vector3 endPos;
        public float speed;
    }

    public class CrossRoadCarModule : GameModule
    {
        public CrossRoadCarModule(int orderID) : base(orderID) { }

        VoidParamIntCDs multiCD = null;

        List<CrossRoad> crossRoadList
        {
            get
            {
                return CrossRoadModelManager.GetInstance().roadModel.crossRoadList;
            }
        }

        public override void Init()
        {
        }

        public override void Release()
        {
            multiCD.Release();
        }

        public override void Tick(int deltaTimeMS)
        {
            if (!CouldRun())
            {
                return;
            }

            //if (multiCD != null)
            //{
            //    multiCD.Tick(deltaTimeMS);
            //}
        }

        public override void Run()
        {
            base.Run();
            if (multiCD == null)
            {
                multiCD = new VoidParamIntCDs();
                for (int i = 0; i < crossRoadList.Count; i++)
                {
                    var crossRoad = crossRoadList[i];
                    var sParam = new SpawnCarParam()
                    {
                        speed = crossRoad.carSpeed,
                        startPos = crossRoad.startPos,
                        endPos = crossRoad.endPos,
                    };
                    multiCD.AddCD(crossRoad.spawnCarCDVal, sParam, Callback_SpawnCar);
                    //Callback_SpawnCar(null, sParam);
                }
                multiCD.Run();
            }
        }

        public override void Stop()
        {
            base.Stop();
            if (multiCD != null)
            {
                multiCD.Stop();
            }
        }

        public override void Pause()
        {
            base.Pause();
            if (multiCD != null)
            {
                multiCD.Pause();
            }
        }

        protected void Callback_SpawnCar(IntCD CD, IVoidParam spawnCarParam)
        {
            SpawnCarParam _spawnCarParam = spawnCarParam as SpawnCarParam;
            var car = EntityManager.GetInstance().GenEntityGameObject(1, EntityFuncType.CrossRoadCar) as EntityCrossRoadCar;

            //var car = EntityManager.GetInstance().GetRandomEntity(ResType.Car, EntityFuncType.CrossRoadCar) as EntityCrossRoadCar;

            car.Init(_spawnCarParam.startPos, _spawnCarParam.endPos, _spawnCarParam.speed);
            //重要：Entity取出后先设置位置，再把Collider设置成true，防止在未设置位置时触发碰撞
            car.position = _spawnCarParam.startPos;
            try
            {
                car.GetTrans().Find("Collider").gameObject.SetActive(true);
            }
            catch (System.Exception e)
            {
#if UNITY_EDITOR
                throw new System.Exception(string.Format("汽车 {0} 取Collider 失败", car.mainGameObject.name));
#endif
            }


            car.LookAt(_spawnCarParam.endPos);
            car.Active();

            CrossRoadModelManager.GetInstance().entityModel.AddToEntityMovables(car);
            if (CD != null)
            {
                CD.Reset();
                CD.Run();
            }
        }
    }
}

