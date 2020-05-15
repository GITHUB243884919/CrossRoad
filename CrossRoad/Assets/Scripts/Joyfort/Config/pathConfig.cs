using UnityEngine;
using System;
using System.Security;
using System.Collections.Generic;
namespace Config
{
	public class pathConfig
	{
		private pathConfig(){ 
		}
		private static pathConfig _inst;
		public static pathConfig getInstace(){
			if (_inst != null) {
				return _inst;
			}
			_inst = new pathConfig ();
			_inst.InitData ();
			return _inst;
		}
		public Dictionary<string,pathCell> AllData;
		public pathCell getCell(string key){
			pathCell t = null;
			this.AllData.TryGetValue (key, out t);
			return t;
		}
		public pathCell getCell(int key){
			pathCell t = null;
			this.AllData.TryGetValue (key.ToString(), out t);
			return t;
		}
		public readonly int RowNum = 100;
		private void InitData(){
			this.AllData = new Dictionary<string,pathCell> ();
			this.AllData.Add("1",new pathCell(1000,1001,"path_1000_1001"));
			this.AllData.Add("2",new pathCell(1000,1002,"path_1000_1002"));
			this.AllData.Add("3",new pathCell(1000,1003,"path_1000_1003"));
			this.AllData.Add("4",new pathCell(1001,1002,"path_1001_1002"));
			this.AllData.Add("5",new pathCell(1001,1003,"path_1001_1003"));
			this.AllData.Add("6",new pathCell(1001,1004,"path_1001_1004"));
			this.AllData.Add("7",new pathCell(1001,1005,"path_1001_1005"));
			this.AllData.Add("8",new pathCell(1002,1001,"path_1002_1001"));
			this.AllData.Add("9",new pathCell(1002,1003,"path_1002_1003"));
			this.AllData.Add("10",new pathCell(1002,1004,"path_1002_1004"));
			this.AllData.Add("11",new pathCell(1002,1005,"path_1002_1005"));
			this.AllData.Add("12",new pathCell(1003,1001,"path_1003_1001"));
			this.AllData.Add("13",new pathCell(1003,1002,"path_1003_1002"));
			this.AllData.Add("14",new pathCell(1003,1004,"path_1003_1004"));
			this.AllData.Add("15",new pathCell(1003,1005,"path_1003_1005"));
			this.AllData.Add("16",new pathCell(1004,1005,"path_1004_1005"));
			this.AllData.Add("17",new pathCell(1004,1006,"path_1004_1006"));
			this.AllData.Add("18",new pathCell(1004,1011,"path_1004_1011"));
			this.AllData.Add("19",new pathCell(1004,1012,"path_1004_1012"));
			this.AllData.Add("20",new pathCell(1004,1013,"path_1004_1013"));
			this.AllData.Add("21",new pathCell(1005,1004,"path_1005_1004"));
			this.AllData.Add("22",new pathCell(1005,1006,"path_1005_1006"));
			this.AllData.Add("23",new pathCell(1005,1011,"path_1005_1011"));
			this.AllData.Add("24",new pathCell(1005,1012,"path_1005_1012"));
			this.AllData.Add("25",new pathCell(1005,1013,"path_1005_1013"));
			this.AllData.Add("26",new pathCell(1006,1007,"path_1006_1007"));
			this.AllData.Add("27",new pathCell(1006,1008,"path_1006_1008"));
			this.AllData.Add("28",new pathCell(1006,1009,"path_1006_1009"));
			this.AllData.Add("29",new pathCell(1006,1010,"path_1006_1010"));
			this.AllData.Add("30",new pathCell(1007,1006,"path_1007_1006"));
			this.AllData.Add("31",new pathCell(1007,1008,"path_1007_1008"));
			this.AllData.Add("32",new pathCell(1007,1009,"path_1007_1009"));
			this.AllData.Add("33",new pathCell(1007,1010,"path_1007_1010"));
			this.AllData.Add("34",new pathCell(1008,1006,"path_1008_1006"));
			this.AllData.Add("35",new pathCell(1008,1007,"path_1008_1007"));
			this.AllData.Add("36",new pathCell(1008,1009,"path_1008_1009"));
			this.AllData.Add("37",new pathCell(1008,1010,"path_1008_1010"));
			this.AllData.Add("38",new pathCell(1009,1010,"path_1009_1010"));
			this.AllData.Add("39",new pathCell(1009,1011,"path_1009_1011"));
			this.AllData.Add("40",new pathCell(1009,1012,"path_1009_1012"));
			this.AllData.Add("41",new pathCell(1009,1013,"path_1009_1013"));
			this.AllData.Add("42",new pathCell(1010,1009,"path_1010_1009"));
			this.AllData.Add("43",new pathCell(1010,1011,"path_1010_1011"));
			this.AllData.Add("44",new pathCell(1010,1012,"path_1010_1012"));
			this.AllData.Add("45",new pathCell(1010,1013,"path_1010_1013"));
			this.AllData.Add("46",new pathCell(1011,1012,"path_1011_1012"));
			this.AllData.Add("47",new pathCell(1011,1013,"path_1011_1013"));
			this.AllData.Add("48",new pathCell(1011,1014,"path_1011_1014"));
			this.AllData.Add("49",new pathCell(1011,1015,"path_1011_1015"));
			this.AllData.Add("50",new pathCell(1012,1011,"path_1012_1011"));
			this.AllData.Add("51",new pathCell(1012,1013,"path_1012_1013"));
			this.AllData.Add("52",new pathCell(1012,1014,"path_1012_1014"));
			this.AllData.Add("53",new pathCell(1012,1015,"path_1012_1015"));
			this.AllData.Add("54",new pathCell(1013,1011,"path_1013_1011"));
			this.AllData.Add("55",new pathCell(1013,1012,"path_1013_1012"));
			this.AllData.Add("56",new pathCell(1013,1014,"path_1013_1014"));
			this.AllData.Add("57",new pathCell(1013,1015,"path_1013_1015"));
			this.AllData.Add("58",new pathCell(1014,1015,"path_1014_1015"));
			this.AllData.Add("62",new pathCell(1015,1014,"path_1015_1014"));
			this.AllData.Add("66",new pathCell(1016,1017,"path_1016_1017"));
			this.AllData.Add("67",new pathCell(1016,1018,"path_1016_1018"));
			this.AllData.Add("68",new pathCell(1016,1019,"path_1016_1019"));
			this.AllData.Add("69",new pathCell(1016,1020,"path_1016_1020"));
			this.AllData.Add("70",new pathCell(1017,1016,"path_1017_1016"));
			this.AllData.Add("71",new pathCell(1017,1018,"path_1017_1018"));
			this.AllData.Add("72",new pathCell(1017,1019,"path_1017_1019"));
			this.AllData.Add("73",new pathCell(1017,1020,"path_1017_1020"));
			this.AllData.Add("74",new pathCell(1018,1016,"path_1018_1016"));
			this.AllData.Add("75",new pathCell(1018,1017,"path_1018_1017"));
			this.AllData.Add("76",new pathCell(1018,1019,"path_1018_1019"));
			this.AllData.Add("77",new pathCell(1018,1020,"path_1018_1020"));
			this.AllData.Add("78",new pathCell(1019,1020,"path_1019_1020"));
			this.AllData.Add("79",new pathCell(1019,1007,"path_1019_1007"));
			this.AllData.Add("80",new pathCell(1019,1008,"path_1019_1008"));
			this.AllData.Add("81",new pathCell(1020,1019,"path_1020_1019"));
			this.AllData.Add("82",new pathCell(1020,1007,"path_1020_1007"));
			this.AllData.Add("83",new pathCell(1020,1008,"path_1020_1008"));
			this.AllData.Add("84",new pathCell(1001,1000,"path_1001_1000"));
			this.AllData.Add("85",new pathCell(1002,1000,"path_1002_1000"));
			this.AllData.Add("86",new pathCell(1003,1000,"path_1003_1000"));
			this.AllData.Add("87",new pathCell(1004,1000,"path_1004_1000"));
			this.AllData.Add("88",new pathCell(1005,1000,"path_1005_1000"));
			this.AllData.Add("89",new pathCell(1006,1000,"path_1006_1000"));
			this.AllData.Add("90",new pathCell(1007,1000,"path_1007_1000"));
			this.AllData.Add("91",new pathCell(1008,1000,"path_1008_1000"));
			this.AllData.Add("92",new pathCell(1009,1000,"path_1009_1000"));
			this.AllData.Add("93",new pathCell(1010,1000,"path_1010_1000"));
			this.AllData.Add("94",new pathCell(1011,1000,"path_1011_1000"));
			this.AllData.Add("95",new pathCell(1012,1000,"path_1012_1000"));
			this.AllData.Add("96",new pathCell(1013,1000,"path_1013_1000"));
			this.AllData.Add("97",new pathCell(1014,1000,"path_1014_1000"));
			this.AllData.Add("98",new pathCell(1015,1000,"path_1015_1000"));
			this.AllData.Add("99",new pathCell(1016,1000,"path_1016_1000"));
			this.AllData.Add("100",new pathCell(1017,1000,"path_1017_1000"));
			this.AllData.Add("101",new pathCell(1018,1000,"path_1018_1000"));
			this.AllData.Add("102",new pathCell(1019,1000,"path_1019_1000"));
			this.AllData.Add("103",new pathCell(1020,1000,"path_1020_1000"));
			this.AllData.Add("104",new pathCell(1000,1016,"path_1000_1016"));
			this.AllData.Add("105",new pathCell(1000,1017,"path_1000_1017"));
			this.AllData.Add("106",new pathCell(1000,1018,"path_1000_1018"));
		}
	}
	public class pathCell
	{
		///<summary>
		///起点建筑id
		///<summary>
		public readonly int startid;
		///<summary>
		///终点建筑id
		///<summary>
		public readonly int endid;
		///<summary>
		///路线名称
		///<summary>
		public readonly string path;
		public pathCell(int startid,int endid,string path){
			this.startid = startid;
			this.endid = endid;
			this.path = path;
		}
	}
}