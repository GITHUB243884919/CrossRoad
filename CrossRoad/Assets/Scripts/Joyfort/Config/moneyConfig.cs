using UnityEngine;
using System;
using System.Security;
using System.Collections.Generic;
namespace Config
{
	public class moneyConfig
	{
		private moneyConfig(){ 
		}
		private static moneyConfig _inst;
		public static moneyConfig getInstace(){
			if (_inst != null) {
				return _inst;
			}
			_inst = new moneyConfig ();
			_inst.InitData ();
			return _inst;
		}
		public Dictionary<string,moneyCell> AllData;
		public moneyCell getCell(string key){
			moneyCell t = null;
			this.AllData.TryGetValue (key, out t);
			return t;
		}
		public moneyCell getCell(int key){
			moneyCell t = null;
			this.AllData.TryGetValue (key.ToString(), out t);
			return t;
		}
		public readonly int RowNum = 5;
		private void InitData(){
			this.AllData = new Dictionary<string,moneyCell> ();
			this.AllData.Add("0",new moneyCell("money_1","UIAtlas/Main/Gold","UIAtlas/UIAdvertActivity/GoldHeap","prefabs/Effect/Fx_gold","CoinEffect"));
			this.AllData.Add("1",new moneyCell("money_2","UIAtlas/UIAdvertActivity/Gold_2","UIAtlas/UIAdvertActivity/GoldHeap_2","prefabs/Effect/Fx_gold_03","CoinEffect_3"));
			this.AllData.Add("2",new moneyCell("money_3","UIAtlas/UIAdvertActivity/Gold_3","UIAtlas/UIAdvertActivity/GoldHeap_3","prefabs/Effect/Fx_gold_02","CoinEffect_2"));
			this.AllData.Add("3",new moneyCell("money_4","UIAtlas/UIAdvertActivity/Gold_4","UIAtlas/UIAdvertActivity/GoldHeap_4","prefabs/Effect/Fx_gold_04","CoinEffect_4"));
			this.AllData.Add("4",new moneyCell("money_5","UIAtlas/UIAdvertActivity/Gold_5","UIAtlas/UIAdvertActivity/GoldHeap_5","prefabs/Effect/Fx_gold_05","CoinEffect_5"));
		}
	}
	public class moneyCell
	{
		///<summary>
		///货币名称
		///<summary>
		public readonly string moneyname;
		///<summary>
		///图标
		///<summary>
		public readonly string moneyicon;
		///<summary>
		///大图标
		///<summary>
		public readonly string bigmoneyicon;
		///<summary>
		///飘钱特效
		///<summary>
		public readonly string moneyeffect;
		///<summary>
		///世界地图飘钱特效
		///<summary>
		public readonly string mapmoneyeffect;
		public moneyCell(string moneyname,string moneyicon,string bigmoneyicon,string moneyeffect,string mapmoneyeffect){
			this.moneyname = moneyname;
			this.moneyicon = moneyicon;
			this.bigmoneyicon = bigmoneyicon;
			this.moneyeffect = moneyeffect;
			this.mapmoneyeffect = mapmoneyeffect;
		}
	}
}