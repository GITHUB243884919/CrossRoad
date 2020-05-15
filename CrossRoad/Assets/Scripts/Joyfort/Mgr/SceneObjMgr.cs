//using Game;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//public class SceneObjMgr
//{

//    const string cfg_CoinEffectName = "jinbi_eat";
//    const string cfg_DiamondEffectName = "zuanshi_eat";
//    const string cfg_ADCoinEffectName = "kapai_eat";
//    List<SceneObjCoin> m_List = new List<SceneObjCoin>();
//    static SceneObjMgr g_Instance = null;
//    List<SceneObjEffect> m_CoinEffect = new List<SceneObjEffect>();
//    List<SceneObjEffect> m_DiamondEffect = new List<SceneObjEffect>();
//    List<SceneObjEffect> m_ADCoinEffect = new List<SceneObjEffect>();
//    const int cfg_EffectCacheCount = 20;
//    const int cfg_SceneObjCache = 2;
//    GameObject m_SceneObjRoot = null;
//    GameObject m_SceneObjEffectRoot = null;
//    Vector3 vCachePos = new Vector3(9999f, 9999f, 9999f);
//    public static SceneObjMgr GetInstance()
//    {
//        if (g_Instance == null)
//        {
//            g_Instance = new SceneObjMgr();
//            g_Instance.Init();
//        }
//        return g_Instance;
//    }
//    public void Init()
//    {
//        m_List.Clear();
//        if (m_SceneObjRoot == null)
//        {
//            m_SceneObjRoot = new GameObject("SceneObj");
//            m_SceneObjRoot.transform.position = Vector3.zero;
//            m_SceneObjRoot.transform.eulerAngles = Vector3.zero;
//        }
//        if (Application.isPlaying)
//        {
//            if (m_SceneObjEffectRoot == null)
//            {
//                m_SceneObjEffectRoot = new GameObject("SceneObjEffect");
//                m_SceneObjEffectRoot.transform.position = Vector3.zero;
//                m_SceneObjEffectRoot.transform.eulerAngles = Vector3.zero;
//            }

//            InitCache();
//        }

//    }
//    public void CreateCoin(int id, Vector3 vStartPos, bool Run = false, bool IsRevive = false, int style = -1, SceneObjType[] ignoreType = null)
//    {
//        GridGroupDesc grid = TableMgr.GetInstance().GetGridGroupDesc(id);
//        if (grid.SceneObjIDs != null && grid.SceneObjIDs.Count > 0)
//        {
//            foreach (var itemIn in grid.SceneObjIDs)
//            {
//                if (itemIn <= 0)
//                {
//                    continue;
//                }
//                SceneObjDesc desc = TableMgr.GetInstance().GetSceneObjDesc(itemIn);
//                if (ignoreType != null)
//                {
//                    if (ignoreType.Contains(desc.Type))
//                    {
//                        continue;
//                    }
//                }
//                if (desc.Type == SceneObjType.Building && desc.Param != style)
//                {
//                    continue;
//                }
//                if (IsRevive)
//                {
//                    if (desc.Type == SceneObjType.Coin)
//                    {
//                        desc = TableMgr.GetInstance().GetSceneObjDesc(coinArray[Random.Range(0, coinArray.Length)]);
//                    }
//                    else if (desc.Type == SceneObjType.Diamond)
//                    {
//                        continue;
//                    }
//                }
//                //if(desc.Type != 3)
//                //{
//                //    continue;
//                //}
//                CreateSceneObj(desc, vStartPos, Run);
//            }

//        }

//    }
//    public void Pause(bool pause)
//    {
//        foreach (var item in m_List)
//        {
//            if (pause)
//            {
//                if (item.IsWorking)
//                {
//                    item.SetState(SceneObjState.Pause);
//                }
//            }
//            else
//            {
//                if (item.IsPause)
//                {
//                    item.SetState(SceneObjState.Working);
//                }
//            }

