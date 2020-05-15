/*******************************************************************
* FileName:     ActionMessageCenter.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-6
* Description:  
* other:    
********************************************************************/


using System.Collections.Generic;
using System;
using Game.MessageCenter;

namespace UFrame.MessageCenter
{
    public class ActionMessage : IObjectPoolable
    {
        public bool isVaild = false;
        public Action<Message> executor = null;

        public static ObjectPool<ActionMessage> pool = new ObjectPool<ActionMessage>();

        public ActionMessage()
        {
            isVaild = false;
            executor = null;
        }

        public void OnDeathToPool()
        {
            isVaild = false;
            executor = null;
        }

        public void OnRecovery()
        {

        }

        public void Init(bool isVaild, Action<Message> executor)
        {
            this.isVaild = isVaild;
            this.executor = executor;
        }
    }

    public class ActionMessageCenter
    {
        protected Queue<Message> messages = new Queue<Message>();

        protected Dictionary<int, List<ActionMessage>> executorDic =
            new Dictionary<int, List<ActionMessage>>();

        protected List<ActionMessage> removeExecutors = new List<ActionMessage>();

        public System.Action<int> callbackNotFoundMessage = null;

        public void Regist(int messageID, Action<Message> executor)
        {
            List<ActionMessage> excutors = null;
            if (!executorDic.TryGetValue(messageID, out excutors))
            {
                excutors = new List<ActionMessage>();
                executorDic.Add(messageID, excutors);
            }

            bool isFound = false;
            for (int i = 0; i < excutors.Count; i++)
            {
                if (excutors[i].executor == executor)
                {
                    excutors[i].isVaild = true;
                    if (!isFound)
                    {
                        isFound = true;
                    }
                }
            }

            if (!isFound)
            {
                ActionMessage actionMessage = ActionMessage.pool.New();
                actionMessage.Init(true, executor);
                excutors.Add(actionMessage);
            }
        }

        public void UnRegist(int messageID, Action<Message> executor)
        {
            List<ActionMessage> excutors = null;
            if (!executorDic.TryGetValue(messageID, out excutors))
            {
                return;
            }

            for(int i = 0; i < excutors.Count; i++)
            {
                if (excutors[i].executor == executor)
                {
                    excutors[i].isVaild = false;
                }
            }
        }

        public void Send(Message msg, bool immediately = true)
        {
            messages.Enqueue(msg);
        }

        public void Send(int messageID, bool immediately = true)
        {
            var msg = OnlyMessageID.pool.New();
            msg.Init(messageID);
            messages.Enqueue(msg);
        }

        public void Tick()
        {
            while (messages.Count > 0)
            {
                Message msg = messages.Dequeue();
                Execute(msg);
            }

        }

        void Execute(Message msg)
        {
            List<ActionMessage> excutors = null;
            if (!executorDic.TryGetValue(msg.messageID, out excutors))
            {
                callbackNotFoundMessage?.Invoke(msg.messageID);
                return;
            }

            removeExecutors.Clear();
            for (int i = 0; i < excutors.Count; i++)
            {
                var excutor = excutors[i];
                if (excutor.isVaild)
                {
                    excutor.executor(msg);
                }
                else
                {
                    removeExecutors.Add(excutor);
                }
            }
            msg.Release();

            for(int i = 0; i < removeExecutors.Count; i++)
            {
                excutors.Remove(removeExecutors[i]);
                ActionMessage.pool.Delete(removeExecutors[i]);
            }
        }
    }
}
