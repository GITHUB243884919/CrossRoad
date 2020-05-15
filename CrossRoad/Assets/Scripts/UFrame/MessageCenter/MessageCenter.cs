/*******************************************************************
* FileName:     MessageCenter.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-6
* Description:  
* other:    
********************************************************************/


namespace UFrame.MessageCenter
{
    public abstract class Message : IObjectPoolable
    {
        #region pool
        public virtual void OnDeathToPool()
        {
        }

        public void OnRecovery()
        {
        }
        #endregion

        public abstract void Release();

        public int messageID;
    }

    public class OnlyMessageID : Message
    {
        public static ObjectPool<OnlyMessageID> pool = new ObjectPool<OnlyMessageID>();

        public void Init(int messageID)
        {
            this.messageID = messageID;
        }

        public override void Release()
        {
            pool.Delete(this);
        }

    }

}

