/*******************************************************************
* FileName:     GameDefine.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-7
* Description:  
* other:    
********************************************************************/


using System;

namespace Game
{
    /// <summary>
    /// 和策划表resource.xlsx保持一致
    /// 对应loadtype
    /// </summary>
    public enum ResLoadType
    {
        /// <summary>
        /// 只加载prefab 如：游客，汽车
        /// </summary>
        OnlyPrefab = 1,

        /// <summary>
        /// prefab和贴图，如动物
        /// </summary>
        PrefabAndTexture = 2,
    }

    /// <summary>
    /// 和策划表resource.xlsx保持一致
    /// 对应restype
    /// </summary>
    public enum ResType
    {
        /// <summary>
        /// 车
        /// </summary>
        Car = 1,

        /// <summary>
        /// 游客
        /// </summary>
        Visitor = 2,

        /// <summary>
        /// 动物栏
        /// </summary>
        LittleZoo = 3,

        /// <summary>
        /// 动物
        /// </summary>
        Animal = 4,

        /// <summary>
        /// 动物园拼接地块
        /// </summary>
        ZooPart = 5,

        /// <summary>
        /// 摆渡车
        /// </summary>
        Shuttle = 6,

        /// <summary>
        /// 动物展示预制体
        /// </summary>
        AnimalShowPreb = 7,

        /// <summary>
        /// 轮船
        /// </summary>
        Ship = 8,

        /// <summary>
        /// 地面停车场
        /// </summary>
        GroundParking = 9,

        /// <summary>
        /// 游客照相特效
        /// </summary>
        VisitorCameraEff = 10,

        /// <summary>
        /// 游客招手特效
        /// </summary>
        VisitorHandEff= 11,

        /// <summary>
        /// 火车
        /// </summary>
        Train = 14,

        /// <summary>
        /// 过马路车道资源
        /// </summary>
        CrossRoadCarRoad = 15,

        /// <summary>
        /// 过马路动物路资源
        /// </summary>
        CrossRoadAnimalRoad = 16,
    }

    /// <summary>
    /// 实体功能类型
    /// 与策划表无关，但与被控制的单位实体的功能有关
    /// </summary>
    public enum EntityFuncType
    {
        /// <summary>
        /// 游客轿车(门口送游客来)
        /// </summary>
        VisitorCar_EnterZoo = 1,

        /// <summary>
        /// 游客轿车(门口接送游客离开)
        /// </summary>
        VisitorCar_LeaveZoo,

        /// <summary>
        /// 小游戏汽车
        /// </summary>
        Car_LittleGame,

        /// <summary>
        /// 送游客走的摆渡车
        /// </summary>
        Shuttle,

        /// <summary>
        /// 轮船
        /// </summary>
        Ship,


        /// <summary>
        /// 地面停车场轿车
        /// </summary>
        GroundParkingCar,

        /// <summary>
        /// 火车
        /// </summary>
        Train,

        /// <summary>
        /// 过马路小汽车
        /// </summary>
        CrossRoadCar,

        /// <summary>
        /// 从轿车来的游客
        /// </summary>
        Visitor_From_Car    = 101,

        /// <summary>
        /// 从轮船来的游客
        /// </summary>
        Visitor_From_Ship   = 102,

        /// <summary>
        /// 从停车场来的游客
        /// </summary>
        Visitor_From_GroundParking,

        /// <summary>
        /// 散步的动物
        /// </summary>
        Animal_Wander = 501,

        /// <summary>
        /// 小游戏用的动物
        /// </summary>
        Animal_LittleGame = 601,

        /// <summary>
        /// 动物园内的建筑
        /// </summary>
        Building_Zoo = 701,

        /// <summary>
        /// 小游戏建筑
        /// </summary>
        Building_LittleGame = 801
    }

    /// <summary>
    /// 游客状态
    /// </summary>
    public enum VisitorState
    {
        /// <summary>
        /// 进入入口检票口
        /// </summary>
        GotoZooEntry = 1,

        /// <summary>
        /// 动物园入口排队
        /// </summary>
        EntryQueue,

        /// <summary>
        /// 在入口排队首位
        /// </summary>
        StayFirstPosInEntryQueue,

        /// <summary>
        /// 选动物栏
        /// </summary>
        ChoseLittleZoo,

        /// <summary>
        /// 进入动物栏申请
        /// </summary>
        EnterLittleZooApply,

        /// <summary>
        /// 跨组寻路
        /// </summary>
        CrossGroupPath,

        /// <summary>
        /// 选等待位，并等待换到观光位
        /// </summary>
        StayWaitSeat,

        /// <summary>
        /// 换座(等待位到观光位)
        /// </summary>
        StayVisitSeat,

        /// <summary>
        /// 离开（大门口到消失）
        /// </summary>
        LeaveZoo,

        /// <summary>
        /// 离开（所有入口排队满了）
        /// </summary>
        LeaveZooEntryQueueFull,

        /// <summary>
        /// 离开(没有动物栏可选)
        /// </summary>
        LeaveNonLittleZoo,

        /// <summary>
        /// 直接进入动物园
        /// </summary>
        EnterZoo,

