using DG.Tweening;
using Game;
using Game.MessageCenter;
using System;
using System.Collections;
using System.Collections.Generic;
using UFrame;
using UFrame.Logger;
using UFrame.MessageCenter;
using UnityEngine;

namespace CrossRoadGame
{
    public partial class CrossRoadAnimalTeamModule : GameModule
    {
        public CrossRoadAnimalTeamModule(int orderID) : base(orderID) { }

        /// <summary>
        /// 每条马路的第一个位置列表
        /// </summary>
        List<Vector3> crossRoadList {
            get {
                return CrossRoadModelManager.GetInstance().roadModel.animalRoadSegment;
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

        public override void Init()
        {
            //MessageManager.GetInstance().Regist((int)GameMessageDefine.CrossRoadAnimalTeamLeaderMove, this.OnCrossRoadAnimalTeamLeaderMove);
        }

        public override void Release()
        {
            this.Stop();
            //MessageManager.GetInstance().UnRegist((int)GameMessageDefine.CrossRoadAnimalTeamLeaderMove, this.OnCrossRoadAnimalTeamLeaderMove);
        }


        public override void Tick(int deltaTimeMS)
        {
            if (!this.CouldRun())
            {
                return;
            }
            CrossRoadModelManager.GetInstance().entityModel.Tick(deltaTimeMS);
        }

        protected void OnCrossRoadAnimalTeamLeaderMove(Message msg)
        {

        }
    }
}
