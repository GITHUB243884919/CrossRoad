/*******************************************************************
* FileName:     ResourceManager.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-7
* Description:  
* other:    
********************************************************************/

using System.Collections;
using System.Collections.Generic;
using UFrame.Common;
using UnityEngine;
using UnityEngine.Networking;

namespace UFrame.MiniGame
{
    public class ResourceManager : Singleton<ResourceManager>, ISingleton
    {
        Dictionary<int, GameObject> goCache = new Dictionary<int, GameObject>();
        Dictionary<int, Object> objCache = new Dictionary<int, Object>();
        HashSet<int> asyncRequests = new HashSet<int>();

        /// <summary>
        /// 支持将图片放到prefab上的图集处理方案的图片加载接口
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Sprite LoadSpriteFromPrefab(string path)
        {
            Object obj = Resources.Load(path, typeof(SpriteRenderer));
            if (obj != null)
            {
                return (obj as SpriteRenderer).sprite;
            }

            string e = string.Format("资源异常{0},请确认该资源是否存在?", path);
            throw new System.Exception(e);
        }

        public static AudioClip LoadAudioClipFromPrefab(string path)
        {
            Object obj = Resources.Load(path, typeof(AudioSource));
            if (obj != null)
            {
                return (obj as AudioSource).clip;
            }

            string e = string.Format("资源异常{0},请确认该资源是否存在?", path);
            throw new System.Exception(e);
        }

        public void Init() {}

        public void Release()
        {
            goCache.Clear();
            foreach(var kv in objCache)
            {
                Resources.UnloadAsset(kv.Value);
            }
            objCache.Clear();
        }

        public GameObject LoadGameObject(string path)
        {
            int pathHash = path.GetHashCode();
            GameObject go = null;
            if (!goCache.TryGetValue(pathHash, out go))
            {
                go = Resources.Load<GameObject>(path);
                goCache.Add(pathHash, go);
            }

            if (go == null)
            {
                string e = string.Format("资源异常{0},请确认该资源是否存在?", path);
                throw new System.Exception(e);
            }
            return GameObject.Instantiate<GameObject>(go);
        }

        public T LoadObject<T>(string path) where T : Object
        {
            int pathHash = path.GetHashCode();
            Object obj = null;
            if (!objCache.TryGetValue(pathHash, out obj))
            {
                T t = Resources.Load<T>(path);
                objCache.Add(pathHash, t as Object);
                return t;
            }

            return obj as T;
        }

        public void LoadObjectAsync(string path, System.Action<Object> callback)
        {
            int pathHash = path.GetHashCode();

            Object obj = null;

            //没有缓存，没有异步加载
            if (!objCache.TryGetValue(pathHash, out obj) && !asyncRequests.Contains(pathHash))
            {
                RunCoroutine.Run(CoLoadObjectAsync(path, pathHash, callback));
                return;
            }

            //没有缓存，但有异步加载
            if (!objCache.TryGetValue(pathHash, out obj) && asyncRequests.Contains(pathHash))
            {
                RunCoroutine.Run(CoWaitAsync(pathHash, callback));
                return;
            }

            callback?.Invoke(obj);
        }

        protected IEnumerator CoLoadObjectAsync(string path, int pathHash, System.Action<Object> callback)
        {
            var request = Resources.LoadAsync(path);
            while (!request.isDone)
            {
                yield return request;
            }

            objCache.Add(pathHash, request.asset);
            callback?.Invoke(request.asset);
        }

        protected IEnumerator CoWaitAsync(int pathHash, System.Action<Object> callback)
        {
            Object obj = null;
            while(true)
            {
                if (!objCache.TryGetValue(pathHash, out obj))
                {
                    yield return RunCoroutine.WaitForEndOfFrame;
                }

                break;
            }

            callback?.Invoke(obj);
        }

    }
}

