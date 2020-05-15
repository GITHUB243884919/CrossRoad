using UnityEngine;
using System;
using System.Security;
using System.Collections.Generic;
namespace Config
{
	public class buffConfig
	{
		private buffConfig(){ 
		}
		private static buffConfig _inst;
		public static buffConfig getInstace(){
			if (_inst != null) {
				return _inst;
			}
			_inst = new buffConfig ();
			_inst.InitData ();
			return _inst;
		}
		public Dictionary<string,buffCell> AllData;
		public buffCell getCell(string key){
			buffCell t = null;
			this.AllData.TryGetValue (key, out t);
			return t;
		}
		public buffCell getCell(int key){
			buffCell t = null;
			this.AllData.TryGetValue (key.ToString(), out t);
			return t;
		}
		public readonly int RowNum = 15;
		private void InitData(){
			this.AllData = new Dictionary<string,buffCell> ();
			this.AllData.Add("1",new buffCell("Double_1","2倍收益","","所有动物园在1小时内带来2倍收入",1,2f,30,1,"相加"));
			this.AllData.Add("2",new buffCell("Double_1","2倍收益","","所有动物园在2小时内带来2倍收入",1,2f,60,1,"相加"));
			this.AllData.Add("3",new buffCell("Double_1","5倍收益","","所有动物园在1小时内带来5倍收入",1,5f,90,1,"相加"));
			this.AllData.Add("4",new buffCell("Double_1","10倍收益","","所有动物园在1小时内带来10倍收入",1,10f,120,1,"相加"));
			this.AllData.Add("5",new buffCell("Double_1","20倍收益","","所有动物园在1小时内带来20倍收入",1,20f,150,1,"相加"));
			this.AllData.Add("6",new buffCell("Double_1","50倍收益","","所有动物园在1小时内带来50倍收入",1,50f,180,1,"相加"));
			this.AllData.Add("7",new buffCell("Double_1","100倍收益","","所有动物园在1小时内带来100倍收入",1,100f,210,1,"相加"));
			this.AllData.Add("8",new buffCell("Double_1","500倍收益","","所有动物园在1小时内带来500倍收入",1,500f,240,1,"相加"));
			this.AllData.Add("9",new buffCell("Double_1","1000倍收益","","所有动物园在1小时内带来1000倍收入",1,1000f,270,1,"相加"));
			this.AllData.Add("10",new buffCell("Double_1","动物栏秒","","动物栏观赏时间为0",2,0.2f,45,1,"无"));
			this.AllData.Add("11",new buffCell("Double_1","缆车秒","","大巴车乘车速度为0",3,0.2f,120,1,"无"));
			this.AllData.Add("12",new buffCell("Double_1","售票口秒","","售票口售票速度为0",4,0.2f,45,1,"无"));
			this.AllData.Add("13",new buffCell("Double_1","月卡","","月卡",5,2f,300,1,"相乘"));
			this.AllData.Add("14",new buffCell("Double_1","广告","","广告",5,2f,3600,1,"相乘"));
			this.AllData.Add("15",new buffCell("Double_1","季卡","","季卡",5,2f,900,1,"相乘"));
		}
	}
	public class buffCell
	{
		///<summary>
		///名称翻译
		///<summary>
		public readonly string nametranslate;
		///<summary>
		///名称备注
		///<summary>
		public readonly string nameremark;
		///<summary>
		///图标
		///<summary>
		public readonly string icon;
		///<summary>
		///介绍翻译
		///<summary>
		public readonly string desctranslate;
		///<summary>
		///BUFF类型
		///<summary>
		public readonly int bufftype;
		///<summary>
		///参数
		///<summary>
		public readonly float multiple;
		///<summary>
		///持续时间
		///<summary>
		public readonly int time;
		///<summary>
		///同类型同参数是否合并（0不可合并1可合并）
		///<summary>
		public readonly int merge;
		///<summary>
		///与基数关系
		///<summary>
		public readonly string relation;
		public buffCell(string nametranslate,string nameremark,string icon,string desctranslate,int bufftype,float multiple,int time,int merge,string relation){
			this.nametranslate = nametranslate;
			this.nameremark = nameremark;
			this.icon = icon;
			this.desctranslate = desctranslate;
			this.bufftype = bufftype;
			this.multiple = multiple;
			this.time = time;
			this.merge = merge;
			this.relation = relation;
		}
	}
}