
using Game.GlobalData;
using Game.MessageCenter;
using SWS;
using System;
using System.Collections;
using System.Collections.Generic;
using UFrame;
using UFrame.Common;
using UFrame.EntityFloat;
using UFrame.Logger;
using UFrame.MessageCenter;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace CrossRoadGame
{
    public class EntityCrossRoadAnimal : EntityMovable
    {
        public static ObjectPool<EntityCrossRoadAnimal> pool = new ObjectPool<EntityCrossRoadAnimal>();

        /// <summary>
        /// 动画
        /// </summary>
        public SimpleAnimation simpleAnimation = new SimpleAnimation();

        /// <summary>
        /// 行走动物播放时间  
        /// </summary>
        float animalAnimationSpeed;

        /// <summary>
        /// 实体在队伍中的位置编号
        /// </summary>
        public int idxInTeam = 0;

        /// <summary>
        /// 是否在移动
        /// </summary>
        bool isMoving = false;

        /// <summary>
        /// 目标位置
        /// </summary>
        public Vector3 targetPos;

        /// <summary>
        /// 过了马路（过马路最南边的点）
        /// </summary>
        bool isPassedRoad = false;

        /// <summary>
        /// 过了最后一个点(lastRoadPos)
        /// </summary>
        bool arrivedLastPos = false;

        /// <summary>
        /// 每个动物的标准尺寸（z值）
        /// </summary>
        Vector3 standardAnimalBoxSize {
            get {
                return CrossRoadModelManager.GetInstance().standardAnimalBoxSize;
            }
        }

        CrossRoadAnimalTeamModel animalTeamModel {
            get {
                return CrossRoadModelManager.GetInstance().animalTeamModel;
            }
        }

        RoadModel roadModel {
            get {
                return CrossRoadModelManager.GetInstance().roadModel;
            }
        }

        /// <summary>
        /// 当前路的第一个点
        /// </summary>
        Vector3 firstPos {
            get {
                return roadModel.animalRoadSegment[animalTeamModel.currentRoad]
                    + Vector3.back * standardAnimalBoxSize.z * 0.8f;
            }
        }

        /// <summary>
        /// 当前路下一条路的第一个点
        /// </summary>
        Vector3 nextFirstPos {
            get {
                return roadModel.animalRoadSegment[animalTeamModel.currentRoad + 1]
                    + Vector3.back * standardAnimalBoxSize.z * 0.8f;
            }
        }

        /// <summary>
        /// 在下一条路上对应的队伍位置的点
        /// </summary>
        Vector3 nextRoadPos {
            get {
                return nextFirstPos + Vector3.back * standardAnimalBoxSize.z * idxInTeam;
            }
        }

        /// <summary>
        /// 最后一条路过马路后点
        /// </summary>
        Vector3 lastRoadPos {
            get {
                return roadModel.animalRoadSegment[roadModel.animalRoadSegment.Count - 1]
                    + Vector3.forward * standardAnimalBoxSize.z * 0.8f;
            }
        }

        Vector3 preFirstPos {
            get {
                return roadModel.animalRoadSegment[animalTeamModel.currentRoad - 1]
                    + Vector3.back * standardAnimalBoxSize.z * 0.8f;
            }
        }
        /// <summary>
        /// 获取当前配置的过马路文件 crossroadstageCell
        /// </summary>
        Config.crossroadstageCell cellStage {
            get {
                int stageID = CrossRoadModelManager.GetInstance().stageID;
                return Config.crossroadstageConfig.getInstace().getCell(stageID);
            }
        }

        public void Init(float animalAnimationSpeed, float animalMoveSpeed, int idxInTeam)
        {
            this.animalAnimationSpeed = animalAnimationSpeed;
            this.moveSpeed = animalMoveSpeed * 0.001f;
            this.idxInTeam = idxInTeam;
            isMoving = false;
            isPassedRoad = false;
            arrivedLastPos = false;
            DebugFile.GetInstance().MarkGameObject(mainGameObject, "Animal-{0}", idxInTeam);
        }

        public override void Active()
        {
            base.Active();

            SetAnimation();
            PlayIdle();

            MessageManager.GetInstance().Regist((int)GameMessageDefine.CrossRoadAnimalTeamMove,
                OnCrossRoadAnimalTeamMove);
            MessageManager.GetInstance().Regist((int)GameMessageDefine.CrossRoadAnimalTeamStopMove,
                OnCrossRoadAnimalTeamStopMove);

        }

        public override void Deactive()
        {
            this.position = Const.Invisible_Postion;

            MessageManager.GetInstance().UnRegist((int)GameMessageDefine.CrossRoadAnimalTeamMove,
                OnCrossRoadAnimalTeamMove);
            MessageManager.GetInstance().UnRegist((int)GameMessageDefine.CrossRoadAnimalTeamStopMove,
                OnCrossRoadAnimalTeamStopMove);

            base.Deactive();
        }

        void NavTest(GameObject mainGameObject)
        {
            //mainGameObject.GetComponentInChildren<NavMeshAgent>().destination
            //    = CrossRoadModelManager.GetInstance().endAnimalTransferPos;
            var navAgent = mainGameObject.GetComponent<NavMeshAgent>();
            if (navAgent == null)
            {
                navAgent = mainGameObject.AddComponent<NavMeshAgent>();
            }
            navAgent.speed = moveSpeed * 1000;
            navAgent.destination = CrossRoadModelManager.GetInstance().endAnimalPos[14];
        }

        public override void Tick(int deltaTimeMS)
        {
            if (!this.CouldActive())
            {
                return;
            }

            if (!isMoving)
            {
                return;
            }

            if (arrivedLastPos)
            {
                return;
            }

            if (animalTeamModel.currentRoad < cellStage.roadnum - 1)
            {
                //过马路记录
                if (IsPassed(firstPos))
                {
                    isPassedRoad = true;
                    animalTeamModel.passedCurrRoadSet.Add(idxInTeam);
                    DebugFile.GetInstance().WriteKeyFile(string.Format("Animal-{0}", idxInTeam),
                        "PassedFirstPos, currentRoad={0}", animalTeamModel.currentRoad);
                }

                //到达终点记录
                if (IsPassed(nextRoadPos))
                {
                    //所有人到达
                    if (idxInTeam == animalTeamModel.entityCrossRoadAnimalList.Count - 1)
                    {
                        CrossRoadCameraController.GetInstance().MoveCamera(nextFirstPos);
                        animalTeamModel.passedCurrRoadSet.Clear();
                        ++animalTeamModel.currentRoad;
                        DebugFile.GetInstance().WriteKeyFile("CrossRoad", "currentRoad = {0}", animalTeamModel.currentRoad);
                        for (int i = 0; i < animalTeamModel.entityCrossRoadAnimalList.Count; i++)
                        {
                            var animal = animalTeamModel.entityCrossRoadAnimalList[i];
                            animal.isMoving = false;
                            animal.isPassedRoad = false;
                            animal.moveSpeed = 20 * 0.001f;
                            DebugFile.GetInstance().WriteKeyFile(string.Format("Animal-{0}", idxInTeam),
                                "isMoving = false {0}, currentRoad={1}", 1, animalTeamModel.currentRoad);
                        }

                    }
                    position = targetPos;
                    isMoving = false;
                    DebugFile.GetInstance().WriteKeyFile(string.Format("Animal-{0}", idxInTeam),
                        "isMoving = false {0}, currentRoad={1}", 2, animalTeamModel.currentRoad);
                    PlayIdle();
                    return;
                }
            }
            else if (animalTeamModel.currentRoad == cellStage.roadnum - 1)
            {
                if (IsPassed(firstPos))
                {
                    isPassedRoad = true;
                    animalTeamModel.passedCurrRoadSet.Add(idxInTeam);
                }

                if (IsPassed(lastRoadPos))
                {
                    PlayWalk();
                    isMoving = false;
                    position = lastRoadPos;
                    arrivedLastPos = true;
                    SWS_Walk(mainGameObject, idxInTeam);
                    if (idxInTeam == animalTeamModel.entityCrossRoadAnimalList.Count - 1)
                    {
                        ++animalTeamModel.currentRoad;
                        CrossRoadCameraController.GetInstance().MoveCamera(
                            CrossRoadModelManager.GetInstance().endAnimalTransferPos + Vector3.forward * 50f);
                    }
                    return;
                }
            }
            else
            {
                return;
            }

            //到达目标点停止
            if (IsPassed(targetPos))
            {
                position = targetPos;
                isMoving = false;
                DebugFile.GetInstance().WriteKeyFile(string.Format("Animal-{0}", idxInTeam),
                    "isMoving = false {0}, currentRoad={1}", 3, animalTeamModel.currentRoad);
                PlayIdle();
                return;
            }


            //position += Vector3.forward * moveSpeed * deltaTimeMS;

            ////跨马路的时候速度最快
            //if (IsPassed(firstPos))
            //{
            //    position += Vector3.forward * moveSpeed * deltaTimeMS;
            //}
            //else
            //{
            //    position += Vector3.forward * moveSpeed * 0.4f * deltaTimeMS;
            //}

            //跨马路的时候加速
            if (IsPassed(firstPos))
            {
                //匀加速或者减速
                float animalAcceleration = CrossRoadStageManager.GetInstance().animalAcceleration;
                float animalMaxSpeed = CrossRoadStageManager.GetInstance().animalMaxSpeed;
                float animalMinSpeed = CrossRoadStageManager.GetInstance().animalMinSpeed;

                Vector3 old = Vector3.forward * moveSpeed * deltaTimeMS;
                float delta = (moveSpeed + animalAcceleration * 0.001f) * deltaTimeMS;
                float max = animalMaxSpeed * 0.001f * deltaTimeMS;
                float min = animalMinSpeed * 0.001f * deltaTimeMS;
                if (animalAcceleration > 0)
                {
                    delta = Mathf.Min(delta, max);
                }
                else
                {
                    delta = Mathf.Max(delta, min);
                }
                position += Vector3.forward * delta;
                moveSpeed = delta / deltaTimeMS;
            }
            else
            {
                position += Vector3.forward * moveSpeed * deltaTimeMS;
            }


            ////前面的比后面的快
            //float delta = moveSpeed * (1 - idxInTeam / 15f);
            //position += Vector3.forward * delta * deltaTimeMS;
            //Debug.LogErrorFormat("{0}, {1}, {2}", idxInTeam, moveSpeed, delta);			

            ////匀加速或者减速
            //float animalAcceleration = CrossRoadStageManager.GetInstance().animalAcceleration;
            //float animalMaxSpeed = CrossRoadStageManager.GetInstance().animalMaxSpeed;
            //float animalMinSpeed = CrossRoadStageManager.GetInstance().animalMinSpeed;

            //Vector3 old = Vector3.forward * moveSpeed * deltaTimeMS;
            //float delta = (moveSpeed + animalAcceleration * 0.001f) * deltaTimeMS;
            //float max = animalMaxSpeed * 0.001f * deltaTimeMS;
            //float min = animalMinSpeed * 0.001f * deltaTimeMS;
            //if (animalAcceleration > 0) 
            //{
            //	delta = Mathf.Min(delta, max);
            //}
            //else 
            //{
            //	delta = Mathf.Max(delta, min);
            //}
            //position += Vector3.forward * delta;
            //moveSpeed = delta / deltaTimeMS;
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

        public void PlayWalk()
        {
            simpleAnimation.Play("walk");
        }

        public void PlayPose()
        {
            simpleAnimation.Play("pose");
        }

        public void PlayIdle()
        {
            simpleAnimation.Play("idle");
        }

        /// <summary>
        /// 设置动画循环播放
        /// </summary>
        private void SetAnimation()
        {
            simpleAnimation.SetAnimSpeed("walk", animalAnimationSpeed);
            simpleAnimation.SetAnimLoop("walk");
            simpleAnimation.SetAnimLoop("pose");
            simpleAnimation.SetAnimLoop("idle");
        }

        protected void OnCrossRoadAnimalTeamMove(Message msg)
        {
            Vector3 pos = Vector3.zero;
            if (animalTeamModel.currentRoad == cellStage.roadnum - 1)
            {
                pos = lastRoadPos;
            }
            else if (animalTeamModel.currentRoad < cellStage.roadnum - 1)
            {
                pos = nextRoadPos;
            }
            else
            {
                return;
            }

            if (arrivedLastPos)
            {
                return;
            }

            DebugFile.GetInstance().WriteKeyFile(string.Format("Animal-{0}", idxInTeam),
                "OnCrossRoadAnimalTeamMove {0}", 1);

            if (isMoving)
            {
                return;
            }
            isMoving = true;
            DebugFile.GetInstance().WriteKeyFile(string.Format("Animal-{0}", idxInTeam),
                "OnCrossRoadAnimalTeamMove {0}, currentRoad={1}", 2, animalTeamModel.currentRoad);
            PlayWalk();
            //下一段路对应的位置
            targetPos = pos;

        }

        //protected IEnumerator CoWaitAsync(Vector3 pos)
        //{
        //    //yield return RunCoroutine.WaitForEndOfFrame;
        //    yield return new WaitForSeconds(0.01f);
        //    isMoving = true;
        //    DebugFile.GetInstance().WriteKeyFile(string.Format("Animal-{0}", idxInTeam),
        //        "OnCrossRoadAnimalTeamMove {0}, currentRoad={1}", 2, animalTeamModel.currentRoad);
        //    PlayWalk();
        //    //下一段路对应的位置
        //    targetPos = pos;
        //}

        protected void OnCrossRoadAnimalTeamStopMove(Message msg)
        {
            //第一位的忽略
            if (idxInTeam == 0)
            {
                return;
            }

            DebugFile.GetInstance().WriteKeyFile(string.Format("Animal-{0}", idxInTeam),
                "OnCrossRoadAnimalTeamStopMove {0}", 2);

            //已经跨过马路的忽略
            if (isPassedRoad)
            {
                return;
            }

            DebugFile.GetInstance().WriteKeyFile(string.Format("Animal-{0}", idxInTeam),
                "OnCrossRoadAnimalTeamStopMove {0}, currentRoad={1}", 3, animalTeamModel.currentRoad);
            //没有过马路的，设置目标位置：当前路的位置
            int idx = idxInTeam - animalTeamModel.passedCurrRoadSet.Count;
            targetPos = firstPos + Vector3.back * standardAnimalBoxSize.z * idx;
        }

        protected bool IsPassed(Vector3 pos)
        {
            return position.z >= pos.z || Math_F.ApproximateNumber(position.z, pos.z, 0.001f);
        }

        void SWS_Walk(GameObject mainGameObject, int idx)
        {
            var spm = mainGameObject.GetComponent<splineMove>();
            if (spm == null)
            {
                spm = mainGameObject.AddComponent<splineMove>();
            }
            spm.speed = moveSpeed * 1000f;
            //create path manager game object
            GameObject newPath = new GameObject(string.Format("Path{0} (Runtime Creation)", idx));
            PathManager path = newPath.AddComponent<PathManager>();

            //declare waypoint positions
            Vector3[] positions = new Vector3[] { position,
                CrossRoadModelManager.GetInstance().endAnimalTransferPos,
                CrossRoadModelManager.GetInstance().endAnimalPos[idx] };
            Transform[] waypoints = new Transform[positions.Length];

            //instantiate waypoints
            for (int i = 0; i < positions.Length; i++)
            {
                GameObject newPoint = new GameObject("Waypoint " + i);
                waypoints[i] = newPoint.transform;
                waypoints[i].position = positions[i];
            }

            //assign waypoints to path
            path.Create(waypoints, true);

            spm.SetPath(path);
            spm.StartMove();

            UnityEvent SWS_Event = spm.events[2];
            SWS_Event.RemoveAllListeners();
            SWS_Event.AddListener(
                delegate {
                    PlayPose();
                }
            );
        }
    }
}
