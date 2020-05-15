//using UnityEngine;
//using System.Collections.Generic;
//using System;
//using LitJson;
//using Tools;
//using System.Text;
//using System.Collections;

//public class SerBaseData {
//#if UNITY_EDITOR
//    public string playerID = "34db6fa80a303255f8653b5beae1f085db488973";
//#else
//    public string playerID = SystemInfo.deviceUniqueIdentifier;
//#endif
//    public string gameID = "1453150879";
//    public string timestamp;
//    public string signature;
//}

//public class LoginData : SerBaseData{
//    public string sync = "true";
//    public string opType = "LOGIN";
//#if UNITY_IOS
//    public string os = "ios";
//#else
//    public string os = "android";
//#endif
//}

//public class UpLoadData : SerBaseData {
//    public string sync = "true";
//    public string opType = "UP";

//    public string diamond;
//    public string money;
//    public string extData;
//}

//public class ExitGameData : SerBaseData
//{
//    public string sync = "false";
//    public string opType = "LOGOUT";
//}

//public class ServerMgr
//{
//    public static ServerMgr _inst;
//    public static ServerMgr Inst {
//        get {
//            if (_inst == null) {
//                _inst = new ServerMgr();
//            }
//            return _inst;
//        }
//    }


//    //国外---
//    private const string TestUrl = "http://test-appgame-gun.lefou666.com:8080/miniGameAppGun/option.do";
//    private const string ReleaseUrl = "https://appgame-gun.lefou666.com/miniGameAppGun/option.do";


//    private const float m_SendIntervalTime = 60;
//    private long m_CurSendTime;
//    private string curUrl;
//    private Dictionary<string, string> headers = new Dictionary<string, string>() {
//        {"Content-Type","application/json"}
//    };

//    /// <summary>
//    /// 登录服务器
//    /// </summary>
//    /// <param name="onSuceess"></param>
//    /// <param name="onFail"></param>
//    public void LoginSer(Action<WWW> onSuceess = null, Action<string> onFail = null) {
//        LoginData data = new LoginData();
//        // SHA1加密（先屏蔽）
//        var originalData = JsonMapper.ToJson(data);
//        var encryptionResults = Encryption.getEncryptionResults(originalData);
//        data.timestamp = encryptionResults[0].ToString();
//        data.signature = encryptionResults[1].ToString();
//        var json = JsonMapper.ToJson(data);
//        Debug.Log("SendHttpToServer-->SendJson:" + json + "-->url=" + curUrl);
//        HttpTool.GetInstance().Post(ReleaseUrl, Encoding.Default.GetBytes(json), onSuceess, onFail, headers);
//    }
    
//    /// <summary>
//    /// 上传玩家数据-
//    /// </summary>
//    /// <param name="str"></param>
//    public void UpLoadPlayData(string str,bool isNeedOffline = false) {
//        var cur = DateTime.Now.Ticks;
//        //当前的时间差值秒
//        var curIntervalTime = (cur - m_CurSendTime) / 10000000;
//        if (curIntervalTime < 0 || curIntervalTime > m_SendIntervalTime)
//        {
//            UpLoadData data = new UpLoadData();
//            data.diamond = Global.gPlayerData.iOwnDiamond.ToString();
//            data.money = Global.gPlayerData.nOwnMoney.ToString();
//            data.extData = str;

//            // SHA1加密（先屏蔽）
//            var originalData = JsonMapper.ToJson(data);
//            var encryptionResults = Encryption.getEncryptionResults(originalData);
//            data.timestamp = encryptionResults[0].ToString();
//            data.signature = encryptionResults[1].ToString();
//            var json = JsonMapper.ToJson(data);

//            Debug.Log("SendHttpToServer-->SendJson:" + json + "-->url=" + curUrl);
//            HttpTool.GetInstance().Post(ReleaseUrl, Encoding.Default.GetBytes(json), (string res) => {
//                Debug.Log("上传服务器成功" + res);
//                m_CurSendTime = DateTime.Now.Ticks;
//                if (isNeedOffline && GameEvents.common.showOffline !=null) {
//                    var josndata = JsonMapper.ToObject(res);
//                    if (((IDictionary)josndata["result"]).Contains("timestamp"))
//                    {
//                        string str2 = josndata["result"]["timestamp"].ToString();
//                        Global.gPlayerData.CurServerTime = long.Parse(str2);
//                        long offlineTime = Global.gPlayerData.CurServerTime - Global.gPlayerData.LastOffLineTime;
//                        GameEvents.common.showOffline.Invoke(offlineTime);
//                        Global.gPlayerData.LastOffLineTime = Global.gPlayerData.CurServerTime;
//                    }                    
//                }
//            }, (string res) => {
//                Debug.Log("上传服务器失败" + res);
//            }, headers);
//        }            
//    }



//    public void OnExitGame() {
//        ExitGameData data = new ExitGameData();
//        var json = JsonMapper.ToJson(data);

//        Debug.Log("SendHttpToServer-->SendJson:" + json + "-->url=" + curUrl);
//        HttpTool.GetInstance().Post(ReleaseUrl, Encoding.Default.GetBytes(json), (string res) => {
//            Debug.Log("ExitGame:" + res);
//        }, (string res) => {

//        }, headers);
//    }
//}