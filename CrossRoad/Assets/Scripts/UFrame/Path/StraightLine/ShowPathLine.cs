/*******************************************************************
* FileName:     ShowPathLine.cs
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
    public class ShowPathLine : MonoBehaviour
    {
        public bool isShowPath = true;
        public bool isYValid = false;
        public bool isClose = false;
        public void DrawPathLineRender()
        {
            var lr = transform.GetComponent<LineRenderer>();
            var pathPosList = new List<Vector3>();
            if (!isShowPath)
            {
                lr.positionCount = 0;
                return;
            }

            for (int i = 0; i < transform.childCount; i++)
            {
                var pathPos = transform.GetChild(i);
                pathPosList.Add(pathPos.transform.position);
                //lr.positionCount = pathPosList.Count;
                //lr.SetPositions(pathPosList.ToArray());
            }

            if (isClose && transform.childCount > 0)
            {
                pathPosList.Add(transform.GetChild(0).transform.position);
            }
            lr.positionCount = pathPosList.Count;
            lr.SetPositions(pathPosList.ToArray());
        }
    }

}
