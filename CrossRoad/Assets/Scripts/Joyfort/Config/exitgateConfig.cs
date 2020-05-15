using UnityEngine;
using System;
using System.Security;
using System.Collections.Generic;
namespace Config
{
	public class exitgateConfig
	{
		private exitgateConfig(){ 
		}
		private static exitgateConfig _inst;
		public static exitgateConfig getInstace(){
			if (_inst != null) {
				return _inst;
			}
			_inst = new exitgateConfig ();
			_inst.InitData ();
			return _inst;
		}
		public Dictionary<string,exitgateCell> AllData;
		public exitgateCell getCell(string key){
			exitgateCell t = null;
			this.AllData.TryGetValue (key, out t);
			return t;
		}
		public exitgateCell getCell(int key){
			exitgateCell t = null;
			this.AllData.TryGetValue (key.ToString(), out t);
			return t;
		}
		public readonly int RowNum = 8;
		private void InitData(){
			this.AllData = new Dictionary<string,exitgateCell> ();
			this.AllData.Add("0",new exitgateCell("1号售票口",1,500,5,"10",5,"8","path_positive_1","path_negative_1",10,1,9,2000,"end/ggchezhan/ggchezhan_01",10,14));
			this.AllData.Add("1",new exitgateCell("2号售票口",2,500,5,"10",5,"8","path_positive_2","path_negative_2",10,2,9,2000,"end/ggchezhan/ggchezhan_01 (1)",10,14));
			this.AllData.Add("2",new exitgateCell("3号售票口",3,500,5,"10",5,"8","path_positive_3","path_negative_3",10,3,9,2000,"end/ggchezhan/ggchezhan_01 (2)",10,14));
			this.AllData.Add("3",new exitgateCell("4号售票口",4,500,5,"10",5,"8","path_positive_4","path_negative_4",10,4,9,2000,"end/ggchezhan/ggchezhan_01 (3)",10,14));
			this.AllData.Add("4",new exitgateCell("5号售票口",5,500,5,"10",5,"8","path_positive_5","path_negative_5",10,5,9,2000,"end/ggchezhan/ggchezhan_01 (4)",10,14));
			this.AllData.Add("5",new exitgateCell("6号售票口",6,500,5,"10",5,"8","path_positive_6","path_negative_6",10,6,9,2000,"end/ggchezhan/ggchezhan_01 (5)",10,14));
			this.AllData.Add("6",new exitgateCell("7号售票口",7,500,5,"10",5,"8","path_positive_7","path_negative_7",10,7,9,2000,"end/ggchezhan/ggchezhan_01 (6)",10,14));
			this.AllData.Add("7",new exitgateCell("8号售票口",8,500,5,"10",5,"8","path_positive_8","path_negative_8",10,8,9,2000,"end/ggchezhan/ggchezhan_01 (7)",10,14));
		}
	}
	public class exitgateCell
	{
		///<summary>
		///名称
		///<summary>
		public readonly string name;
		///<summary>
		///开启条件
		///<summary>
		public readonly int open;
		///<summary>
		///数量
		///<summary>
		public readonly int number;
		///<summary>
		///票价基础
		///<summary>
		public readonly int baseprice;
		///<summary>
		///票价公式
		///<summary>
		public readonly string priceupformula;
		///<summary>
		///售票速度
		///<summary>
		public readonly int speed;
		///<summary>
		///售票速度公式
		///<summary>
		public readonly string speedupformula;
		///<summary>
		///正乘车口路线
		///<summary>
		public readonly string positiveexitgate;
		///<summary>
		///反乘车口路线
		///<summary>
		public readonly string negativeexitgate;
		///<summary>
		///最大排队数量
		///<summary>
		public readonly int maxnumofperqueue;
		///<summary>
		///售票口数量
		///<summary>
		public readonly int space;
		///<summary>
		///售票口数量公式
		///<summary>
		public readonly int spaceformula;
		///<summary>
		///等级上限
		///<summary>
		public readonly int lvmax;
		///<summary>
		///场景路径
		///<summary>
		public readonly string gameobjectpath;
		///<summary>
		///升级消耗初始
		///<summary>
		public readonly int depbase;
		///<summary>
		///升级消耗公式
		///<summary>
		public readonly int depformula;
		public exitgateCell(string name,int open,int number,int baseprice,string priceupformula,int speed,string speedupformula,string positiveexitgate,string negativeexitgate,int maxnumofperqueue,int space,int spaceformula,int lvmax,string gameobjectpath,int depbase,int depformula){
			this.name = name;
			this.open = open;
			this.number = number;
			this.baseprice = baseprice;
			this.priceupformula = priceupformula;
			this.speed = speed;
			this.speedupformula = speedupformula;
			this.positiveexitgate = positiveexitgate;
			this.negativeexitgate = negativeexitgate;
			this.maxnumofperqueue = maxnumofperqueue;
			this.space = space;
			this.spaceformula = spaceformula;
			this.lvmax = lvmax;
			this.gameobjectpath = gameobjectpath;
			this.depbase = depbase;
			this.depformula = depformula;
		}
	}
}