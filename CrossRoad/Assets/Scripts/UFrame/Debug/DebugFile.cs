/*******************************************************************
* FileName:     DebugFile.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-29
* Description:  
* other:    
********************************************************************/


using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;
using UFrame.Common;
using UFrame.Logger;

namespace UFrame
{
    public class DebugFile : Singleton<DebugFile>, ISingleton
    {
        string debugFileDir;
        string writeLastFilePath;
        bool isInit = false;

        public void Init()
        {
            if (isInit)
            {
                return;
            }

            isInit = true;
#if UNITY_EDITOR
            debugFileDir = "./UFrameDebugFile/";
            writeLastFilePath = debugFileDir + "last.txt";

            if (Directory.Exists(debugFileDir))
            {
                DirectoryInfo dir = new DirectoryInfo(debugFileDir);
                foreach (var file in dir.GetFiles())
                {

                    file.Delete();
                }

                return;
            }

            Directory.CreateDirectory(debugFileDir);
#endif
        }

        //static string keyFilePath 
        [Conditional("DebugFile")]
        public void WriteLast(string str)
        {
#if UNITY_EDITOR
            File.WriteAllText(writeLastFilePath, str, Encoding.UTF8);
#endif
        }

        [Conditional("DebugFile")]
        public void WriteKeyFile(string key, string format, params object[] args)
        {
#if UNITY_EDITOR
            string content = string.Format(format, args);
            LogWarp.LogFormat(content);
            string keyFilePath = string.Format("{0}{1}.txt", debugFileDir, key);
            string lineContent = string.Format("{0}|{1}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), content);
            File.AppendAllText(keyFilePath, lineContent, Encoding.UTF8);
#endif
        }

        [Conditional("GET_EVENT")]
        public void WriteAppsGetEventFile(string platformName, string eventName, Dictionary<string, object> properties = null)
        {
            eventName = "事件名= " + eventName;
            if (properties != null)
            {
                int number = 1;
                foreach (var item in properties)
                {
                    //Logger.LogWarp.LogErrorFormat("AA测试打点信息   事件名={0}，事件属性：key={1}，value={2} ", eventName, item.Key, item.Value);
                    eventName = eventName + string.Format("，事件属性{0}：key={1}，value={2}", number, item.Key, item.Value);
                    number += 1;
                }
            }

            string keyFilePath = string.Format("{0}{1}.txt", debugFileDir, platformName);
            string lineContent = string.Format("{0}|{1}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), eventName);

            File.AppendAllText(keyFilePath, lineContent, Encoding.UTF8);
        }

        [Conditional("DebugFile")]
        public void WriteKeyFile(int key, string format, params object[] args)
        {
#if UNITY_EDITOR
            string content = string.Format(format, args);
            LogWarp.LogFormat(content);
            string keyFilePath = string.Format("{0}{1}.txt", debugFileDir, key);
            string lineContent = string.Format("{0}|{1}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), content);
            File.AppendAllText(keyFilePath, lineContent, Encoding.UTF8);
#endif
        }

        [Conditional("DebugFile")]
        static public void GenContent(ref string content, string format, params object[] args)
        {
            content =  string.Format(format, args);
        }

        [Conditional("DebugFile")]
        public void MarkGameObject(GameObject go, string format, params object[] args)
        {
#if UNITY_EDITOR
            go.name = string.Format(format, args);
#endif
        }
    }

}
