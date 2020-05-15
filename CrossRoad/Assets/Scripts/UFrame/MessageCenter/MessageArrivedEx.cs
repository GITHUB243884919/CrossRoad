using UFrame.MessageCenter;
using UFrame.BehaviourFloat;

namespace UFrame
{
    public class MessageArrivedEx : Message
    {
        public static ObjectPool<MessageArrivedEx> pool = new ObjectPool<MessageArrivedEx>();

        public BehaviourBase followPath;
        public MessageArrivedEx()
        {
            //this.messageID = (int)UFrameBuildinMessage.ArrivedSuperMarket;
        }

        public override void OnDeathToPool()
        {
            this.followPath = null;
        }

        public override void Release()
        {
            pool.Delete(this);
        }

        public void Init(int messageID, BehaviourBase followPath)
        {
            this.messageID = messageID;
            this.followPath = followPath;
        }

        public static MessageArrivedEx Send(int messageID, BehaviourBase followPath)
        {
            var msg = pool.New();
            msg.Init(messageID, followPath);
            MessageManager.GetInstance().Send(msg);
            return msg;
        }

        public override string ToString()
        {
            //return string.Format("MessageArrived isArrivedEnd={0}, nextPosIdx={1}, pathLen={2}",
            //    followPath.isArrivedEnd, followPath.nextPosIdx, followPath.pathPosList.Length);

            return string.Format("MessageArrivedEx");
        }
    }
}
