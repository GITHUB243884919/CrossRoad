using UnityEngine;
using System;
using System.Security;
using System.Collections.Generic;
namespace Config
{
	public class parkingConfig
	{
		private parkingConfig(){ 
		}
		private static parkingConfig _inst;
		public static parkingConfig getInstace(){
			if (_inst != null) {
				return _inst;
			}
			_inst = new parkingConfig ();
			_inst.InitData ();
			return _inst;
		}
		public Dictionary<string,parkingCell> AllData;
		public parkingCell getCell(string key){
			parkingCell t = null;
			this.AllData.TryGetValue (key, out t);
			return t;
		}
		public parkingCell getCell(int key){
			parkingCell t = null;
			this.AllData.TryGetValue (key.ToString(), out t);
			return t;
		}
		public readonly int RowNum = 20;
		private void InitData(){
			this.AllData = new Dictionary<string,parkingCell> ();
			this.AllData.Add("1",new parkingCell("停车场",0,0,4,15,1,163,"1",2,6,2,58,"100",4,"1",3,165,new int[]{1,2,3,4,5,6,7,8},"parking_01",new string[]{"9001","9002","9003","9004","9005","9006","9007","9008"},new int[]{-1,1,1,-1,1,-1,1,-1},new int[]{0,10,25,50,100,150,165},new int[]{1,1,1,1,1,1,1},new int[]{3003,3003,3005,3007,3010,3013,3014},new int[]{0,1,1,1,1,1,1},6));
			this.AllData.Add("2",new parkingCell("停车场",1,0,4,15,1,163,"4",2,6,2,58,"400",4,"4",3,320,new int[]{1,2,3,4,5,6,7,8},"parking_01",new string[]{"9001","9002","9003","9004","9005","9006","9007","9008"},new int[]{-1,1,1,-1,1,-1,1,-1},new int[]{0,10,25,50,100,150,200,250,300,320},new int[]{1,1,1,1,1,1,1,1,1,1},new int[]{3003,3004,3006,3008,3011,3013,3014,3018,3021,3021},new int[]{0,1,1,1,1,1,1,1,1,1},9));
			this.AllData.Add("3",new parkingCell("停车场",2,0,4,15,1,163,"16",2,6,2,58,"1600",4,"16",3,450,new int[]{1,2,3,4,5,6,7,8},"parking_01",new string[]{"9001","9002","9003","9004","9005","9006","9007","9008"},new int[]{-1,1,1,-1,1,-1,1,-1},new int[]{0,10,25,50,100,150,200,250,300,400,450},new int[]{1,1,1,1,1,1,1,1,1,1,1},new int[]{3003,3004,3006,3008,3011,3014,3016,3018,3021,3026,3028},new int[]{0,1,1,1,1,1,1,1,1,1,1},10));
			this.AllData.Add("4",new parkingCell("停车场",3,0,4,15,1,163,"64",2,6,2,58,"6400",4,"64",3,570,new int[]{1,2,3,4,5,6,7,8},"parking_01",new string[]{"9001","9002","9003","9004","9005","9006","9007","9008"},new int[]{-1,1,1,-1,1,-1,1,-1},new int[]{0,10,25,50,100,150,200,250,300,400,500,570},new int[]{1,1,1,1,1,1,1,1,1,1,1,1},new int[]{3003,3005,3006,3008,3012,3014,3017,3018,3021,3026,3031,3034},new int[]{0,1,1,1,1,1,1,1,1,1,1,1},11));
			this.AllData.Add("5",new parkingCell("停车场",4,0,4,15,1,163,"256",2,6,2,58,"25600",4,"256",3,720,new int[]{1,2,3,4,5,6,7,8},"parking_01",new string[]{"9001","9002","9003","9004","9005","9006","9007","9008"},new int[]{-1,1,1,-1,1,-1,1,-1},new int[]{0,10,25,50,100,150,200,250,300,400,500,600,700,720},new int[]{1,1,1,1,1,1,1,1,1,1,1,1,1,1},new int[]{3003,3005,3007,3009,3012,3015,3017,3020,3022,3026,3029,3035,3040,3041},new int[]{0,1,1,1,1,1,1,1,1,1,1,1,1,1},13));
			this.AllData.Add("6",new parkingCell("停车场",5,0,4,15,1,163,"2",2,6,2,58,"200",4,"2",3,165,new int[]{1,2,3,4,5,6,7,8},"parking_01",new string[]{"9001","9002","9003","9004","9005","9006","9007","9008"},new int[]{-1,1,1,-1,1,-1,1,-1},new int[]{0,10,25,50,100,150,165},new int[]{1,1,1,1,1,1,1},new int[]{3003,3003,3005,3007,3010,3013,3014},new int[]{0,1,1,1,1,1,1},6));
			this.AllData.Add("7",new parkingCell("停车场",6,0,4,15,1,163,"8",2,6,2,58,"800",4,"8",3,320,new int[]{1,2,3,4,5,6,7,8},"parking_01",new string[]{"9001","9002","9003","9004","9005","9006","9007","9008"},new int[]{-1,1,1,-1,1,-1,1,-1},new int[]{0,10,25,50,100,150,200,250,300,320},new int[]{1,1,1,1,1,1,1,1,1,1},new int[]{3003,3004,3006,3008,3011,3012,3015,3018,3020,3021},new int[]{0,1,1,1,1,1,1,1,1,1},9));
			this.AllData.Add("8",new parkingCell("停车场",7,0,4,15,1,163,"32",2,6,2,58,"3200",4,"32",3,450,new int[]{1,2,3,4,5,6,7,8},"parking_01",new string[]{"9001","9002","9003","9004","9005","9006","9007","9008"},new int[]{-1,1,1,-1,1,-1,1,-1},new int[]{0,10,25,50,100,150,200,250,300,400,450},new int[]{1,1,1,1,1,1,1,1,1,1,1},new int[]{3003,3004,3006,3008,3011,3013,3016,3019,3021,3026,3028},new int[]{0,1,1,1,1,1,1,1,1,1,1},10));
			this.AllData.Add("9",new parkingCell("停车场",8,0,4,15,1,163,"128",2,6,2,58,"12800",4,"128",3,570,new int[]{1,2,3,4,5,6,7,8},"parking_01",new string[]{"9001","9002","9003","9004","9005","9006","9007","9008"},new int[]{-1,1,1,-1,1,-1,1,-1},new int[]{0,10,25,50,100,150,200,250,300,400,500,570},new int[]{1,1,1,1,1,1,1,1,1,1,1,1},new int[]{3003,3005,3007,3009,3012,3014,3017,3019,3022,3026,3031,3034},new int[]{0,1,1,1,1,1,1,1,1,1,1,1},11));
			this.AllData.Add("10",new parkingCell("停车场",9,0,4,15,1,163,"512",2,6,2,58,"51200",4,"512",3,720,new int[]{1,2,3,4,5,6,7,8},"parking_01",new string[]{"9001","9002","9003","9004","9005","9006","9007","9008"},new int[]{-1,1,1,-1,1,-1,1,-1},new int[]{0,10,25,50,100,150,200,250,300,400,500,600,700,720},new int[]{1,1,1,1,1,1,1,1,1,1,1,1,1,1},new int[]{3003,3006,3007,3009,3012,3014,3017,3020,3022,3027,3031,3036,3040,3041},new int[]{0,1,1,1,1,1,1,1,1,1,1,1,1,1},13));
			this.AllData.Add("11",new parkingCell("停车场",10,0,4,15,1,163,"4",2,6,2,58,"400",4,"4",3,165,new int[]{1,2,3,4,5,6,7,8},"parking_01",new string[]{"9001","9002","9003","9004","9005","9006","9007","9008"},new int[]{-1,1,1,-1,1,-1,1,-1},new int[]{0,10,25,50,100,150,165},new int[]{1,1,1,1,1,1,1},new int[]{3003,3004,3005,3007,3010,3013,3014},new int[]{0,1,1,1,1,1,1},6));
			this.AllData.Add("12",new parkingCell("停车场",11,0,4,15,1,163,"16",2,6,2,58,"1600",4,"16",3,320,new int[]{1,2,3,4,5,6,7,8},"parking_01",new string[]{"9001","9002","9003","9004","9005","9006","9007","9008"},new int[]{-1,1,1,-1,1,-1,1,-1},new int[]{0,10,25,50,100,150,200,250,300,320},new int[]{1,1,1,1,1,1,1,1,1,1},new int[]{3003,3004,3006,3008,3011,3013,3016,3018,3021,3022},new int[]{0,1,1,1,1,1,1,1,1,1},9));
			this.AllData.Add("13",new parkingCell("停车场",12,0,4,15,1,163,"64",2,6,2,58,"6400",4,"64",3,450,new int[]{1,2,3,4,5,6,7,8},"parking_01",new string[]{"9001","9002","9003","9004","9005","9006","9007","9008"},new int[]{-1,1,1,-1,1,-1,1,-1},new int[]{0,10,25,50,100,150,200,250,300,400,450},new int[]{1,1,1,1,1,1,1,1,1,1,1},new int[]{3003,3005,3006,3008,3012,3013,3016,3019,3021,3026,3028},new int[]{0,1,1,1,1,1,1,1,1,1,1},10));
			this.AllData.Add("14",new parkingCell("停车场",13,0,4,15,1,163,"256",2,6,2,58,"25600",4,"256",3,570,new int[]{1,2,3,4,5,6,7,8},"parking_01",new string[]{"9001","9002","9003","9004","9005","9006","9007","9008"},new int[]{-1,1,1,-1,1,-1,1,-1},new int[]{0,10,25,50,100,150,200,250,300,400,500,570},new int[]{1,1,1,1,1,1,1,1,1,1,1,1},new int[]{3003,3005,3007,3009,3012,3014,3017,3019,3022,3027,3031,3034},new int[]{0,1,1,1,1,1,1,1,1,1,1,1},11));
			this.AllData.Add("15",new parkingCell("停车场",14,0,4,15,1,163,"1024",2,6,2,58,"102400",4,"1024",3,720,new int[]{1,2,3,4,5,6,7,8},"parking_01",new string[]{"9001","9002","9003","9004","9005","9006","9007","9008"},new int[]{-1,1,1,-1,1,-1,1,-1},new int[]{0,10,25,50,100,150,200,250,300,400,500,600,700,720},new int[]{1,1,1,1,1,1,1,1,1,1,1,1,1,1},new int[]{3003,3006,3008,3010,3013,3015,3018,3020,3022,3027,3032,3036,3040,3041},new int[]{0,1,1,1,1,1,1,1,1,1,1,1,1,1},13));
			this.AllData.Add("16",new parkingCell("停车场",15,0,4,15,1,163,"8",2,6,2,58,"800",4,"8",3,165,new int[]{1,2,3,4,5,6,7,8},"parking_01",new string[]{"9001","9002","9003","9004","9005","9006","9007","9008"},new int[]{-1,1,1,-1,1,-1,1,-1},new int[]{0,10,25,50,100,150,165},new int[]{1,1,1,1,1,1,1},new int[]{3003,3004,3006,3008,3011,3013,3014},new int[]{0,1,1,1,1,1,1},6));
			this.AllData.Add("17",new parkingCell("停车场",16,0,4,15,1,163,"32",2,6,2,58,"3200",4,"32",3,320,new int[]{1,2,3,4,5,6,7,8},"parking_01",new string[]{"9001","9002","9003","9004","9005","9006","9007","9008"},new int[]{-1,1,1,-1,1,-1,1,-1},new int[]{0,10,25,50,100,150,200,250,300,320},new int[]{1,1,1,1,1,1,1,1,1,1},new int[]{3003,3004,3006,3008,3011,3013,3016,3019,3021,3022},new int[]{0,1,1,1,1,1,1,1,1,1},9));
			this.AllData.Add("18",new parkingCell("停车场",17,0,4,15,1,163,"128",2,6,2,58,"12800",4,"128",3,450,new int[]{1,2,3,4,5,6,7,8},"parking_01",new string[]{"9001","9002","9003","9004","9005","9006","9007","9008"},new int[]{-1,1,1,-1,1,-1,1,-1},new int[]{0,10,25,50,100,150,200,250,300,400,450},new int[]{1,1,1,1,1,1,1,1,1,1,1},new int[]{3003,3005,3007,3009,3012,3014,3017,3019,3022,3026,3029},new int[]{0,1,1,1,1,1,1,1,1,1,1},10));
			this.AllData.Add("19",new parkingCell("停车场",18,0,4,15,1,163,"512",2,6,2,58,"51200",4,"512",3,570,new int[]{1,2,3,4,5,6,7,8},"parking_01",new string[]{"9001","9002","9003","9004","9005","9006","9007","9008"},new int[]{-1,1,1,-1,1,-1,1,-1},new int[]{0,10,25,50,100,150,200,250,300,400,500,570},new int[]{1,1,1,1,1,1,1,1,1,1,1,1},new int[]{3003,3006,3007,3009,3012,3014,3017,3020,3022,3027,3031,3034},new int[]{0,1,1,1,1,1,1,1,1,1,1,1},11));
			this.AllData.Add("20",new parkingCell("停车场",19,0,4,15,1,163,"2048",2,6,2,58,"204800",4,"2048",3,720,new int[]{1,2,3,4,5,6,7,8},"parking_01",new string[]{"9001","9002","9003","9004","9005","9006","9007","9008"},new int[]{-1,1,1,-1,1,-1,1,-1},new int[]{0,10,25,50,100,150,200,250,300,400,500,600,700,720},new int[]{1,1,1,1,1,1,1,1,1,1,1,1,1,1},new int[]{3003,3006,3008,3010,3013,3015,3018,3020,3023,3028,3032,3036,3041,3042},new int[]{0,1,1,1,1,1,1,1,1,1,1,1,1,1},13));
		}
	}
	public class parkingCell
	{
		///<summary>
		///停车场名称
		///<summary>
		public readonly string buildname;
		///<summary>
		///所属场景
		///<summary>
		public readonly int scene;
		///<summary>
		///停车费初始
		///<summary>
		public readonly int price;
		///<summary>
		///停车费公式
		///<summary>
		public readonly int priceformula;
		///<summary>
		///停车场来人速度初始
		///<summary>
		public readonly int touristbase;
		///<summary>
		///停车场来人速度公式
		///<summary>
		public readonly int touristformula;
		///<summary>
		///速度最大等级
		///<summary>
		public readonly int touristmaxlv;
		///<summary>
		///停车场来人速度升级消耗初始
		///<summary>
		public readonly string touristcastbase;
		///<summary>
		///停车场来人速度升级公式
		///<summary>
		public readonly int touristcastformula;
		///<summary>
		///停车场最大位置初始
		///<summary>
		public readonly int spacebase;
		///<summary>
		///停车场最大位置公式
		///<summary>
		public readonly int spaceformula;
		///<summary>
		///最大位置等级
		///<summary>
		public readonly int spacemaxlv;
		///<summary>
		///停车场最大位置升级消耗初始
		///<summary>
		public readonly string spaceupcastbase;
		///<summary>
		///停车场最大位置升级消耗公式
		///<summary>
		public readonly int spaceupcastformula;
		///<summary>
		///停车场利润升级初始消耗
		///<summary>
		public readonly string depletebase;
		///<summary>
		///停车场利润升级消耗公式
		///<summary>
		public readonly int depleteformula;
		///<summary>
		///等级上限
		///<summary>
		public readonly int lvmax;
		///<summary>
		///停车场开启等级
		///<summary>
		public readonly int[] openlv;
		///<summary>
		///停车场预制体名称
		///<summary>
		public readonly string prefabsname;
		///<summary>
		///开启组资源
		///<summary>
		public readonly string[] openggroup;
		///<summary>
		///停车位位置
		///<summary>
		public readonly int[] openseatdir;
		///<summary>
		///等级阶段
		///<summary>
		public readonly int[] lvshage;
		///<summary>
		///奖励类型
		///<summary>
		public readonly int[] lvrewardtype;
		///<summary>
		///奖励道具ID
		///<summary>
		public readonly int[] lvreward;
		///<summary>
		///奖励星星数量
		///<summary>
		public readonly int[] star;
		///<summary>
		///总星数
		///<summary>
		public readonly int starsum;
		public parkingCell(string buildname,int scene,int price,int priceformula,int touristbase,int touristformula,int touristmaxlv,string touristcastbase,int touristcastformula,int spacebase,int spaceformula,int spacemaxlv,string spaceupcastbase,int spaceupcastformula,string depletebase,int depleteformula,int lvmax,int[] openlv,string prefabsname,string[] openggroup,int[] openseatdir,int[] lvshage,int[] lvrewardtype,int[] lvreward,int[] star,int starsum){
			this.buildname = buildname;
			this.scene = scene;
			this.price = price;
			this.priceformula = priceformula;
			this.touristbase = touristbase;
			this.touristformula = touristformula;
			this.touristmaxlv = touristmaxlv;
			this.touristcastbase = touristcastbase;
			this.touristcastformula = touristcastformula;
			this.spacebase = spacebase;
			this.spaceformula = spaceformula;
			this.spacemaxlv = spacemaxlv;
			this.spaceupcastbase = spaceupcastbase;
			this.spaceupcastformula = spaceupcastformula;
			this.depletebase = depletebase;
			this.depleteformula = depleteformula;
			this.lvmax = lvmax;
			this.openlv = openlv;
			this.prefabsname = prefabsname;
			this.openggroup = openggroup;
			this.openseatdir = openseatdir;
			this.lvshage = lvshage;
			this.lvrewardtype = lvrewardtype;
			this.lvreward = lvreward;
			this.star = star;
			this.starsum = starsum;
		}
	}
}