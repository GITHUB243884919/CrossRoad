/*******************************************************************
* FileName:     TickBase.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-6
* Description:  
* other:    
********************************************************************/


namespace UFrame
{
    public abstract class TickBase
    {
        bool isRunning = false;
        public bool isPause = false;
    
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
            this.isPause = !this.isPause;
        }

        public virtual bool CouldRun()
        {
            return this.isRunning && !this.isPause;
        }

        public virtual bool IsRunning()
        {
            return this.isRunning && !this.isPause;
        }

    }

}

