///** 
// *Author:       ouchen 
// *Date:         2018-11-28 
// *Description:  事件派发
// *History: 
//*/

//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Reflection;
//using UnityEngine;

//public class EventManager
//{
//    static Dictionary<string, List<Events>> FuncDict = new Dictionary<string, List<Events>>();


//    public static void Add(string key, Action func)
//    {
//        Add(key, func.Target, func.Method,0);
//    }
//    public static void Add<T1>(string key, Action<T1> func)
//    {
//        Add(key, func.Target, func.Method,1);
//    }
//    public static void Add<T1, T2>(string key, Action<T1, T2> func)
//    {
//        Add(key, func.Target, func.Method,2);
//    }
//    public static void Add<T1, T2, T3>(string key, Action<T1, T2, T3> func)
//    {
//        Add(key, func.Target, func.Method,3);
//    }
//    public static void Add<T1, T2, T3, T4>(string key, Action<T1, T2, T3, T4> func)
//    {
//        Add(key, func.Target, func.Method,4);
//    }
//    private static void Add(string key, object target, MethodInfo metod,int paramNum)
//    {
//        List<Events> list = null;
//        if (!FuncDict.TryGetValue(key, out list))
//        {
//            list = new List<Events>();
//            FuncDict[key] = list;
//        }
//        Events ev;
//        ev.info = metod;
//        ev.Target = target;
//        ev.ParamCount = paramNum;
//        FuncDict[key].Add(ev);
//    }


//    public static void Remove(string key, Action func)
//    {
//        Remove(key, func.Target, func.Method,0);
//    }
//    public static void Remove<T1>(string key, Action<T1> func)
//    {
//        Remove(key, func.Target, func.Method,1);
//    }
//    public static void Remove<T1, T2>(string key, Action<T1, T2> func)
//    {
//        Remove(key, func.Target, func.Method,2);
//    }
//    public static void Remove<T1, T2, T3>(string key, Action<T1, T2, T3> func)
//    {
//        Remove(key, func.Target, func.Method,3);
//    }
//    public static void Remove<T1, T2, T3, T4>(string key, Action<T1, T2, T3, T4> func)
//    {
//        Remove(key, func.Target, func.Method,4);
//    }
//    private static void Remove(string key, object target, MethodInfo method,int paramNum)
//    {
//        List<Events> list = null;
//        List<Events> RemoveList = new List<Events>();
//        if (FuncDict.TryGetValue(key, out list))
//        {
//            foreach(var item in list)
//            {
//                if(item.Target == target && item.info == method && paramNum == item.ParamCount)
//                {
//                    RemoveList.Add(item);
//                }
//            }
//            if(RemoveList.Count > 0)
//            {
//                foreach(var item in RemoveList)
//                {
//                    list.Remove(item);
//                }
//                RemoveList.Clear();
//            }
//        }
//    }

//    public static void Trigger(string key)
//    {
//        TriggerFinal(key);
//    }
//    public static void Trigger<T1>(string key, T1 t1)
//    {
//        TriggerFinal(key, t1);
//    }
//    public static void Trigger<T1, T2>(string key, T1 t1, T2 t2)
//    {
//        TriggerFinal(key, t1, t2);
//    }
//    public static void Trigger<T1, T2, T3>(string key, T1 t1, T2 t2, T3 t3)
//    {
//        TriggerFinal(key, t1, t2, t3);
//    }
//    public static void Trigger<T1, T2, T3, T4>(string key, T1 t1, T2 t2, T3 t3, T4 t4)
//    {
//        TriggerFinal(key, t1, t2, t3, t4);
//    }
//    private static void TriggerFinal(string key, params object[] objlist)
//    {
//        List<Events> list = null;
//        if (FuncDict.TryGetValue(key, out list))
//        {
//            int paramCount = objlist.Length;
//            foreach (var item in list)
//            {
//                if(item.ParamCount == paramCount)
//                {
//                    item.Trigger(objlist);
//                }
//            }
//        }
//    }
//}
//public struct Events
//{
//    public MethodInfo info;
//    public object Target;
//    public int ParamCount;
//    public void Trigger(params object[] objList)
//    {
//        info.Invoke(Target, objList);
//        //try
//        //{
//        //    info.Invoke(Target, objList);
//        //}
//        //catch (Exception ex)
//        //{
//        //    Debug.Log(ex.Message);
//        //}
//    }
//}
