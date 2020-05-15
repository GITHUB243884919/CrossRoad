/*******************************************************************
* FileName:     SingletonMono.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-6
* Description:  
* other:    
********************************************************************/


namespace UFrame.Common
{
    public class SingletonMono<T> : UnityEngine.MonoBehaviour
        where T : UnityEngine.MonoBehaviour
    {
        static public T GetInstance()
        {
            return s_Instance;

        }

        static public void DestroyInstance()
        {
            s_Instance = default(T);
        }

        public virtual void Awake()
        {
            s_Instance = this as T;
        }

        public virtual void OnDestroy()
        {
            DestroyInstance();
        }

        protected SingletonMono() { }

        private static T s_Instance = default(T);
    }
}

