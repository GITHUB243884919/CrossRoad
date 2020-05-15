/*******************************************************************
* FileName:     RunCoroutine.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-19
* Description:  
* other:    
********************************************************************/


using UnityEngine;
using System.Collections;


namespace UFrame.Common
{
    public class RunCoroutine : SingletonMonoSelfAdd<RunCoroutine>
    {
        static WaitForEndOfFrame waitForEndOfFrame = null;
        public static Coroutine Run(IEnumerator routine)
        {
            return GetInstance("RunCoroutine").StartCoroutine(routine);
        }

        public static void Stop(Coroutine runCrt)
        {
            if (null == runCrt)
            {
                return;
            }

            GetInstance("RunCoroutine").StopCoroutine(runCrt);
        }

        public static WaitForEndOfFrame WaitForEndOfFrame
        {
            get
            {
                if (waitForEndOfFrame == null)
                {
                    waitForEndOfFrame = new WaitForEndOfFrame();
                }

                return waitForEndOfFrame;
            }
        }
    }
}
