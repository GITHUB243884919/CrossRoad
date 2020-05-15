using System;
using UFrame;
using UnityEngine;

namespace CrossRoadGame
{
    public enum CrossRoadGameState
    {
        /// <summary>
        /// 加载原始场景
        /// </summary>
        LoadOrgScene,

        /// <summary>
        /// 按需加载场景物件
        /// </summary>
        LoadSceneObject,

        /// <summary>
        /// 加载动物栏动物
        /// </summary>
        LoadAnimal,
    }

    public class FSMCrossRoadGame : FSMMachine
    {

        public string loadingPageName = "";

        public FSMCrossRoadGame()
        {
        }

        public override void Release()
        {
            base.Release();
        }

        public void SetLoadingPageSlider()
        {
            (PageMgr.allPages[loadingPageName] as UICrossRoadLoading).SliderValueLoading(
                1f / numState);
        }

        /// <summary>
        /// 把动物按StandardAnimalBox缩放，以z轴为准
        /// 策划在场景中定义StandardAnimalBox，每个动物都这么大
        /// 每个动物增加一个名为Collider的节点，上面附加一个boxCollider，根据这个确定动物的原始大小
        /// </summary>
        public static GameObject Scale_Z(GameObject go, float z)
        {
            Vector3 goOrgSize = go.GetComponentInChildren<BoxCollider>().size;

            float s = z / goOrgSize.z;
            go.transform.localScale *= s;

            return go;
        }

        public static float GetSize_Z(GameObject go)
        {
            return go.GetComponent<BoxCollider>().size.z;
        }

    }
}

