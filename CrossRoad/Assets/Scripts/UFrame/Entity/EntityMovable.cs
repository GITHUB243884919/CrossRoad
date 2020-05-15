/*******************************************************************
* FileName:     EntityMovable.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-6
* Description:  
* other:    
********************************************************************/


namespace UFrame.EntityFloat
{
    public class EntityMovable : EntityGameObject
    {
        public float moveSpeed = Const.Invalid_Float;
        public bool isActive = false;

        public virtual void Active()
        {
            this.isActive = true;
        }

        public virtual void Deactive()
        {
            this.isActive = false;
        }

        public virtual bool CouldActive()
        {
            return this.isActive;
        }

        public virtual void Tick(int deltaTimeMS) { }
    }
}


