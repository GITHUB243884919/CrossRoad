using UnityEngine;
using System;
using System.Security;
using System.Collections.Generic;
namespace Config
{
	public class sceneaddvisitorConfig
	{
		private sceneaddvisitorConfig(){ 
		}
		private static sceneaddvisitorConfig _inst;
		public static sceneaddvisitorConfig getInstace(){
			if (_inst != null) {
				return _inst;
			}
			_inst = new sceneaddvisitorConfig ();
			_inst.InitData ();
			return _inst;
		}
		public Dictionary<string,sceneaddvisitorCell> AllData;
		public sceneaddvisitorCell getCell(string key){
			sceneaddvisitorCell t = null;
			this.AllData.TryGetValue (key, out t);
			return t;
		}
		public sceneaddvisitorCell getCell(int key){
			sceneaddvisitorCell t = null;
			this.AllData.TryGetValue (key.ToString(), out t);
			return t;
		}
		public readonly int RowNum = 2;
		private void InitData(){
			this.AllData = new Dictionary<string,sceneaddvisitorCell> ();
			this.AllData.Add("0",new sceneaddvisitorCell("轮船游客","path_ship_into","path_ship_into",6f));
			this.AllData.Add("1",new sceneaddvisitorCell("火车游客","path_train_into","path_train_into",60f));
		}
	}
	public class sceneaddvisitorCell
	{
		///<summary>
		///备注
		///<summary>
		public readonly string visitortype;
		///<summary>
		///入场路线
		///<summary>
		public readonly string intopath;
		///<summary>
		///离开路线
		///<summary>
		public readonly string outpath;
		///<summary>
		///移动速度
		///<summary>
		public readonly float movespeed;
		public sceneaddvisitorCell(string visitortype,string intopath,string outpath,float movespeed){
			this.visitortype = visitortype;
			this.intopath = intopath;
			this.outpath = outpath;
			this.movespeed = movespeed;
		}
	}
}