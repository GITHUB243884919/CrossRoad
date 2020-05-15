/*******************************************************************
* FileName:     ObjectPool.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-6
* Description:  
* other:    
********************************************************************/


using System;
using System.Collections.Generic;
using Game.MessageCenter;

namespace UFrame
{
    public interface IObjectPoolable
    {
        /// <summary>
        /// 假删除
        /// </summary>
        void OnDeathToPool();

        /// <summary>
        /// 真删除
        /// </summary>
        void OnRecovery();
    }

    public class ObjectPool<T> where T : class, IObjectPoolable, new()
    {
        protected Queue<IObjectPoolable> objs = new Queue<IObjectPoolable>();

        public T New()
        {
            T ret = null;
            if (objs.Count == 0)
            {
                ret = new T();
                return ret;
            }

            ret = objs.Dequeue() as T;
            return ret;
        }


        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="obj"></param>
        public void Delete(T obj)
        {
            objs.Enqueue(obj);
            obj.OnDeathToPool();
        }

        /// <summary>
        /// 真全清空
        /// </summary>
        public void RecoveryAll()
        {
            foreach( var v in this.objs)
            {
                v.OnRecovery();
            }

            objs.Clear();
        }

        public int GetCount()
        {
            return this.objs.Count;
        }
    }

}
