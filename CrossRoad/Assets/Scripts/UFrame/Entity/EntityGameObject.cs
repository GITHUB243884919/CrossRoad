/*******************************************************************
* FileName:     EntityGameObject.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-6
* Description:  
* other:    
********************************************************************/


using UnityEngine;

namespace UFrame.EntityFloat
{
    public class EntityGameObject : Entity
    {
        public GameObject mainGameObject = null;

        public Transform cacheTrans = null;

        public override Vector3 position
        {
            get{ return GetTrans().position; }

            set{ GetTrans().position = value; } 
        }

        public override Quaternion rotation
        {
            get { return GetTrans().rotation; }
            set { GetTrans().rotation = value; }
        }

        public override Vector3 forward
        {
            get { return GetTrans().forward; }
            set { GetTrans().forward = value; }
        }

        public override void Rotate(Vector3 eulers, Space relativeTo = Space.Self)
        {
            this.GetTrans().Rotate(eulers, relativeTo);
        }

        public override Vector3 InverseTransformPoint(Vector3 worldPos)
        {
            return this.GetTrans().InverseTransformPoint(worldPos);
        }

        public override void LookAt(Vector3 target)
        {
            this.GetTrans().LookAt(target);
        }

        public void ClearCatchTrans()
        {
            cacheTrans = null;
        }

        public Transform GetTrans()
        {
            if (this.cacheTrans == null)
            {
                this.cacheTrans = mainGameObject.transform;
            }

            return this.cacheTrans;
        }



    }


}
