using UFrame.MessageCenter;

namespace UFrame
{
    public class WanderArrived : Message
    {
        public static ObjectPool<WanderArrived> pool = new ObjectPool<WanderArrived>();

        public int entityID;

        public WanderArrived()
        {
            this.messageID = (int)UFrameBuildinMessage.WanderArrived;
        }

        public override void Release()
        {
            pool.Delete(this);
        }

        public void Init(int entityID)
        {
            this.entityID = entityID;
        }

        public static WanderArrived Send(int entityID)
        {
            var msg = pool.New();
            msg.Init(entityID);
            MessageManager.GetInstance().Send(msg);
            return msg;
        }

        public override string ToString()
        {
            return string.Format("WanderArrived entityID={0}", this.entityID);
        }
    }
}

