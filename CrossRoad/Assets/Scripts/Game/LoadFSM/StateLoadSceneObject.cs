using UnityEngine;
using UFrame;
using UFrame.MiniGame;
using Game;
using System.Collections.Generic;

namespace CrossRoadGame
{
    /// <summary>
    /// 加载场景物件 路/终点
    /// 构建所有路的rect区域(一个路的资源一个), 和每条路的最南面的中点(一个路的资源一个)
    /// 构造生成车的相关数据，具体车的生成有module接管
    /// </summary>
    public class StateLoadSceneObject : FSMState
    {
        bool isToStateLoadAnimal = false;
        float preHalfWidth = 0;
        float preOffset = 0f;
        Vector3 lastRoadPos = Vector3.zero;
        GameObject endPos;

        List<string> animalRoadResPath = new List<string>();

        string loadingPageName
        {
            get
            {
                return (this.fsmCtr as FSMCrossRoadGame).loadingPageName;
            }
        }

        int stageID
        {
            get
            {
                return CrossRoadModelManager.GetInstance().stageID;
            }
        }

        public RoadModel roadModel
        {
            get
            {
                return CrossRoadModelManager.GetInstance().roadModel;
            }
        }

        public Vector3 startPoint
        {
            get
            {
                return CrossRoadModelManager.GetInstance().startPoint;
            }
        }

        public StateLoadSceneObject(int stateName, FSMMachine fsmCtr) :
            base(stateName, fsmCtr)
        {
        }

        public override void Enter(int preStateName)
        {
            base.Enter(preStateName);

            preHalfWidth = 0;
            preOffset = 0f;
            lastRoadPos = Vector3.zero;

            isToStateLoadAnimal = false;

            Load();
            
            isToStateLoadAnimal = true;
        }

        public override void Tick(int deltaTimeMS)
        {
        }

        public override void Leave()
        {
            (this.fsmCtr as FSMCrossRoadGame).SetLoadingPageSlider();
            base.Leave();
        }

        public override void AddAllConvertCond()
        {
            AddConvertCond((int)CrossRoadGameState.LoadAnimal, ToStateLoadAnimal);
        }

        protected bool ToStateLoadAnimal()
        {
            return isToStateLoadAnimal;
        }

        protected void Load()
        {
            LoadRoad();
            LoadEndPos();
            LoadNavMesh();
        }

        void LoadNavMesh()
        {
            //ResourceManager.GetInstance().LoadGameObject("Plank 1").transform.position = endPos.transform.position + Vector3.back * 50;
            //ResourceManager.GetInstance().LoadGameObject("LocalNavMeshBuilder").transform.position = endPos.transform.position + Vector3.back * 50;
            NavMeshSourceTag[] nvs = endPos.GetComponentsInChildren<NavMeshSourceTag>();
            for(int i = 0; i < nvs.Length; i++)
            {
                nvs[i].enabled = true;
            }
        }

        /// <summary>
        /// 加载路，并存储路的rect区域
        /// </summary>
        protected void LoadRoad()
        {
            roadModel.Release();
            var animalRoad = LoadAnimalRoad();
            roadModel.spaceRoad = FSMCrossRoadGame.GetSize_Z(animalRoad);

            var cellStage = Config.crossroadstageConfig.getInstace().getCell(stageID);
            for (int i = 0; i < cellStage.roadnum; i++)
            {
                int numRoad = cellStage.roadweightmin;
                if (cellStage.roadweightmin != cellStage.roadweightmax)
                {
                    numRoad = Random.Range(cellStage.roadweightmin, cellStage.roadweightmax + 1);
                }

                int roadResID = Config.crossroadroadConfig.getInstace().getCell(numRoad).resid;

                var road = ResourceManager.GetInstance().LoadGameObject(Config.resourceConfig.getInstace().getCell(roadResID).prefabpath);


                int carInterval = Math_F.FloatToInt1000(Random.Range(cellStage.Intervalmin, cellStage.Intervalmax));

                lastRoadPos = startPoint + (roadModel.spaceRoad * i * Vector3.forward);
                Vector3 bcSize = road.GetComponent<BoxCollider>().size;

                float halfWidth = bcSize.z * 0.5f;
                if (i > 0)
                {
                    lastRoadPos += (preHalfWidth + halfWidth + preOffset) * Vector3.forward;
                    preOffset += (preHalfWidth + halfWidth);
                }

                preHalfWidth = halfWidth;
                var roadRect = new Rect(
                    lastRoadPos.x, lastRoadPos.z,
                    bcSize.x, bcSize.z);
                roadModel.AddCrossRoad(
                    numRoad, roadRect, lastRoadPos, lastRoadPos + (Vector3.left * bcSize.x * 0.5f),
                    lastRoadPos + (Vector3.right * bcSize.x * 0.5f),
                    carInterval, RoadDir.LeftToRight, cellStage.speedmin, cellStage.speedmax);

                road.transform.position = lastRoadPos;

                if (i < cellStage.roadnum - 1)
                {
                    roadModel.AddToAnimalRoadSegment(lastRoadPos, roadRect, false);
                }
                else
                {
                    roadModel.AddToAnimalRoadSegment(lastRoadPos, roadRect, true);
                }
#if UNITY_EDITOR 
                road.name = string.Format("CarRoad-{0}-{1}", i, halfWidth);
#endif

                animalRoad.transform.position = lastRoadPos - Vector3.forward * 0.5f * (roadModel.spaceRoad + bcSize.z);
#if UNITY_EDITOR
                animalRoad.name = string.Format("AnimalRoad-{0}", i);
#endif
                if (i < cellStage.roadnum - 1)
                {
                    animalRoad = LoadAnimalRoad();
                }
            }
        }

        protected void LoadEndPos()
        {
            endPos = ResourceManager.GetInstance().LoadGameObject(Config.globalConfig.getInstace().CrossRoadSceneEnd);
            endPos.transform.position = lastRoadPos
                + Vector3.forward * (endPos.GetComponent<BoxCollider>().size.z * 0.5f + preHalfWidth);
            GetEndAnimalePos();
        }

        void GetEndAnimalePos()
        {
            List<Vector3> vector3s = new List<Vector3>();
            if (endPos != null)
            {
                var tableView = endPos.transform.Find("AnimalStopSpot");
                foreach (Transform child in tableView)
                {
                    vector3s.Add(child.position);
                }
                CrossRoadModelManager.GetInstance().endAnimalPos = vector3s;
                Transform animalGatherSpot = endPos.transform.Find("AnimalGatherSpot");
                CrossRoadModelManager.GetInstance().endAnimalTransferPos = animalGatherSpot.position;
            }

        }

        protected GameObject LoadAnimalRoad()
        {
            if (animalRoadResPath.Count == 0)
            {
                foreach (var val in Config.resourceConfig.getInstace().AllData.Values)
                {
                    if (val.restype == (int)ResType.CrossRoadAnimalRoad)
                    {
                        animalRoadResPath.Add(val.prefabpath);
                    }
                }
            }

            int idx = Random.Range(0, animalRoadResPath.Count);
            GameObject go = ResourceManager.GetInstance().LoadGameObject(animalRoadResPath[idx]);
            
            //go = FSMCrossRoadGame.Scale_Z(go, CrossRoadModelManager.GetInstance().standardAnimalBoxSize.z * Config.crossroadstageConfig.getInstace().getCell(stageID).animalnum);
            return go;
        }
    }
}
