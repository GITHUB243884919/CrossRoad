using UnityEngine;
using System;
using System.Security;
using System.Collections.Generic;
namespace Config
{
	public class skillConfig
	{
		private skillConfig(){ 
		}
		private static skillConfig _inst;
		public static skillConfig getInstace(){
			if (_inst != null) {
				return _inst;
			}
			_inst = new skillConfig ();
			_inst.InitData ();
			return _inst;
		}
		public Dictionary<string,skillCell> AllData;
		public skillCell getCell(string key){
			skillCell t = null;
			this.AllData.TryGetValue (key, out t);
			return t;
		}
		public skillCell getCell(int key){
			skillCell t = null;
			this.AllData.TryGetValue (key.ToString(), out t);
			return t;
		}
		public readonly int RowNum = 20;
		private void InitData(){
			this.AllData = new Dictionary<string,skillCell> ();
			this.AllData.Add("1",new skillCell("饲养员技能1",2,1,1,"","观赏时间缩短10%","",0,1));
			this.AllData.Add("2",new skillCell("饲养员技能2",2,1,2,"","5%几率双倍收益","",0,1));
			this.AllData.Add("3",new skillCell("饲养员技能3",2,2,3,"","牛B技能3","",0,1));
			this.AllData.Add("4",new skillCell("饲养员技能4",2,2,4,"","牛B技能4","",0,1));
			this.AllData.Add("5",new skillCell("饲养员技能5",2,3,5,"","牛B技能5","",0,1));
			this.AllData.Add("6",new skillCell("饲养员技能6",2,3,6,"","牛B技能6","",0,1));
			this.AllData.Add("7",new skillCell("饲养员技能7",2,4,7,"","牛B技能7","",0,1));
			this.AllData.Add("8",new skillCell("饲养员技能8",2,4,8,"","牛B技能8","",0,1));
			this.AllData.Add("9",new skillCell("饲养员技能9",2,5,9,"","牛B技能9","",0,1));
			this.AllData.Add("10",new skillCell("饲养员技能10",2,5,10,"","牛B技能10","",0,1));
			this.AllData.Add("11",new skillCell("高级动物技能1",2,1,11,"","牛B技能11","",0,3));
			this.AllData.Add("12",new skillCell("高级动物技能2",2,1,12,"","牛B技能12","",0,3));
			this.AllData.Add("13",new skillCell("高级动物技能3",2,1,13,"","牛B技能13","",0,3));
			this.AllData.Add("14",new skillCell("高级动物技能4",2,1,14,"","牛B技能14","",0,3));
			this.AllData.Add("15",new skillCell("高级动物技能5",2,1,15,"","牛B技能15","",0,3));
			this.AllData.Add("16",new skillCell("高级动物技能6",2,1,16,"","牛B技能16","",0,3));
			this.AllData.Add("17",new skillCell("高级动物技能7",2,1,17,"","牛B技能17","",0,3));
			this.AllData.Add("18",new skillCell("高级动物技能8",2,1,18,"","牛B技能18","",0,3));
			this.AllData.Add("19",new skillCell("高级动物技能9",2,1,19,"","牛B技能19","",0,3));
			this.AllData.Add("20",new skillCell("高级动物技能10",1,1,20,"","牛B技能20","",600,3));
		}
	}
	public class skillCell
	{
		///<summary>
		///名称
		///<summary>
		public readonly string skillname;
		///<summary>
		///类型
		///<summary>
		public readonly int type;
		///<summary>
		///品质
		///<summary>
		public readonly int quality;
		///<summary>
		///buffid
		///<summary>
		public readonly int buffid;
		///<summary>
		///图标
		///<summary>
		public readonly string icon;
		///<summary>
		///技能描述
		///<summary>
		public readonly string desc;
		///<summary>
		///技能参数
		///<summary>
		public readonly string parameter;
		///<summary>
		///冷却时间
		///<summary>
		public readonly int cdtime;
		///<summary>
		///所用对象
		///<summary>
		public readonly int target;
		public skillCell(string skillname,int type,int quality,int buffid,string icon,string desc,string parameter,int cdtime,int target){
			this.skillname = skillname;
			this.type = type;
			this.quality = quality;
			this.buffid = buffid;
			this.icon = icon;
			this.desc = desc;
			this.parameter = parameter;
			this.cdtime = cdtime;
			this.target = target;
		}
	}
}