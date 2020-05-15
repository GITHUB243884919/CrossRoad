/*******************************************************************
* FileName:     Singleton.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-6
* Description:  
* other:    
********************************************************************/


namespace UFrame.Common
{
    public interface ISingleton
    {
        void Init();
    }

    public class Singleton<T> where T : ISingleton, new()
    {
        static public T GetInstance()
        {
            if (null == s_Instance)
            {
                s_Instance = new T();
                s_Instance.Init();
            }
            return s_Instance;

        }

        static public void DestroyInstance()
        {
            s_Instance = default(T);
        }

        protected Singleton() { }

        private static T s_Instance = default(T);
    }
}

