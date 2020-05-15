using UnityEngine;
using System;
using System.Security;
using System.Collections.Generic;
namespace Config
{
	public class missionConfig
	{
		private missionConfig(){ 
		}
		private static missionConfig _inst;
		public static missionConfig getInstace(){
			if (_inst != null) {
				return _inst;
			}
			_inst = new missionConfig ();
			_inst.InitData ();
			return _inst;
		}
		public Dictionary<string,missionCell> AllData;
		public missionCell getCell(string key){
			missionCell t = null;
			this.AllData.TryGetValue (key, out t);
			return t;
		}
		public missionCell getCell(int key){
			missionCell t = null;
			this.AllData.TryGetValue (key.ToString(), out t);
			return t;
		}
		public readonly int RowNum = 84;
		private void InitData(){
			this.AllData = new Dictionary<string,missionCell> ();
			this.AllData.Add("1",new missionCell(2,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1001",1,10,"3004",1,0));
			this.AllData.Add("2",new missionCell(3,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1001",2,2,"3004",1,0));
			this.AllData.Add("3",new missionCell(4,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1001",3,2,"3004",1,0));
			this.AllData.Add("4",new missionCell(5,"Ui_Text_88","停车场广告宣传升至{0}级","Ui_Text_88",1,"999",1,10,"3004",1,0));
			this.AllData.Add("5",new missionCell(6,"Ui_Text_89","停车场停车位升至{0}级","Ui_Text_89",1,"999",2,2,"3004",1,0));
			this.AllData.Add("6",new missionCell(7,"Ui_Text_90","停车场招揽游客升至{0}级","Ui_Text_90",1,"999",3,11,"3004",1,0));
			this.AllData.Add("7",new missionCell(8,"Ui_Text_91","售票口门票价格升至{0}级","Ui_Text_91",2,"-1",1,10,"3004",1,0));
			this.AllData.Add("8",new missionCell(9,"Ui_Text_92","{0}号售票口售票效率升至{1}级","Ui_Text_92",2,"0",2,8,"3004",1,0));
			this.AllData.Add("9",new missionCell(10,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1001",1,25,"3004",1,0));
			this.AllData.Add("10",new missionCell(11,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1001",2,3,"3004",1,0));
			this.AllData.Add("11",new missionCell(12,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1001",3,3,"3004",1,0));
			this.AllData.Add("12",new missionCell(13,"Ui_Text_96","{0}迎接{1}位观光游客","Ui_Text_96",4,"1001",1,5,"3004",0,0));
			this.AllData.Add("13",new missionCell(14,"Ui_Text_88","停车场广告宣传升至{0}级","Ui_Text_88",1,"999",1,25,"3004",1,0));
			this.AllData.Add("14",new missionCell(15,"Ui_Text_89","停车场停车位升至{0}级","Ui_Text_89",1,"999",2,3,"3004",1,0));
			this.AllData.Add("15",new missionCell(16,"Ui_Text_90","停车场招揽游客升至{0}级","Ui_Text_90",1,"999",3,22,"3004",1,0));
			this.AllData.Add("16",new missionCell(17,"Ui_Text_91","售票口门票价格升至{0}级","Ui_Text_91",2,"-1",1,25,"3004",1,0));
			this.AllData.Add("17",new missionCell(18,"Ui_Text_92","{0}号售票口售票效率升至{1}级","Ui_Text_92",2,"1",2,14,"3004",1,0));
			this.AllData.Add("18",new missionCell(19,"Ui_Text_121","开启{0}","Ui_Text_121",6,"1002",1,1,"3004",1,0));
			this.AllData.Add("19",new missionCell(20,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1002",1,10,"3005",1,0));
			this.AllData.Add("20",new missionCell(21,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1002",2,2,"3005",1,0));
			this.AllData.Add("21",new missionCell(22,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1002",3,2,"3005",1,0));
			this.AllData.Add("22",new missionCell(23,"Ui_Text_96","{0}迎接{1}位观光游客","Ui_Text_96",4,"1002",1,5,"3005",0,0));
			this.AllData.Add("23",new missionCell(24,"Ui_Text_88","停车场广告宣传升至{0}级","Ui_Text_88",1,"999",1,50,"3005",1,0));
			this.AllData.Add("24",new missionCell(25,"Ui_Text_89","停车场停车位升至{0}级","Ui_Text_89",1,"999",2,7,"3005",1,0));
			this.AllData.Add("25",new missionCell(26,"Ui_Text_90","停车场招揽游客升至{0}级","Ui_Text_90",1,"999",3,31,"3005",1,0));
			this.AllData.Add("26",new missionCell(27,"Ui_Text_91","售票口门票价格升至{0}级","Ui_Text_91",2,"-1",1,50,"3005",1,0));
			this.AllData.Add("27",new missionCell(28,"Ui_Text_92","{0}号售票口售票效率升至{1}级","Ui_Text_92",2,"2",2,12,"3006",1,0));
			this.AllData.Add("28",new missionCell(29,"Ui_Text_101","观看发布广告视频{0}次","Ui_Text_101",5,"Add_Viptiming_Advert",1,1,"3006",0,0));
			this.AllData.Add("29",new missionCell(30,"Ui_Text_121","开启{0}","Ui_Text_121",6,"1003",1,1,"3006",1,0));
			this.AllData.Add("30",new missionCell(31,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1003",1,10,"3006",1,0));
			this.AllData.Add("31",new missionCell(32,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1003",2,2,"3006",1,0));
			this.AllData.Add("32",new missionCell(33,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1003",3,2,"3007",1,0));
			this.AllData.Add("33",new missionCell(34,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1002",1,50,"3006",1,0));
			this.AllData.Add("34",new missionCell(35,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1002",2,4,"3006",1,0));
			this.AllData.Add("35",new missionCell(36,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1002",3,5,"3006",1,0));
			this.AllData.Add("36",new missionCell(37,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1001",1,100,"3007",1,0));
			this.AllData.Add("37",new missionCell(38,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1001",2,6,"3007",1,0));
			this.AllData.Add("38",new missionCell(39,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1001",3,9,"3007",1,0));
			this.AllData.Add("39",new missionCell(40,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1004",1,10,"3007",1,0));
			this.AllData.Add("40",new missionCell(41,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1004",2,2,"3008",1,0));
			this.AllData.Add("41",new missionCell(42,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1004",3,2,"3008",1,0));
			this.AllData.Add("42",new missionCell(43,"Ui_Text_96","{0}迎接{1}位观光游客","Ui_Text_96",4,"1004",1,10,"3008",0,0));
			this.AllData.Add("43",new missionCell(44,"Ui_Text_88","停车场广告宣传升至{0}级","Ui_Text_88",1,"999",1,84,"3008",1,0));
			this.AllData.Add("44",new missionCell(45,"Ui_Text_89","停车场停车位升至{0}级","Ui_Text_89",1,"999",2,11,"3008",1,0));
			this.AllData.Add("45",new missionCell(46,"Ui_Text_90","停车场招揽游客升至{0}级","Ui_Text_90",1,"999",3,35,"3008",1,0));
			this.AllData.Add("46",new missionCell(47,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1005",1,10,"3008",1,0));
			this.AllData.Add("47",new missionCell(48,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1005",2,2,"3008",1,0));
			this.AllData.Add("48",new missionCell(49,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1005",3,2,"3008",1,0));
			this.AllData.Add("49",new missionCell(50,"Ui_Text_96","{0}迎接{1}位观光游客","Ui_Text_96",4,"1005",1,10,"3008",0,0));
			this.AllData.Add("50",new missionCell(51,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1011",1,10,"3008",1,0));
			this.AllData.Add("51",new missionCell(52,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1011",2,2,"3008",1,0));
			this.AllData.Add("52",new missionCell(53,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1011",3,2,"3008",1,0));
			this.AllData.Add("53",new missionCell(54,"Ui_Text_96","{0}迎接{1}位观光游客","Ui_Text_96",4,"1011",1,10,"3008",0,0));
			this.AllData.Add("54",new missionCell(55,"Ui_Text_88","停车场广告宣传升至{0}级","Ui_Text_88",1,"999",1,100,"3009",1,0));
			this.AllData.Add("55",new missionCell(56,"Ui_Text_89","停车场停车位升至{0}级","Ui_Text_89",1,"999",2,12,"3009",1,0));
			this.AllData.Add("56",new missionCell(57,"Ui_Text_90","停车场招揽游客升至{0}级","Ui_Text_90",1,"999",3,37,"3009",1,0));
			this.AllData.Add("57",new missionCell(58,"Ui_Text_91","售票口门票价格升至{0}级","Ui_Text_91",2,"-1",1,100,"3009",1,0));
			this.AllData.Add("58",new missionCell(59,"Ui_Text_92","{0}号售票口售票效率升至{1}级","Ui_Text_92",2,"3",2,4,"3009",1,0));
			this.AllData.Add("59",new missionCell(60,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1012",1,45,"3009",1,0));
			this.AllData.Add("60",new missionCell(61,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1012",2,3,"3009",1,0));
			this.AllData.Add("61",new missionCell(62,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1012",3,4,"3009",1,0));
			this.AllData.Add("62",new missionCell(63,"Ui_Text_96","{0}迎接{1}位观光游客","Ui_Text_96",4,"1012",1,10,"3010",0,0));
			this.AllData.Add("63",new missionCell(64,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1013",1,50,"3010",1,0));
			this.AllData.Add("64",new missionCell(65,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1013",2,4,"3010",1,0));
			this.AllData.Add("65",new missionCell(66,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1013",3,5,"3010",1,0));
			this.AllData.Add("66",new missionCell(67,"Ui_Text_96","{0}迎接{1}位观光游客","Ui_Text_96",4,"1013",1,10,"3010",0,0));
			this.AllData.Add("67",new missionCell(68,"Ui_Text_88","停车场广告宣传升至{0}级","Ui_Text_88",1,"999",1,125,"3010",1,0));
			this.AllData.Add("68",new missionCell(69,"Ui_Text_89","停车场停车位升至{0}级","Ui_Text_89",1,"999",2,13,"3010",1,0));
			this.AllData.Add("69",new missionCell(70,"Ui_Text_90","停车场招揽游客升至{0}级","Ui_Text_90",1,"999",3,41,"3010",1,0));
			this.AllData.Add("70",new missionCell(71,"Ui_Text_91","售票口门票价格升至{0}级","Ui_Text_91",2,"-1",1,125,"3010",1,0));
			this.AllData.Add("71",new missionCell(72,"Ui_Text_92","{0}号售票口售票效率升至{1}级","Ui_Text_92",2,"0",2,71,"3010",1,0));
			this.AllData.Add("72",new missionCell(73,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1014",1,75,"3011",1,0));
			this.AllData.Add("73",new missionCell(74,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1014",2,5,"3011",1,0));
			this.AllData.Add("74",new missionCell(75,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1014",3,7,"3011",1,0));
			this.AllData.Add("75",new missionCell(76,"Ui_Text_96","{0}迎接{1}位观光游客","Ui_Text_96",4,"1014",1,10,"3011",0,0));
			this.AllData.Add("76",new missionCell(77,"Ui_Text_88","停车场广告宣传升至{0}级","Ui_Text_88",1,"999",1,150,"3011",1,0));
			this.AllData.Add("77",new missionCell(78,"Ui_Text_89","停车场停车位升至{0}级","Ui_Text_89",1,"999",2,15,"3011",1,0));
			this.AllData.Add("78",new missionCell(79,"Ui_Text_90","停车场招揽游客升至{0}级","Ui_Text_90",1,"999",3,46,"3011",1,0));
			this.AllData.Add("79",new missionCell(80,"Ui_Text_91","售票口门票价格升至{0}级","Ui_Text_91",2,"-1",1,150,"3011",1,0));
			this.AllData.Add("80",new missionCell(81,"Ui_Text_92","{0}号售票口售票效率升至{1}级","Ui_Text_92",2,"4",2,3,"3011",1,0));
			this.AllData.Add("81",new missionCell(82,"Ui_Text_93","{0}利润升至{1}级","Ui_Text_93",3,"1015",1,100,"3012",1,0));
			this.AllData.Add("82",new missionCell(83,"Ui_Text_94","{0}观光位升至{1}级","Ui_Text_94",3,"1015",2,6,"3012",1,0));
			this.AllData.Add("83",new missionCell(84,"Ui_Text_95","{0}观光效率升至{1}级","Ui_Text_95",3,"1015",3,9,"3012",1,0));
			this.AllData.Add("84",new missionCell(85,"Ui_Text_96","{0}迎接{1}位观光游客","Ui_Text_96",4,"1015",1,10,"3012",0,0));
		}
	}
	public class missionCell
	{
		///<summary>
		///后一个任务ID
		///<summary>
		public readonly int nextid;
		///<summary>
		///任务名翻译
		///<summary>
		public readonly string nametranslate;
		///<summary>
		///名称备注
		///<summary>
		public readonly string nameremarks;
		///<summary>
		///任务描述翻译
		///<summary>
		public readonly string description;
		///<summary>
		///任务类型
		///<summary>
		public readonly int tasktype;
		///<summary>
		///任务参数1
		///<summary>
		public readonly string taskparam1;
		///<summary>
		///任务参数2
		///<summary>
		public readonly int taskparam2;
		///<summary>
		///进度要求
		///<summary>
		public readonly int need;
		///<summary>
		///任务奖励
		///<summary>
		public readonly string reward;
		///<summary>
		///是否跳转
		///<summary>
		public readonly int skip;
		///<summary>
		///场景
		///<summary>
		public readonly int scene;
		public missionCell(int nextid,string nametranslate,string nameremarks,string description,int tasktype,string taskparam1,int taskparam2,int need,string reward,int skip,int scene){
			this.nextid = nextid;
			this.nametranslate = nametranslate;
			this.nameremarks = nameremarks;
			this.description = description;
			this.tasktype = tasktype;
			this.taskparam1 = taskparam1;
			this.taskparam2 = taskparam2;
			this.need = need;
			this.reward = reward;
			this.skip = skip;
			this.scene = scene;
		}
	}
}