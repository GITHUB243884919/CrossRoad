
using UFrame;
using UnityEngine;

namespace CrossRoadGame
{
    /// <summary>
    /// 加载原始场景
    /// </summary>
    public class StateLoadOrgScene : FSMState
    {
        bool isToStateLoadSceneObject = false;

        Vector3 standardAnimalBoxSize
        {
            get
            {
                return CrossRoadModelManager.GetInstance().standardAnimalBoxSize;
            }

            set
            {
                CrossRoadModelManager.GetInstance().standardAnimalBoxSize = value;
            }
        }

        Vector3 startPoint
        {
            get
            {
                return CrossRoadModelManager.GetInstance().startPoint;
            }

            set
            {
                CrossRoadModelManager.GetInstance().startPoint = value;
            }
        }

        string loadingPageName
        {
            get
            {
                return (this.fsmCtr as FSMCrossRoadGame).loadingPageName;
            }
        }

        public StateLoadOrgScene(int stateName, FSMMachine fsmCtr) :
            base(stateName, fsmCtr)
        {
        }

        public override void Enter(int preStateName)
        {
            base.Enter(preStateName);

            isToStateLoadSceneObject = false;

            SceneMgr.Inst.LoadSceneAsync(Config.globalConfig.getInstace().CrossRoadScene,
                FinishedCallback, null);
        }

        protected void FinishedCallback()
        {
            (this.fsmCtr as FSMCrossRoadGame).SetLoadingPageSlider();

            GetStartPoint();
            GetStandardAnimalBoxSize();
            GetScreenConnerLine();
            isToStateLoadSceneObject = true;
        }

        public override void Tick(int deltaTimeMS)
        {
        }

        public override void Leave()
        {
            base.Leave();
        }

        public override void AddAllConvertCond()
        {
            AddConvertCond((int)CrossRoadGameState.LoadSceneObject, ToStateLoadSceneObject);
        }

        protected bool ToStateLoadSceneObject()
        {
            return isToStateLoadSceneObject;
        }

        void GetStandardAnimalBoxSize()
        {
            standardAnimalBoxSize = GameObject.Find("StandardAnimalBox").GetComponent<BoxCollider>().size;
        }

        void GetStartPoint()
        {
            startPoint = GameObject.Find("StartPos").transform.position;
        }

        void GetScreenConnerLine()
        {
            var pLB = GetScreenPointInWorld(Camera.main, new Vector2(0, 0));
            CrossRoadModelManager.GetInstance().spLBW = pLB;
#if UNITY_EDITOR
            GameObject.CreatePrimitive(PrimitiveType.Capsule).transform.position = pLB;
#endif

            var pLT = GetScreenPointInWorld(Camera.main, new Vector2(0, Screen.height));
            CrossRoadModelManager.GetInstance().spLTW = pLT;
#if UNITY_EDITOR
            GameObject.CreatePrimitive(PrimitiveType.Capsule).transform.position = pLT;
#endif

            var pRB = GetScreenPointInWorld(Camera.main, new Vector2(Screen.width, 0));
            CrossRoadModelManager.GetInstance().spRBW = pRB;
#if UNITY_EDITOR
            GameObject.CreatePrimitive(PrimitiveType.Capsule).transform.position = pRB;
#endif

            var pRT = GetScreenPointInWorld(Camera.main, new Vector2(Screen.width, Screen.height));
            CrossRoadModelManager.GetInstance().spRTW = pRT;
#if UNITY_EDITOR
            GameObject.CreatePrimitive(PrimitiveType.Capsule).transform.position = pRT;
#endif

        }

        Vector3 GetScreenPointInWorld(Camera cam, Vector2 screenPos)
        {
            Ray ray = cam.ScreenPointToRay(screenPos);

            return UFrame.Math_F.GetIntersectWithLineAndGround(ray.origin, ray.direction);
        }
    }
}
