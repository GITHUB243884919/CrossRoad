using UnityEngine;
using System;
using System.Security;
using System.Collections.Generic;
namespace Config
{
	public class crossroadstageConfig
	{
		private crossroadstageConfig(){ 
		}
		private static crossroadstageConfig _inst;
		public static crossroadstageConfig getInstace(){
			if (_inst != null) {
				return _inst;
			}
			_inst = new crossroadstageConfig ();
			_inst.InitData ();
			return _inst;
		}
		public Dictionary<string,crossroadstageCell> AllData;
		public crossroadstageCell getCell(string key){
			crossroadstageCell t = null;
			this.AllData.TryGetValue (key, out t);
			return t;
		}
		public crossroadstageCell getCell(int key){
			crossroadstageCell t = null;
			this.AllData.TryGetValue (key.ToString(), out t);
			return t;
		}
		public readonly int RowNum = 49;
		private void InitData(){
			this.AllData = new Dictionary<string,crossroadstageCell> ();
			this.AllData.Add("1",new crossroadstageCell(5,6,5f,1,1,5f,8f,3f,5f,1,10,2));
			this.AllData.Add("2",new crossroadstageCell(5,6,5f,1,1,7f,10f,3f,5f,1,10,2));
			this.AllData.Add("3",new crossroadstageCell(5,6,5f,1,2,5f,8f,3f,5f,1,10,2));
			this.AllData.Add("4",new crossroadstageCell(5,6,5f,1,2,7f,10f,3f,5f,1,10,2));
			this.AllData.Add("5",new crossroadstageCell(5,6,5f,1,3,9f,12f,3f,5f,1,10,2));
			this.AllData.Add("6",new crossroadstageCell(5,6,5f,1,3,11f,14f,2f,4f,1,10,2));
			this.AllData.Add("7",new crossroadstageCell(5,6,5f,1,3,13f,16f,2f,4f,1,10,2));
			this.AllData.Add("8",new crossroadstageCell(5,6,5f,2,2,9f,12f,3f,5f,1,10,2));
			this.AllData.Add("9",new crossroadstageCell(5,6,5f,2,2,11f,14f,2f,4f,1,10,2));
			this.AllData.Add("10",new crossroadstageCell(5,6,5f,2,2,13f,16f,2f,4f,1,10,2));
			this.AllData.Add("11",new crossroadstageCell(10,6,5f,2,3,9f,12f,3f,5f,1,10,2));
			this.AllData.Add("12",new crossroadstageCell(10,6,5f,2,3,11f,14f,2f,4f,1,10,2));
			this.AllData.Add("13",new crossroadstageCell(10,6,5f,2,3,13f,16f,2f,4f,1,10,2));
			this.AllData.Add("14",new crossroadstageCell(10,6,5f,2,4,15f,18f,2f,4f,1,10,2));
			this.AllData.Add("15",new crossroadstageCell(10,6,5f,2,4,15f,18f,1f,3f,1,10,2));
			this.AllData.Add("16",new crossroadstageCell(10,6,5f,2,4,17f,20f,2f,4f,1,10,2));
			this.AllData.Add("17",new crossroadstageCell(10,6,5f,2,4,17f,20f,1f,3f,1,10,2));
			this.AllData.Add("18",new crossroadstageCell(10,6,5f,3,3,15f,18f,2f,4f,1,10,2));
			this.AllData.Add("19",new crossroadstageCell(10,6,5f,3,3,15f,18f,1f,3f,1,10,2));
			this.AllData.Add("20",new crossroadstageCell(10,6,5f,3,3,17f,20f,2f,4f,1,10,2));
			this.AllData.Add("21",new crossroadstageCell(15,6,5f,3,3,17f,20f,1f,3f,1,10,2));
			this.AllData.Add("22",new crossroadstageCell(15,6,5f,3,4,15f,18f,2f,4f,1,10,2));
			this.AllData.Add("23",new crossroadstageCell(15,6,5f,3,4,15f,18f,1f,3f,1,10,2));
			this.AllData.Add("24",new crossroadstageCell(15,6,5f,3,4,17f,20f,2f,4f,1,10,2));
			this.AllData.Add("25",new crossroadstageCell(15,6,5f,3,4,17f,20f,1f,3f,1,10,2));
			this.AllData.Add("26",new crossroadstageCell(15,6,5f,3,5,19f,22f,1f,3f,1,10,2));
			this.AllData.Add("27",new crossroadstageCell(15,6,5f,3,5,21f,24f,1f,3f,1,10,2));
			this.AllData.Add("28",new crossroadstageCell(15,6,5f,3,5,23f,26f,1f,3f,1,10,2));
			this.AllData.Add("29",new crossroadstageCell(15,6,5f,4,4,19f,22f,1f,3f,1,10,2));
			this.AllData.Add("30",new crossroadstageCell(15,6,5f,4,4,21f,24f,1f,3f,1,10,2));
			this.AllData.Add("31",new crossroadstageCell(15,6,5f,4,4,23f,26f,1f,3f,1,10,2));
			this.AllData.Add("32",new crossroadstageCell(15,6,5f,4,5,19f,22f,1f,3f,1,10,2));
			this.AllData.Add("33",new crossroadstageCell(15,6,5f,4,5,21f,24f,1f,3f,1,10,2));
			this.AllData.Add("34",new crossroadstageCell(15,6,5f,4,5,23f,26f,1f,3f,1,10,2));
			this.AllData.Add("35",new crossroadstageCell(15,6,5f,4,6,25f,28f,1f,3f,1,10,2));
			this.AllData.Add("36",new crossroadstageCell(15,6,5f,4,6,27f,30f,1f,3f,1,10,2));
			this.AllData.Add("37",new crossroadstageCell(15,6,5f,4,6,25f,28f,1f,2f,1,10,2));
			this.AllData.Add("38",new crossroadstageCell(15,6,5f,4,6,27f,30f,1f,2f,1,10,2));
			this.AllData.Add("39",new crossroadstageCell(15,6,5f,5,5,25f,28f,1f,3f,1,10,2));
			this.AllData.Add("40",new crossroadstageCell(15,6,5f,5,5,27f,30f,1f,3f,1,10,2));
			this.AllData.Add("41",new crossroadstageCell(15,6,5f,5,5,25f,28f,1f,2f,1,10,2));
			this.AllData.Add("42",new crossroadstageCell(15,6,5f,5,5,27f,30f,1f,2f,1,10,2));
			this.AllData.Add("43",new crossroadstageCell(15,6,5f,5,6,25f,28f,1f,3f,1,10,2));
			this.AllData.Add("44",new crossroadstageCell(15,6,5f,5,6,27f,30f,1f,3f,1,10,2));
			this.AllData.Add("45",new crossroadstageCell(15,6,5f,5,6,25f,28f,1f,2f,1,10,2));
			this.AllData.Add("46",new crossroadstageCell(15,6,5f,5,6,27f,30f,1f,2f,1,10,2));
			this.AllData.Add("47",new crossroadstageCell(15,6,5f,6,6,29f,32f,1f,2f,1,10,2));
			this.AllData.Add("48",new crossroadstageCell(15,6,5f,6,6,31f,34f,1f,2f,1,10,2));
			this.AllData.Add("49",new crossroadstageCell(15,6,5f,6,6,33f,36f,1f,2f,1,10,2));
		}
	}
	public class crossroadstageCell
	{
		///<summary>
		///初始动物数量
		///<summary>
		public readonly int animalnum;
		///<summary>
		///马路数量
		///<summary>
		public readonly int roadnum;
		///<summary>
		///小动物移动速度
		///<summary>
		public readonly float animalspeed;
		///<summary>
		///车道刷量下限
		///<summary>
		public readonly int roadweightmin;
		///<summary>
		///车道刷量上限
		///<summary>
		public readonly int roadweightmax;
		///<summary>
		///行驶速度下限
		///<summary>
		public readonly float speedmin;
		///<summary>
		///行驶速度上限
		///<summary>
		public readonly float speedmax;
		///<summary>
		///刷新间隔下限
		///<summary>
		public readonly float Intervalmin;
		///<summary>
		///刷新间隔上限
		///<summary>
		public readonly float Intervalmax;
		///<summary>
		///货币奖励类型
		///<summary>
		public readonly int rewardtype;
		///<summary>
		///奖励数量
		///<summary>
		public readonly int firstgoldreward;
		///<summary>
		///奖励翻倍
		///<summary>
		public readonly int warddouble;
		public crossroadstageCell(int animalnum,int roadnum,float animalspeed,int roadweightmin,int roadweightmax,float speedmin,float speedmax,float Intervalmin,float Intervalmax,int rewardtype,int firstgoldreward,int warddouble){
			this.animalnum = animalnum;
			this.roadnum = roadnum;
			this.animalspeed = animalspeed;
			this.roadweightmin = roadweightmin;
			this.roadweightmax = roadweightmax;
			this.speedmin = speedmin;
			this.speedmax = speedmax;
			this.Intervalmin = Intervalmin;
			this.Intervalmax = Intervalmax;
			this.rewardtype = rewardtype;
			this.firstgoldreward = firstgoldreward;
			this.warddouble = warddouble;
		}
	}
}