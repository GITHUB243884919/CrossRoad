using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFrame
{
    public interface IVoidParam
    {

    }

    public class VoidParamIntCD
    {
        public IntCD CD;
        public IVoidParam cbParam;
        public System.Action<IntCD, IVoidParam> cb;
    }

    public class VoidParamIntCDs : TickBase
    {
        List<VoidParamIntCD> CDs = new List<VoidParamIntCD>();


        /// <summary>
        /// cdCallback中能收到cd, 如果caCallback希望下一轮cd
        /// 继续执行，在适当的时候调用cd.Reset()和cd.Run()
        /// </summary>
        /// <param name="cdVal"></param>
        /// <param name="cdCallback"></param>
        public void AddCD(int cdVal, IVoidParam voidParam, System.Action<IntCD, IVoidParam> cdCallback)
        {
            var voidParamCD = new VoidParamIntCD
            {
                CD = new IntCD(cdVal),
                cbParam = voidParam,
                cb = cdCallback,
            };
            voidParamCD.CD.Run();
            CDs.Add(voidParamCD);
        }

        public override void Tick(int deltaTimeMS)
        {
            if (!CouldRun())
            {
                return;
            }

            for (int i = 0; i < CDs.Count; i++)
            {
                CDs[i].CD.Tick(deltaTimeMS);
                int realCDVal = CDs[i].CD.org;
                if (CDs[i].CD.IsRunning() && CDs[i].CD.IsFinish())
                {
                    if (CDs[i].CD.cd < 0)
                    {
                        realCDVal += (-CDs[i].CD.cd);
                    }
                    CDs[i].CD.Stop();
                    CDs[i].cb?.Invoke(CDs[i].CD, CDs[i].cbParam);
                }
            }
        }

        public void Release()
        {
            for (int i = 0; i < CDs.Count; i++)
            {
                CDs[i].CD.Stop();
            }

            CDs.Clear();
        }
    }
}
