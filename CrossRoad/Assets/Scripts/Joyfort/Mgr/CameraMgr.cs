//using System;
//using DG.Tweening;
//using UnityEngine;
//using DG.Tweening.Plugins.Options;
//using DG.Tweening.Core;

//public class CameraMgr : MonoBehaviour
//{
//    #region variable
//    private const float PIAngle = 180f;
//    private Vector3 Run_Pos_Offset = new Vector3(6.4f, 20f, -3.24f);
//    private Vector3 Run_Rotation = new Vector3(44, -135, 0);

//    private Vector3 Finish_Rotation = new Vector3(40, 194, 0);

//    private Vector3 m_runPos = new Vector3(5f, 20f, -3.24f);

//    private Vector3 UI_Pos_Offset = new Vector3(-3.77f, 2.3f, 4.7f);
//    private Vector3 UI_Rotation = new Vector3(40, 48, 0);

//    private CarControl m_TargetCar;

//    private GameObject m_CameraObj;
//    private Camera m_MainCamera;

//    private Camera m_OldCamera;
//    private GameObject m_OldCameraObj;
//    private bool isVictoryStart = false;

//    private Vector4 m_curSkyColor = new Vector4();
//    private DG.Tweening.Core.TweenerCore<Vector4, Vector4, DG.Tweening.Plugins.Options.VectorOptions> skyColorDoTween;

//    public static CameraMgr Inst { get; private set; }

//    #endregion

//    #region Monobehaviour

//    void Awake()
//    {
//        Inst = this;
//        m_OldCameraObj = GameObject.FindGameObjectWithTag("MainCamera");
//        m_OldCamera = m_OldCameraObj.GetComponent<Camera>();

//        this.ChangeUIState();
//        this.SetRunOffset();

//        //EventManager.Add<Vector4, bool>(EventEnum.RaceEndlessSetSkyColor, this.SetSkyColor);
//    }

//    //Update is called once per frame
//    void LateUpdate()
//    {
//        if (!RaceMgr.Inst.IsPlaying)
//            return;
//        this.SetRunPos();
//        if (m_TargetCar != null && m_TargetCar.isFinish)
//        {
//            this.PlayFinishTween();
//        }
//    }

//    #endregion

//    #region public function

//    //设置相机的关注 目标对象
//    public void SetTarget(CarControl target)
//    {
//        if(m_CameraObj == null || !m_CameraObj.activeInHierarchy)
//        {
//            m_CameraObj = m_OldCameraObj;
//            m_MainCamera = m_OldCamera;
//            EnableOldCamera(true);
//        }
//        m_TargetCar = target;
//        //this.ChangeRunState();
//    }
//    public void SetMainCamera(Camera camera)
//    {
//        EnableOldCamera(false);
//        m_MainCamera = camera;
//        m_CameraObj = m_MainCamera.gameObject;
//    }
//    public void EnableOldCamera(bool enable)
//    {
//        if(m_OldCamera != null)
//        {
//            m_OldCamera.gameObject.SetActive(enable);
//        }
//    }
//    //修改为 比赛视角
//    public void ChangeRunState()
//    {
//        this.m_runPos = new Vector3(Run_Pos_Offset.x, Run_Pos_Offset.y, Run_Pos_Offset.z);
//        this.isVictoryStart = false;
//        this.SetRunPos();
//        this.SetRunRotate();
//        //this.ChangeSkyColor();
//    }

//    //修改为 UI视角
//    public void ChangeUIState()
//    {
//        //m_CameraObj.transform.localPosition = UI_Pos_Offset;
//        //m_CameraObj.transform.localRotation = Quaternion.Euler(UI_Rotation);
//    }

//    //解决某些手机关闭2D摄像机时候会导致3D摄像机不渲染
//    public void ClearCameraTexture()
//    {
//        this.m_MainCamera.targetTexture = null;
//    }

//    #endregion

//    #region private function
//    Vector3 vTransPosCache = Vector3.zero;
//    private void SetRunPos()
//    {
//        if (m_TargetCar != null)
//        {
//            vTransPosCache = m_TargetCar.position;
//            if(vTransPosCache.y < 0)
//            {
//                vTransPosCache.y = 0;
//            }
//            m_CameraObj.transform.position = vTransPosCache + m_runPos;
//        }
//    }

//    private void SetRunRotate()
//    {
//        m_CameraObj.transform.localRotation = Quaternion.Euler(Run_Rotation);
//    }
//    TweenerCore<Vector3, Vector3, VectorOptions> tweenAngles;
//    TweenerCore<Vector3, Vector3, VectorOptions> tweenRunPos;
//    private void PlayFinishTween()
//    {
//        if (isVictoryStart) return;
//        this.isVictoryStart = true;
//        if (tweenAngles != null)
//            tweenAngles.Kill();
//        if (tweenRunPos != null)
//            tweenRunPos.Kill();

//        tweenAngles = DOTween.To(() => m_CameraObj.transform.eulerAngles, v => m_CameraObj.transform.eulerAngles = v, this.Finish_Rotation, GameDefine.Victory_Ani_Duration).SetEase(Ease.OutQuad);
//        Vector3 targetRunPos = new Vector3(m_runPos.x - 10.7f, 4.4f, m_runPos.z - 6.73f);
//        tweenRunPos = DOTween.To(() => m_runPos, v => m_runPos = v, targetRunPos, GameDefine.Victory_Ani_Duration).SetEase(Ease.OutQuad);
//    }

//    private float GetRadian(float angle)
//    {
//        return angle / PIAngle * Mathf.PI;
//    }

//    //private float GetRadian(float angle)
//    //{
//    //    return angle / PIAngle * Mathf.PI;
//    //}

//    private void SetRunOffset()
//    {
//        float y = Run_Pos_Offset.y;// || 20;//高度y

//        float xAngle = Run_Rotation.x;// || 42;//x旋转角度
//        float yAngle = Run_Rotation.y;// || 235;//y旋转角度
//        float xRadian = GetRadian(xAngle);
//        float yRadian = GetRadian(yAngle);

//        float xz = y / Mathf.Tan(xRadian);
//        float x = xz * Mathf.Sin(yRadian);
//        float z = xz * Mathf.Cos(yRadian);

//        float dis = GameDefine.Race_Camera_Down_Offset;
//        float deltaX = dis * Mathf.Sin(yRadian);
//        float deltaZ = dis * Mathf.Cos(yRadian);

//        Run_Pos_Offset = new Vector3((float)(-x + deltaX), (float)y, (float)(-z + deltaZ));
//    }
    
//    public void SetSkyColor(Vector4 tarColor, bool isTween)
//    {
//        if (isTween)
//        {
//            if(this.skyColorDoTween != null) this.skyColorDoTween.Kill();
//            this.skyColorDoTween = DOTween.To(() => m_curSkyColor, v => m_curSkyColor = v, tarColor, GameDefine.Endless_Sky_Switch_Duration).SetEase(Ease.Linear).OnUpdate(this.DoSetSkyColor);
//        }
//        else
//        {
//            m_curSkyColor = tarColor;
//            this.DoSetSkyColor();
//        }
//    }

//    private void DoSetSkyColor()
//    {
//        m_OldCamera.backgroundColor = this.m_curSkyColor;
//    }

//    #endregion

//    public Vector4 CurSkyColor
//    {
//        get
//        {
//            return m_curSkyColor;
//        }
//    }
//}
