/*******************************************************************
* FileName:     GameMessageDefine.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-8
* Description:  
* other:    
********************************************************************/


namespace Game.MessageCenter
{
    public enum GameMessageDefine
    {

        UIMessage_AddToTick = 10001,

        /// <summary>
        /// 小游戏关卡加载完成
        /// </summary>
        LoadCrossRoadLevelFinished,

        /// <summary>
        /// 小游戏开始
        /// </summary>
        CrossRoadStartGame,

        /// <summary>
        /// 小游戏单条马路成功
        /// </summary>
        CrossRoadGameSingleRoadSucceed,

        /// <summary>
        /// 小游戏失败
        /// </summary>
        CrossRoadGameFailure,

        /// <summary>
        /// 过马路相机停止移动
        /// </summary>
        CrossRoadCameraStopMove,

        /// <summary>
        /// 过马路关卡数递增
        /// </summary>
        IncreaseCrossRoadStageID,

        /// <summary>
        /// 队伍移动
        /// </summary>
        CrossRoadAnimalTeamMove,

        /// <summary>
        /// 队伍停止移动
        /// </summary>
        CrossRoadAnimalTeamStopMove,

    }
}

