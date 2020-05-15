using System.Collections;
using System.Collections.Generic;
using UFrame;
using UFrame.Common;
using UnityEngine;

namespace Game
{
    public class GameModulesTick : TickBase
    {
        public GameModules manager;
        public override void Tick(int deltaTimeMS)
        {
            if (!this.CouldRun())
            {
                return;
            }
            for (int i = 0, iMax = this.manager.gameModules.Count; i < iMax; i++)
            {
                this.manager.gameModules[i].Tick(deltaTimeMS);
            }
        }

        public override void Stop()
        {
            base.Stop();
            for (int i = 0, iMax = this.manager.gameModules.Count; i < iMax; i++)
            {
                this.manager.gameModules[i].Stop();
            }
        }

        public override void Pause()
        {
            base.Pause();
            for (int i = 0, iMax = this.manager.gameModules.Count; i < iMax; i++)
            {
                this.manager.gameModules[i].Pause();
            }
        }

        public override void Run()
        {
            base.Run();
            for (int i = 0, iMax = this.manager.gameModules.Count; i < iMax; i++)
            {
                this.manager.gameModules[i].Run();
            }
        }
    }

    public class GameModules
    {
        GameModulesTick tickObj;
        public List<GameModule> gameModules = new List<GameModule>();

        public GameModules()
        {
            this.tickObj = new GameModulesTick();
            this.tickObj.manager = this;
        }

        public void AddMoudle(GameModule module)
        {
            this.gameModules.Add(module);
            this.Sort();
            module.Init();
        }

        protected void Sort()
        {
            gameModules.Sort(SortFunc);
        }

        protected int SortFunc(GameModule a, GameModule b)
        {
            return a.orderID.CompareTo(b.orderID);
        }

        public void Run()
        {
            tickObj.Run();
        }

        public void Stop()
        {
            tickObj.Stop();
        }

        public void Pause()
        {
            tickObj.Pause();
        }

        public void Tick(int deltaTimeMS)
        {
            tickObj.Tick(deltaTimeMS);
        }

        public void Release()
        {
            Stop();
            foreach(var val in gameModules)
            {
                val.Release();
            }
            gameModules.Clear();
        }

    }

}
