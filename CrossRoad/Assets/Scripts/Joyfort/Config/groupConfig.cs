using UnityEngine;
using System;
using System.Security;
using System.Collections.Generic;
namespace Config
{
	public class groupConfig
	{
		private groupConfig(){ 
		}
		private static groupConfig _inst;
		public static groupConfig getInstace(){
			if (_inst != null) {
				return _inst;
			}
			_inst = new groupConfig ();
			_inst.InitData ();
			return _inst;
		}
		public Dictionary<string,groupCell> AllData;
		public groupCell getCell(string key){
			groupCell t = null;
			this.AllData.TryGetValue (key, out t);
			return t;
		}
		public groupCell getCell(int key){
			groupCell t = null;
			this.AllData.TryGetValue (key.ToString(), out t);
			return t;
		}
		public readonly int RowNum = 80;
		private void InitData(){
			this.AllData = new Dictionary<string,groupCell> ();
			this.AllData.Add("1",new groupCell("老虎栏|狼栏|狮子栏",new int[]{1001,1002,1003},0,new int[]{3,4,3},100,100,9409,53f));
			this.AllData.Add("2",new groupCell("熊栏|鳄鱼栏",new int[]{1004,1005},0,new int[]{5,5},100,100,9408,53f));
			this.AllData.Add("3",new groupCell("袋鼠栏|猴子栏|鹿栏",new int[]{1011,1012,1013},0,new int[]{3,4,3},100,100,9402,53f));
			this.AllData.Add("4",new groupCell("犀牛栏|大象栏",new int[]{1014,1015},0,new int[]{5,5},100,100,9403,53f));
			this.AllData.Add("5",new groupCell("老虎栏|狼栏|狮子栏",new int[]{2001,2002,2003},1,new int[]{3,4,3},100,100,9409,53f));
			this.AllData.Add("6",new groupCell("熊栏|鳄鱼栏",new int[]{2004,2005},1,new int[]{5,5},100,100,9408,53f));
			this.AllData.Add("7",new groupCell("袋鼠栏|猴子栏|鹿栏",new int[]{2011,2012,2013},1,new int[]{3,4,3},100,100,9402,53f));
			this.AllData.Add("8",new groupCell("犀牛栏|大象栏",new int[]{2014,2015},1,new int[]{5,5},100,100,9403,53f));
			this.AllData.Add("9",new groupCell("老虎栏|狼栏|狮子栏",new int[]{3001,3002,3003},2,new int[]{3,4,3},100,100,9409,53f));
			this.AllData.Add("10",new groupCell("熊栏|鳄鱼栏",new int[]{3004,3005},2,new int[]{5,5},100,100,9408,53f));
			this.AllData.Add("11",new groupCell("袋鼠栏|猴子栏|鹿栏",new int[]{3011,3012,3013},2,new int[]{3,4,3},100,100,9402,53f));
			this.AllData.Add("12",new groupCell("犀牛栏|大象栏",new int[]{3014,3015},2,new int[]{5,5},100,100,9403,53f));
			this.AllData.Add("13",new groupCell("老虎栏|狼栏|狮子栏",new int[]{4001,4002,4003},3,new int[]{3,4,3},100,100,9409,53f));
			this.AllData.Add("14",new groupCell("熊栏|鳄鱼栏",new int[]{4004,4005},3,new int[]{5,5},100,100,9408,53f));
			this.AllData.Add("15",new groupCell("袋鼠栏|猴子栏|鹿栏",new int[]{4011,4012,4013},3,new int[]{3,4,3},100,100,9402,53f));
			this.AllData.Add("16",new groupCell("犀牛栏|大象栏",new int[]{4014,4015},3,new int[]{5,5},100,100,9403,53f));
			this.AllData.Add("17",new groupCell("老虎栏|狼栏|狮子栏",new int[]{5001,5002,5003},4,new int[]{3,4,3},100,100,9409,53f));
			this.AllData.Add("18",new groupCell("熊栏|鳄鱼栏",new int[]{5004,5005},4,new int[]{5,5},100,100,9408,53f));
			this.AllData.Add("19",new groupCell("袋鼠栏|猴子栏|鹿栏",new int[]{5011,5012,5013},4,new int[]{3,4,3},100,100,9402,53f));
			this.AllData.Add("20",new groupCell("犀牛栏|大象栏",new int[]{5014,5015},4,new int[]{5,5},100,100,9403,53f));
			this.AllData.Add("21",new groupCell("孔雀栏|鹳栏|鹰栏",new int[]{6016,6017,6018},5,new int[]{3,4,3},100,100,9504,53f));
			this.AllData.Add("22",new groupCell("鸵鸟栏|天鹅栏",new int[]{6019,6020},5,new int[]{5,5},100,100,9505,53f));
			this.AllData.Add("23",new groupCell("蜥脚龙栏|角龙栏",new int[]{6007,6008},5,new int[]{5,5},100,100,9506,106f));
			this.AllData.Add("24",new groupCell("剑龙栏|甲龙栏",new int[]{6009,6010},5,new int[]{5,5},100,100,9507,106f));
			this.AllData.Add("25",new groupCell("孔雀栏|鹳栏|鹰栏",new int[]{7016,7017,7018},6,new int[]{3,4,3},100,100,9504,53f));
			this.AllData.Add("26",new groupCell("鸵鸟栏|天鹅栏",new int[]{7019,7020},6,new int[]{5,5},100,100,9505,53f));
			this.AllData.Add("27",new groupCell("蜥脚龙栏|角龙栏",new int[]{7007,7008},6,new int[]{5,5},100,100,9506,106f));
			this.AllData.Add("28",new groupCell("剑龙栏|甲龙栏",new int[]{7009,7010},6,new int[]{5,5},100,100,9507,106f));
			this.AllData.Add("29",new groupCell("孔雀栏|鹳栏|鹰栏",new int[]{8016,8017,8018},7,new int[]{3,4,3},100,100,9504,53f));
			this.AllData.Add("30",new groupCell("鸵鸟栏|天鹅栏",new int[]{8019,8020},7,new int[]{5,5},100,100,9505,53f));
			this.AllData.Add("31",new groupCell("蜥脚龙栏|角龙栏",new int[]{8007,8008},7,new int[]{5,5},100,100,9506,106f));
			this.AllData.Add("32",new groupCell("剑龙栏|甲龙栏",new int[]{8009,8010},7,new int[]{5,5},100,100,9507,106f));
			this.AllData.Add("33",new groupCell("孔雀栏|鹳栏|鹰栏",new int[]{9016,9017,9018},8,new int[]{3,4,3},100,100,9504,53f));
			this.AllData.Add("34",new groupCell("鸵鸟栏|天鹅栏",new int[]{9019,9020},8,new int[]{5,5},100,100,9505,53f));
			this.AllData.Add("35",new groupCell("蜥脚龙栏|角龙栏",new int[]{9007,9008},8,new int[]{5,5},100,100,9506,106f));
			this.AllData.Add("36",new groupCell("剑龙栏|甲龙栏",new int[]{9009,9010},8,new int[]{5,5},100,100,9507,106f));
			this.AllData.Add("37",new groupCell("孔雀栏|鹳栏|鹰栏",new int[]{10016,10017,10018},9,new int[]{3,4,3},100,100,9504,53f));
			this.AllData.Add("38",new groupCell("鸵鸟栏|天鹅栏",new int[]{10019,10020},9,new int[]{5,5},100,100,9505,53f));
			this.AllData.Add("39",new groupCell("蜥脚龙栏|角龙栏",new int[]{10007,10008},9,new int[]{5,5},100,100,9506,106f));
			this.AllData.Add("40",new groupCell("剑龙栏|甲龙栏",new int[]{10009,10010},9,new int[]{5,5},100,100,9507,106f));
			this.AllData.Add("41",new groupCell("骆驼栏|马栏|野猪栏",new int[]{11001,11002,11003},10,new int[]{3,4,3},100,100,9609,53f));
			this.AllData.Add("42",new groupCell("长颈鹿栏|鲨鱼栏",new int[]{11004,11005},10,new int[]{5,5},100,100,9608,53f));
			this.AllData.Add("43",new groupCell("浣熊栏|野鸡栏|海龟栏",new int[]{11011,11012,11013},10,new int[]{3,4,3},100,100,9602,53f));
			this.AllData.Add("44",new groupCell("羚羊栏|豹子栏",new int[]{11014,11015},10,new int[]{5,5},100,100,9603,53f));
			this.AllData.Add("45",new groupCell("骆驼栏|马栏|野猪栏",new int[]{12001,12002,12003},11,new int[]{3,4,3},100,100,9609,53f));
			this.AllData.Add("46",new groupCell("长颈鹿栏|鲨鱼栏",new int[]{12004,12005},11,new int[]{5,5},100,100,9608,53f));
			this.AllData.Add("47",new groupCell("浣熊栏|野鸡栏|海龟栏",new int[]{12011,12012,12013},11,new int[]{3,4,3},100,100,9602,53f));
			this.AllData.Add("48",new groupCell("羚羊栏|豹子栏",new int[]{12014,12015},11,new int[]{5,5},100,100,9603,53f));
			this.AllData.Add("49",new groupCell("骆驼栏|马栏|野猪栏",new int[]{13001,13002,13003},12,new int[]{3,4,3},100,100,9609,53f));
			this.AllData.Add("50",new groupCell("长颈鹿栏|鲨鱼栏",new int[]{13004,13005},12,new int[]{5,5},100,100,9608,53f));
			this.AllData.Add("51",new groupCell("浣熊栏|野鸡栏|海龟栏",new int[]{13011,13012,13013},12,new int[]{3,4,3},100,100,9602,53f));
			this.AllData.Add("52",new groupCell("羚羊栏|豹子栏",new int[]{13014,13015},12,new int[]{5,5},100,100,9603,53f));
			this.AllData.Add("53",new groupCell("骆驼栏|马栏|野猪栏",new int[]{14001,14002,14003},13,new int[]{3,4,3},100,100,9609,53f));
			this.AllData.Add("54",new groupCell("长颈鹿栏|鲨鱼栏",new int[]{14004,14005},13,new int[]{5,5},100,100,9608,53f));
			this.AllData.Add("55",new groupCell("浣熊栏|野鸡栏|海龟栏",new int[]{14011,14012,14013},13,new int[]{3,4,3},100,100,9602,53f));
			this.AllData.Add("56",new groupCell("羚羊栏|豹子栏",new int[]{14014,14015},13,new int[]{5,5},100,100,9603,53f));
			this.AllData.Add("57",new groupCell("骆驼栏|马栏|野猪栏",new int[]{15001,15002,15003},14,new int[]{3,4,3},100,100,9609,53f));
			this.AllData.Add("58",new groupCell("长颈鹿栏|鲨鱼栏",new int[]{15004,15005},14,new int[]{5,5},100,100,9608,53f));
			this.AllData.Add("59",new groupCell("浣熊栏|野鸡栏|海龟栏",new int[]{15011,15012,15013},14,new int[]{3,4,3},100,100,9602,53f));
			this.AllData.Add("60",new groupCell("羚羊栏|豹子栏",new int[]{15014,15015},14,new int[]{5,5},100,100,9603,53f));
			this.AllData.Add("61",new groupCell("兔子栏|旱獭栏|野牛栏",new int[]{16016,16017,16018},15,new int[]{3,4,3},100,100,9504,53f));
			this.AllData.Add("62",new groupCell("企鹅栏|海豹栏",new int[]{16019,16020},15,new int[]{5,5},100,100,9505,53f));
			this.AllData.Add("63",new groupCell("翼龙栏|盗龙栏",new int[]{16007,16008},15,new int[]{5,5},100,100,9506,106f));
			this.AllData.Add("64",new groupCell("暴龙栏|鸟脚龙栏",new int[]{16009,16010},15,new int[]{5,5},100,100,9507,106f));
			this.AllData.Add("65",new groupCell("兔子栏|旱獭栏|野牛栏",new int[]{17016,17017,17018},16,new int[]{3,4,3},100,100,9504,53f));
			this.AllData.Add("66",new groupCell("企鹅栏|海豹栏",new int[]{17019,17020},16,new int[]{5,5},100,100,9505,53f));
			this.AllData.Add("67",new groupCell("翼龙栏|盗龙栏",new int[]{17007,17008},16,new int[]{5,5},100,100,9506,106f));
			this.AllData.Add("68",new groupCell("暴龙栏|鸟脚龙栏",new int[]{17009,17010},16,new int[]{5,5},100,100,9507,106f));
			this.AllData.Add("69",new groupCell("兔子栏|旱獭栏|野牛栏",new int[]{18016,18017,18018},17,new int[]{3,4,3},100,100,9504,53f));
			this.AllData.Add("70",new groupCell("企鹅栏|海豹栏",new int[]{18019,18020},17,new int[]{5,5},100,100,9505,53f));
			this.AllData.Add("71",new groupCell("翼龙栏|盗龙栏",new int[]{18007,18008},17,new int[]{5,5},100,100,9506,106f));
			this.AllData.Add("72",new groupCell("暴龙栏|鸟脚龙栏",new int[]{18009,18010},17,new int[]{5,5},100,100,9507,106f));
			this.AllData.Add("73",new groupCell("兔子栏|旱獭栏|野牛栏",new int[]{19016,19017,19018},18,new int[]{3,4,3},100,100,9504,53f));
			this.AllData.Add("74",new groupCell("企鹅栏|海豹栏",new int[]{19019,19020},18,new int[]{5,5},100,100,9505,53f));
			this.AllData.Add("75",new groupCell("翼龙栏|盗龙栏",new int[]{19007,19008},18,new int[]{5,5},100,100,9506,106f));
			this.AllData.Add("76",new groupCell("暴龙栏|鸟脚龙栏",new int[]{19009,19010},18,new int[]{5,5},100,100,9507,106f));
			this.AllData.Add("77",new groupCell("兔子栏|旱獭栏|野牛栏",new int[]{20016,20017,20018},19,new int[]{3,4,3},100,100,9504,53f));
			this.AllData.Add("78",new groupCell("企鹅栏|海豹栏",new int[]{20019,20020},19,new int[]{5,5},100,100,9505,53f));
			this.AllData.Add("79",new groupCell("翼龙栏|盗龙栏",new int[]{20007,20008},19,new int[]{5,5},100,100,9506,106f));
			this.AllData.Add("80",new groupCell("暴龙栏|鸟脚龙栏",new int[]{20009,20010},19,new int[]{5,5},100,100,9507,106f));
		}
	}
	public class groupCell
	{
		///<summary>
		///备注
		///<summary>
		public readonly string test;
		///<summary>
		///建筑id
		///<summary>
		public readonly int[] startid;
		///<summary>
		///关联场景id
		///<summary>
		public readonly int scene;
		///<summary>
		///组内随机权重
		///<summary>
		public readonly int[] groupweight;
		///<summary>
		///再次观光概率
		///<summary>
		public readonly int againweight;
		///<summary>
		///前往该组概率
		///<summary>
		public readonly int gotoweight;
		///<summary>
		///资源加载id
		///<summary>
		public readonly int zoopartresID;
		///<summary>
		///地块尺寸
		///<summary>
		public readonly float groundsize;
		public groupCell(string test,int[] startid,int scene,int[] groupweight,int againweight,int gotoweight,int zoopartresID,float groundsize){
			this.test = test;
			this.startid = startid;
			this.scene = scene;
			this.groupweight = groupweight;
			this.againweight = againweight;
			this.gotoweight = gotoweight;
			this.zoopartresID = zoopartresID;
			this.groundsize = groundsize;
		}
	}
}