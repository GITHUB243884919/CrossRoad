/*******************************************************************
* FileName:     ShowPathLineEditor.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-6
* Description:  
* other:    
********************************************************************/


using UnityEngine;
using UnityEditor;

namespace UFrame.Path.StraightLine
{
    [CustomEditor(typeof(ShowPathLine))]
    public class ShowPathLineEditor : Editor
    {
        private ShowPathLine showPathLine;

        void OnEnable()
        {
            showPathLine = target as ShowPathLine;

        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("刷新"))
            {
                showPathLine.DrawPathLineRender();
            }
        }
    }


}
