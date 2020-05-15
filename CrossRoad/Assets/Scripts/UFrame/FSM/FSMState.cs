/*******************************************************************
* FileName:     FSMState.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-6
* Description:  
* other:    
********************************************************************/


using System.Collections.Generic;


namespace UFrame
{
    public abstract class FSMState : TickBase
    {
        public int stateName;
        public int preStateName;
        public FSMMachine fsmCtr;

        Dictionary<int, System.Func<bool>> convertConds = new Dictionary<int, System.Func<bool>>();
        public abstract void AddAllConvertCond();

        public FSMState(int stateName, FSMMachine fsmCtr)
        {
            this.stateName = stateName;
            this.fsmCtr = fsmCtr;
        }

        public virtual void Enter(int preStateName)
        {
            this.preStateName = preStateName;
            this.AddAllConvertCond();
        }

        public virtual void Leave()
        {
            convertConds.Clear();
        }

        public void AddConvertCond(int stateName, System.Func<bool> callback)
        {
            convertConds.Add(stateName, callback);
        }

        public int ChangeState()
        {
            int newState = Const.Invalid_Int;
            foreach (var kv in convertConds)
            {
                if (kv.Value())
                {
                    newState = kv.Key;
                }
            }

            return newState;
        }

        public virtual void Release()
        {
            this.fsmCtr = null;
            this.preStateName = Const.Invalid_Int;
        }
    }
}
