using CrossRoadGame;
using Game.MessageCenter;
using System.Collections;
using System.Collections.Generic;
using UFrame;
using UnityEngine;

public class AnimalCollisionEnterHelp : MonoBehaviour
{

    /// <summary>
    /// 行走动物播放时间  
    /// </summary>
    bool isCollision {
        get {
            return CrossRoadStageManager.GetInstance().isCollision;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Car" && isCollision)
        {
            MessageManager.GetInstance().Send((int)GameMessageDefine.CrossRoadGameFailure);
        }
    }
  
}
