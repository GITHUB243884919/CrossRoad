using System.Collections;
using System.Collections.Generic;
using UFrame;
using UFrame.EntityFloat;
using UFrame.MessageCenter;
using UnityEngine;

namespace Game.MessageCenter
{
    public class UIMessage_AddToTick : Message
    {
        public UIPage page;

        public static ObjectPool<UIMessage_AddToTick> pool = new ObjectPool<UIMessage_AddToTick>();

        public UIMessage_AddToTick()
        {
            this.messageID = (int)GameMessageDefine.UIMessage_AddToTick;
        }

        public void Init(UIPage page)
        {
            this.page = page;
        }

        public override void Release()
        {
            pool.Delete(this);
        }

        public static UIMessage_AddToTick Send(UIPage page)
        {
            var msg = pool.New();
            msg.Init(page);
            MessageManager.GetInstance().Send(msg);
            return msg;
        }

        public override string ToString()
        {
            return string.Format("UIMessage_AddToTick page={0}", page.name);
        }
    }
}