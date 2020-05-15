/*******************************************************************
* FileName:     MessageDefine.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-8
* Description:  
* other:    
********************************************************************/


using UFrame.MessageCenter;
using UFrame.BehaviourFloat;

namespace UFrame
{

    public enum UFrameBuildinMessage
    {
        None = 0,
        /// <summary>
        /// 到达 对应FollowPath
        /// </summary>
        Arrived,

        /// <summary>
        /// 到达 对应Wander
        /// </summary>
        WanderArrived,

        /// <summary>
        /// 到达 对应FollowPathEx
        /// </summary>
        ArrivedEx,

        /// <summary>
        /// 到达 对应FollowPathRightAngles
        /// </summary>
        ArrivedRightAngle,

        /// <summary>
        /// 到达 对应FollowPathSuperMarket
        /// </summary>
        ArrivedSuperMarket,

        CameraDebug,
    }
}

namespace UFrame
{
    public class MessageString : Message
    {
        public string str;
        public static ObjectPool<MessageString> pool = new ObjectPool<MessageString>();

        public MessageString()
        {
        }

        public override void OnDeathToPool()
        {
        }

        public override void Release()
        {
            pool.Delete(this);
        }

        public void Init(int messageID, string str)
        {
            this.messageID = messageID;
            this.str = str;
        }

        public static MessageString Send(int messageID, string str)
        {
            var msg = pool.New();
            msg.Init(messageID, str);
            MessageManager.GetInstance().Send(msg);
            return msg;
        }

        public override string ToString()
        {
            return string.Format("MessageString str={0}", str);
        }
    }

    public class MessageInt : Message
    {
        public int val;
        public static ObjectPool<MessageInt> pool = new ObjectPool<MessageInt>();

        public MessageInt()
        {
        }

        public override void OnDeathToPool()
        {
        }

        public override void Release()
        {
            pool.Delete(this);
        }

        public void Init(int messageID, int val)
        {
            this.messageID = messageID;
            this.val = val;
        }

        public static MessageInt Send(int messageID, int val)
        {
            var msg = pool.New();
            msg.Init(messageID, val);
            MessageManager.GetInstance().Send(msg);
            return msg;
        }

        public override string ToString()
        {
            return string.Format("MessageInt val={0}", val);
        }
    }


}




