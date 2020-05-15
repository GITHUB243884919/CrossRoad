using UFrame.MessageCenter;
using UFrame.BehaviourFloat;

namespace UFrame
{
    public class MessageArrived : Message
    {
        public static ObjectPool<MessageArrived> pool = new ObjectPool<MessageArrived>();

        public FollowPath followPath;
        public MessageArrived()
        {
            this.messageID = (int)UFrameBuildinMessage.Arrived;
        }

        public override void OnDeathToPool()
        {
            this.followPath = null;
            //base.OnDeathToPool();
        }

        public override void Release()
        {
            pool.Delete(this);
        }

        public void Init(FollowPath followPath)
        {
            this.followPath = followPath;
        }

        public static MessageArrived Send(FollowPath followPath)
        {
            var msg = pool.New();
            msg.Init(followPath);
            MessageManager.GetInstance().Send(msg);
            return msg;
        }

        public override string ToString()
        {
            return string.Format("MessageArrived isArrivedEnd={0}, nextPosIdx={1}, pathLen={2}",
                followPath.isArrivedEnd, followPath.nextPosIdx, followPath.pathPosList.Count);
        }
    }
}
