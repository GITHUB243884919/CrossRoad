using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using LitJson;

public static class Encryption {
    public static ArrayList getEncryptionResults (string data) {
        var token = "baJplpEcfiO6d7H8d9W0lK2j3Y4x5G6s";
        // 时间戳
        TimeSpan ts = DateTime.UtcNow - new DateTime (1970, 1, 1, 0, 0, 0, 0);
        var timestamp = Convert.ToInt64 (ts.TotalSeconds).ToString ();
        JsonData finalData = JsonMapper.ToObject(data);
        //把原始数据塞成一个数组
        List<string> arr = new List<string>();
        // 把finalData转成字典，并取所有的Values值
        var valueStr = "";
        var finalDic=  ((IDictionary)finalData).Values;
        for (int i = 0; i < finalDic.Count; i++)
        {
            //  所有的非object都塞进去，
             var value = finalData[i];
             if (value!= null)
             {
                if (value.IsObject)
                {
                    valueStr = JsonMapper.ToJson(value);
                }else
                {
                     valueStr = value.ToString();
                }
                arr.Add(valueStr);
             }
        }

        // 最后把token和时间塞进去
        arr.Add(token);
        arr.Add(timestamp);
        // 排序有string.CompareOrdinal参数才是用ascii码顺序排的
        arr.Sort(string.CompareOrdinal);
        // 转成字符串
        var str= string.Join("", arr.ToArray());
       // UnityEngine.Debug.Log("字符串:" + str);
        // sha1加密
        var finalStr = Sha1(str);
        // 返回的是时间戳和最后秘钥
        ArrayList encryptionResults = new ArrayList(){timestamp,finalStr};
        return encryptionResults;
    }
    public static string Sha1 (this string str) {
        var buffer = Encoding.UTF8.GetBytes (str);
        var data = SHA1.Create ().ComputeHash (buffer);
        var sb = new StringBuilder ();
        foreach (var t in data) {
            sb.Append (t.ToString ("x2"));//小写
        }
        return sb.ToString ();
    }
    
}