/*******************************************************************
* FileName:     LongCD.cs
* Author:       Fan Zheng Yong
* Date:         2019-12-5
* Description:  
* other:    
********************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFrame
{
    public class LongCD : TickBase
    {
        public long org;
        public long cd;

        public LongCD(long org)
        {
            ResetOrg(org);
        }

        public override void Tick(int delta)
        {
            if (!this.CouldRun())
            {
                return;
            }

            if (!IsFinish())
            {
                this.cd -= delta;
            }   
        }

        public void Reset()
        {
            //cd可能跑过，因此不能直接赋值
            this.cd += this.org;
        }

        //public void ResetToOrg()
        //{
        //    this.cd = this.org;
        //}

        public void ResetOrg(long org)
        {
            this.org = org;
            this.cd = org;
        }

        public void SafeReset()
        {
            if (IsFinish())
            {
                Reset();
            }
        }

        public bool IsFinish()
        {
            return (this.cd <= 0);
        }

        public long GetLeft()
        {
            return cd;
        }
    }
}

