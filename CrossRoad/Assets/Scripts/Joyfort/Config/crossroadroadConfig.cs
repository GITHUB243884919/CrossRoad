using UnityEngine;
using System;
using System.Security;
using System.Collections.Generic;
namespace Config
{
	public class crossroadroadConfig
	{
		private crossroadroadConfig(){ 
		}
		private static crossroadroadConfig _inst;
		public static crossroadroadConfig getInstace(){
			if (_inst != null) {
				return _inst;
			}
			_inst = new crossroadroadConfig ();
			_inst.InitData ();
			return _inst;
		}
		public Dictionary<string,crossroadroadCell> AllData;
		public crossroadroadCell getCell(string key){
			crossroadroadCell t = null;
			this.AllData.TryGetValue (key, out t);
			return t;
		}
		public crossroadroadCell getCell(int key){
			crossroadroadCell t = null;
			this.AllData.TryGetValue (key.ToString(), out t);
			return t;
		}
		public readonly int RowNum = 6;
		private void InitData(){
			this.AllData = new Dictionary<string,crossroadroadCell> ();
			this.AllData.Add("1",new crossroadroadCell(12001));
			this.AllData.Add("2",new crossroadroadCell(12002));
			this.AllData.Add("3",new crossroadroadCell(12003));
			this.AllData.Add("4",new crossroadroadCell(12004));
			this.AllData.Add("5",new crossroadroadCell(12005));
			this.AllData.Add("6",new crossroadroadCell(12006));
		}
	}
	public class crossroadroadCell
	{
		///<summary>
		///车道资源关联id
		///<summary>
		public readonly int resid;
		public crossroadroadCell(int resid){
			this.resid = resid;
		}
	}
}