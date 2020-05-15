using UnityEngine;
using System;
using System.Security;
using System.Collections.Generic;
namespace Config
{
	public class gamechapterConfig
	{
		private gamechapterConfig(){ 
		}
		private static gamechapterConfig _inst;
		public static gamechapterConfig getInstace(){
			if (_inst != null) {
				return _inst;
			}
			_inst = new gamechapterConfig ();
			_inst.InitData ();
			return _inst;
		}
		public Dictionary<string,gamechapterCell> AllData;
		public gamechapterCell getCell(string key){
			gamechapterCell t = null;
			this.AllData.TryGetValue (key, out t);
			return t;
		}
		public gamechapterCell getCell(int key){
			gamechapterCell t = null;
			this.AllData.TryGetValue (key.ToString(), out t);
			return t;
		}
		public readonly int RowNum = 5;
		private void InitData(){
			this.AllData = new Dictionary<string,gamechapterCell> ();
			this.AllData.Add("1",new gamechapterCell(5,0,6,new int[]{6,2,1,1,0,0},5,10,2,5,1,0,120,0,"1|3",50,5,0,2,"Little_scene01_001","Little_scene01_002","Little_scene01_003","lu01","shuimian","lu_01_2"));
			this.AllData.Add("2",new gamechapterCell(10,0,6,new int[]{1,2,3,3,1,0},5,10,2,5,1,0,120,0,"5|10",50,5,0,2,"Little_scene01_002","Little_scene01_003","Little_scene01_004","lu01","shuimian","lu_01_2"));
			this.AllData.Add("3",new gamechapterCell(15,0,6,new int[]{0,1,2,3,3,1},5,10,2,5,1,0,120,0,"10|20",50,5,0,2,"Little_scene01_003","Little_scene01_004","Little_scene01_005","lu01","shuimian","lu_01_2"));
			this.AllData.Add("4",new gamechapterCell(20,0,6,new int[]{0,0,2,4,2,2},5,10,2,5,1,0,120,0,"15|30",50,5,0,2,"Little_scene01_001","Little_scene01_003","Little_scene01_005","lu01","shuimian","lu_01_2"));
			this.AllData.Add("5",new gamechapterCell(25,0,6,new int[]{0,0,0,2,3,5},5,10,2,5,1,0,120,0,"20|40",50,5,0,2,"Little_scene01_002","Little_scene01_004","Little_scene01_005","lu01","shuimian","lu_01_2"));
		}
	}
	public class gamechapterCell
	{
		///<summary>
		///初始动物数量
		///<summary>
		public readonly int animalnum;
		///<summary>
		///动物最大刷新数量
		///<summary>
		public readonly int animalmax;
		///<summary>
		///马路数量
		///<summary>
		public readonly int roadnum;
		///<summary>
		///车道刷新权重
		///<summary>
		public readonly int[] laneweight;
		///<summary>
		///行驶速度下限
		///<summary>
		public readonly int speedstar;
		///<summary>
		///行驶速度上限
		///<summary>
		public readonly int speedend;
		///<summary>
		///刷新间隔下限
		///<summary>
		public readonly int Intervalstar;
		///<summary>
		///刷新间隔上限
		///<summary>
		public readonly int Intervalend;
		///<summary>
		///胜利条件
		///<summary>
		public readonly int win;
		///<summary>
		///失败条件
		///<summary>
		public readonly int lose;
		///<summary>
		///首通金币奖励
		///<summary>
		public readonly int firstgoldreward;
		///<summary>
		///复刷金币奖励
		///<summary>
		public readonly int repeatgoldreward;
		///<summary>
		///钻石奖励
		///<summary>
		public readonly string diamondreward;
		///<summary>
		///首通碎片奖励
		///<summary>
		public readonly int firstdropward;
		///<summary>
		///首通碎片数量
		///<summary>
		public readonly int firstdropnum;
		///<summary>
		///复刷碎片掉落概率
		///<summary>
		public readonly int repeatdropward;
		///<summary>
		///奖励翻倍
		///<summary>
		public readonly int warddouble;
		///<summary>
		///场景装饰物1
		///<summary>
		public readonly string decorate1;
		///<summary>
		///场景装饰物2
		///<summary>
		public readonly string decorate2;
		///<summary>
		///场景装饰物3
		///<summary>
		public readonly string decorate3;
		///<summary>
		///主路贴图
		///<summary>
		public readonly string road;
		///<summary>
		///地表贴图
		///<summary>
		public readonly string ground;
		///<summary>
		///场景模型
		///<summary>
		public readonly string scene;
		public gamechapterCell(int animalnum,int animalmax,int roadnum,int[] laneweight,int speedstar,int speedend,int Intervalstar,int Intervalend,int win,int lose,int firstgoldreward,int repeatgoldreward,string diamondreward,int firstdropward,int firstdropnum,int repeatdropward,int warddouble,string decorate1,string decorate2,string decorate3,string road,string ground,string scene){
			this.animalnum = animalnum;
			this.animalmax = animalmax;
			this.roadnum = roadnum;
			this.laneweight = laneweight;
			this.speedstar = speedstar;
			this.speedend = speedend;
			this.Intervalstar = Intervalstar;
			this.Intervalend = Intervalend;
			this.win = win;
			this.lose = lose;
			this.firstgoldreward = firstgoldreward;
			this.repeatgoldreward = repeatgoldreward;
			this.diamondreward = diamondreward;
			this.firstdropward = firstdropward;
			this.firstdropnum = firstdropnum;
			this.repeatdropward = repeatdropward;
			this.warddouble = warddouble;
			this.decorate1 = decorate1;
			this.decorate2 = decorate2;
			this.decorate3 = decorate3;
			this.road = road;
			this.ground = ground;
			this.scene = scene;
		}
	}
}