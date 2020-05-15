using UnityEngine;
using System;
using System.Security;
using System.Collections.Generic;
namespace Config
{
	public class advertConfig
	{
		private advertConfig(){ 
		}
		private static advertConfig _inst;
		public static advertConfig getInstace(){
			if (_inst != null) {
				return _inst;
			}
			_inst = new advertConfig ();
			_inst.InitData ();
			return _inst;
		}
		public Dictionary<string,advertCell> AllData;
		public advertCell getCell(string key){
			advertCell t = null;
			this.AllData.TryGetValue (key, out t);
			return t;
		}
		public advertCell getCell(int key){
			advertCell t = null;
			this.AllData.TryGetValue (key.ToString(), out t);
			return t;
		}
		public readonly int RowNum = 7;
		private void InitData(){
			this.AllData = new Dictionary<string,advertCell> ();
			this.AllData.Add("Add_Tourist_Advert",new advertCell("增加游客广告"));
			this.AllData.Add("Add_Ticket_Advert",new advertCell("售票口秒CD广告"));
			this.AllData.Add("Add_Visit_Advert",new advertCell("动物栏秒CD广告"));
			this.AllData.Add("Add_Double_Advert",new advertCell("每日利润翻倍广告"));
			this.AllData.Add("Add_Offline_Advert",new advertCell("离线奖励广告"));
			this.AllData.Add("Add_Reward_Advert",new advertCell("奖励货币广告"));
			this.AllData.Add("Add_FreeItem_Advert",new advertCell("贵宾定时广告"));
		}
	}
	public class advertCell
	{
		///<summary>
		///描述
		///<summary>
		public readonly string test;
		public advertCell(string test){
			this.test = test;
		}
	}
}