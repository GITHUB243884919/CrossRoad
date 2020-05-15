/*******************************************************************
* FileName:     PoolManager.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-7
* Description:  
* other:    
********************************************************************/


using System.Collections;
using System.Collections.Generic;
using UFrame;
using UFrame.Common;
using UFrame.EntityFloat;
using UnityEngine;
using Game;
using Config;
using UFrame.MiniGame;

namespace Game.MiniGame
{
    public class PoolManager : Singleton<PoolManager>, ISingleton
    {
        /// <summary>
        /// GameObject类型的pool，key = entityResType
        /// </summary>
        Dictionary<int, GameObjectPool> gameObjectPools =
            new Dictionary<int, GameObjectPool>();

        /// <summary>
        /// 如有预加载需求，预先把从资源管理器中把需要做成pool的对象都pool化
        /// AddGameObjectPool(.....)
        /// </summary>
        public void Init()
        {
            
        }

        public void Release()
        {
            foreach(var val in gameObjectPools.Values)
            {
                var pool = val;
                pool.RecoveryAll();
            }

            gameObjectPools.Clear();
        }

        /// <summary>
        /// 根据策划资源表的主键加载到POOL
        /// </summary>
        /// <param name="entityResType">对应资源表主键</param>
        /// <returns></returns>
        public GameObjectPool GetGameObjectPool(int entityResType)
        {
            GameObjectPool pool = null;

            if (!gameObjectPools.TryGetValue(entityResType, out pool))
            {
                var cell = Config.resourceConfig.getInstace().getCell(entityResType);
                if (null == cell)
                {
                    string e = string.Format("资源表没有这个资源 {0}", entityResType);
                    throw new System.Exception(e);
                }

                ResLoadType eResLoadType = (ResLoadType)cell.loadtype;
                switch (eResLoadType)
                {
                    case ResLoadType.OnlyPrefab:
                        return this.LoadOnlyPrefab(cell, entityResType);
                    case ResLoadType.PrefabAndTexture:
                        return this.PrefabAndTexture(cell, entityResType);
                    default:
                        string e = string.Format("没有这种加载类型{0}", cell.loadtype);
                        throw new System.Exception(e);
                        
                }

            }

            return pool;
        }

        public GameObjectPool LoadOnlyPrefab(resourceCell cell, int entityResType)
        {
            GameObjectPool pool = null;
            pool = AddGameObjectPool(cell.prefabpath, entityResType);
            return pool;
        }


        public GameObjectPool PrefabAndTexture(resourceCell cell, int entityResType)
        {
            GameObjectPool pool = null;
            pool = AddGameObjectAndTexturePool(cell, entityResType);
            return pool;
        }


        public GameObjectPool IsBelongGameObjectPool(int entityResType)
        {
            GameObjectPool pool = null;
            gameObjectPools.TryGetValue(entityResType, out pool);
            return pool;
        }

        protected GameObjectPool AddGameObjectPool(string path, int entityResType)
        {
            var go = ResourceManager.GetInstance().LoadGameObject(path);
            go.transform.position = Const.Invisible_Postion;
            var pool = new GameObjectPool();
            pool.SetSeed(go);
            gameObjectPools.Add(entityResType, pool);
            return pool;
        }

        protected GameObjectPool AddGameObjectAndTexturePool(resourceCell cell, int entityResType)
        {
            var go = ResourceManager.GetInstance().LoadGameObject(cell.prefabpath);
            go.transform.position = Const.Invisible_Postion;
            var pool = new GameObjectPool();     
            pool.SetSeed(go);

            var texture = ResourceManager.GetInstance().LoadObject<Texture>(cell.texturepath);
            var smr = go.GetComponentInChildren<SkinnedMeshRenderer>();
            smr.material.SetTexture("_MainTex", texture);
            gameObjectPools.Add(entityResType, pool);
            return pool;
        }
    }
}
