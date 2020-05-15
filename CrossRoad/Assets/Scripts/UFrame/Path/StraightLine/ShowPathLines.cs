/*******************************************************************
* FileName:     ShowPathLines.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-6
* Description:  
* other:    
********************************************************************/


using System.Collections.Generic;
using UnityEngine;

namespace UFrame.Path.StraightLine
{
    [ExecuteInEditMode]
    public class ShowPathLines : MonoBehaviour
    {
        public bool isShowPath = true;

        public void DrawPathLineRender()
        {
            ////防止路径根节点位置被修改
            ProtecteRoot();

            for (int i = 0; i < transform.childCount; i++)
            {
                var path = transform.GetChild(i);

                var lr = path.GetComponent<LineRenderer>();
                var pathPosList = new List<Vector3>();
                for (int j = 0; j < path.childCount; j++)
                {
                    var pathPos = path.GetChild(j);
                    pathPosList.Add(pathPos.transform.position);
                }

                if (!isShowPath)
                {
                    lr.positionCount = 0;
                    continue;
                }

                lr.positionCount = pathPosList.Count;
                lr.SetPositions(pathPosList.ToArray());
            }
        }

        public void ProtecteRoot()
        {
            ProtecteNode(transform);
            for (int i = 0; i < transform.childCount; i++)
            {
                var path = transform.GetChild(i);
                ProtecteNode(path);
            }
        }

        public static void ProtecteNode(Transform node)
        {
            node.position = Vector3.zero;
            node.rotation = Quaternion.identity;
            node.localScale = Vector3.one;
        }
    }

}
