/*******************************************************************
* FileName:     Entity.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-6
* Description:  
* other:    
********************************************************************/


using UnityEngine;

namespace UFrame.EntityFloat
{
    public abstract class Entity : IObjectPoolable
    {
        public int entityID;

        /// <summary>
        /// 对一个唯一的prefab,和策划资源表的主键对应
        /// </summary>
        public int entityResType;

        /// <summary>
        /// 与策划表无关，跟单位实体的功能有关
        /// </summary>
        public int entityFuncType;

        public virtual void OnDeathToPool()
        {
            //this.entityID = -1;
        }

        public virtual void OnRecovery()
        {

        }

        public abstract void Rotate(Vector3 eulers, Space relativeTo = Space.Self);

        public abstract Vector3 position { get; set; }

        public abstract Quaternion rotation { get; set; }

        public abstract Vector3 forward { get; set; }

        public abstract Vector3 InverseTransformPoint(Vector3 worldPos);

        public abstract void LookAt(Vector3 target);

    }

}
