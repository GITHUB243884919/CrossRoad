using Game;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public static class ExtendTool
{
    public static T AddMissingComponent<T>(this GameObject obj) where T : Component
    {
        T script = obj.GetComponent<T>();
        if (script == null)
        {
            script = obj.AddComponent<T>();
        }
        return script;
    }
    //角度转方向
    public static Vector3 AngleToForward(this Vector3 Angle)
    {
        Quaternion qu = Quaternion.Euler(Angle);

        return (qu * Vector3.forward).normalized;
    }
    //方向转角度
    public static Vector3 ForwardToAngle(this Vector3 Forward)
    {
        if (Forward == Vector3.zero)
        {
            return Forward;
        }
        Quaternion qu = Quaternion.LookRotation(Forward);
        return qu.eulerAngles;
    }
    //角度转换成 +-180
    public static Vector3 AngleBetween180(this Vector3 vAngle)
    {
        if (vAngle.x > 180f)
        {
            vAngle.x -= 360f;
        }
        if (vAngle.y > 180f)
        {
            vAngle.y -= 360f;
        }
        if (vAngle.z > 180f)
        {
            vAngle.z -= 360f;
        }
        return vAngle;
    }
    //角度转化成 0 - 360
    public static Vector3 AngleTo360(this Vector3 vAngle)
    {
        if (vAngle.x < 0)
        {
            vAngle.x += 360f;
        }
        if (vAngle.y < 0)
        {
            vAngle.y += 360f;
        }
        if (vAngle.z < 0)
        {
            vAngle.z += 360f;
        }
        return vAngle;
    }

    //计算方向按轴旋转一定角度后的方向
    public static Vector3 ForwardRotateAngle(this Vector3 vCurDir, float dgree, Vector3 Axis)
    {
        Vector3 vAngle = vCurDir.ForwardToAngle();
        vAngle += Axis.normalized * dgree;
        return vAngle.AngleToForward();
    }
    //public static void SetText(this Text text, string key, params object[] formatStr)
    //{
    //    TranslationDesc desc = TableMgr.GetInstance().GetTranslationDescDesc(key);
    //    string content = null;
    //    if (LoadingMgr.Inst.CurLanguageType == SystemLanguage.Chinese)
    //    {
    //        content = desc.chinese;
    //    }
    //    else if (LoadingMgr.Inst.CurLanguageType == SystemLanguage.Russian)
    //    {
    //        content = desc.russian;
    //    }
    //    else if (LoadingMgr.Inst.CurLanguageType == SystemLanguage.English)
    //    {
    //        content = desc.english;
    //    }
    //    else if (LoadingMgr.Inst.CurLanguageType == SystemLanguage.French)
    //    {
    //        content = desc.french;
    //    }
    //    if (formatStr != null)
    //    {
    //        text.text = string.Format(content, formatStr);
    //    }
    //    else
    //    {
    //        text.text = content;
    //    }

    //}
    public static void FormatDistance(this Text text, float distance, int fixNum = 2)
    {
        string result = string.Empty;
        // string format = "0.";
        //for (int i = 0; i < fixNum; ++i)
        //{
        //    format += "0";
        //}
        //if (distance >= 1000)
        //{
        //    result = (distance / 1000f).ToString(format) + "km";
        //}
        //else
        //{
        int dis = (int)distance;
        result = string.Concat(dis, "m");
        // }
        text.text = result;
    }
    //格式化金币显示
    public static void FormatCoin(this Text text, decimal coin, int fixNum = 2, string ConnectStr = null, int forward = 0)
    {
        string result = string.Empty;
        string format = "0.";
        for (int i = 0; i < fixNum; ++i)
        {
            format += "0";
        }
        if (coin >= 100000000000000000000000000m)
        {
            result = (coin / 100000000000000000000000000m).ToString(format) + "ae";
        }
        else if (coin >= 1000000000000000000000000m)
        {
            result = (coin / 1000000000000000000000000m).ToString(format) + "ad";
        }
        else if (coin >= 1000000000000000000000m)
        {
            result = (coin / 1000000000000000000000m).ToString(format) + "ac";
        }
        else if (coin >= 1000000000000000000m)
        {
            result = (coin / 1000000000000000000m).ToString(format) + "ab";
        }
        else if (coin >= 1000000000000000)
        {
            result = (coin / 1000000000000000m).ToString(format) + "aa";
        }
        else if (coin >= 1000000000000)
        {
            result = (coin / 1000000000000m).ToString(format) + "t";
        }
        else if (coin >= 1000000000)
        {
            result = (coin / 1000000000m).ToString(format) + "b";
        }
        else if (coin >= 1000000)
        {
            result = (coin / 1000000m).ToString(format) + "m";
        }
        else if (coin >= 1000)
        {
            result = (coin / 1000m).ToString(format) + "k";
        }
        else
        {
            result = coin.ToString("0");
        }
        if (ConnectStr != null)
        {
            if (forward == 0)
            {
                text.text = string.Concat(ConnectStr, result);

            }
            else
            {
                text.text = string.Concat(result, ConnectStr);
            }
        }
        else
        {
            text.text = result;
        }
    }
    //更改layer
    public static void ChangeLayer(this GameObject obj, int Layer)
    {
        obj.layer = Layer;
        Transform[] transChildren = obj.GetComponentsInChildren<Transform>(true);
        if (transChildren != null && transChildren.Length > 0)
        {
            foreach (var item in transChildren)
            {
                if (item != obj.transform)
                {
                    ChangeLayer(item.gameObject, Layer);
                }
            }
        }
    }
    public static void FormatCoin(this InputField text, decimal coin, int fixNum = 2, string ConnectStr = null, int forward = 0)
    {
        string result = string.Empty;
        string format = "0.";
        for (int i = 0; i < fixNum; ++i)
        {
            format += "0";
        }
        if (coin >= 100000000000000000000000000m)
        {
            result = (coin / 100000000000000000000000000m).ToString(format) + "ae";
        }
        else if (coin >= 1000000000000000000000000m)
        {
            result = (coin / 1000000000000000000000000m).ToString(format) + "ad";
        }
        else if (coin >= 1000000000000000000000m)
        {
            result = (coin / 1000000000000000000000m).ToString(format) + "ac";
        }
        else if (coin >= 1000000000000000000m)
        {
            result = (coin / 1000000000000000000m).ToString(format) + "ab";
        }
        else if (coin >= 1000000000000000)
        {
            result = (coin / 1000000000000000m).ToString(format) + "aa";
        }
        else if (coin >= 1000000000000)
        {
            result = (coin / 1000000000000m).ToString(format) + "t";
        }
        else if (coin >= 1000000000)
        {
            result = (coin / 1000000000m).ToString(format) + "b";
        }
        else if (coin >= 1000000)
        {
            result = (coin / 1000000m).ToString(format) + "m";
        }
        else if (coin >= 1000)
        {
            result = (coin / 1000m).ToString(format) + "k";
        }
        else
        {
            result = coin.ToString("0");
        }
        if (ConnectStr != null)
        {
            if (forward == 0)
            {
                text.text = string.Concat(ConnectStr, result);

            }
            else
            {
                text.text = string.Concat(result, ConnectStr);
            }
        }
        else
        {
            text.text = result;
        }
    }

    public static string GetFormatCoin(decimal coin, int fixNum = 2)
    {
        string result = string.Empty;
        string format = "0.";
        for (int i = 0; i < fixNum; ++i)
        {
            format += "0";
        }
        if (coin >= 100000000000000000000000000m)
        {
            result = (coin / 100000000000000000000000000m).ToString(format) + "ae";
        }
        else if (coin >= 1000000000000000000000000m)
        {
            result = (coin / 1000000000000000000000000m).ToString(format) + "ad";
        }
        else if (coin >= 1000000000000000000000m)
        {
            result = (coin / 1000000000000000000000m).ToString(format) + "ac";
        }
        else if (coin >= 1000000000000000000m)
        {
            result = (coin / 1000000000000000000m).ToString(format) + "ab";
        }
        else if (coin >= 1000000000000000)
        {
            result = (coin / 1000000000000000m).ToString(format) + "aa";
        }
        else if (coin >= 1000000000000)
        {
            result = (coin / 1000000000000m).ToString(format) + "t";
        }
        else if (coin >= 1000000000)
        {
            result = (coin / 1000000000m).ToString(format) + "b";
        }
        else if (coin >= 1000000)
        {
            result = (coin / 1000000m).ToString(format) + "m";
        }
        else if (coin >= 1000)
        {
            result = (coin / 1000m).ToString(format) + "k";
        }
        else
        {
            result = coin.ToString("0");
        }
        return result;
    }

    public static decimal FormatUnitToNumber(string value)
    {
        decimal result = 0m;
        string num = string.Empty;
        if (value.EndsWith("k"))
        {
            num = value.Substring(0, value.Length - 1);
            result = decimal.Parse(num) * 1000m;
        }
        else if (value.EndsWith("m"))
        {
            num = value.Substring(0, value.Length - 1);
            result = decimal.Parse(num) * 1000000m;
        }
        else if (value.EndsWith("b"))
        {
            num = value.Substring(0, value.Length - 1);
            result = decimal.Parse(num) * 1000000000m;
        }
        else if (value.EndsWith("t"))
        {
            num = value.Substring(0, value.Length - 1);
            result = decimal.Parse(num) * 1000000000000m;
        }
        else if (value.EndsWith("aa"))
        {
            num = value.Substring(0, value.Length - 2);
            result = decimal.Parse(num) * 1000000000000000m;
        }
        else if (value.EndsWith("ab"))
        {
            num = value.Substring(0, value.Length - 2);
            result = decimal.Parse(num) * 1000000000000000000m;
        }
        else if (value.EndsWith("ac"))
        {
            num = value.Substring(0, value.Length - 2);
            result = decimal.Parse(num) * 1000000000000000000000m;
        }
        else if (value.EndsWith("ad"))
        {
            num = value.Substring(0, value.Length - 2);
            result = decimal.Parse(num) * 1000000000000000000000000m;
        }
        else if (value.EndsWith("ae"))
        {
            num = value.Substring(0, value.Length - 2);
            result = decimal.Parse(num) * 100000000000000000000000000m;
        }
        else
        {
            num = value;
            result = decimal.Parse(num);
        }
        return result;
    }

    static AndroidJavaObject m_class = null;
    public static void DoShake(this GameObject obj, long waitTime, long doTime, bool isCircle)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if(m_class == null)
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            m_class = jc.GetStatic<AndroidJavaObject>("currentActivity");
        }
        if(m_class != null)
        {
            m_class.Call("DoVibrate", waitTime, doTime, isCircle);
        }
        else
        {
            Debug.Log("Do Shake Class Null");
        }
