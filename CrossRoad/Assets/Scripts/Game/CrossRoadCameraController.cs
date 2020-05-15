using UnityEngine;
using UFrame.Common;
using DG.Tweening;
using UFrame;
using Game.MessageCenter;
using UFrame.MessageCenter;
using Game;

namespace CrossRoadGame
{
    /// <summary>
    /// 过马路摄像机移动类
    /// </summary>
    public class CrossRoadCameraController : SingletonMono<CrossRoadCameraController> 
    {
        public float moveDuration = 1f;

        Transform cachedTrans = null;

        Vector3 offset;
        
        RoadModel roadModel
        {
            get
            {
                return CrossRoadModelManager.GetInstance().roadModel;
            }
        }

        bool isCameraMoving
        {
            get
            {
                return CrossRoadModelManager.GetInstance().isCameraMoving;
            }
            set
            {
                CrossRoadModelManager.GetInstance().isCameraMoving = value;
            }
        }

        public override void Awake()
        {
            base.Awake();

            cachedTrans = transform;
            MessageManager.GetInstance().Regist((int)GameMessageDefine.LoadCrossRoadLevelFinished, OnLoadCrossRoadLevelFinished);
        }

        public override void OnDestroy()
        {
            MessageManager.GetInstance().UnRegist((int)GameMessageDefine.LoadCrossRoadLevelFinished, OnLoadCrossRoadLevelFinished);

            base.OnDestroy();
        }

#if UNITY_EDITOR
        int numLookAt = 0;
        void OnGUI()
        {
            if (GUI.Button(new Rect(10, 100, 200, 50), "Cross Road Camera"))
            {
                TestCamera();
            }

            if (GUI.Button(new Rect(10, 200, 200, 50), "Back To Zoo"))
            {
                CrossRoadStageManager.GetInstance().UnLoad();
            }

            if (GUI.Button(new Rect(10, 300, 200, 50), "New Stage"))
            {
                //DebugFile.GetInstance().DeleteAllFiles();
                CrossRoadStageManager.GetInstance().UnLoad();
                CrossRoadStageManager.GetInstance().Load(1);
            }

            if (GUI.Button(new Rect(10, 400, 200, 50), "Retry"))
            {
                //todo 本关卡复活
                //动物卸载，并重新加载
                //相机回到初始
                //MessageManager.GetInstance().Send((int)GameMessageDefine.IncreaseCrossRoadStageID);
            }

        }

        public void TestCamera()
        {
            numLookAt++;
            int idx = numLookAt % roadModel.animalRoadSegment.Count;
            MoveCamera(roadModel.animalRoadSegment[idx]);
        }
#endif

        public void MoveCamera(Vector3 lookAt)
        {
            Vector3 target = CalcCameraMoveTo(lookAt);
            TweenMoveCamera(target);
        }

        Vector3 CalcCameraMoveTo(Vector3 lookAtPos)
        {
            return lookAtPos + offset;
        }

        void TweenMoveCamera(Vector3 target)
        {
            isCameraMoving = true;
            Vector3 tweenPos = cachedTrans.position;
            Tween tw = DOTween.To(() => tweenPos, x => tweenPos = x,
                target, moveDuration);
            tw.SetEase(Ease.InSine);
            tw.OnUpdate(() =>
            {
                cachedTrans.position = tweenPos;
            });

            tw.OnComplete(() =>
            {
                MessageManager.GetInstance().Send((int)GameMessageDefine.CrossRoadCameraStopMove);
                isCameraMoving = false;
            });
        }

        protected void OnLoadCrossRoadLevelFinished(Message msg)
        {
            offset = cachedTrans.position - roadModel.animalRoadSegment[0];
        }
    }

}