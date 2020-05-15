using Game;
using Game.MessageCenter;
using UFrame.Logger;
using System;
using UFrame;
using UnityEngine;
using Game.GlobalData;

public class LoadingMgr : MonoBehaviour
{
    private static LoadingMgr _inst;
    public static LoadingMgr Inst
    {
        get
        {
            return _inst;
        }
    }
    private GameObject _rootObject;
    public GameObject RootObject
    {
        get
        {
            return this._rootObject;
        }
    }

    /// <summary>
    /// 是否用用户数据
    /// 发布时候值为true
    /// 如果设置成false每次都是新的用户数据，相当于是新号
    /// </summary>
    public bool isUsedData;

    /// <summary>
    /// 异常是否退出
    /// </summary>
    public bool isExceptionQuit;

    public bool isShowException;

    public SystemLanguage language = SystemLanguage.Chinese;

    public bool isUsedlanguage = true;
    /// <summary>
    /// 场景加载类型
    /// </summary>
    public RunTimeLoaderType runTimeLoaderType = RunTimeLoaderType.Game;


    /// <summary>
    /// runTimeLoaderType为编辑器模式下，额外加载的地块数
    /// </summary>
    public int ExtendLoadGroupNum = 1;


    public bool debugCamera = false;


    long loginTicks = 0;
    [HideInInspector]
    public bool isRunning = false;

    private void Awake()
    {
        _inst = this;
        //检测是否降低分辨率
        // this.CheckToResetScreenSolution();
        //DeviceInfo.Init();

        Application.lowMemory += this.OnMemoryWarnning;
        Application.logMessageReceived += this.OnLogCallback;
        //帧率
#if UNITY_EDITOR
        Application.targetFrameRate = 30;
        string strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        Debug.LogErrorFormat("{0} 开始运行 模式={1}", strTime, runTimeLoaderType);
#else
        Application.targetFrameRate = 60;
#endif
        //禁止休眠
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //后台运行
#if UNITY_EDITOR
        Application.runInBackground = true;
#else
        Application.runInBackground = false;
#endif

        this._rootObject = gameObject;

        loginTicks = DateTime.Now.Ticks;

        InitPlayerData();

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        //InitThridPart();
        //PageMgr.ShowPage<UILevel>();
        CrossRoadGame.CrossRoadStageManager.GetInstance().Init();
        CrossRoadGame.CrossRoadStageManager.GetInstance().Load(1);
    }

    protected void InitPlayerData()
    {
        if (isUsedData)
        {
            return;
        }

        LogWarp.Log("DeleteKey");
        PlayerPrefs.DeleteKey("PlayerData");
    }

    void OnApplicationQuit()
    {
        //注意！！！！禁止在这里添加任何代码！！！
        string strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        Debug.LogErrorFormat("{0} 退出 ", strTime);
        //if (GlobalDataManager.GetInstance().playerData.GetOfflineSecond() > 0)
        {
            //GlobalDataManager.GetInstance().playerData.Logout();
        }
        PlayerData.Save(GlobalDataManager.GetInstance().playerData);
    }

    void OnApplicationPause(bool pause)
    {
        //注意！！！！禁止在这里添加任何代码！！！
        if (pause && isRunning)
        {
            //暂停更新离线时间
            //if (GlobalDataManager.GetInstance().playerData.GetOfflineSecond() > 0)
            {
                //GlobalDataManager.GetInstance().playerData.Logout();
            }
            PlayerData.Save(GlobalDataManager.GetInstance().playerData);

            return;
        }

        //通知计算离线
        //MessageManager.GetInstance().Send((int)GameMessageDefine.CalcOffline);
    }

    void OnApplicationFocus(bool focus)
    {
        //注意！！！！禁止在这里添加任何代码！！！
        if (!focus && isRunning)
        {
            //失去焦点更新离线时间
            //if (GlobalDataManager.GetInstance().playerData.GetOfflineSecond() > 0)
            {
                //GlobalDataManager.GetInstance().playerData.Logout();
            }
            PlayerData.Save(GlobalDataManager.GetInstance().playerData);

            return;
        }

        //通知计算离线
        //MessageManager.GetInstance().Send((int)GameMessageDefine.CalcOffline);
    }

    public void OnMemoryWarnning()
    {
        //注意！！！！禁止在这里添加任何代码！！！
#if UNITY_EDITOR
        string e = "触发了 OnMemoryWarnning!!!";
        throw new System.Exception(e);
#else
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
#endif
    }

    public void OnLogCallback(string condition, string stackTrace, LogType type)
    {
        //注意！！！！禁止在这里添加任何代码！！！
        if (!isShowException)
        {
            return;
        }

        if (type == LogType.Exception)
        {
            string uiException = condition + "\n\n";
            uiException += stackTrace;
            PageMgr.ShowPage<UIShowException>(uiException);
#if UNITY_EDITOR
            //UnityEditor.EditorApplication.isPlaying = false;
            UnityEditor.EditorApplication.isPaused = true;
            string strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff");
            Debug.LogErrorFormat("{0} 游戏异常中断", strTime);
#else
            if (isExceptionQuit)
            {
                Application.Quit();
            }
#endif
        }
    }

    //void InitThridPart()
    //{
    //    //广告初始化
    //    if (Application.platform == RuntimePlatform.Android)
    //    {
    //        this.InitAds();
    //    }

    //    //AppsFlyer(APS)初始化
    //    AppsFlyerEvent.GetInstance().Init();

    //    //ThinkingAnalytics(TA)初始化
    //    //ThirdPartTA.Instance.Init();
    //}

    //public void InitAds()
    //{
    //    LogWarp.Log("InitAds...");

    //    // added by bruke 20190819
    //    //if (GameDefine.Only_Support_MopubAds)
    //    {
    //        LogWarp.Log("加载Mopub广告");
    //        ThirdPartAds.Init(this.transform);
    //        return;
    //    }

    //    //if (GameDefine.Only_Support_TTAds)
    //    //{
    //    //    Debug.Log("加载穿山甲广告");
    //    //    ThirdPartTTAds.Init(this.transform);
    //    //    return;
    //    //}
    //    // end bruke

    //    // MoPub
    //    if (Application.platform == RuntimePlatform.IPhonePlayer)
    //    {
    //        //ThirdPartAds.Init(this.transform);

    //        //if (Tools.StorageTool.GetLocalAreaType() == LocalAreaType.China)
    //        //{
    //        //    // 穿山甲
    //        //    Debug.Log("加载国内广告");
    //        //    ThirdPartTTAds.Init(this.transform);
    //        //    //                GameDefine.EnableDiamondShop = false;
    //        //}
    //        //else
    //        {
    //            // Mopub
    //            //Debug.Log("加载国外广告");
    //            ThirdPartAds.Init(this.transform);
    //            //                GameDefine.EnableDiamondShop = true;
    //        }
    //    }
    //    else if (Application.platform == RuntimePlatform.Android)
    //    {
    //        //#if MOPUB_ANDROID
    //        //ThirdPartAds.Init(this.transform);

    //        //if (Tools.StorageTool.GetLocalAreaType() == LocalAreaType.China)
    //        //{
    //        //    // 穿山甲
    //        //    Debug.Log("加载国内广告");
    //        //    ThirdPartTTAds.Init(this.transform);
    //        //}
    //        //else
    //        {
    //            // Mopub
    //            //Debug.Log("加载国外广告");
    //            ThirdPartAds.Init(this.transform);
    //        }
    //        //#endif
    //    }

    //}

}
