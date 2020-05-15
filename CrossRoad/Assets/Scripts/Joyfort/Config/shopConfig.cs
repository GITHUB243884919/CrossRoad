using UnityEngine;
using System;
using System.Security;
using System.Collections.Generic;
namespace Config
{
	public class shopConfig
	{
		private shopConfig(){ 
		}
		private static shopConfig _inst;
		public static shopConfig getInstace(){
			if (_inst != null) {
				return _inst;
			}
			_inst = new shopConfig ();
			_inst.InitData ();
			return _inst;
		}
		public Dictionary<string,shopCell> AllData;
		public shopCell getCell(string key){
			shopCell t = null;
			this.AllData.TryGetValue (key, out t);
			return t;
		}
		public shopCell getCell(int key){
			shopCell t = null;
			this.AllData.TryGetValue (key.ToString(), out t);
			return t;
		}
		public readonly int RowNum = 5;
		private void InitData(){
			this.AllData = new Dictionary<string,shopCell> ();
			this.AllData.Add("1",new shopCell("麒麟","好大的麒麟",1,"",1,2,10,0,5,"","",5f,10f,""));
			this.AllData.Add("2",new shopCell("1000倍","所有收益提升1000倍",-1,"",-1,-1,-1,-1,-1,"","",-1f,-1f,""));
			this.AllData.Add("3",new shopCell("新手大礼包","针对新手的性价比礼包",-1,"",-1,-1,-1,-1,-1,"","",-1f,-1f,""));
			this.AllData.Add("4",new shopCell("钻石","",-1,"",-1,-1,-1,-1,-1,"","",-1f,-1f,""));
			this.AllData.Add("5",new shopCell("现金","",-1,"",-1,-1,-1,-1,-1,"","",-1f,-1f,""));
		}
	}
	public class shopCell
	{
		///<summary>
		///名称
		///<summary>
		public readonly string name;
		///<summary>
		///描述
		///<summary>
		public readonly string desc;
		///<summary>
		///商品类型
		///<summary>
		public readonly int type;
		///<summary>
		///类型名称
		///<summary>
		public readonly string typename;
		///<summary>
		///显示类型
		///<summary>
		public readonly int showtype;
		///<summary>
		///商品售价类型
		///<summary>
		public readonly int moneytype;
		///<summary>
		///商品售价
		///<summary>
		public readonly int moneynum;
		///<summary>
		///商品数量
		///<summary>
		public readonly int number;
		///<summary>
		///商品id
		///<summary>
		public readonly int goodsid;
		///<summary>
		///上架时间
		///<summary>
		public readonly string uptime;
		///<summary>
		///下架时间
		///<summary>
		public readonly string lowtime;
		///<summary>
		///动物园动物移动速度
		///<summary>
		public readonly float movespeed;
		///<summary>
		///动物园动物移动半径
		///<summary>
		public readonly float moveradius;
		///<summary>
		///商品图标
		///<summary>
		public readonly string icon;
		public shopCell(string name,string desc,int type,string typename,int showtype,int moneytype,int moneynum,int number,int goodsid,string uptime,string lowtime,float movespeed,float moveradius,string icon){
			this.name = name;
			this.desc = desc;
			this.type = type;
			this.typename = typename;
			this.showtype = showtype;
			this.moneytype = moneytype;
			this.moneynum = moneynum;
			this.number = number;
			this.goodsid = goodsid;
			this.uptime = uptime;
			this.lowtime = lowtime;
			this.movespeed = movespeed;
			this.moveradius = moveradius;
			this.icon = icon;
		}
	}
}