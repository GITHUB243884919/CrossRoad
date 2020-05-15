using UnityEngine;

namespace Utils
{
    public class DebugLogger
    {
        public static DebugLogger GetInstance()
        {
            return Utils.Singleton<DebugLogger>.Instance;
        }
        
        public void Log(string logContent)
        {
            if (isDebugMode)
            {
                Debug.Log(logContent);
            }
        }

        public bool isDebugMode { get; set; }
    }
}