using System;
using UnityEngine;

namespace Utils
{   
     public class Singleton<T> where T : class ,new()
     {
         private static T s_instance;
         private static readonly object s_synclock = new object();
         
         private Singleton()
         {
         }

         public static T Instance
         {
             get
             {
                 if (s_instance == null)
                 {
                     lock (s_synclock)
                     {
                         if (s_instance == null)
                         {
                             s_instance = new T();
                         }
                     }
                 }
                 return s_instance;
             }
             set { s_instance = value; }
         }
     }
}

