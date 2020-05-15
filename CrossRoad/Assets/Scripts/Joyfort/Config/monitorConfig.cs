using UnityEngine;
using System;
using System.Security;
using System.Collections.Generic;
namespace Config
{
	public class monitorConfig
	{
		private monitorConfig(){ 
		}
		private static monitorConfig _inst;
		public static monitorConfig getInstace(){
			if (_inst != null) {
				return _inst;
			}
			_inst = new monitorConfig ();
			_inst.InitData ();
			return _inst;
		}
		public Dictionary<string,monitorCell> AllData;
		public monitorCell getCell(string key){
			monitorCell t = null;
			this.AllData.TryGetValue (key, out t);
			return t;
		}
		public monitorCell getCell(int key){
			monitorCell t = null;
			this.AllData.TryGetValue (key.ToString(), out t);
			return t;
		}
		public readonly int RowNum = 30;
		private void InitData(){
			this.AllData = new Dictionary<string,monitorCell> ();
			this.AllData.Add("1",new monitorCell("parking1_levelup","利润翻倍"));
			this.AllData.Add("2",new monitorCell("parking2_levelup","停车位扩建"));
			this.AllData.Add("3",new monitorCell("parking3_levelup","每分钟招揽游客"));
			this.AllData.Add("4",new monitorCell("ticket_num_levelup0","售票口门票价格"));
			this.AllData.Add("5",new monitorCell("ticket_num_levelup1","售票口1号"));
			this.AllData.Add("6",new monitorCell("ticket_num_levelup2","售票口2号"));
			this.AllData.Add("7",new monitorCell("ticket_num_levelup3","售票口3号"));
			this.AllData.Add("8",new monitorCell("ticket_num_levelup4","售票口4号"));
			this.AllData.Add("9",new monitorCell("ticket_num_levelup5","售票口5号"));
			this.AllData.Add("10",new monitorCell("ticket_num_levelup6","售票口6号"));
			this.AllData.Add("11",new monitorCell("ticket_num_levelup7","售票口7号"));
			this.AllData.Add("12",new monitorCell("ticket_num_levelup8","售票口8号"));
			this.AllData.Add("13",new monitorCell("build1_levelup","观光费用"));
			this.AllData.Add("14",new monitorCell("build2_levelup","观光位增加"));
			this.AllData.Add("15",new monitorCell("build3_levelup","每分钟观光游客"));
			this.AllData.Add("16",new monitorCell("animal_levelup","动物培养"));
			this.AllData.Add("17",new monitorCell("video_type_addtourist_star","游轮增加游客广告"));
			this.AllData.Add("18",new monitorCell("video_type_addtourist_finish","游轮增加游客广告"));
			this.AllData.Add("19",new monitorCell("video_type_tickettime_star","售票口秒CD广告"));
			this.AllData.Add("20",new monitorCell("video_type_tickettime_finish","售票口秒CD广告"));
			this.AllData.Add("21",new monitorCell("video_type_buildtime_star","动物栏秒CD广告"));
			this.AllData.Add("22",new monitorCell("video_type_buildtime_finish","动物栏秒CD广告"));
			this.AllData.Add("23",new monitorCell("video_type_double_star","利润翻倍广告"));
			this.AllData.Add("24",new monitorCell("video_type_double_finish","利润翻倍广告"));
			this.AllData.Add("25",new monitorCell("video_type_offlinereward_star","离线翻倍广告"));
			this.AllData.Add("26",new monitorCell("video_type_offlinereward_finish","离线翻倍广告"));
			this.AllData.Add("27",new monitorCell("video_type_viptiming_star","贵宾定时广告"));
			this.AllData.Add("28",new monitorCell("video_type_viptiming_finish","贵宾定时广告"));
			this.AllData.Add("29",new monitorCell("mission_id","任务步骤id"));
			this.AllData.Add("30",new monitorCell("scene_name","世界地图场景名称"));
		}
	}
	public class monitorCell
	{
		///<summary>
		///类型
		///<summary>
		public readonly string keytype;
		///<summary>
		///描述
		///<summary>
		public readonly string valuetype;
		public monitorCell(string keytype,string valuetype){
			this.keytype = keytype;
			this.valuetype = valuetype;
		}
	}
}