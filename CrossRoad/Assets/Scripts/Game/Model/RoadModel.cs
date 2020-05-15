using System.Collections.Generic;
using UFrame;
using UFrame.Logger;
using UnityEngine;

namespace CrossRoadGame
{
    public enum RoadDir
    {
        /// <summary>
        /// 从左边往右边
        /// </summary>
        LeftToRight,

        /// <summary>
        /// 从右边到左边
        /// </summary>
        RightToLeft,
    }

    public class CrossRoad
    {
        /// <summary>
        /// 起点
        /// </summary>
        public Vector3 startPos;

        /// <summary>
        /// 终点
        /// </summary>
        public Vector3 endPos;

        /// <summary>
        ///  发车方向
        /// </summary>
        public RoadDir roadDir;

        /// <summary>
        /// 发车cd
        /// </summary>
        public int spawnCarCDVal;

        /// <summary>
        /// 车的速度
        /// </summary>
        public float carSpeed;
    }

    public class RoadModel
    {
        /// <summary>
        /// 每个车道数据，一个路的资源可能会有2条以上的路
        /// </summary>
        public List<CrossRoad> crossRoadList = new List<CrossRoad>();

        /// <summary>
        /// 用rect描述的每个路的资源的区域
        /// </summary>
        public List<Rect> crossRoadRectArea = new List<Rect>();

        /// <summary>
        /// 动物行走路段-每条路的资源的最南面的中心点
        /// </summary>
        public List<Vector3> animalRoadSegment = new List<Vector3>();

        /// <summary>
        /// 横跨路的间隔
        /// </summary>
        public float spaceRoad = 0f;

        public RoadModel()
        {
            Init();
        }

        void Init()
        {

        }

        public void AddCrossRoad(int numRoad, Rect rc, Vector3 center, Vector3 startPos, Vector3 endPos, int cdVal, RoadDir roadDir, float carSpeedMin, float carSpeedMax)
        {
            //一条路的资源被分成几份
            //1条道的路分成2份
            //2条道的路分成4份
            //3条道的路分成6份
            Vector3 offset = Vector3.forward * rc.height / (numRoad * 2);
            
            for (int i = 0; i < numRoad; i++)
            {
                //方向随机
                float p = Random.Range(0.1f, 1.0f);
                var _roadDir = roadDir;
                var _startPos = startPos;
                var _endPos = endPos;
                if (p > 0.5f)
                {
                    _roadDir = RoadDir.RightToLeft;
                    Vector3 tmp = _startPos;
                    _startPos = _endPos;
                    _endPos = tmp;
                }
                //路的最南面起点
                Vector3 startbottom = _startPos;
                startbottom += Vector3.back * rc.height * 0.5f;
                //GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = startbottom;

                Vector3 endbottom = _endPos;
                endbottom += Vector3.back * rc.height * 0.5f;
                //GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = endbottom;

                // i  第几条道     需要加的offset    公式=i * 2 + 1
                // 0   1           1 =               0 * 2 + 1
                // 1   2           3 =               1 * 2 + 1
                // 2   3           5 =               2 * 2 + 1
                int offsetMul = i * 2 + 1;
                var calcStart = startbottom + offset * offsetMul;

                float screenClampX = CrossRoadStageManager.GetInstance().carStartOffsetX;
                //if (roadDir == RoadDir.LeftToRight)
                //{
                //    screenClampX = GetXInLineByY(calcStart.z,
                //        Math_F.Vector3_2D(CrossRoadModelManager.GetInstance().spLBW),
                //        Math_F.Vector3_2D(CrossRoadModelManager.GetInstance().spLTW));

                //    calcStart.x = Mathf.Max(calcStart.x, screenClampX);
                //}
                //else
                //{
                //    screenClampX = GetXInLineByY(calcStart.z,
                //        Math_F.Vector3_2D(CrossRoadModelManager.GetInstance().spRBW),
                //        Math_F.Vector3_2D(CrossRoadModelManager.GetInstance().spRTW));

                //    calcStart.x = Mathf.Min(calcStart.x, screenClampX);
                //}
                if (_roadDir == RoadDir.LeftToRight)
                {
                    calcStart.x = center.x + (-1 * screenClampX);
                }
                else
                {
                    calcStart.x = center.x + (1 * screenClampX);
                }
#if UNITY_EDITOR
                var goStart = GameObject.CreatePrimitive(PrimitiveType.Cube);
                goStart.transform.position = calcStart;
                goStart.name = "CalcStart-" + calcStart.ToString() + " " + _roadDir;
#endif

                var calcEnd = endbottom + offset * offsetMul;
#if UNITY_EDITOR
                var goEnd = GameObject.CreatePrimitive(PrimitiveType.Cube);
                goEnd.transform.position = calcEnd;
                goEnd.name = "CalcEnd-" + calcEnd.ToString() + " " + _roadDir;
#endif
                float randomSpeed = Random.Range(carSpeedMin, carSpeedMax);
                var crossRoad = new CrossRoad();
                crossRoad.startPos = calcStart;
                crossRoad.endPos = calcEnd;
                crossRoad.spawnCarCDVal = cdVal;
                crossRoad.carSpeed = randomSpeed;
                crossRoad.roadDir = _roadDir;
                crossRoadList.Add(crossRoad);
            }

            crossRoadRectArea.Add(rc);
        }

        public void AddToAnimalRoadSegment(Vector3 crossRoadpos, Rect rc, bool isAddTop)
        {
            var bottom = crossRoadpos + Vector3.back * rc.height * 0.5f;
#if UNITY_EDITOR
            GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = bottom;
#endif
            animalRoadSegment.Add(bottom);

            if (isAddTop)
            {
                var top = crossRoadpos + Vector3.forward * rc.height * 0.5f;
#if UNITY_EDITOR
                GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = top;
#endif
                animalRoadSegment.Add(top);
            }


            //LogWarp.LogErrorFormat("测试：    往动物等待位添加东西  {0} ", animalRoadSegment.Count);
        }

        public float GetXInLineByY(float y, Vector2 p1, Vector2 p2)
        {
            return (y - p1.y) * (p2.x - p1.x) / (p2.y - p1.y) + p1.x;
        }

        public void Release()
        {
            crossRoadList.Clear();
            crossRoadRectArea.Clear();
            animalRoadSegment.Clear();

            spaceRoad = 0f;
        }

    }
}
