///*******************************************************************
//* FileName:     BehaviourMessageDefine.cs
//* Author:       Fan Zheng Yong
//* Date:         2019-8-6
//* Description:  
//* other:    
//********************************************************************/


//using UFrame.MessageCenter;
//using UFrame.BehaviourFloat;

//namespace UFrame
//{

//    public enum BehaviourMessageDefine
//    {
//        None = 0,
//        Arrived,
//    }

//    public class MessageArrived : Message
//    {
//        public static ObjectPool<MessageArrived> pool;
        
//        public FollowPath followPath;
//        public MessageArrived()
//        {
//            this.messageID = (int)BehaviourMessageDefine.Arrived;
//        }

//        public override void OnDeathToPool()
//        {
//            this.followPath = null;
//            base.OnDeathToPool();
//        }

//        public override void Release()
//        {
//            pool.Delete(this);
//        }
//    }
//}



