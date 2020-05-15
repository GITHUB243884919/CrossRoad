using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour {
    private static SceneMgr _inst;
    public static SceneMgr Inst {
        get {
            if (_inst == null) {
                _inst = LoadingMgr.Inst.gameObject.AddComponent<SceneMgr>();
            }
            return _inst;
        }
    }

    private void Awake()
    {
        _inst = this;
    }

    private void OnDestroy()
    {
        _inst = null;
    }


    public void LoadSceneAsync(string sceneName,Action callback,Action<float> progress = null) {
        StartCoroutine(ILoadSceneAsync(sceneName, callback, progress));        
    }

    private IEnumerator ILoadSceneAsync(string sceneName, Action callback, Action<float> progress = null) {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        float progressValue = 0f;
        while (!async.isDone) {
            if (progress!= null) {
                progressValue = async.progress;
                progress.Invoke(progressValue);
            }            
            yield return null;
        }

        callback?.Invoke();
    }

    public string CurAcitveSceneName {
        get {
            return SceneManager.GetActiveScene().name;
        }
    }

    

    public void RemoveScene(string sceneNmae) {
        SceneManager.UnloadSceneAsync(sceneNmae);
        //StartCoroutine(CoUnloadSceneAsync(sceneNmae));
    }

    //protected IEnumerator CoUnloadSceneAsync(string sceneName)
    //{
    //    var scene = SceneManager.GetActiveScene();
    //    AsyncOperation async = SceneManager.UnloadSceneAsync(scene);
    //    //AsyncOperation async = SceneManager.UnloadSceneAsync(sceneName);
    //    while (!async.isDone)
    //    {
    //        Debug.LogErrorFormat("unloading {0}", async.progress);
    //        yield return async;
    //    }
    //    Debug.LogErrorFormat("unloaded {0}", async.progress);
    //}
}
