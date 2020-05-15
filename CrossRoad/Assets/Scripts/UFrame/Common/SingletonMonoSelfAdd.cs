/*******************************************************************
* FileName:     SingletonMonoSelfAdd.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-19
* Description:  
* other:    
********************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFrame.Common
{
    public class SingletonMonoSelfAdd<T> : UnityEngine.MonoBehaviour
        where T : UnityEngine.MonoBehaviour
    {
        static public T GetInstance(string objName)
        {
            if (s_Instance == default(T))
            {
                GameObject go = null;
                if (!s_Objs.TryGetValue(objName, out go))
                {
                    go = new GameObject(objName);
                    s_Objs.Add(objName, go);
                    GameObject.DontDestroyOnLoad(go);
                }
                else
                {
                    throw new System.Exception("用做单件组件的场景节点重名，请换一个节点名称");
                }

                s_Instance = go.GetComponent<T>();
                if (s_Instance == default(T))
                {
                    s_Instance = go.AddComponent<T>();
                }
            }

            return s_Instance;
        }

        static public void DestroyInstance()
        {
            s_Instance = default(T);
        }

        public virtual void OnDestroy()
        {
            GameObject go;
            if (s_Objs.TryGetValue(s_Instance.name, out go))
            {
                s_Objs.Remove(s_Instance.name);
            }

            DestroyInstance();
            if (go != null)
            {
                GameObject.Destroy(go);
                go = null;
            }
        }

        protected SingletonMonoSelfAdd() { }

        private static T s_Instance = default(T);

        static Dictionary<string, GameObject> s_Objs = new Dictionary<string, GameObject>();
    }

}
