/*******************************************************************
* FileName:     DoubleCD.cs
* Author:       Fan Zheng Yong
* Date:         2019-12-5
* Description:  
* other:    
********************************************************************/


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFrame
{
    [Serializable]
    public class DoubleCD : TickBase
    {
        public double org;
        public double cd;

        public DoubleCD(double org)
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
                this.cd -= (double)(delta / 1000d);
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

        public void ResetOrg(double org)
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

        public double GetLeft()
        {
            return cd;
        }
    }
}

