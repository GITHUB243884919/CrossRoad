/*******************************************************************
* FileName:     GameMoudle.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-6
* Description:  
* other:    
********************************************************************/


using UFrame;

namespace Game
{
    public enum ModuleType
    {
        /// <summary>
        /// 场景
        /// </summary>
        Scene = 1,

        /// <summary>
        /// 全局的
        /// </summary>
        Global,
    }
    public abstract class GameModule : TickBase
    {
        public GameModule(int orderID, ModuleType moduleType = ModuleType.Scene)
        {
            this.orderID = orderID;
            this.moduleType = moduleType;
        }
        public int orderID = 0;
        public ModuleType moduleType = ModuleType.Scene;
        public abstract void Init();
        public abstract void Release();
    }
}

