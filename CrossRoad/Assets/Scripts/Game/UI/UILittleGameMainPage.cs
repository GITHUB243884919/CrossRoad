using Game.GlobalData;
using Game.MessageCenter;
using System;
using System.Collections;
using System.Collections.Generic;
using UFrame;
using UFrame.MessageCenter;
using UFrame.MiniGame;
using UnityEngine;
using UnityEngine.UI;

namespace CrossRoadGame
{
    public class UILittleGameMainPage : UIPage
    {
        Image uIBackgroundImage;
        Button reviveButton;
        private Text money_1_Text;
        private Text money_2_Text;
        private Text numberText;
        PlayerData playerData;
        Sprite sprite;
        private Slider scheduleSlider;
        private Text scheduleSlider_text;

        /// <summary>
        /// 获取当前配置的过马路文件 crossroadstageCell
        /// </summary>
        Config.crossroadstageCell cellStage {
            get {
                int stageID = CrossRoadModelManager.GetInstance().stageID;
                return Config.crossroadstageConfig.getInstace().getCell(stageID);
            }
        }

        int singleRoadSucceed =0;
        public UILittleGameMainPage() : base(UIType.Fixed, UIMode.DoNothing, UICollider.None)
        {
            uiPath = "UIPrefab/UIGame";
        }
        public override void Awake(GameObject go)
        {
            base.Awake(go);
            GetTransPrefabAllTextShow(this.transform, true);
            
            MessageManager.GetInstance().Regist((int)GameMessageDefine.CrossRoadCameraStopMove, this.OnGetCrossRoadCameraStopMove);
            MessageManager.GetInstance().Regist((int)GameMessageDefine.CrossRoadGameSingleRoadSucceed, this.OnGetCrossRoadGameSingleRoadSucceed);

            RegistAllCompent();

        }
        public override void Active()
        {
            base.Active();
            uIBackgroundImage.gameObject.SetActive(true);
            reviveButton.gameObject.SetActive(true);
            IninCompentData();

        }
        public override void Hide()
        {
            base.Hide();
            IninCompentData();

            //MessageManager.GetInstance().UnRegist((int)GameMessageDefine.CrossRoadCameraStopMove, this.OnGetCrossRoadCameraStopMove);
            //MessageManager.GetInstance().UnRegist((int)GameMessageDefine.CrossRoadGameSingleRoadSucceed, this.OnGetCrossRoadGameSingleRoadSucceed);
        }
        private void OnGetCrossRoadGameSingleRoadSucceed(Message obj)
        {
            singleRoadSucceed += 1;
            float value = AddPercentage(singleRoadSucceed, cellStage.roadnum);
            scheduleSlider.value = value;
            scheduleSlider_text.text = value * 100+"%";
            numberText.text = string.Format( GetL10NString("Ui_Text_123"), CrossRoadModelManager.GetInstance().stageID);
        }

        /// <summary>
        /// 内部组件的查找
        /// </summary>
        private void RegistAllCompent()
        {
            uIBackgroundImage = RegistCompent<Image>("UIBackgroundImage");
            reviveButton = RegistBtnAndClick("ReviveButton", OnClickReceiveButton);
            uIBackgroundImage.gameObject.SetActive(true);
            reviveButton.gameObject.SetActive(true);

            scheduleSlider = RegistCompent<Slider>("UP/ScheduleSlider");
            scheduleSlider_text = RegistCompent<Text>("UP/ScheduleSlider/FillArea/ValueText");
            numberText = RegistCompent<Text>("UP/MoneyGroup/NumberText");
            SetCorrectShowImage();
        }

      
        /// <summary>
        /// 修改对应的UiImage的sprite
        /// </summary>
        private void SetCorrectShowImage()
        {
            //int sceneID = GlobalDataManager.GetInstance().playerData.playerZoo.currSceneID;
            //int scenetype = Config.sceneConfig.getInstace().getCell(sceneID).moneyid;
            string iconPath = Config.moneyConfig.getInstace().getCell(0).moneyicon;
            sprite = ResourceManager.LoadSpriteFromPrefab(iconPath);
            

        }
        /// <summary>
        /// 相机移动结束
        /// </summary>
        /// <param name="obj"></param>
        private void OnGetCrossRoadCameraStopMove(Message obj)
        {
            uIBackgroundImage.gameObject.SetActive(false);
        }

        /// <summary>
        /// 开始游戏隐藏按钮
        /// </summary>
        /// <param name="obj"></param>
        private void OnClickReceiveButton(string obj)
        {
            uIBackgroundImage.gameObject.SetActive(false);
            reviveButton.gameObject.SetActive(false);
            //UFrame.MessageManager.GetInstance().Send((int)GameMessageDefine.CrossRoadStartGame);

        }


        private void IninCompentData()
        {
            singleRoadSucceed = 0;
            scheduleSlider.value = 0;
            scheduleSlider_text.text = "0%";
            numberText.text = string.Format(GetL10NString("Ui_Text_123"), CrossRoadModelManager.GetInstance().stageID);
        }
        /// <summary>
        /// 获取金钱改变
        /// </summary>
        /// <param name="obj"></param>
        private void OnGetBroadcastCoinOfPlayerDataMSSC(Message obj)
        {
            //money_1_Text.text = playerData.playerZoo.playerCoin.GetCoinByScene(playerData.playerZoo.currSceneID).coinShow;
            //money_2_Text.text = "0";
        }
    }
}

