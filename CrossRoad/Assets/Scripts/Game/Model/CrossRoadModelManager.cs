using System.Collections;
using System.Collections.Generic;
using UFrame.Common;
using UnityEngine;

namespace CrossRoadGame
{
    public class CrossRoadModelManager : Singleton<CrossRoadModelManager>, ISingleton
    {
        /// <summary>
        /// 路的数据
        /// </summary>
        public RoadModel roadModel;

        /// <summary>
        /// enity数据
        /// </summary>
        public CrossRoadEntityModel entityModel;

        /// <summary>
        /// 关卡id
        /// </summary>
        public int stageID;

        /// <summary>
        /// 动物标准尺寸
        /// </summary>
        public Vector3 standardAnimalBoxSize;

        /// <summary>
        /// 终点动物杯挂点
        /// </summary>
        public List<Vector3> endAnimalPos;

        /// <summary>
        /// 终点动物中转挂点
        /// </summary>
        public Vector3 endAnimalTransferPos;


        /// <summary>
        /// 第一条路资源的位置
        /// </summary>
        public Vector3 startPoint;


        /// <summary>
        /// 屏幕左下世界坐标
        /// </summary>
        public Vector3 spLBW;

        /// <summary>
        /// 屏幕左上世界坐标
        /// </summary>
        public Vector3 spLTW;

        /// <summary>
        /// 屏幕右下世界坐标
        /// </summary>
        public Vector3 spRBW;

        /// <summary>
        /// 屏幕右上世界坐标
        /// </summary>
        public Vector3 spRTW;

        /// <summary>
        /// 相机是否在移动
        /// </summary>
        public bool isCameraMoving  = false;


        public CrossRoadAnimalTeamModel animalTeamModel;

        public void Init()
        {
            roadModel = new RoadModel();
            entityModel = new CrossRoadEntityModel();
            endAnimalPos = new List<Vector3>();
            endAnimalTransferPos = new Vector3();
            animalTeamModel = new CrossRoadAnimalTeamModel();
        }

        public void Release()
        {
            if (roadModel != null)
            {
                roadModel.Release();
            }

            if (entityModel != null)
            {
                entityModel.Release();
            }

            //if (animalEntityModel != null)
            //{
            //    animalEntityModel.Release();
            //}

            //if (animalModel != null)
            //{
            //    animalModel.Release();
            //}

            if (animalTeamModel != null)
            {
                animalTeamModel.Release();
            }
        }
    }

}
