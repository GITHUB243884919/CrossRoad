/*******************************************************************
* FileName:     BehaviourBase.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-6
* Description:  
* other:    
********************************************************************/


using UnityEngine;
using UFrame.EntityFloat;

namespace UFrame.BehaviourFloat
{
    public abstract class BehaviourBase : TickBase
    {
        public EntityMovable ownerEntity;
        protected Vector3 orgPos = Vector3.zero;
        public float speed = 0;
        protected float tickSpeed = 0;
        protected Vector3 tarPos = Vector3.zero;
        protected Vector3 tickDir = Vector3.zero;
        protected Vector3 tickMove = Vector3.zero;
        protected Vector3 tickPos = Vector3.zero;

        public virtual void Release()
        {

        }

        protected virtual bool IsPassed(Vector3 pos)
        {
            var localPos = this.ownerEntity.InverseTransformPoint(pos);
            return localPos.z <= 0;
        }
    }

    /// <summary>
    /// 速度模式
    /// </summary>
    public enum SpeedMode
    {
        /// <summary>
        /// 匀速
        /// </summary>
        Uniform,

        /// <summary>
        /// 加速
        /// </summary>
        Up,

        /// <summary>
        /// 减速
        /// </summary>
        Down
    }
}

