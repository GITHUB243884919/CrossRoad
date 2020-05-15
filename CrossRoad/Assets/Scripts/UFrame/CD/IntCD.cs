/*******************************************************************
* FileName:     IntCD.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-13
* Description:  
* other:    
********************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFrame
{
    public class IntCD : TickBase
    {
        public int org;
        public int cd;

        public IntCD(int org)
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

        public void ResetOrg(int org)
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

        public int GetLeft()
        {
            return cd;
        }
    }
}

