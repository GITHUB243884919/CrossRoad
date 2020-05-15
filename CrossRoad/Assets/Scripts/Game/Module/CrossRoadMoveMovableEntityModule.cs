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
    /// <summary>
    /// 小游戏动物的控制类
    /// 接受消息哪个小游戏进行状态改变
    /// </summary>
    public partial class CrossRoadMoveMovableEntityModule : GameModule
    {
        public CrossRoadMoveMovableEntityModule(int orderID) : base(orderID) { }

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

        //Vector3 standardAnimalBoxSize {
        //    get {
        //        return CrossRoadModelManager.GetInstance().standardAnimalBoxSize;
        //    }
        //}

        public override void Init()
        {
            //LogWarp.LogErrorFormat("测试：    AnimalDataMoule   init ,  crossRoadList.Count={0} ", crossRoadList.Count);
            //MessageManager.GetInstance().Regist((int)GameMessageDefine.CrossRoadGameAnimalMove, this.OnGetCrossRoadGameAnimalMove);
            //MessageManager.GetInstance().Regist((int)GameMessageDefine.CrossRoadGameSingleRoadSucceed, this.OnGetCrossRoadGameSingleRoadSucceed);
        }
        public override void Release()
        {
            this.Stop();
            //MessageManager.GetInstance().UnRegist((int)GameMessageDefine.CrossRoadGameAnimalMove, this.OnGetCrossRoadGameAnimalMove);
            //MessageManager.GetInstance().UnRegist((int)GameMessageDefine.CrossRoadGameSingleRoadSucceed, this.OnGetCrossRoadGameSingleRoadSucceed);
        }
        public override void Tick(int deltaTimeMS)
        {
            if (!this.CouldRun())
            {
                return;
            }
            CrossRoadModelManager.GetInstance().entityModel.Tick(deltaTimeMS);
        }
        //int currentRoad = 0;
        //int animalSubscripts = -1;
        ///// <summary>
        ///// 收到单个小动物移动的消息
        ///// </summary>
        ///// <param name="obj"></param>
        //private void OnGetCrossRoadGameAnimalMove(Message obj)
        //{
        //    var msg = obj as CrossRoadGameAnimalMoveMessage;
        //    //LogWarp.LogErrorFormat("收到   控制小动物移动的消息，   实体={0}   下一个预计位置={1} ", StateLoadAnimal.entityCrossRoadAnimalList[msg.location].position, StateLoadAnimal.entityCrossRoadAnimalList[msg.location].endPos);
        //    if (msg.location==cellStage.animalnum-1)
        //    {
        //        StateLoadAnimal.entityCrossRoadAnimalList[msg.location].TestAnimalMove(true);
        //        currentRoad += 1;
        //    }
        //    else
        //    {
        //        StateLoadAnimal.entityCrossRoadAnimalList[msg.location].TestAnimalMove();

        //    }
        //}

        ///// <summary>
        ///// 收到单条马路小动物已经全部过完的消息
        ///// </summary>
        ///// <param name="obj"></param>
        //private void OnGetCrossRoadGameSingleRoadSucceed(Message obj)
        //{
        //    /*
        //     判断是否是全部马路过完
        //     A，过完单个小马路
        //         1.动物实体赋新值
        //         2.移动相机
        //         3.进行下一阶段的场景长按点击
        //     B，过完全部的小游戏
        //         1.场景弹出胜利界面
        //         2.奖励计算
        //         等等
        //      */
        //    int idx = currentRoad+1;
        //    if (cellStage.roadnum > idx)
        //    {
        //        //LogWarp.LogErrorFormat("测试：  当前马路条数   {0}", currentRoad);
        //        CrossRoadCameraController.GetInstance().MoveCamera(crossRoadList[currentRoad]);

        //        var ve = CrossRoadGameModule. GetMovetArgetVector3s(currentRoad);

        //        for (int i = 0; i < cellStage.animalnum; i++)
        //        {

        //            StateLoadAnimal.entityCrossRoadAnimalList[i].SetEntityInit(ve[i]);

        //            //LogWarp.LogErrorFormat("测试：     测试 动物{0}   现在位置{1}  目标位置{2}",i, StateLoadAnimal.entityCrossRoadAnimalList[i].startPos, StateLoadAnimal.entityCrossRoadAnimalList[i].endPos);

        //        }
                
        //        //移动相机

        //    }
        //    else if (cellStage.roadnum == idx)
        //    {
        //        LogWarp.LogErrorFormat("测试：  需要到达终点");
        //        var endAnimalPos = CrossRoadStageManager.GetInstance().endAnimalPos;

        //        CrossRoadCameraController.GetInstance().MoveCamera(crossRoadList[currentRoad]);

        //        for (int i = 0; i < cellStage.animalnum; i++)
        //        {

        //            StateLoadAnimal.entityCrossRoadAnimalList[i].SetEntityInit(endAnimalPos[i]);

        //            //LogWarp.LogErrorFormat("测试：     测试 动物{0}   现在位置{1}  目标位置{2}", i, StateLoadAnimal.entityCrossRoadAnimalList[i].startPos, StateLoadAnimal.entityCrossRoadAnimalList[i].endPos);

        //        }

        //    }
        //    else
        //    {
        //        LogWarp.LogErrorFormat("测试：  关卡完成 ");
        //        var endAnimalPos = CrossRoadStageManager.GetInstance().endAnimalPos;
        //        CrossRoadCameraController.GetInstance().MoveCamera(endAnimalPos[0]);

        //        PageMgr.ShowPage<UIGameVictoryPage>();

        //    }


        //}

    }



}
