/*******************************************************************
* FileName:     UFrameMath_Float.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-6
* Description:  
* other:    
********************************************************************/


using System.Collections.Generic;
using UnityEngine;

namespace UFrame
{
    public class Math_F
    {
        private static readonly float PI = 3.14159265359f;

        public static int FloatToInt(float val)
        {
            return (int)((double)(val));
        }

        public static int FloatToInt1000(float val)
        {
            return (int)((double)((val) * 1000d));
        }

        /// <summary>
        /// double 数据转时间格式（分钟，秒）
        /// </summary>
        /// <param name="isDouble"></param>
        /// <returns></returns>
        public static string OnDounbleToFormatTime_Minute(int isDouble)
        {
            int second = isDouble % 60;//秒
            int second01 = second / 10;//秒的十位数
            int second02 = second % 10;//秒的个位数

            int minute = isDouble / 60;//分
            int minute01 = minute / 10;//分的十位
            int minute02 = minute % 10;//分的个位

            string str = string.Format("{0}{1}:{2}{3}", minute01, minute02, second01, second02);

            return str;
        }
        public static string OnDounbleToFormatTime_Anhour(int isDouble)
        {
            System.TimeSpan ts = new System.TimeSpan(0, 0, System.Convert.ToInt32(isDouble));
            string str = "{0}:{1}:{2}";
            //str = ts.Hours.ToString() + ":" + ts.Minutes.ToString() + ":" + ts.Seconds;
            string str_Hours = "";
            string str_Minutes = "";
            string str_Seconds = "";

            if (ts.Hours > 0)
            {
                str_Hours = ts.Hours.ToString();
            }
            else
            {
                str_Hours = "00";
            }
            if (ts.Minutes > 0)
            {
                if (ts.Minutes/10>0)
                    str_Minutes = ts.Minutes.ToString();
                else
                    str_Minutes = "0" + ts.Minutes.ToString();
            }
            else
            {
                str_Minutes = "00";
            }
            if (ts.Seconds > 0)
            {
                if (ts.Seconds/10>0)
                    str_Seconds = ts.Seconds.ToString();
                else
                    str_Seconds = "0" + ts.Seconds.ToString();
            }
            else
            {
                str_Seconds = "00";
            }
            str = string.Format(str,str_Hours,str_Minutes,str_Seconds);
            return str;


        }

        public static Vector2 Vector3_2D(Vector3 v3)
        {
            Vector2 v2 = Vector2.zero;
            v2.x = v3.x;
            v2.y = v3.z;

            return v2;
        }

        /// <summary>
        /// 线和平面的交点
        /// 前两个参数是线的参数（线上的点，线的方向）
        /// 后两个参数是平面参数（法向量，平面上的一点）
        /// </summary>
        /// <param name="point">直线上某一点</param>
        /// <param name="direct">直线的方向</param>
        /// <param name="planeNormal">垂直于平面的的向量</param>
        /// <param name="planePoint">平面上的任意一点</param>
        /// <returns></returns>
        public static Vector3 GetIntersectWithLineAndPlane(Vector3 point, Vector3 direct, Vector3 planeNormal, Vector3 planePoint)
        {
            float d = Vector3.Dot(planePoint - point, planeNormal) / Vector3.Dot(direct.normalized, planeNormal);
            return d * direct.normalized + point;
        }


