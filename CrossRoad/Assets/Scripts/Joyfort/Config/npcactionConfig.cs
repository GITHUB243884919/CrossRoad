using UnityEngine;
using System;
using System.Security;
using System.Collections.Generic;
namespace Config
{
	public class npcactionConfig
	{
		private npcactionConfig(){ 
		}
		private static npcactionConfig _inst;
		public static npcactionConfig getInstace(){
			if (_inst != null) {
				return _inst;
			}
			_inst = new npcactionConfig ();
			_inst.InitData ();
			return _inst;
		}
		public Dictionary<string,npcactionCell> AllData;
		public npcactionCell getCell(string key){
			npcactionCell t = null;
			this.AllData.TryGetValue (key, out t);
			return t;
		}
		public npcactionCell getCell(int key){
			npcactionCell t = null;
			this.AllData.TryGetValue (key.ToString(), out t);
			return t;
		}
		public readonly int RowNum = 4;
		private void InitData(){
			this.AllData = new Dictionary<string,npcactionCell> ();
			this.AllData.Add("0",new npcactionCell("idle",60,-1,-1f));
			this.AllData.Add("1",new npcactionCell("walk",0,-1,-1f));
			this.AllData.Add("2",new npcactionCell("pose01",20,9101,5f));
			this.AllData.Add("3",new npcactionCell("pose02",20,9100,5f));
		}
	}
	public class npcactionCell
	{
		///<summary>
		///动作名称
		///<summary>
		public readonly string actionname;
		///<summary>
		///权重
		///<summary>
		public readonly int weight;
		///<summary>
		///对应特效
		///<summary>
		public readonly int effectresid;
		///<summary>
		///特效高度
		///<summary>
		public readonly float effectY;
		public npcactionCell(string actionname,int weight,int effectresid,float effectY){
			this.actionname = actionname;
			this.weight = weight;
			this.effectresid = effectresid;
			this.effectY = effectY;
		}
	}
}