//        }
//    }
//    public void Start()
//    {
//        foreach (var item in m_List)
//        {
//            item.StartWorking();
//        }
//    }
//    public void Finish()
//    {
//        foreach (var item in m_List)
//        {
//            item.SetState(SceneObjState.Invail);
//        }
//    }
//    public SceneObjEffect CreateEffect(SceneObjType Type, Vector3 vPos, float DelayDestroyTime = 1f)
//    {
//        SceneObjEffect obj = FindFreeEffect(Type);
//        if (obj == null)
//        {
//            obj = CreateEffectFromFile(Type);
//            switch (Type)
//            {
//                case SceneObjType.Coin:
//                    m_CoinEffect.Add(obj);
//                    break;
//                case SceneObjType.Diamond:
//                    m_DiamondEffect.Add(obj);
//                    break;
//            }
//        }
//        if (obj != null)
//        {
//            if (obj.gameObject.activeSelf)
//            {
//                obj.gameObject.SetActive(false);
//            }
//            obj.gameObject.SetActive(true);
//            obj.transform.position = vPos;
//            obj.StartDelayDestroy(DelayDestroyTime);
//        }

//        return obj;
//    }
//    int[] coinArray = new int[] { 7, 9, 15 };
//    void CreateSceneObj(SceneObjDesc desc, Vector3 vStartPos, bool Run = false)
//    {
//        if (desc.ID <= 0)
//        {
//            return;
//        }
//        var item = FindFree(desc.ID);
//        if (item == null)
//        {
//            item = SceneObjCoin.Create(desc, m_SceneObjRoot.transform, vStartPos);
//            m_List.Add(item);
//        }
//        else
//        {
//            item.Init(desc, vStartPos);
//        }
//        if (Run || desc.Type == SceneObjType.Building)
//        {
//            item.SetState(SceneObjState.Prepare);
//        }
//    }
//    SceneObjCoin FindFree(int id)
//    {
//        return m_List.Find(p => p.IsFree && p.Desc.ID == id);
//    }
//    SceneObjEffect FindFreeEffect(SceneObjType type)
//    {
//        List<SceneObjEffect> list = null;
//        switch (type)
//        {
//            case SceneObjType.Coin:
//                list = m_CoinEffect;
//                break;
//            case SceneObjType.Diamond:
//                list = m_DiamondEffect;
//                break;
//            case SceneObjType.Special:
//                list = m_ADCoinEffect;
//                break;
//        }
//        if (list != null)
//        {
//            return list.Find(p => p.isFree);
//        }
//        return null;
//    }
//    void InitCache()
//    {
//        SceneObjEffect item = null;
//        m_CoinEffect.Clear();
//        m_DiamondEffect.Clear();
//        m_ADCoinEffect.Clear();
//        for (int i = 0; i < cfg_EffectCacheCount; ++i)
//        {
//            item = CreateEffectFromFile(0);
//            m_CoinEffect.Add(item);
//            item.gameObject.SetActive(true);
//            item.transform.position = vCachePos;
//            if (i < cfg_EffectCacheCount * 0.5f)
//            {
//                item = CreateEffectFromFile(SceneObjType.Diamond);
//                m_DiamondEffect.Add(item);
//                item.gameObject.SetActive(true);
//                item.transform.position = vCachePos;
//                item = CreateEffectFromFile(SceneObjType.Special);
//                m_ADCoinEffect.Add(item);
//                item.gameObject.SetActive(true);
//                item.transform.position = vCachePos;
//            }
//        }
//        var list = TableMgr.GetInstance().GetSceneObjDescList();
//        if (list != null && list.Count > 0)
//        {
//            foreach (var listin in list)
//            {
//                int i = 0;
//                while (i < cfg_SceneObjCache)
//                {
//                    if (listin.Type == SceneObjType.Building && i > 0)
//                    {
//                        ++i;
//                        continue;
//                    }
//                    var Cache = SceneObjCoin.Create(listin, m_SceneObjRoot.transform, vCachePos);
//                    Cache.SetState(SceneObjState.Invail);
//                    m_List.Add(Cache);
//                    ++i;
//                }
//            }
//        }
//        SceneObjFly.LoadCache(m_SceneObjEffectRoot.transform);
//    }
//    SceneObjEffect CreateEffectFromFile(SceneObjType type)
//    {
//        GameObject obj = null;
//        switch (type)
//        {
//            case SceneObjType.Coin:
//                obj = ResourceMgr.LoadEffect(cfg_CoinEffectName);
//                break;
//            case SceneObjType.Diamond:
//                obj = ResourceMgr.LoadEffect(cfg_DiamondEffectName);
//                break;
//            case SceneObjType.Special:
//                obj = ResourceMgr.LoadEffect(cfg_ADCoinEffectName);
//                break;
//        }
//        if (obj != null)
//        {
//            obj.transform.SetParent(m_SceneObjEffectRoot.transform);
//            return obj.AddMissingComponent<SceneObjEffect>();
//        }
//        return null;
//    }

