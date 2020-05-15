using UnityEngine;
using System;
using System.Security;
using System.Collections.Generic;
namespace Config
{
	public class animalatlasConfig
	{
		private animalatlasConfig(){ 
		}
		private static animalatlasConfig _inst;
		public static animalatlasConfig getInstace(){
			if (_inst != null) {
				return _inst;
			}
			_inst = new animalatlasConfig ();
			_inst.InitData ();
			return _inst;
		}
		public Dictionary<string,animalatlasCell> AllData;
		public animalatlasCell getCell(string key){
			animalatlasCell t = null;
			this.AllData.TryGetValue (key, out t);
			return t;
		}
		public animalatlasCell getCell(int key){
			animalatlasCell t = null;
			this.AllData.TryGetValue (key.ToString(), out t);
			return t;
		}
		public readonly int RowNum = 38;
		private void InitData(){
			this.AllData = new Dictionary<string,animalatlasCell> ();
			this.AllData.Add("1",new animalatlasCell(1001,"老虎栏",0,new int[]{0,1,2,3,4}));
			this.AllData.Add("2",new animalatlasCell(1002,"狼栏",1,new int[]{0,1,2,3,4}));
			this.AllData.Add("3",new animalatlasCell(1003,"狮子栏",2,new int[]{0,1,2,3,4}));
			this.AllData.Add("4",new animalatlasCell(1004,"熊栏",3,new int[]{0,1,2,3,4}));
			this.AllData.Add("5",new animalatlasCell(1005,"鳄鱼栏",4,new int[]{0,1,2,3,4}));
			this.AllData.Add("6",new animalatlasCell(1011,"袋鼠栏",5,new int[]{0,1,2,3,4}));
			this.AllData.Add("7",new animalatlasCell(1012,"猴子栏",6,new int[]{0,1,2,3,4}));
			this.AllData.Add("8",new animalatlasCell(1013,"鹿栏",7,new int[]{0,1,2,3,4}));
			this.AllData.Add("9",new animalatlasCell(1014,"犀牛栏",8,new int[]{0,1,2,3,4}));
			this.AllData.Add("10",new animalatlasCell(1015,"大象栏",9,new int[]{0,1,2,3,4}));
			this.AllData.Add("11",new animalatlasCell(6016,"孔雀栏",10,new int[]{0,1,2,3,4}));
			this.AllData.Add("12",new animalatlasCell(6017,"鹳栏",11,new int[]{0,1,2,3,4}));
			this.AllData.Add("13",new animalatlasCell(6018,"鹰栏",12,new int[]{0,1,2,3,4}));
			this.AllData.Add("14",new animalatlasCell(6019,"鸵鸟栏",13,new int[]{0,1,2,3,4}));
			this.AllData.Add("15",new animalatlasCell(6020,"天鹅栏",14,new int[]{0,1,2,3,4}));
			this.AllData.Add("16",new animalatlasCell(6007,"蜥脚龙栏",15,new int[]{0,1,2,3,4}));
			this.AllData.Add("17",new animalatlasCell(6008,"角龙栏",16,new int[]{0,1,2,3,4}));
			this.AllData.Add("18",new animalatlasCell(6009,"剑龙栏",17,new int[]{0,1,2,3,4}));
			this.AllData.Add("19",new animalatlasCell(6010,"甲龙栏",18,new int[]{0,1,2,3,4}));
			this.AllData.Add("20",new animalatlasCell(11001,"骆驼栏",19,new int[]{0,1,2,3,4}));
			this.AllData.Add("21",new animalatlasCell(11002,"马栏",20,new int[]{0,1,2,3,4}));
			this.AllData.Add("22",new animalatlasCell(11003,"野猪栏",21,new int[]{0,1,2,3,4}));
			this.AllData.Add("23",new animalatlasCell(11004,"长颈鹿栏",22,new int[]{0,1,2,3,4}));
			this.AllData.Add("24",new animalatlasCell(11005,"鲨鱼栏",23,new int[]{0,1,2,3,4}));
			this.AllData.Add("25",new animalatlasCell(11011,"浣熊栏",24,new int[]{0,1,2,3,4}));
			this.AllData.Add("26",new animalatlasCell(11012,"野鸡栏",25,new int[]{0,1,2,3,4}));
			this.AllData.Add("27",new animalatlasCell(11013,"海龟栏",26,new int[]{0,1,2,3,4}));
			this.AllData.Add("28",new animalatlasCell(11014,"羚羊栏",27,new int[]{0,1,2,3,4}));
			this.AllData.Add("29",new animalatlasCell(11015,"豹子栏",28,new int[]{0,1,2,3,4}));
			this.AllData.Add("30",new animalatlasCell(16016,"兔子栏",29,new int[]{0,1,2,3,4}));
			this.AllData.Add("31",new animalatlasCell(16017,"旱獭栏",30,new int[]{0,1,2,3,4}));
			this.AllData.Add("32",new animalatlasCell(16018,"野牛栏",31,new int[]{0,1,2,3,4}));
			this.AllData.Add("33",new animalatlasCell(16019,"企鹅栏",32,new int[]{0,1,2,3,4}));
			this.AllData.Add("34",new animalatlasCell(16020,"海豹栏",33,new int[]{0,1,2,3,4}));
			this.AllData.Add("35",new animalatlasCell(17007,"翼龙栏",34,new int[]{0,1,2,3,4}));
			this.AllData.Add("36",new animalatlasCell(17008,"盗龙栏",35,new int[]{0,1,2,3,4}));
			this.AllData.Add("37",new animalatlasCell(17009,"兽脚龙栏",36,new int[]{0,1,2,3,4}));
			this.AllData.Add("38",new animalatlasCell(17010,"鸟脚龙栏",37,new int[]{0,1,2,3,4}));
		}
	}
	public class animalatlasCell
	{
		///<summary>
		///动物栏id
		///<summary>
		public readonly int buildid;
		///<summary>
		///中文备注
		///<summary>
		public readonly string text;
		///<summary>
		///动物大类型
		///<summary>
		public readonly int bigtype;
		///<summary>
		///动物小类型排序
		///<summary>
		public readonly int[] smalltypesort;
		public animalatlasCell(int buildid,string text,int bigtype,int[] smalltypesort){
			this.buildid = buildid;
			this.text = text;
			this.bigtype = bigtype;
			this.smalltypesort = smalltypesort;
		}
	}
}