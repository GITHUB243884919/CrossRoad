using UFrame.Logger;
using System.Collections;
using System.Collections.Generic;
using UFrame.EntityFloat;
using UnityEngine;

namespace UFrame.BehaviourFloat
{
    public class PathWander : BehaviourBase
    {
        internal struct PathData
        {
            internal List<Vector3> vertices;
            internal float lenTotal;
            internal List<float> segmentLens;
        }
 
        private PathData pathData;
        private float pathLenTotal;
        private List<float> pathSegmentLens;
        private float beginTime;
        private float normalizedSpeed;
        private bool reverse;

        private static void InitPath(ref PathData pathData)
        {
            if (pathData.vertices == null || pathData.vertices.Count < 1)
                return;
            pathData.lenTotal = 0;
            pathData.segmentLens = new List<float>(pathData.vertices.Count);
            pathData.segmentLens.Add(0);
            for (int i = 1; i < pathData.vertices.Count; i++)
            {
                pathData.segmentLens.Add(Vector3.Distance(pathData.vertices[i - 1], pathData.vertices[i]));
                pathData.lenTotal += pathData.segmentLens[i];
            }
        }

        private static Vector3 PathEvaluate(ref PathData pathData, float time, out Vector3 tgtPos, out Vector3 currPos)
        {
            currPos = Vector3.zero;
            tgtPos = Vector3.zero;
            if (pathData.vertices == null || pathData.vertices.Count < 1)
                return Vector3.zero;

            float len = pathData.lenTotal * time;
            float currLen = 0;
            float lenOfSegment = pathData.segmentLens[pathData.segmentLens.Count - 1];
            int segment = pathData.segmentLens.Count - 2;
            for (int i = 0; i < pathData.segmentLens.Count - 1; i++)
            {
                currLen += pathData.segmentLens[i];
                if (len >= currLen && len < currLen + pathData.segmentLens[i + 1])
                {
                    segment = i;
                    lenOfSegment = len - currLen;
                    break;
                }
            }
            currPos = pathData.vertices[segment];
            tgtPos = pathData.vertices[segment + 1];
            return Vector3.Lerp(pathData.vertices[segment], pathData.vertices[segment + 1], lenOfSegment / pathData.segmentLens[segment + 1]);
        }

        private void AnimTo(float normalizedTime)
        {
            Vector3 tgtPos;
            Vector3 currPos;
            Vector3 pos = PathEvaluate(ref pathData, normalizedTime, out tgtPos, out currPos);
            ownerEntity.position = pos;
            ownerEntity.LookAt(reverse ? currPos : tgtPos);
        }


        public override void Tick(int deltaTimeMS)
        {
            if (!CouldRun())
                return;

            float normalizedTime = (Time.time - beginTime) * normalizedSpeed;
            normalizedTime = reverse ? 1 - normalizedTime : normalizedTime;
            if ((!reverse && normalizedTime - 1 >= 0.001f) || (reverse && normalizedTime <= 0.001f))
            {
                reverse = !reverse;
                WanderArrived.Send(this.ownerEntity.entityID);
                return;
            }
            AnimTo(normalizedTime);
        }


        public void Init(EntityMovable ownerEntity, List<Vector3> pathPosList, float speed, float normalizedTime = 0)
        {
            if (this.IsRunning())
            {
                string e = string.Format("{0}, {1}, {2} PathWander非法初始化", ownerEntity.entityID, ownerEntity.mainGameObject.name,
                    ownerEntity.mainGameObject.GetInstanceID());
                throw new System.Exception(e);
            }

            if (pathPosList == null)
            {
                LogWarp.LogWarningFormat("[PathWander] 无法找到动物漫步路径, AnimalId={0}", ownerEntity.entityID);
            }
            this.pathData = new PathData() { vertices = pathPosList };
            InitPath(ref pathData);
            this.ownerEntity = ownerEntity;
            this.speed = speed;
            this.normalizedSpeed = pathData.lenTotal > 0 ? speed / pathData.lenTotal : 0.1f;
        }

        public override void Run()
        {
            base.Run();

            beginTime = Time.time;
            AnimTo(reverse ? 1 : 0);

            //LogWarp.LogFormat("PathWander Run path vertices.count={0}", pathData.vertices.Count);
        }
    }
}
