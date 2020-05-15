using UnityEngine;
using System;
using System.Security;
using System.Collections.Generic;
namespace Config
{
	public class guideConfig
	{
		private guideConfig(){ 
		}
		private static guideConfig _inst;
		public static guideConfig getInstace(){
			if (_inst != null) {
				return _inst;
			}
			_inst = new guideConfig ();
			_inst.InitData ();
			return _inst;
		}
		public Dictionary<string,guideCell> AllData;
		public guideCell getCell(string key){
			guideCell t = null;
			this.AllData.TryGetValue (key, out t);
			return t;
		}
		public guideCell getCell(int key){
			guideCell t = null;
			this.AllData.TryGetValue (key.ToString(), out t);
			return t;
		}
		public readonly int RowNum = 14;
		private void InitData(){
			this.AllData = new Dictionary<string,guideCell> ();
			this.AllData.Add("0",new guideCell("Guide_Text_1",100f));
			this.AllData.Add("1",new guideCell("Guide_Text_2",100f));
			this.AllData.Add("2",new guideCell("Guide_Text_3",100f));
			this.AllData.Add("4",new guideCell("Guide_Text_4",100f));
			this.AllData.Add("7",new guideCell("Guide_Text_5",100f));
			this.AllData.Add("9",new guideCell("Guide_Text_6",100f));
			this.AllData.Add("11",new guideCell("Guide_Text_7",100f));
			this.AllData.Add("14",new guideCell("Guide_Text_8",100f));
			this.AllData.Add("16",new guideCell("Guide_Text_9",100f));
			this.AllData.Add("18",new guideCell("Guide_Text_10",100f));
			this.AllData.Add("19",new guideCell("Guide_Text_11",100f));
			this.AllData.Add("23",new guideCell("Guide_Text_12",100f));
			this.AllData.Add("24",new guideCell("Guide_Text_13",100f));
			this.AllData.Add("25",new guideCell("Guide_Text_14",100f));
		}
	}
	public class guideCell
	{
		///<summary>
		///关联翻译
		///<summary>
		public readonly string guidetext;
		///<summary>
		///时间
		///<summary>
		public readonly float time;
		public guideCell(string guidetext,float time){
			this.guidetext = guidetext;
			this.time = time;
		}
	}
}