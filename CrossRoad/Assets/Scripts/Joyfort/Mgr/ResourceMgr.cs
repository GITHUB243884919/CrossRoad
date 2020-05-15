using Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceMgr
{
    //加载车
    public static GameObject LoadCar(string PrefabName)
    {
        int hash = PrefabName.GetHashCode();
        GameObject obj = null;
        if (!CarCache.TryGetValue(hash, out obj))
        {
            obj = Resources.Load<GameObject>(cfg_CarPath + PrefabName);
            CarCache[hash] = obj;
        }
        if (obj == null)
        {
            Debug.LogError("车不存在:" + PrefabName);
            return null;
        }
        return GameObject.Instantiate(obj);
    }
    //加载车
    public static GameObject LoadNewCar(string PrefabName)
    {
        int hash = PrefabName.GetHashCode();
        GameObject obj = null;
        if (!NewCarCache.TryGetValue(hash, out obj))
        {
            obj = Resources.Load<GameObject>(cfg_NewCarPath + PrefabName);
            NewCarCache[hash] = obj;
        }
        if (obj == null)
        {
            Debug.LogError("车不存在:" + PrefabName);
            return null;
        }
        return GameObject.Instantiate(obj);
    }
    /// <summary>
    /// 加载路组件
    /// </summary>
    /// <param name="PrefabName"></param>
    /// <returns></returns>
    public static GameObject LoadRoadComponent(string PrefabName)
    {
        int hash = PrefabName.GetHashCode();
        GameObject obj = null;
        if (!RoadCache.TryGetValue(hash, out obj))
        {
            obj = Resources.Load<GameObject>(cfg_RoadComponentPath + PrefabName);
            RoadCache[hash] = obj;
        }
        if (obj == null)
        {
            Debug.LogError("路组件不存在:" + PrefabName);
            return null;
        }
        return GameObject.Instantiate(obj);
    }
//    //加载路
//    public static GameObject LoadRoad(string PrefabName, bool withoutShadow = false)
//    {
//        int hash = PrefabName.GetHashCode();
//        GameObject obj = null;
//        if (!RoadCache.TryGetValue(hash, out obj))
//        {
//            obj = Resources.Load<GameObject>(cfg_RoadPath + PrefabName);
//            RoadCache[hash] = obj;
//        }
//        if (obj == null)
//        {
//            Debugger.LogError("路块不存在:" + PrefabName);
//            return null;
//        }
//        //var roadItem = GameObject.Instantiate(obj).AddMissingComponent<RoadItem>();
//        var roadItem = GameObject.Instantiate(obj);
//#if UNITY_IOS

//#else
//#endif
//        if (!withoutShadow)
//        {
//            roadItem.gameObject.AddMissingComponent<PlanShadowItem>();
//        }
//        roadItem.name = PrefabName;
//        // roadItem.SetType(PrefabName);
//        return roadItem;
//    }

    //加载特效
    public static GameObject LoadEffect(string PrefabName)
    {
        int hash = PrefabName.GetHashCode();
        GameObject obj = null;
        if (!EffectCache.TryGetValue(hash, out obj))
        {
            obj = Resources.Load<GameObject>(cfg_EffectPath + PrefabName);
            EffectCache[hash] = obj;
        }
        if (obj == null)
        {
            Debug.LogError("特效不存在:" + PrefabName);
            return null;
        }
        return GameObject.Instantiate(obj);
    }
    //加载纹理
    public static Texture LoadRoadTexture(string Name)
    {
        int hash = Name.GetHashCode();
        Texture tex = null;
        if (!TexCache.TryGetValue(hash, out tex))
        {
            tex = Resources.Load<Texture>(cfg_RoadTexPath + Name);
            TexCache[hash] = tex;
        }
        if (tex == null)
        {
            Debug.LogError("贴图不存在:" + Name);
            return null;
        }
        return tex;
    }
    //加载建筑
    public static GameObject LoadBuilding(string PrefabName)
    {
        int hash = PrefabName.GetHashCode();
        GameObject obj = null;
        if (!BuildingCache.TryGetValue(hash, out obj))
        {
            obj = Resources.Load<GameObject>(cfg_BuildingPath + PrefabName);
            BuildingCache[hash] = obj;
        }
        if (obj == null)
        {
            Debug.LogError("建筑不存在:" + PrefabName);
            return null;
        }
        return GameObject.Instantiate(obj);
    }
    //加载WayPoint相关
    public static GameObject LoadWayPoint(string PrefabName)
    {
        int hash = PrefabName.GetHashCode();
        GameObject obj = null;
        if (!EffectCache.TryGetValue(hash, out obj))
        {
            obj = Resources.Load<GameObject>(cfg_WayPointPath + PrefabName);
            EffectCache[hash] = obj;
        }
        if (obj == null)
        {
            Debug.LogError("路点不存在:" + PrefabName);
            return null;
        }
        return GameObject.Instantiate(obj);
    }
    public static Sprite LoadSprite(string path)
    {
        if ((Resources.Load(path, typeof(SpriteRenderer)) as SpriteRenderer) != null)
        {
            return (Resources.Load(path, typeof(SpriteRenderer)) as SpriteRenderer).sprite;
        }
        else
        {
            return null;
        }
    }
//    public static IEnumerator LoadAllRoadTexture(Action LoadCallBack)
//    {
//        List<SceneColorDesc> list = TableMgr.GetInstance().GetSceneColorDescList();

//        for (int i = 0; i < list.Count; i++)
//        {
//            string PrefabName = list[i].texture;
//            int hash = PrefabName.GetHashCode();
//            Texture obj = null;
//            if (!TexCache.TryGetValue(hash, out obj))
//            {
//                obj = Resources.Load<Texture>(cfg_RoadTexPath + PrefabName);
//                if (obj != null)
//                {
//                    TexCache[hash] = obj;
//                }
//#if UNITY_EDITOR
//                yield return null;
//#else
//                yield return new WaitForSeconds(0.1f);
//#endif
//                if (LoadCallBack != null)
//                {
//                    LoadCallBack.Invoke();
//                }
//            }
//            if (obj == null)
//            {
//                Debugger.LogError("贴图不存在:" + PrefabName);
//            }
//        }

//    }
//    public static IEnumerator LoadAllEffect(Action LoadCallBack)
//    {
//        var list = TableMgr.GetInstance().GeEffectDescList();

//        for (int i = 0; i < list.Count; i++)
//        {
//            string PrefabName = list[i].Name;
//            int hash = PrefabName.GetHashCode();
//            GameObject obj = null;
//            if (!EffectCache.TryGetValue(hash, out obj))
//            {
//                obj = Resources.Load<GameObject>(cfg_EffectPath + PrefabName);
//                EffectCache[hash] = obj;
//#if UNITY_EDITOR
//                yield return null;
//#else
//                yield return new WaitForSeconds(0.1f);
//#endif
//                if (LoadCallBack != null)
//                {
//                    LoadCallBack.Invoke();
//                }
//            }
//            if (obj == null)
//            {
//                Debugger.LogError("特效不存在:" + PrefabName);
//            }
//        }

//    }

    //加载场景物体
    public static GameObject LoadSceneObj(string PrefabName)
    {
        int hash = PrefabName.GetHashCode();
        GameObject obj = null;
        if (!SceneObjCache.TryGetValue(hash, out obj))
        {
            obj = Resources.Load<GameObject>(cfg_SceneObjPath + PrefabName);
            SceneObjCache[hash] = obj;
        }
        if (obj == null)
        {
            Debug.LogError("场景物品不存在:" + PrefabName);
            return null;
        }
        return GameObject.Instantiate(obj);//.AddMissingComponent<RoadItem>();
    }
    public static GameObject LoadCarHeadInfo(string prefabName)
    {
        int hash = prefabName.GetHashCode();
        GameObject obj = null;
        if (!CarHeadInfoCache.TryGetValue(hash, out obj))
        {
            obj = Resources.Load<GameObject>(cfg_CarHeadInfoPath + prefabName);
            CarHeadInfoCache[hash] = obj;
        }
        if (obj == null)
        {
            Debug.LogError("场景物品不存在:" + prefabName);
            return null;
        }
        return GameObject.Instantiate(obj);
    }
    public static GameObject LoadCarCharacter(string prefabName)
    {
        int hash = prefabName.GetHashCode();
        GameObject obj = null;
        if (!CarCharacterPathCache.TryGetValue(hash, out obj))
        {
            obj = Resources.Load<GameObject>(cfg_CarCharacterPath + prefabName);
            CarCharacterPathCache[hash] = obj;
        }
        if (obj == null)
        {
            Debug.LogError("场景物品不存在:" + prefabName);
            return null;
        }
        return GameObject.Instantiate(obj);
    }

    //加载关卡预制件
    public static GameObject LoadLevelPrefab(string prefabName)
    {
        int hash = prefabName.GetHashCode();
        GameObject obj = null;
        if (!LevelPrefabCache.TryGetValue(hash, out obj))
        {
            obj = Resources.Load<GameObject>(cfg_LevelPrefabPath + prefabName);
            LevelPrefabCache[hash] = obj;
        }
        if (obj == null)
        {
            Debug.LogError("关卡预制件不存在:" + prefabName);
            return null;
        }
        return GameObject.Instantiate(obj);
    }
    #region
    const string cfg_CarPath = "Prefab/Car/";
    const string cfg_NewCarPath = "Prefab/NewCar/";
    const string cfg_RoadPath = "Prefab/Road/";
    const string cfg_RoadComponentPath = "Prefab/RoadComponent/";
    const string cfg_EffectPath = "Prefab/Effect/";
    const string cfg_RoadTexPath = "Texture/Road/";
    const string cfg_WayPointPath = "Prefab/WayPoint/";
    const string cfg_SceneObjPath = "Prefab/Other/";
    const string cfg_CarHeadInfoPath = "Prefab/CarHeadInfo/";
    const string cfg_CarCharacterPath = "Prefab/Character/";
    const string cfg_BuildingPath = "Prefab/Other/Building/";
    const string cfg_LevelPrefabPath = "Prefab/Level/";

    static Dictionary<int, GameObject> CarCache = new Dictionary<int, GameObject>();
    static Dictionary<int, GameObject> NewCarCache = new Dictionary<int, GameObject>();
    static Dictionary<int, GameObject> RoadCache = new Dictionary<int, GameObject>();
    static Dictionary<int, GameObject> EffectCache = new Dictionary<int, GameObject>();
    static Dictionary<int, Texture> TexCache = new Dictionary<int, Texture>();
    static Dictionary<int, GameObject> SceneObjCache = new Dictionary<int, GameObject>();
    static Dictionary<int, GameObject> CarHeadInfoCache = new Dictionary<int, GameObject>();
    static Dictionary<int, GameObject> CarCharacterPathCache = new Dictionary<int, GameObject>();
    static Dictionary<int, GameObject> BuildingCache = new Dictionary<int, GameObject>();
    static Dictionary<int, GameObject> LevelPrefabCache = new Dictionary<int, GameObject>();
    #endregion
}
