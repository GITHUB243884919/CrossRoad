using UnityEngine;
using System;
using System.Security;
using System.Collections.Generic;
namespace Config
{
	public class globalConfig
	{
		private globalConfig(){ 
		}
		private static globalConfig _inst;
		public static globalConfig getInstace(){
			if (_inst != null) {
				return _inst;
			}
			_inst = new globalConfig ();
			return _inst;
		}
		///<summary>
		///动物栏相机偏移变量
		///<summary>
		public readonly float BuildingViewRatioY= 0.75f;  
		///<summary>
		///自然游客驾车进入路线
		///<summary>
		public readonly string NaturalVisitorInto= "path_touristcar_into";  
		///<summary>
		///自然游客离开园内步行路线
		///<summary>
		public readonly string NaturalVisitorOut_1= "path_touristwalk_out";  
		///<summary>
		///自然游客离开园外驾车路线
		///<summary>
		public readonly string NaturalVisitorOut_2= "path_touristcar_out";  
		///<summary>
		///广告游客进入园外游轮路线
		///<summary>
		public readonly string AdvertVisitorInto_1= "path_ship_into";  
		///<summary>
		///广告游客进入园外步行路线
		///<summary>
		public readonly string AdvertVisitorInto_2= "path_shiptouristwalk_into";  
		///<summary>
		///广告游客离开步行路线
		///<summary>
		public readonly string AdvertVisitorOut= "path_shiptouristwalk_out";  
		///<summary>
		///动物园观光路径总起点
		///<summary>
		public readonly string FirstPathOfZoo= "path_1000_1001";  
		///<summary>
		///动物休闲动作
		///<summary>
		public readonly string AnimalIdle= "idle";  
		///<summary>
		///动物行走动作
		///<summary>
		public readonly string AnimalWalk= "walk";  
		///<summary>
		///动物展示动作
		///<summary>
		public readonly string AnimalPose= "pose";  
		///<summary>
		///工程名称
		///<summary>
		public readonly string ZooSceneName= "dwy_9";  
		///<summary>
		///游客移动速度
		///<summary>
		public readonly float ZooVisitorSpeed= 6f;  
		///<summary>
		///游客排队移动速度
		///<summary>
		public readonly float ZooVisitorQueueSpeed= 8f;  
		///<summary>
		///自然游客小汽车移动速度
		///<summary>
		public readonly float ZooCarSpeed= 20f;  
		///<summary>
		///停车排模型数字显示个位
		///<summary>
		public readonly string ParkingNumber0= "damen/parking_01/gewei";  
		///<summary>
		///停车排模型数字显示十位
		///<summary>
		public readonly string ParkingNumber1= "damen/parking_01/shiwei";  
		///<summary>
		///最大停车位数量上限
		///<summary>
		public readonly int MaxParkingNumber= 99;  
		///<summary>
		///小游戏动物移动速度
		///<summary>
		public readonly float LittleGameAnimalSpeed= 0.5f;  
		///<summary>
		///动物园终点车库资源组加载id
		///<summary>
		public readonly int EndPartResID= 9401;  
		///<summary>
		///动物园场景单组资源宽度参数
		///<summary>
		public readonly float ZooPartResLen= 53f;  
		///<summary>
		///场景自带地块数
		///<summary>
		public readonly int DefaultOpenGroup= 0;  
		///<summary>
		///停车牌按钮
		///<summary>
		public readonly string ParkingButton= "parking_01";  
		///<summary>
		///售票口按钮
		///<summary>
		public readonly string StationButton= "damen_02";  
		///<summary>
		///大象馆按钮
		///<summary>
		public readonly string ElephantButton= "1001";  
		///<summary>
		///摆渡车载客路线
		///<summary>
		public readonly string ShuttleGotoPath= "path_shuttlegoto";  
		///<summary>
		///摆渡车回库路线
		///<summary>
		public readonly string ShuttleGobackPath= "path_shuttlegoback";  
		///<summary>
		///摆渡车游客下车去电梯路线
		///<summary>
		public readonly string ShuttleVisitorLeavePath= "path_shuttlevisitorleave";  
		///<summary>
		///载客去 动态
		///<summary>
		public readonly string Path_ShuttleGotoDynamic= "path_shuttlepath1";  
		///<summary>
		///载客去 静态
		///<summary>
		public readonly string Path_ShuttleGotoStatuc= "path_shuttlegoto2";  
		///<summary>
		///回来 静态
		///<summary>
		public readonly string Path_ShuttleGobackStatic= "path_shuttlepath2";  
		///<summary>
		///回来 动态
		///<summary>
		public readonly string Path_ShuttleGobackDynamic= "path_shuttlepath3";  
		///<summary>
		///初始最大组数
		///<summary>
		public readonly int InitMaxGroupNum= 8;  
		///<summary>
		///摆渡车基础速度
		///<summary>
		public readonly float ShuttleBaseSpeed= 30f;  
		///<summary>
		///通用按钮点击音效
		///<summary>
		public readonly string UiButtonSoynd= "music/button";  
		///<summary>
		///动物园场景背景音乐
		///<summary>
		public readonly string ZooSceneBGM= "music/zoogame_bgm_01_mp3";  
		///<summary>
		///动物解锁展示音效
		///<summary>
		public readonly string AnimalOpenMusic= "music/AnimalOpenMusic";  
		///<summary>
		///动物栏开启音效
		///<summary>
		public readonly string BuildOpenMusic= "music/BuildOpenMusic";  
		///<summary>
		///建筑升级按钮音效
		///<summary>
		public readonly string BuildUpButtonMusic= "music/BuildUpButtonMusic";  
		///<summary>
		///界面获得金币特效音效
		///<summary>
		public readonly string UIMoneyMusic= "music/UIMoneyMusic";  
		///<summary>
		///场景金币特效音效
		///<summary>
		public readonly string SceneMoneyMusic= "music/SceneMoneyMusic";  
		///<summary>
		///游客动作
		///<summary>
		public readonly string VisitorIdle= "idle";  
		///<summary>
		///游客动作
		///<summary>
		public readonly string VisitorWalk= "walk";  
		///<summary>
		///车动作
		///<summary>
		public readonly string CarWalk= "walk";  
		///<summary>
		///乘车人数
		///<summary>
		public readonly int MaxShuttleVisitor= 4;  
		///<summary>
		///车间隔
		///<summary>
		public readonly float MaxShuttleInterval = 0.5f;  
		///<summary>
		///下人时间间隔
		///<summary>
		public readonly float ShuttleVisitorGetOffInterval= 0.2f;  
		///<summary>
		///摆渡车站数值显示个位
		///<summary>
		public readonly string ExitgateNumber0= "jianzhu/tingchepai_01/gewei";  
		///<summary>
		///摆渡车站数值显示十位
		///<summary>
		public readonly string ExitgateNumber1= "jianzhu/tingchepai_01/shiwei";  
		///<summary>
		///停车牌挂点预制体路径
		///<summary>
		public readonly string ParkingNumberCoinEffectNode= "jianzhu/shoufei_01/effect_node";  
		///<summary>
		///观光位金币特效Y偏移量
		///<summary>
		public readonly float LittleZooCoinEffectOffsetY= 8f;  
		///<summary>
		///动物栏叹号特效Y偏移量
		///<summary>
		public readonly float LittleZooExclamatoryEffectOffsetY= 5f;  
		///<summary>
		///金币特效资源路径
		///<summary>
		public readonly string CoinEffectRes= "prefabs/Effect/Fx_gold";  
		///<summary>
		///叹号特效资源路径
		///<summary>
		public readonly string ExclamatoryEffectRes= "prefabs/Effect/Fx_tanhao";  
		///<summary>
		///摆渡车广告游客下车出去路线
		///<summary>
		public readonly string AdvertVisitorLeavePath= "path_advertlevisitorleave";  
		///<summary>
		///创建账号初始现金数量
		///<summary>
		public readonly string InitialGoldNumber= "1000";  
		///<summary>
		///广告视频游客轮船行驶速度
		///<summary>
		public readonly float ZooShipSpeed= 20f;  
		///<summary>
		///游客轮船下人时间间隔
		///<summary>
		public readonly float ShipVisitorGetOffInterval= 0.4f;  
		///<summary>
		///停车场新手引导小手挂点
		///<summary>
		public readonly string GuideParking= "damen/parking_01/effect_node";  
		///<summary>
		///动物栏新手引导小手挂点
		///<summary>
		public readonly string GuideBuild= "LittleZoo/1001/effect_node";  
		///<summary>
		///摆渡车新手引导小手挂点
		///<summary>
		public readonly string GuideExitgate= "end/ggchezhan/effect_node";  
		///<summary>
		///新手引导小手资源路径
		///<summary>
		public readonly string GuideHand= "UIAtlas/GuideHandUp";  
		///<summary>
		///新手引导点击特效场景资源路径
		///<summary>
		public readonly string GuideSceneClickEffect= "prefabs/Effect/Fx_Sence_Hand";  
		///<summary>
		///新手引导点击特效界面资源路径
		///<summary>
		public readonly string GuideUiClickEffect= "prefabs/Effect/Fx_Ui_Hand";  
		///<summary>
		///默认开启的动物栏数量
		///<summary>
		public readonly int DefaultOpenLittleZoo= 1;  
		///<summary>
		///轮船来人最小数（包含）
		///<summary>
		public readonly int AdvertVisitorMin= 20;  
		///<summary>
		///轮船来人最大数（包含）
		///<summary>
		public readonly int AdvertVisitorMax= 30;  
		///<summary>
		///建筑点击特效
		///<summary>
		public readonly string BuildClickAnim= "build";  
		///<summary>
		///观光位CD缩放比例
		///<summary>
		public readonly float VisitSeatCDScale= 0.04f;  
		///<summary>
		///观光位CD资源UI
		///<summary>
		public readonly string VisitSeatCDGameObject= "prefabs/VisitSeatGB";  
		///<summary>
		///场景飘字资源TextUI
		///<summary>
		public readonly string SceneFlutterTextGameObject= "prefabs/SceneFlutterText";  
		///<summary>
		///模型建筑开启升级特效
		///<summary>
		public readonly string BuildUpEffect= "prefabs/Effect/Fx_yanhua";  
		///<summary>
		///游客离开移动速度
		///<summary>
		public readonly float ZooVisitorBackSpeed= 6f;  
		///<summary>
		///入口返回路线
		///<summary>
		public readonly string EntryGoBackPath= "path_1000_out";  
		///<summary>
		///入口排队观察路线
		///<summary>
		public readonly string EntryQueueObservePath= "path_touristwalk_into0";  
		///<summary>
		///入口排队首位偏移量
		///<summary>
		public readonly float EntryQueueFirstPosOffset= 6f;  
		///<summary>
		///场景正方向
		///<summary>
		public readonly float[] SceneForward= new float[]{-1f,0f,0f};  
		///<summary>
		///动物栏观光点特效挂点
		///<summary>
		public readonly string BuildVisitEffect= "SceneUIMountPoint/MountPointGameObject/LittleZooMountPoint";  
		///<summary>
		///售票口排队点特效挂点
		///<summary>
		public readonly string TicketsQueueEffect= "SceneUIMountPoint/MountPointGameObject/TicketMountPoint";  
		///<summary>
		///售票口金币特效Y偏移量
		///<summary>
		public readonly float TicketEffectOffsetY= 15f;  
		///<summary>
		///动物园第一个点和大门的偏移
		///<summary>
		public readonly float ZooFirstPosOffset= 11f;  
		///<summary>
		///动物购买表现模型旋转速度
		///<summary>
		public readonly float AnimalShowRotateSpeed= 0.5f;  
		///<summary>
		///动物价格基数
		///<summary>
		public readonly int AnimalPriceBase= 1;  
		///<summary>
		///地面停车场进入基准路
		///<summary>
		public readonly string GroundParkingBaseInPath= "path_touristcar_intoup1";  
		///<summary>
		///地面停车场进入基准线
		///<summary>
		public readonly string GroundParkingBaseInLine= "path_touristcar_intoupline";  
		///<summary>
		///地面停车场首位偏移量
		///<summary>
		public readonly float GroundParkingFristSpaceOffset= 8f;  
		///<summary>
		///地面停车场车位高
		///<summary>
		public readonly float GroundParkingSpace= 5f;  
		///<summary>
		///地面停车场车车位的停车点偏移量
		///<summary>
		public readonly float GroundParkingSpacePosOffset= 7f;  
		///<summary>
		///地面停车场游客观察位基准线
		///<summary>
		public readonly string GroundParkingVisitorObserveBaseLine= "path_tourist_intoupline";  
		///<summary>
		///地面停车场离开基准线
		///<summary>
		public readonly string GroundParkingBaseOutLine= "path_touristcar_outupline";  
		///<summary>
		///汽车直角转弯半径
		///<summary>
		public readonly float CarRightAnglesRadius= 3f;  
		///<summary>
		///地面停车场离开路线
		///<summary>
		public readonly string GroundParkingBaseOutPath= "path_touristcar_outline";  
		///<summary>
		///去地面停车场游客基准路线
		///<summary>
		public readonly string GroundParkingVistorBasePath= "path_touristwalk_ground";  
		///<summary>
		///单组地面停车场的车位数
		///<summary>
		public readonly int NumGroundParkingGroupSpace= 6;  
		///<summary>
		///地面停车场的车位最大组数
		///<summary>
		public readonly int MaxNumGroundParkingGroup= 8;  
		///<summary>
		///售票口新手引导小手挂点
		///<summary>
		public readonly string GuideTicket= "damen/damen_02/effect_node";  
		///<summary>
		///游客动画时长
		///<summary>
		public readonly float VisitorMinAnimLen= 2f;  
		///<summary>
		///最小离线时间（秒）
		///<summary>
		public readonly int MinOfflineSecond= 120;  
		///<summary>
		///动物园场景默认开启组数
		///<summary>
		public readonly int ExtendLoadGroup= 2;  
		///<summary>
		///初始升级价格
		///<summary>
		public readonly string AnimalLvUpBase= "20";  
		///<summary>
		///等级上限
		///<summary>
		public readonly int AnimalLvUpLimit= 100;  
		///<summary>
		///售票口未开启图标
		///<summary>
		public readonly string AddTicketIcon= "UIAtlas/UIBuild/AddTicketIcon";  
		///<summary>
		///售票口已开启图标
		///<summary>
		public readonly string LvUpTicketIcon= "UIAtlas/UIFerryCar/FerryCarNumIcon";  
		///<summary>
		///头顶CD时间缩放
		///<summary>
		public readonly float CdTimeZoom= 0.7f;  
		///<summary>
		///广告游客进入园外游轮路线
		///<summary>
		public readonly string AdvertShipOut_1= "path_ship_out";  
		///<summary>
		///头顶CD时间偏移X坐标
		///<summary>
		public readonly float CdTimeOffset_x= 1f;  
		///<summary>
		///头顶CD时间偏移Y坐标
		///<summary>
		public readonly float CdTimeOffset_y= 2.5f;  
		///<summary>
		///头顶CD时间偏移Z坐标
		///<summary>
		public readonly float CdTimeOffset_z= 1f;  
		///<summary>
		///通用界面按钮置灰图片资源
		///<summary>
		public readonly string CurrencyButtonGray= "UIAtlas/UIBuild/ButtonGray";  
		///<summary>
		///广告等待时间
		///<summary>
		public readonly float WaitingADUITime= 30f;  
		///<summary>
		///离线广告收益倍数
		///<summary>
		public readonly int WaitingADProfit= 2;  
		///<summary>
		///增加游客触发百分比
		///<summary>
		public readonly int IncreaseTouristRatio= 80;  
		///<summary>
		///增加游客触发时间
		///<summary>
		public readonly int IncreaseTouristTime= 30;  
		///<summary>
		///增加游客cd
		///<summary>
		public readonly int IncreaseTouristCD= 120;  
		///<summary>
		///观光加速触发百分比
		///<summary>
		public readonly int VisitAccelerateRatio= 80;  
		///<summary>
		///观光加速触发时间
		///<summary>
		public readonly int VisitAccelerateTime= 60;  
		///<summary>
		///观光加速cd
		///<summary>
		public readonly int VisitAccelerateCD= 120;  
		///<summary>
		///快速售票触发百分比
		///<summary>
		public readonly int TicketAccelerateRatio= 50;  
		///<summary>
		///快速售票触发时间
		///<summary>
		public readonly int TicketAccelerateTime= 45;  
		///<summary>
		///快速售票cd
		///<summary>
		public readonly int TicketAccelerateCD= 120;  
		///<summary>
		///贵宾定时广告触发时间
		///<summary>
		public readonly int AdvertFreeItemTime= 60;  
		///<summary>
		///贵宾定时广告cd
		///<summary>
		public readonly int AdvertFreeItemCD= 60;  
		///<summary>
		///贵宾定时广告奖励现金图标路径
		///<summary>
		public readonly string AdvertAddFreeItem= "UIAtlas/UIAdvertActivity/GoldHeap";  
		///<summary>
		///世界地图离开状态金币跳动cd
		///<summary>
		public readonly int LeaveSceneCoinCD= 5;  
		///<summary>
		///每日领取广告BUFF次数
		///<summary>
		public readonly int AdvertBUFFTimes= 6;  
		///<summary>
		///利润翻倍广告6次奖励现金时间
		///<summary>
		public readonly int AdvertGoldReward= 300;  
		///<summary>
		///广告增加游客界面图标
		///<summary>
		public readonly string AdvertAddTourist= "UIAtlas/UIAdvert/TouristIcon";  
		///<summary>
		///广告售票口秒CD界面图标
		///<summary>
		public readonly string AdvertAddTicket= "UIAtlas/UIAdvert/SellTicketIcon";  
		///<summary>
		///广告动物栏秒CD界面图标
		///<summary>
		public readonly string AdvertAddVisit= "UIAtlas/UIAdvert/TourIcon";  
		///<summary>
		///AppsFlyer开发者KEY
		///<summary>
		public readonly string AppsFlyerDevKey= "xRAhSTPuby52xae6bq3dDe";  
		///<summary>
		///全屏飘钱特效
		///<summary>
		public readonly string ScreenGoldEffect= "prefabs/Effect/UI_Money";  
		///<summary>
		///养成属性词条特效
		///<summary>
		public readonly string AttributeUpEffect= "prefabs/Effect/Fx_Ui_NumberUp";  
		///<summary>
		///售票口秒CD特效路径
		///<summary>
		public readonly string TicketCdEffect= "prefabs/Effect/Fx_ShouPiaoKou";  
		///<summary>
		///停车场扩建特效路径
		///<summary>
		public readonly string ParkingUpEffect= "prefabs/Effect/Fx_ParkingUp";  
		///<summary>
		///现金获得广告产出时间数（秒）
		///<summary>
		public readonly int MoneyAcceleratekOutput= 300;  
		///<summary>
		///动物培养开启所需星星
		///<summary>
		public readonly int AnimalupgradingNeed= 9999;  
		///<summary>
		///游客表情最小缩放
		///<summary>
		public readonly float ExpressionScaleOrg= 8f;  
		///<summary>
		///游客表情最大缩放
		///<summary>
		public readonly float ExpressionScaleMax= 10f;  
		///<summary>
		///游客表情缩放时长
		///<summary>
		public readonly float ExpressionScaleDuration= 0.3f;  
		///<summary>
		///游客表情Y轴坐标
		///<summary>
		public readonly float ExpressionPosY= 7f;  
		///<summary>
		///游客表情持续时间
		///<summary>
		public readonly float ExpressionDuration= 5f;  
		///<summary>
		///创建账号初始星星数量
		///<summary>
		public readonly int InitialStarNumber= 0;  
		///<summary>
		///现金获得广告金币奖励系数
		///<summary>
		public readonly int MoneyAcceleratekReward= 2;  
		///<summary>
		///贵宾定时广告气球刷新坐标
		///<summary>
		public readonly string AdvertFreeItemPos= "106,4,-44";  
		///<summary>
		///离线时间最大上限（秒）
		///<summary>
		public readonly int WaitingADTimeMax= 43200;  
		///<summary>
		///close牌路径
		///<summary>
		public readonly string closeGameObjectPath= "{0}/animimation/close";  
		///<summary>
		///动物图鉴界面通用板子
		///<summary>
		public readonly string AnimalKindCellPath= "AnimalKindCell";  
		///<summary>
		///过马路小游戏场景名称
		///<summary>
		public readonly string CrossRoadScene= "CrossRoadScene";  
		///<summary>
		///过马路小游戏场景终点资源路径
		///<summary>
		public readonly string CrossRoadSceneEnd= "ResourcesDepend_ZooGame/Little_end/Little_end";  
} 
}