//    public List<SceneObjCoin> CurSceneObjs
//    {
//        get
//        {
//            return m_List;
//        }
//    }




//    static PlayerDataCache m_PlayerCache;
//    public static void AddCoin(decimal coin)
//    {
//        m_PlayerCache.Coin += coin;
//    }
//    public static void AddDiamond(int Diamond)
//    {
//        m_PlayerCache.Diamond += Diamond;
//    }
//    public static void AddADCoin(int coin)
//    {
//        m_PlayerCache.AdCoin += coin;
//    }
//    public static void FlushPlayerCache()
//    {
//        m_PlayerCache.Flush();
//    }
//    public static PlayerDataCache PlayerEarningCache
//    {
//        get
//        {
//            return m_PlayerCache;
//        }
//    }

//    #region SceneObjCreator
//    const string cfg_MainCameraName = "Camera";
//    const string cfg_LightBaseName = "Light";
//    public const string cfg_SceneBottomName = "SceneBottom";
//    const string cfg_BuildingPath = "Prefab/Other/Building/";
//    public static GameObject LoadBuilding(int level, bool isAI, Transform mapTrans = null, bool LoadFromCache = true)
//    {
//        GameObject descObj = null;
//        var desc = TableMgr.GetInstance().GetBuidingByLevel(level, isAI);
//        Vector3 vPos = Vector3.zero;
//        Vector3 vRotation = Vector3.zero;
//        if (desc.ID > 0)
//        {
//            if (LoadFromCache)
//            {
//                descObj = ResourceMgr.LoadBuilding(desc.PrefabName);
//            }
//            else
//            {
//                descObj = Resources.Load<GameObject>(cfg_BuildingPath + desc.PrefabName);
//                if (descObj != null)
//                {
//                    descObj = GameObject.Instantiate(descObj);
//                }
//            }
//            if (mapTrans != null)
//            {
//                vPos = mapTrans.TransformPoint(desc.Pos);
//                vRotation = mapTrans.transform.eulerAngles + vRotation;
//            }
//        }
//        if (descObj != null)
//        {
//            descObj.transform.position = vPos;
//            descObj.transform.eulerAngles = vRotation;
//            var obj = descObj.transform.Find(cfg_SceneBottomName);
//            if (obj != null)
//            {
//                if (Application.isPlaying)
//                {
//                    obj.GetComponent<MeshRenderer>().material.color = desc.BottomColor;
//                }
//                else
//                {
//                    obj.GetComponent<MeshRenderer>().sharedMaterial.color = desc.BottomColor;
//                }
//            }
//            descObj.AddMissingComponent<PlanShadowItem>();
//        }
//        return descObj;
//    }
//    public static void LoadWeather(int weatherID)
//    {

//    }
//    public static BottomTrail LoadBottomTrail(Transform followTrans, Transform DropTrans,float followKeepTime, float DropKeepTime)
//    {
//        return BottomTrail.Create(followTrans, DropTrans, followKeepTime,DropKeepTime);
//    }
//    public static SceneObjFly LoadSceneObjFly(Vector3 vStartPos, Transform targetTrans, SceneObjType eEffectType, float flySpeed)
//    {
//        return SceneObjFly.Create(vStartPos, targetTrans, eEffectType, flySpeed);
//    }
//    #endregion
//}
//public struct PlayerDataCache
//{
//    public decimal Coin;
//    public int Diamond;
//    public int AdCoin;

//    public void Flush()
//    {

//        Clear();
//    }
//    void Clear()
//    {
//        Coin = 0;
//        Diamond = 0;
//        AdCoin = 0;
//    }
//}
//public class SceneObjEffect : MonoBehaviour
//{
//    float m_fDelayTime = 0;
//    public void StartDelayDestroy(float fDelayTime)
//    {
//        m_fDelayTime = fDelayTime;
//    }
//    private void Update()
//    {
//        if (m_fDelayTime > 0)
//        {
//            m_fDelayTime -= Time.deltaTime;
//            if (m_fDelayTime < 0)
//            {
//                gameObject.SetActive(false);
//            }
//        }
//    }
//    public bool isFree
//    {
//        get
//        {
//            return m_fDelayTime <= 0;
//        }
//    }
//}