        /// <summary>
        /// 走向出口的路的起点
        /// </summary>
        GotoStartOfExitGateEntryPath,

        /// <summary>
        /// 走向出口排队位
        /// </summary>
        GotoExitGateEntryQueue,

        /// <summary>
        /// 从大门离开
        /// </summary>
        LeaveFromZooEntry,

        /// <summary>
        /// 走向停车场
        /// </summary>
        GotoParking,

        /// <summary>
        /// 离开走向地面停车场
        /// </summary>
        GotoGroundParking,
    }

    /// <summary>
    /// 游戏运行模式
    /// </summary>
    public enum RunTimeLoaderType
    {
        /// <summary>
        /// 游戏模式
        /// </summary>
        Game = 1,

        /// <summary>
        /// 编辑模式
        /// </summary>
        Editor = 2,
    }

    /// <summary>
    /// 摆渡车状态
    /// </summary>
    public enum ShuttleState
    {
        /// <summary>
        /// 载客走路线(可能因场景扩展而变化)
        /// </summary>
        GotoDynamicPath,

        /// <summary>
        /// 载客走计算路线(不会因场景扩展而变化)
        /// </summary>
        GotoCalcPath,

        /// <summary>
        /// 回来计算路线(可能因场景扩展而变化)
        /// </summary>
        GobackCalcPath,

        /// <summary>
        /// 回来动态路线(可能因场景扩展而变化)
        /// </summary>
        GobackDynamicPath,
    }

    /// <summary>
    /// 游客阶段
    /// </summary>
    public enum VisitorStage
    {
        /// <summary>
        /// 去动物园
        /// </summary>
        GotoZoo,

        /// <summary>
        /// 去停车场
        /// </summary>
        GotoParking,
    }
    [Serializable]

    public enum BuffType
    {
        /// <summary>
        /// 金币收入系数  累加buff
        /// </summary>
        RatioCoinInComeAdd = 1,

        /// <summary>
        /// 设置游览CD
        /// </summary>
        SetVisitCDVal,

        /// <summary>
        /// 设置出口CD
        /// </summary>
        SetExitEntryCDVal,

        /// <summary>
        /// 设置入口CD
        /// </summary>
        SetEntryGateCDVal,

        /// <summary>
        /// 金币收入系数  累乘buff
        /// </summary>
        RatioCoinInComeMul,

    }

    //轮船状态
    public enum ShipState
    {
        Goto,

        Goback,
    }


    public enum GameLoaderState
    {
        /// <summary>
        /// 连接服务器相关操作
        /// </summary>
        Server,

        /// <summary>
        /// 加载原始场景
        /// </summary>
        LoadOrgScene,

        /// <summary>
        /// 按需分块加载场景
        /// </summary>
        LoadPartScenes,

        /// <summary>
        /// 加载动物栏动物
        /// </summary>
        LoadAnimalInLittleZoo,
    }

    public enum ItemType
    {
        /// <summary>
        /// 钻石
        /// </summary>
        Diamond = 1,

        /// <summary>
        /// 金币
        /// </summary>
        Coin,

        /// <summary>
        /// BUFF
        /// </summary>
        Buff,

        /// <summary>
        /// 星星
        /// </summary>
        Star,
    }

    public enum ItemUse
    {
        /// <summary>
        /// 获得生效
        /// </summary>
        Get_Effective = 1,

        /// <summary>
        /// 获得暂时不生效,使用时才生效
        /// </summary>
        Use_Effective,
    }

    /// <summary>
    /// 建筑类型
    /// </summary>
    public enum BuildingType
    {
        /// <summary>
        /// 停车场
        /// </summary>
        Parking = 1,

        /// <summary>
        /// 大门
        /// </summary>
        EntryGate,

        /// <summary>
        /// 动物栏
        /// </summary>
        LittleZoo,

        /// <summary>
        /// 场景最后那个，最后一个非动物栏地块
        /// </summary>
        LastBuilding,
    }

    /// <summary>
    /// 新手引导步骤
    /// </summary>
    public enum NewBieGuild
    {
        Step_1 = 1,
        Step_2,
        Step_3,
        Step_4,
        Step_5,
        Step_6,
        Step_7,
        Step_8,
        Step_9,
        Step_10,
        Step_11,
        Step_12,
        Step_13,
        Step_14,
        Step_15,
        Step_16,
        Step_17,
        Step_18,
        Step_19,
        Step_20,
        Step_21,
        Step_22,
        Step_23,
        Step_24,
        Step_25,
        Step_26,
    }

    public class GameConst
    {
        /// <summary>
        /// 占位
        /// </summary>
        public static int Place_Holder_ID = -2;

        /// <summary>
        /// 第一个场景的id,通知策划配表符合这个规则
        /// </summary>
        public static int First_SceneID = 0;

        /// <summary>
        /// 第一个场景的金币类型,通知策划配表符合这个规则
        /// </summary>
        public static int First_CoinTypeID = 0;

        /// <summary>
        /// 第一个动物栏id,通知策划配表符合这个规则
        /// </summary>
        public static int First_LittleZooID = 1001;

    }

}


