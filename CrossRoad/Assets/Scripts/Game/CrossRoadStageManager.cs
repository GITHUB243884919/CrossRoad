using Game;
using Game.MessageCenter;
using Game.MiniGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UFrame;
using UFrame.Logger;
using UFrame.Common;
using UFrame.EntityFloat;
using UFrame.MessageCenter;
using UnityEngine;

namespace CrossRoadGame
{
    public class CrossRoadStageManager : SingletonMono<CrossRoadStageManager>
    {

        /// <summary>
        /// 测试关卡id
        /// </summary>
        public int TeststageID = Const.Invalid_Int;

        /// <summary>
        /// 动物动画播放速度
        /// </summary>
        public float animalAnimSpeed = 1f;

        /// <summary>
        /// 动物移动速度
        /// </summary>
        public float animalMoveSpeed = 5f;


		public float animalAcceleration = 0.5f;

		public float animalMaxSpeed = 10;

		public float animalMinSpeed = 10;

		/// <summary>
		/// 按钮长按时间
		/// </summary>
		public float buttonClickLongPressTime = 0.3f;

        /// <summary>
        /// 是否开启碰撞
        /// </summary>
        public bool isCollision = true;

        ///// <summary>
        ///// 动物的位置偏移Z
        ///// </summary>
        //public float animalPosOffsetZ = 10f;

        /// <summary>
        /// 汽车起点距离中心点的X方向的偏移量
        /// </summary>
        public float carStartOffsetX = 30;

        FSMCrossRoadGame fsmLoad = null;
        GameModules gameModules = null;
        int moduleOrderID = 0;
        public override void Awake()
        {
            base.Awake();
        }

        //public void Start()
        //{
        //    Init();
        //}

        public void Update()
        {
            int deltaMS = Math_F.FloatToInt1000(Time.deltaTime);
            MessageManager.GetInstance().Tick();
            fsmLoad.Tick(deltaMS);
            gameModules.Tick(deltaMS);
        }

        bool isInit = false;
        public void Init()
        {
            if (isInit)
            {
                return;
            }
            //初始化加载状态机
            fsmLoad = new FSMCrossRoadGame();
            fsmLoad.AddState(new StateLoadOrgScene((int)CrossRoadGameState.LoadOrgScene, fsmLoad));
            fsmLoad.AddState(new StateLoadSceneObject((int)CrossRoadGameState.LoadSceneObject, fsmLoad));
            fsmLoad.AddState(new StateLoadAnimal((int)CrossRoadGameState.LoadAnimal, fsmLoad));
            fsmLoad.Run();

            //module容器
            gameModules = new GameModules();
            //注册消息
            MessageManager.GetInstance().Regist((int)GameMessageDefine.LoadCrossRoadLevelFinished, OnLoadCrossRoadLevelFinished);
            MessageManager.GetInstance().Regist((int)GameMessageDefine.CrossRoadGameFailure, OnGetCrossRoadGameFailure);

            isInit = true;
        }

        public void Load(int stageID)
        {
            if (stageID < 0 || stageID > Config.crossroadstageConfig.getInstace().RowNum)
            {
                string e = string.Format("过马路关卡数不正确 {0}", stageID);
                throw new System.Exception(e);
            }

            CrossRoadModelManager.GetInstance().stageID = stageID;
            PageMgr.CloseAllPage(true, "");
            PageMgr.ShowPage<UICrossRoadLoading>();
            SetFSM("UICrossRoadLoading");
            LoadModule();
        }

        void SetFSM(string loadingPageName)
        {
            fsmLoad.loadingPageName = loadingPageName;
            fsmLoad.GotoState((int)CrossRoadGameState.LoadOrgScene);
        }

        void LoadModule()
        {
            //gameModules.AddMoudle(
            //    new PlayerDataModule(moduleOrderID++));
            gameModules.AddMoudle(
                new CrossRoadCarModule(moduleOrderID++));
            gameModules.AddMoudle(
                new CrossRoadMoveMovableEntityModule(moduleOrderID++));
            gameModules.AddMoudle(
                new CrossRoadAnimalTeamModule(moduleOrderID++));
        }

        public void UnLoad()
        {
            gameModules.Release();
            CrossRoadModelManager.GetInstance().Release();
            PoolManager.GetInstance().Release();

            Resources.UnloadUnusedAssets();
            System.GC.Collect();
        }

        public void Release()
        {
            MessageManager.GetInstance().UnRegist((int)GameMessageDefine.LoadCrossRoadLevelFinished, OnLoadCrossRoadLevelFinished);
            MessageManager.GetInstance().UnRegist((int)GameMessageDefine.CrossRoadGameFailure, OnGetCrossRoadGameFailure);

        }

        /// <summary>
        /// 小游戏关卡加载成功
        /// </summary>
        /// <param name="msg"></param>
        protected void OnLoadCrossRoadLevelFinished(Message msg)
        {
            gameModules.Run();
        }

        /// <summary>
        /// 小游戏失败
        /// </summary>
        /// <param name="obj"></param>
        private void OnGetCrossRoadGameFailure(Message obj)
        {
            gameModules.Stop();
            //弹出UI  失败
            LogWarp.LogError("游戏失败");

            PageMgr.ShowPage<UIGameFailPage>();


        }
    }
}