        /// <summary>
        /// 线和地平面的交点
        /// 地平面：法向量是(0, 1, 0),地平面上点是(1, 0, 0)
        /// </summary>
        /// <param name="point"></param>
        /// <param name="direct"></param>
        /// <returns></returns>
        public static Vector3 GetIntersectWithLineAndGround(Vector3 point, Vector3 direct)
        {
            return GetIntersectWithLineAndPlane(point, direct, Vector3.up, Vector3.right);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgPos"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static Vector3 RandomInsideCircle(Vector3 orgPos, float radius)
        {
            var v2 = Random.insideUnitCircle * radius;
            return new Vector3(orgPos.x + v2.x, orgPos.y, orgPos.z + v2.y);
        }


        public static bool ApproximateNumber(float a, float b, float approximateValue = 0.01f)
        {
            if (Mathf.Abs(a - b) > approximateValue)
            {
                return false;
            }

            return true;
        }

        public static bool Approximate2D(Vector2 a, Vector2 b, float approximateValue = 0.01f)
        {

            if (!ApproximateNumber(a.x, b.x, approximateValue))
            {
                return false;
            }

            if (!ApproximateNumber(a.y, b.y, approximateValue))
            {
                return false;
            }

            return true;
        }

        public static bool Approximate3D(Vector3 a, Vector3 b, float approximateValue = 0.01f)
        {
            if (!ApproximateNumber(a.x, b.x, approximateValue))
            {
                return false;
            }

            if (!ApproximateNumber(a.y, b.y, approximateValue))
            {
                return false;
            }

            if (!ApproximateNumber(a.z, b.z, approximateValue))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// sour到dest的单位方向向量
        /// </summary>
        /// <param name="sour"></param>
        /// <param name="dest"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static Vector3 TwoPositionDir(Vector3 sour, Vector3 dest)
        {
            return (dest - sour).normalized;
        }


        /// <summary>
        /// 两个单位向量夹角的角度
        /// 两个参数必须是单位向量
        /// </summary>
        /// <param name="sour"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public static float TwoDirYAngle(Vector3 sour, Vector3 dest)
        {
            return RadianToAngle(TwoDirYRadian(sour, dest));
        }

        /// <summary>
        /// 两个单位向量夹角的弧度
        /// 两个参数必须是单位向量
        /// </summary>
        /// <param name="sour"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public static float TwoDirYRadian(Vector3 sour, Vector3 dest)
        {
            float dot = Vector3.Dot(sour, dest);
            return Mathf.Acos(dot);
        }

        public static float RadianToAngle(float radian)
        {
            return radian * 180 / PI;
        }

        public static float AngleToRadian(float angle)
        {
            return angle * PI / 180;
        }

        /// <summary>
        /// 圆桌概率 weights  必须升序排列 返回weight的索引
        /// </summary>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static int TableProbability(float [] weights)
        {
            float sumWeight = 0;
            for(int i = 0; i < weights.Length; i++)
            {
                sumWeight += weights[i];
            }

            float r = Random.Range(0f, 1f);
            float min = 0f;
            float max = 0f;
            for(int i = 0; i < weights.Length; i++)
            {
                min = max;
                max += weights[i] / sumWeight;
                if (r >= min && r < max)
                {
                    //Logger.LogWarp.LogFormat("r={0}, p={1}", r, weights[i] / sumWeight);
                    return i;
                }
                //Logger.LogWarp.LogFormat("min{0}, max{1}", min, max);
            }

            Logger.LogWarp.LogErrorFormat("概率计算异常 r={0}", r);
            return -1;
        }

        public static float TableProbability(List<float> weights)
        {
            float sumWeight = 0;
            weights.Sort();
            for (int i = 0; i < weights.Count; i++)
            {
                sumWeight += weights[i];
            }

            float r = Random.Range(0f, 1f);
            float min = 0f;
            float max = 0f;
            for (int i = 0; i < weights.Count; i++)
            {
                min = max;
                max += weights[i] / sumWeight;
                if (r >= min && r < max)
                {
                    Logger.LogWarp.LogFormat("r={0}, p={1}, p2={2}", r, weights[i] / sumWeight, weights[i]);
                    return weights[i];
                }
                //Logger.LogWarp.LogFormat("min{0}, max{1}", min, max);
            }

            Logger.LogWarp.LogErrorFormat("概率计算异常 r={0}", r);
            return -1f;
        }

        public static int TableProbability(List<int> weights)
        {
            float sumWeight = 0;
            weights.Sort();
            for (int i = 0; i < weights.Count; i++)
            {
                sumWeight += weights[i];
            }

            float r = Random.Range(0f, 1f);
            float min = 0f;
            float max = 0f;
            for (int i = 0; i < weights.Count; i++)
            {
                min = max;
                max += weights[i] / sumWeight;
                if (r >= min && r < max)
                {
                    //Logger.LogWarp.LogErrorFormat("r={0}, p={1}, p2={2}", r, weights[i] / sumWeight, weights[i]);
                    return weights[i];
                }
                //Logger.LogWarp.LogFormat("min{0}, max{1}", min, max);
            }

            Logger.LogWarp.LogErrorFormat("概率计算异常 r={0}", r);
            return -1;
        }

        public static int TableProbability(List<int> weights, ref int index)
        {
            float sumWeight = 0;
            for (int i = 0; i < weights.Count; i++)
            {
                sumWeight += weights[i];
            }

            float r = Random.Range(0f, 1f);
            float min = 0f;
            float max = 0f;
            for (int i = 0; i < weights.Count; i++)
            {
                min = max;
                max += weights[i] / sumWeight;
                if (r >= min && r < max)
                {
                    //Logger.LogWarp.LogErrorFormat("r={0}, p={1}, p2={2}", r, weights[i] / sumWeight, weights[i]);
                    index = i;
                    return weights[i];
                }
                //Logger.LogWarp.LogFormat("min{0}, max{1}", min, max);
            }

            Logger.LogWarp.LogErrorFormat("概率计算异常 r={0}", r);
            return -1;
        }

        /// <summary>
        /// 判定点是否在凸多边形内
        /// http://www.manongjc.com/article/92247.html
        /// </summary>
        /// <param name="p"></param>
        /// <param name="vertexs"></param>
        /// <returns></returns>
        public static bool IsPointInPolygon(Vector2 p, List<Vector2> vertexs)
        {
            int crossNum = 0;
            int vertexCount = vertexs.Count;

            for (int i = 0; i < vertexCount; i++)
            {
                Vector2 v1 = vertexs[i];
                Vector2 v2 = vertexs[(i + 1) % vertexCount];

                if (((v1.y < p.y) && (v2.y > p.y))
                    || ((v1.y > p.y) && (v2.y < p.y)))
                {
                    if (p.x < v1.x + (p.y - v1.y) / (v2.y - v1.y) * (v2.x - v1.x))
                    {
                        crossNum += 1;
                    }
                }
            }

            if (crossNum % 2 == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        static GameObject crossObj;
        /// <summary>
        /// 获得点和凸多边形各边中距离最近的那个交点
        /// </summary>
        /// <param name="p"></param>
        /// <param name="vertexs"></param>
        /// <returns></returns>
        public static Vector2 GetClosestCrossPoint(Vector2 p, List<Vector2> vertexs)
        {
            Vector2 result = Vector2.zero;
            int vertexCount = vertexs.Count;
            float len = float.MaxValue;
            int idx = 0;
            for (int i = 0; i < vertexCount; i++)
            {
                Vector2 v1 = vertexs[i];
                Vector2 v2 = vertexs[(i + 1) % vertexCount];
                
                Vector2 cross;
                float dis = PointToLineDistanceSqr2D(p, v1, v2, out cross);
                if (dis < len)
                {
                    idx = i;
                    len = dis;
                    result = cross;
                }
            }
            return result;
        }

        public static Vector2 GetClosestCrossPoint(Vector2 p, List<Vector2> vertexs, 
            out Vector2 lineStart, out int idxStart, out Vector2 lineEnd, out int idxEnd)
        {
            Vector2 result = Vector2.zero;
            lineStart = Vector2.zero;
            lineEnd = Vector2.zero;
            idxStart = 0;
            idxEnd = 0;
            int vertexCount = vertexs.Count;
            float len = float.MaxValue;
            int idx = 0;
            for (int i = 0; i < vertexCount; i++)
            {
                idxStart = i;
                idxEnd = (i + 1) % vertexCount;
                Vector2 v1 = vertexs[idxStart];
                Vector2 v2 = vertexs[idxEnd];

                Vector2 cross;
                float dis = PointToLineDistanceSqr2D(p, v1, v2, out cross);
                if (dis < len)
                {
                    idx = i;
                    len = dis;
                    result = cross;
                    lineStart = v1;
                    lineEnd = v2;
                }
            }
            return result;
        }

        public static float PointToLineDistance3D(Vector3 point, Vector3 linePoint1, Vector3 linePoint2)
        {
            float fProj = Vector3.Dot(point - linePoint1, (linePoint1 - linePoint2).normalized);
            return Mathf.Sqrt((point - linePoint1).sqrMagnitude - fProj * fProj);
        }

        public static float PointToLineDistance2D(Vector2 point, Vector2 linePoint1, Vector2 linePoint2)
        {
            float proj = Vector3.Dot(point - linePoint1, (linePoint1 - linePoint2).normalized);
            return Mathf.Sqrt((point - linePoint1).sqrMagnitude - proj * proj);
        }

        /// <summary>
        /// 点到直线的距离的平方，out带出交点
        /// </summary>
        /// <param name="point"></param>
        /// <param name="linePoint1"></param>
        /// <param name="linePoint2"></param>
        /// <param name="cross"></param>
        /// <returns></returns>
        public static float PointToLineDistanceSqr2D(Vector2 point, Vector2 linePoint1, Vector2 linePoint2, out Vector2 cross)
        {
            Vector2 norm = (linePoint2 - linePoint1).normalized;
            float proj = Vector3.Dot(point - linePoint1, norm);
            cross = linePoint1 + norm * proj;
            return (point - linePoint1).sqrMagnitude - proj * proj;
        }

        public static float IntToFloat_SToHr(int val)
        {
            var value = val / 3600f;
            return value;
        }

    }
}

