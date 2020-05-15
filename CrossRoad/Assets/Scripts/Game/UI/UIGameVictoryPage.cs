using Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UFrame.Logger;
using UFrame.MiniGame;
using Game.GlobalData;
using UFrame;
using Game.MessageCenter;
using UFrame.MessageCenter;

namespace CrossRoadGame
{
    /// <summary>
    /// 小游戏成功界面
    /// </summary>
    public class UIGameVictoryPage : UIPage
    {
        public UIGameVictoryPage() : base(UIType.Fixed, UIMode.DoNothing, UICollider.None)
        {
            uiPath = "UIPrefab/UIGameVictory";
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

        PlayerData playerData;

        /// <summary>
        /// 当前场景对应的金钱图标
        /// </summary>
        Sprite sprite;

        /// <summary>
        /// 返回主界面
        /// </summary>
        Button returnButton;
        /// <summary>
        /// 再来一次
        /// </summary>
        Button receiveButton;
        /// <summary>
        /// 玩家金钱
        /// </summary>
        Text money_1_Text;
        /// <summary>
        /// 玩家钻石
        /// </summary>
        Text money_2_Text;
        /// <summary>
        /// 收益金钱
        /// </summary>
        Text rewardGold_text;
        /// <summary>
        /// 收益钻石
        /// </summary>
        Text rewardRmb_text;

        Image money_1_GoldIcon;
        Image mewardGold_Image;
        public override void Awake(GameObject go)
        {
            base.Awake(go);
            GetTransPrefabAllTextShow(this.transform, true);
            playerData = GlobalDataManager.GetInstance().playerData;
            RegistAllCompent();
            //MessageManager.GetInstance().Regist((int)GameMessageDefine.BroadcastCoinOfPlayerDataMSSC, this.OnGetBroadcastCoinOfPlayerDataMSSC);//接受金钱变动的信息
            IninCompentData();
        }
        public override void Active()
        {
            base.Active();

        }
        public override void Hide()
        {
            base.Hide();
            //MessageManager.GetInstance().UnRegist((int)GameMessageDefine.BroadcastCoinOfPlayerDataMSSC, this.OnGetBroadcastCoinOfPlayerDataMSSC);//接受金钱变动的信息

        }
        /// <summary>
        /// 内部组件的查找
        /// </summary>
        private void RegistAllCompent()
        {

            returnButton = RegistBtnAndClick("ButtonGroup/ReturnButton", OnClickReturnButton);
            receiveButton = RegistBtnAndClick("ButtonGroup/ReceiveButton", OnClickReceiveButton);

            money_1_Text = RegistCompent<Text>("up/coinBg/Text");
            money_2_Text = RegistCompent<Text>("up/diamondBg/Text");

            //money_1_Text.text = playerData.playerZoo.playerCoin.GetCoinByScene(playerData.playerZoo.currSceneID).coinShow;
            money_1_Text.text = "0";
            money_2_Text.text = "0";

            rewardGold_text = RegistCompent<Text>("Reward/RewardGold/Text");
            rewardRmb_text = RegistCompent<Text>("Reward/RewardRmb/Text");
           

            money_1_GoldIcon = RegistCompent<Image>("up/coinBg/coinIcon");
            mewardGold_Image = RegistCompent<Image>("Reward/RewardGold/Image");


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
            money_1_GoldIcon.sprite = sprite;
            mewardGold_Image.sprite = sprite;
           
        }

        /// <summary>
        /// 按钮  返回主界面
        /// </summary>
        /// <param name="obj"></param>
        private void OnClickReturnButton(string obj)
        {
            SendIncreaseCrossRoadStageID();

            //SetValueOfPlayerData.Send((int)GameMessageDefine.AddCoinOfPlayerDataMSSC, 0, cellStage.firstgoldreward, 0);

            //CrossRoadStageManager.GetInstance().UnLoad();
            //ZooGameLoader.GetInstance().BackFromCrossRoad();
        }

        private void SendIncreaseCrossRoadStageID()
        {
            MessageManager.GetInstance().Send((int)GameMessageDefine.IncreaseCrossRoadStageID);
        }

        /// <summary>
        /// 观看视频翻倍
        /// </summary>
        /// <param name="obj"></param>
        private void OnClickReceiveButton(string obj)
        {
            LogWarp.LogError(" 观看视频翻倍  ");
            SendIncreaseCrossRoadStageID();
            //SetValueOfPlayerData.Send((int)GameMessageDefine.AddCoinOfPlayerDataMSSC, 0, cellStage.firstgoldreward*2, 0);
            //CrossRoadStageManager.GetInstance().UnLoad();
            //ZooGameLoader.GetInstance().BackFromCrossRoad();

        }

        private void IninCompentData()
        {
            rewardGold_text.text = cellStage.firstgoldreward.ToString();
            rewardRmb_text.text = "0";
        }

        //private void OnGetBroadcastCoinOfPlayerDataMSSC(Message obj)
        //{
        //    money_1_Text.text = playerData.playerZoo.playerCoin.GetCoinByScene(playerData.playerZoo.currSceneID).coinShow;
        //    //money_2_Text.text = "0";
        //}
        
    }
}