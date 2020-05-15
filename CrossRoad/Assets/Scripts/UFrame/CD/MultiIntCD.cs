/*******************************************************************
* FileName:     MultiIntCD.cs
* Author:       Fan Zheng Yong
* Date:         2020-3-7
* Description:  
* other:    
********************************************************************/


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFrame
{
    public class MultiIntCD : TickBase
    {
        Dictionary<IntCD, System.Action<int, IntCD>> cdCallbackDict
            = new Dictionary<IntCD, System.Action<int, IntCD>>();


        /// <summary>
        /// cdCallback中能收到cd, 如果caCallback希望下一轮cd
        /// 继续执行，在适当的时候调用cd.Reset()和cd.Run()
        /// </summary>
        /// <param name="cdVal"></param>
        /// <param name="cdCallback"></param>
        public void AddCD(int cdVal, System.Action<int, IntCD> cdCallback)
        {
            var cd = new IntCD(cdVal);
            cd.Run();
            cdCallbackDict.Add(cd, cdCallback);
        }

        public override void Tick(int deltaTimeMS)
        {
            if (!CouldRun())
            {
                return;
            }

            foreach (var kv in cdCallbackDict)
            {
                var cd = kv.Key;
                var action = kv.Value;
                cd.Tick(deltaTimeMS);
                int realCDVal = cd.org;
                if (cd.IsRunning() && cd.IsFinish())
                {
                    if (cd.cd < 0)
                    {
                        realCDVal += (-cd.cd);
                    }
                    cd.Stop();
                    action?.Invoke(realCDVal, cd);
                    
                }
            }
        }

        public void Release()
        {
            foreach (var key in cdCallbackDict.Keys)
            {
                key.Stop();
            }

            cdCallbackDict.Clear();
        }
    }
}

