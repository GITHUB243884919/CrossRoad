using UnityEngine;
using System;
using System.Security;
using System.Collections.Generic;
namespace Config
{
	public class TAEventConfig
	{
		private TAEventConfig(){ 
		}
		private static TAEventConfig _inst;
		public static TAEventConfig getInstace(){
			if (_inst != null) {
				return _inst;
			}
			_inst = new TAEventConfig ();
			_inst.InitData ();
			return _inst;
		}
		public Dictionary<string,TAEventCell> AllData;
		public TAEventCell getCell(string key){
			TAEventCell t = null;
			this.AllData.TryGetValue (key, out t);
			return t;
		}
		public TAEventCell getCell(int key){
			TAEventCell t = null;
			this.AllData.TryGetValue (key.ToString(), out t);
			return t;
		}
		public readonly int RowNum = 20;
		private void InitData(){
			this.AllData = new Dictionary<string,TAEventCell> ();
			this.AllData.Add("1",new TAEventCell("注册日志","","","","","","","","","",""));
			this.AllData.Add("2",new TAEventCell("启动游戏","","","","","","","","","",""));
			this.AllData.Add("3",new TAEventCell("进入引导","guide_id","{0}","guide_name","新手第{0}步","","","","","",""));
			this.AllData.Add("4",new TAEventCell("引导过程","guide_id","{0}","guide_name","新手第{0}步","","","","","",""));
			this.AllData.Add("5",new TAEventCell("结束引导","guide_id","{0}","guide_name","新手第{0}步","","","","","",""));
			this.AllData.Add("6",new TAEventCell("停车场升级","parking_id","{0}","parking_name","{0}","level","{0}","","","",""));
			this.AllData.Add("7",new TAEventCell("售票口升级","ticket_id","{0}","ticket_name","{0}","level","{0}","","","",""));
			this.AllData.Add("8",new TAEventCell("动物栏升级","build_id","{0}","build_name","{0}","build_attribute_id","{0}","build_attribute_name","{0}","level","{0}"));
			this.AllData.Add("9",new TAEventCell("动物培养","animal_id","{0}","animal_name","{0}","level","{0}","","","",""));
			this.AllData.Add("10",new TAEventCell("开启场景","scene_name","{0}","","","     ","","","","",""));
			this.AllData.Add("11",new TAEventCell("完成激励视频","video_type","{0}","video_name","{0}","video_num","{0}","","","",""));
			this.AllData.Add("12",new TAEventCell("完成任务","mission_id","{0}","","","","","","","",""));
			this.AllData.Add("13",new TAEventCell("","","","","","","","","","",""));
			this.AllData.Add("14",new TAEventCell("","","","","","","","","","",""));
			this.AllData.Add("15",new TAEventCell("","","","","","","","","","",""));
			this.AllData.Add("16",new TAEventCell("","","","","","","","","","",""));
			this.AllData.Add("17",new TAEventCell("","","","","","","","","","",""));
			this.AllData.Add("18",new TAEventCell("","","","","","","","","","",""));
			this.AllData.Add("19",new TAEventCell("","","","","","","","","","",""));
			this.AllData.Add("20",new TAEventCell("","","","","","","","","","",""));
		}
	}
	public class TAEventCell
	{
		///<summary>
		///中文事件名
		///<summary>
		public readonly string isName;
		///<summary>
		///属性1
		///<summary>
		public readonly string attribute1_key;
		///<summary>
		///属性1值
		///<summary>
		public readonly string attribute1_value;
		///<summary>
		///属性2
		///<summary>
		public readonly string attribute2_key;
		///<summary>
		///属性2值
		///<summary>
		public readonly string attribute2_value;
		///<summary>
		///属性3
		///<summary>
		public readonly string attribute3_key;
		///<summary>
		///属性3值
		///<summary>
		public readonly string attribute3_value;
		///<summary>
		///属性4
		///<summary>
		public readonly string attribute4_key;
		///<summary>
		///属性4值
		///<summary>
		public readonly string attribute4_value;
		///<summary>
		///属性5
		///<summary>
		public readonly string attribute5_key;
		///<summary>
		///属性5值
		///<summary>
		public readonly string attribute5_value;
		public TAEventCell(string isName,string attribute1_key,string attribute1_value,string attribute2_key,string attribute2_value,string attribute3_key,string attribute3_value,string attribute4_key,string attribute4_value,string attribute5_key,string attribute5_value){
			this.isName = isName;
			this.attribute1_key = attribute1_key;
			this.attribute1_value = attribute1_value;
			this.attribute2_key = attribute2_key;
			this.attribute2_value = attribute2_value;
			this.attribute3_key = attribute3_key;
			this.attribute3_value = attribute3_value;
			this.attribute4_key = attribute4_key;
			this.attribute4_value = attribute4_value;
			this.attribute5_key = attribute5_key;
			this.attribute5_value = attribute5_value;
		}
	}
}