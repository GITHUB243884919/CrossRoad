/*******************************************************************
* FileName:     MonoTickBase.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-6
* Description:  
* other:    
********************************************************************/


using UnityEngine;

namespace UFrame
{
    public abstract class MonoTickBase : MonoBehaviour
    {
        bool isRunning = false;
        bool isPause = false;

        public abstract void Tick(int deltaTimeMS);

        public virtual void Run()
        {
            this.isRunning = true;
        }

        public virtual void Stop()
        {
            this.isRunning = false;
        }

        public virtual void Pause()
        {
            this.isPause = true;
        }

        public virtual bool CouldRun()
        {
            return this.isRunning && !this.isPause;
        }
    }
}

