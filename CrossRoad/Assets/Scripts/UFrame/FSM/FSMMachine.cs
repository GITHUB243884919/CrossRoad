/*******************************************************************
* FileName:     FSMMachine.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-6
* Description:  
* other:    
********************************************************************/



using System.Collections.Generic;

namespace UFrame
{
    public class FSMMachine : TickBase
    {
        Dictionary<int, FSMState> states = new Dictionary<int, FSMState>();
        FSMState currState;
        int preStateName;
        FSMState defaultState;

        public void AddState(FSMState state)
        {
            states.Add(state.stateName, state);
        }

        public void SetDefaultState(int stateName)
        {
            defaultState = states[stateName];
        }

        public void GotoState(int stateName)
        {
            if (currState != null)
            {
                if (currState.stateName == stateName)
                {
                    return;
                }
                preStateName = currState.stateName;
                currState.Leave();
            }

            currState = states[stateName];
            currState.Enter(preStateName);
        }

        public override void Tick(int deltaTimeMS)
        {
            if (!CouldRun())
            {
                return;
            }

            if (currState == null && defaultState != null)
            {
                GotoState(defaultState.stateName);
            }

            if (currState != null)
            {
                currState.Tick(deltaTimeMS);
                if (currState != null)
                {
                    int newState = currState.ChangeState();
                    if (newState != Const.Invalid_Int)
                    {
                        GotoState(newState);
                    }
                }
            }
        }

        public override void Stop()
        {
            base.Stop();
            if (currState != null)
            {
                currState.Leave();
                currState = null;
            }
        }

        public FSMState GetCurrentState()
        {
            return this.currState;
        }

        public virtual void Release()
        {
            Stop();
            foreach (var v in states.Values)
            {
                v.Release();
            }
            states.Clear();
            preStateName = Const.Invalid_Int;
        }

        public int numState
        {
            get
            {
                return states.Count;
            }
        }
    }


}

