/** 
 *Author:       ouchen  
 *Date:         2018-11-26
 *Description:  http工具
 *History: 
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace JoyfortTools
{
    public class HttpTool : MonoBehaviour
    {

        static HttpTool g_Instance = null;
        public static HttpTool GetInstance()
        {
            if (g_Instance == null)
            {
                GameObject obj = new GameObject("HttpTool");
                g_Instance = obj.AddComponent<HttpTool>();
                g_Instance.InitDataCache();
            }
            return g_Instance;
        }
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void Post(string url, byte[] data, Action<WWW> onSuceess = null, Action<string> onFail = null, Dictionary<string, string> headers = null)
        {
            StartCoroutine(StartPosHttp(url, data, onSuceess, onFail, headers));
        }

        public void Get(string url, Action<WWW> onSuceess = null, Action<string> onFail = null)
        {
            StartCoroutine(StartGetHttp(url, onSuceess, onFail));
        }

        IEnumerator StartPosHttp(string url, byte[] data, Action<WWW> onSuceess = null, Action<string> onFail = null, Dictionary<string, string> headers = null)
        {
            WWW www = null;
            if (headers == null)
            {
                www = new WWW(url, data);
            }
            else
            {
                www = new WWW(url, data, headers);
            }

            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                if (onFail != null)
                {
                    onFail(www.error);
                }
            }
            else
            {
                if (onSuceess != null)
                {
                    onSuceess(www);
                }
            }
        }
        IEnumerator StartGetHttp(string url, Action<WWW> onSuceess = null, Action<string> onFail = null)
        {
            WWW www = new WWW(url);
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                if (onFail != null)
                {
                    onFail(www.error);
                }
            }
            else
            {
                if (onSuceess != null)
                {
                    onSuceess(www);
                }
            }
        }

        Queue<HttpDesc> m_CallBackList = new Queue<HttpDesc>();
        Queue<HttpDesc> m_DataCache = new Queue<HttpDesc>();
        public void Post(string url, byte[] data, Action<string> onSuceess = null, Action<string> onFail = null, Dictionary<string, string> headers = null)
        {
            Action ac = new Action(() =>
            {
                HttpDesc desc = FindFreeCache();
                try
                {
                    HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                    req.Timeout = 5000;
                    if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                    {
                        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback((object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => { return true; });
                        req.ProtocolVersion = HttpVersion.Version11;
                    }
                    req.Method = "POST";
                    if (headers != null && headers.Count > 0)
                    {
                        BuildHeaders(req, headers);
                    }
                    if (data != null && data.Length > 0)
                    {
                        using (Stream stream = req.GetRequestStream())
                        {
                            stream.Write(data, 0, data.Length);
                        }
                    }

                    using (WebResponse wr = req.GetResponse())
                    {
                        //在这里对接收到的页面内容进行处理
                        using (Stream st = wr.GetResponseStream())
                        {
                            ReciveData(st, desc, onSuceess, onFail);
                            AddToQueue(desc);
                        }
                    }
                }
                catch (Exception ex)
                {
                    desc.Fill(false, ex.Message, onSuceess, onFail);
                    AddToQueue(desc);
                }
            });
            ac.BeginInvoke((IAsyncResult ar) =>
            {
                if (ar.IsCompleted)
                {
                    Action result = (Action)ar.AsyncState;
                    if (result != null)
                    {
                        result.EndInvoke(ar);
                    }
                }
            }, ac);
        }
        public void Get(string url, Action<string> onSuceess = null, Action<string> onFail = null)
        {
            Action ac = new Action(() =>
            {
                HttpDesc desc = FindFreeCache();
                try
                {
                    HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                    req.Method = "GET";
                    using (WebResponse wr = req.GetResponse())
                    {
                        //在这里对接收到的页面内容进行处理
                        using (Stream st = wr.GetResponseStream())
                        {
                            ReciveData(st, desc, onSuceess, onFail);
                            AddToQueue(desc);
                        }
                    }
                }
                catch (Exception ex)
                {
                    desc.Fill(false, ex.Message, onSuceess, onFail);
                    AddToQueue(desc);
                }
            });
            ac.BeginInvoke((IAsyncResult ar) =>
            {
                if (ar.IsCompleted)
                {
                    Action result = (Action)ar.AsyncState;
                    if (result != null)
                    {
                        result.EndInvoke(ar);
                    }
                }
            }, ac);

        }
        public void AddToQueue(HttpDesc desc)
        {
            m_CallBackList.Enqueue(desc);
        }
        public void AddToCache(HttpDesc desc)
        {
            m_DataCache.Enqueue(desc);
        }

        private HttpDesc FindFreeCache()
        {
            HttpDesc desc = null;
            lock (HttpDesc.cfg_LockObj)
            {
                if (m_DataCache.Count > 0)
                {

                    desc = m_DataCache.Dequeue();
                }
                else
                {
                    desc = new HttpDesc();
                }
            }

            return desc;
        }
        private void Update()
        {
            lock (HttpDesc.cfg_LockObj)
            {
                if (m_CallBackList.Count > 0)
                {
                    while (m_CallBackList.Count > 0)
                    {
                        HttpDesc desc = m_CallBackList.Dequeue();
                        desc.Excute();
                        AddToCache(desc);

                    }
                }
            }
        }

        public void TestHttp()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers["Content-Type"] = "application/octet-stream";
            Post("https://www.baidu.com/", new byte[0], TestOnSuccess, TestOnFail, headers);
        }
        void TestOnSuccess(string data)
        {
            Debug.Log(data);
        }
        void TestOnFail(string data)
        {
            Debug.Log(data);
        }

        void ReciveData(Stream st, HttpDesc desc, Action<string> onSuceess = null, Action<string> onFail = null)
        {
            using (StreamReader rd = new StreamReader(st))
            {
                string data = rd.ReadToEnd();
                desc.Fill(true, data, onSuceess, onFail);
            }

        }

        public void InitDataCache()
        {
            int i = 0;
            HttpDesc desc;
            while (i < HttpDesc.cfg_CacheCount)
            {
                desc = new HttpDesc();
                m_DataCache.Enqueue(desc);
                i++;
            }
        }
        public void BuildHeaders(HttpWebRequest req, Dictionary<string, string> headers)
        {
            if (headers != null && headers.Count > 0)
            {
                foreach (var item in headers)
                {
                    switch (item.Key)
                    {
                        case cfg_Header_ContentType:
                            req.ContentType = item.Value;
                            break;
                        case cfg_Header_Accept:
                            req.Accept = item.Value;
                            break;
                        case cfg_UserAgent:
                            req.UserAgent = item.Value;
                            break;
                        case cfg_Connection:
                            req.Connection = item.Value;
                            break;
                        default:
                            req.Headers.Add(item.Key, item.Value);
                            break;
                    }
                }
            }
        }
        const string cfg_Header_ContentType = "Content-Type";
        const string cfg_Header_Accept = "Accept";
        const string cfg_UserAgent = "UserAgent";
        const string cfg_Connection = "Connection";
    }
    public class HttpDesc
    {
        public static object cfg_LockObj = new object();
        public static int cfg_CacheCount = 5;
        string Param = string.Empty;
        bool bSuccess = true;
        Action<string> SuccessFunc = null;
        Action<string> FailFunc = null;
        public bool isUse = false;
        public void Fill(bool Success, string szParam = "", Action<string> FSuccessFunc = null, Action<string> FFailFunc = null)
        {
            bSuccess = Success;
            Param = szParam;
            SuccessFunc = FSuccessFunc;
            FailFunc = FFailFunc;
            isUse = true;
        }
        public void Excute()
        {
            if (bSuccess)
            {
                if (SuccessFunc != null)
                {
                    SuccessFunc(Param);
                }
            }
            else
            {
                if (FailFunc != null)
                {
                    FailFunc(Param);
                }
            }
            isUse = false;
        }
    }
}

