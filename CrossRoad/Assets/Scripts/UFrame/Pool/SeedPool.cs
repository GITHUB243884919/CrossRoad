/*******************************************************************
* FileName:     SeedPool.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-7
* Description:  
* other:    
********************************************************************/


using System.Collections.Generic;
using UnityEngine;

namespace UFrame
{
    public abstract class SeedPool<T>
    {
        Queue<T> objs = new Queue<T>();

        protected T seed;
        public void SetSeed(T t)
        {
            this.seed = t;
        }

        public abstract T Spawn();

        /// <summary>
        /// 假删除
        /// </summary>
        public abstract void OnDeathToPool(T t);

        /// <summary>
        /// 真删除
        /// </summary>
        public abstract void OnRecovery(T t);

        public T New()
        {
            if (objs.Count == 0)
            {
                return Spawn();
            }
            return objs.Dequeue();
        }

        public void Delete(T t)
        {
            objs.Enqueue(t);
            OnDeathToPool(t);
        }

        public void RecoveryAll()
        {
            Logger.LogWarp.LogFormat("RecoveryAll {0}", seed.ToString());
            while (objs.Count > 0)
            {
                var obj = objs.Dequeue();
                OnRecovery(obj);
            }

            objs.Clear();
            OnRecovery(seed);
        }
    }
  

    public class GameObjectPool : SeedPool<GameObject>
    {
        public override GameObject Spawn()
        {
            return GameObject.Instantiate<GameObject>(seed);
        }

        public override void OnDeathToPool(GameObject t)
        {
            DebugFile.GetInstance().WriteKeyFile(t.GetInstanceID(), 
                "{0} GameObjectPool.OnDeathToPool", t.GetInstanceID());
        }

        public override void OnRecovery(GameObject t)
        {
            GameObject.Destroy(t);
            t = null;
        }
    }

}
