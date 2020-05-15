using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossRoadGame
{
    public class CrossRoadAnimalTeamModel
    {
        /// <summary>
        /// 所有动物实体列表(按照顺序排队)
        /// </summary>
        public List<EntityCrossRoadAnimal> entityCrossRoadAnimalList = new List<EntityCrossRoadAnimal>();

        /// <summary>
        /// 已经跨过当前路的
        /// </summary>
        public HashSet<int> passedCurrRoadSet = new HashSet<int>();

        ///// <summary>
        ///// 已经到达下一条路对应的位置的
        ///// </summary>
        //public HashSet<int> arrivedNextRoadSet = new HashSet<int>();

        /// <summary>
        /// 队伍正在通过的路
        /// </summary>
        public int currentRoad = 0;
        
        public CrossRoadAnimalTeamModel()
        {
        }

        void Init()
        {

        }

        public void Release()
        {
            entityCrossRoadAnimalList.Clear();
            passedCurrRoadSet.Clear();
            currentRoad = 0;
        }

    }
}
