using Game.MessageCenter;
using System.Collections.Generic;
using UFrame;
using UFrame.MiniGame;
using UnityEngine;
using Game;
using UFrame.Logger;
using System;
using Game.GlobalData;
using UFrame.MessageCenter;

namespace CrossRoadGame
{
    /// <summary>
    /// 加载动物
    /// </summary>
    public class StateLoadAnimal : FSMState
    {
        ///// <summary>
        ///// 所有动物实体列表(按照顺序排队)
        ///// </summary>
        //public static List<EntityCrossRoadAnimal> entityCrossRoadAnimalList;

        public StateLoadAnimal(int stateName, FSMMachine fsmCtr) :
            base(stateName, fsmCtr)
        {
        }

        /// <summary>
        /// 获取各个马路的第一个位置列表 
        /// </summary>
        List<Vector3> animalRoadSegment
        {
            get {
                return CrossRoadModelManager.GetInstance().roadModel.animalRoadSegment;
            }
        }

        Vector3 standardAnimalBoxSize
        {
            get
            {
                return CrossRoadModelManager.GetInstance().standardAnimalBoxSize;
            }
        }

        /// <summary>
        /// 行走动物播放时间  
        /// </summary>
        float animalAnimationSpeed {
            get {
                return CrossRoadStageManager.GetInstance().animalAnimSpeed;
            }
        }

        /// <summary>
        /// 行走动物移动速度
        /// </summary>
        float animalMoveSpeed {
            get {
                return CrossRoadStageManager.GetInstance().animalMoveSpeed;
            }
        }

        CrossRoadAnimalTeamModel animalTeamModel
        {
            get
            {
                return CrossRoadModelManager.GetInstance().animalTeamModel;
            }
        }

        public override void Enter(int preStateName)
        {
            base.Enter(preStateName);
            //MessageManager.GetInstance().Regist((int)GameMessageDefine.SetCrossRoadAnimalObjectData, this.OnSetCrossRoadAnimalObjectData);
            OnLoadAnimal(null);

            WhenLoadFinish();
        }

        protected void OnLoadAnimal(Message msg)
        {
            for (int i = 0; i < 15; i++)
            {
                var animal = EntityManager.GetInstance().GenEntityGameObject(
                    2201, EntityFuncType.Animal_LittleGame) as EntityCrossRoadAnimal;
                animal.Init(animalAnimationSpeed, animalMoveSpeed, i);
                animal.position = animalRoadSegment[animalTeamModel.currentRoad]
                    + Vector3.back * (standardAnimalBoxSize.z * i + standardAnimalBoxSize.z * 0.8f);
                //animal.position = animalRoadSegment[animalTeamModel.currentRoad]
                //    + Vector3.back * (standardAnimalBoxSize.z * i);
                GameObject colliderGB = animal.GetTrans().Find("Collider").gameObject;
                colliderGB.SetActive(true);
                FSMCrossRoadGame.Scale_Z(animal.mainGameObject, standardAnimalBoxSize.z);
                //animal.position = new Vector3(animal.position.x, colliderGB.transform.position.y * -1, animal.position.z);
                animal.position = new Vector3(animal.position.x, 0, animal.position.z);
                if (animal.simpleAnimation != null)
                {
                    animal.simpleAnimation.Init(animal.mainGameObject);
                }

                if (colliderGB.GetComponent<Rigidbody>() == null)
                {
                    colliderGB.AddComponent<Rigidbody>();
                    SetRigidbody(colliderGB);
                }

                if (colliderGB.GetComponent<AnimalCollisionEnterHelp>() == null)
                {
                    colliderGB.AddComponent<AnimalCollisionEnterHelp>();
                }

                animal.Active();
                animalTeamModel.entityCrossRoadAnimalList.Add(animal);
                CrossRoadModelManager.GetInstance().entityModel.AddToEntityMovables(animal);
            }
        }

        /// <summary>
        /// 设置刚体属性
        /// </summary>
        private void SetRigidbody(GameObject gameObject)
        {
            Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
            rigidbody.useGravity = false;
            rigidbody.isKinematic = false;
            rigidbody.mass = 1;
            rigidbody.angularDrag = 0f;
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
        }
        
        public override void Tick(int deltaTimeMS)
        {
    //        Debug.DrawLine(CrossRoadModelManager.GetInstance().spLBW,
    //CrossRoadModelManager.GetInstance().spLTW, Color.red);
    //        Debug.DrawLine(CrossRoadModelManager.GetInstance().spLTW,
    //            CrossRoadModelManager.GetInstance().spRTW, Color.red);
    //        Debug.DrawLine(CrossRoadModelManager.GetInstance().spRTW,
    //            CrossRoadModelManager.GetInstance().spRBW, Color.red);
    //        Debug.DrawLine(CrossRoadModelManager.GetInstance().spRBW,
    //            CrossRoadModelManager.GetInstance().spLBW, Color.red);
        }

        public override void Leave()
        {

            base.Leave();
        }

        public override void AddAllConvertCond()
        {
        }

        void WhenLoadFinish()
        {
            (this.fsmCtr as FSMCrossRoadGame).SetLoadingPageSlider();
            PageMgr.ClosePage((this.fsmCtr as FSMCrossRoadGame).loadingPageName);
            MessageManager.GetInstance().Send((int)GameMessageDefine.LoadCrossRoadLevelFinished);
        }
    }
}