#elif UNITY_IOS && !UNITY_EDITOR
        if(GameInit.g_Init != null)
        {
            GameInit.g_Init.StartCoroutine(DelayDoShake(waitTime,doTime, isCircle));
        }
#endif
    }
    public static IEnumerator DelayDoShake(long waitTime, long doTime, bool isCircle)
    {
        StartShake:

        if (waitTime > 0)
        {
            yield return new WaitForSeconds(waitTime);
        }
        float fCurTime = 0f;
        float fEachTime = 500f;
        while (fCurTime < doTime)
        {
            Handheld.Vibrate();
            //#if UNITY_IOS && !UNITY_EDITOR
            //            Debug.Log("ios do shake");
            //            DoShake(2);
            //#endif
            yield return new WaitForSeconds(fEachTime * 0.001f);
            fCurTime += fEachTime;
        }
        if (isCircle)
        {
            goto StartShake;
        }
    }
    public static void CancelShake(this GameObject obj)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if(m_class != null)
        {
            m_class.Call("CancelVibrate");
        }
#elif UNITY_IOS && !UNITY_EDITOR
        if (GameInit.g_Init != null)
        {
            GameInit.g_Init.StopCoroutine(DelayDoShake(0, 0, false));
        }
#endif

    }
    public static bool CheckInFrontOfMe(this Transform Trans,Transform Target)
    {
       return  Vector3.Dot(Trans.forward, (Target.position - Trans.position).normalized) > 0;
    }
#if UNITY_IOS && !UNITY_EDITOR
    [DllImport("__Internal")]
    public extern static void DoShake(int type);
#endif
    public static void CheckToResetScreenSolution(this MonoBehaviour be)
    {
#if UNITY_IOS
        if(Util.Flag_ExternScreen())
        {
            int scWidth = Screen.width;
            int scHeight = Screen.height;
            int designWidth = 720; //这个是设计分辨率
            int designHeight = 1280;
            if (scWidth <= designWidth || scHeight <= designHeight)
                return;

            float scale = 0.6f;
            scWidth = (int)(scWidth * scale);
            scHeight = (int)(scHeight * scale);
            Screen.SetResolution(scWidth, scHeight, true);
        }
#endif
    }
}
