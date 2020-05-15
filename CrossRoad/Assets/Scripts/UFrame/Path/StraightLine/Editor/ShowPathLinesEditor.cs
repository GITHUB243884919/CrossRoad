/*******************************************************************
* FileName:     ShowPathLinesEditor.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-6
* Description:  
* other:    
********************************************************************/


using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using LitJson;
using UnityEditor.SceneManagement;
using System.Collections.Generic;
using UFrame.Logger;

namespace UFrame.Path.StraightLine
{
    [CustomEditor(typeof(ShowPathLines))]
    public class ShowPathLinesEditor : Editor
    {
        private ShowPathLines showPathLines;

        void OnEnable()
        {
            showPathLines = target as ShowPathLines;
        }

        public override void OnInspectorGUI()
        {
            if (target == null || showPathLines == null)
            {
                return;
            }

            base.OnInspectorGUI();

            if (GUILayout.Button("刷新"))
            {
                showPathLines.gameObject.SetActive(true);
                showPathLines.DrawPathLineRender();
            }

            if (GUILayout.Button("导出"))
            {
                showPathLines.ProtecteRoot();
                showPathLines.gameObject.SetActive(false);
                ExportToCSHARP();
                ShowPathLinesEditor.ApplyPrefab(showPathLines.gameObject);
                //ShowPathLinesEditor.RemovePrefab(showPathLines.gameObject);
            }

            //if (GUILayout.Button("加载"))
            //{
            //    this.LoadScriptableObject();
            //}
        }

        public void TestExport()
        {
            string savePath = "Assets/Resources/test.asset";
            TestScriptObj scriptableObj = ScriptableObject.CreateInstance<TestScriptObj>();
            UnityEditor.AssetDatabase.CreateAsset(scriptableObj, savePath);

            scriptableObj.ival = 1000;
            scriptableObj.vVal = new Vector3(100, 200, 300);
            var p1 = new POS();
            p1.x = 1;
            p1.y = 2;
            p1.z = 3;
            scriptableObj.lvVal.Add(p1);
            scriptableObj.lvVal.Add(p1);
            //scriptableObj.lvVal.Add(new Vector3(10, 20, 30));
            //scriptableObj.lvVal.Add(new Vector3(100, 200, 300));

            EditorUtility.SetDirty(scriptableObj);


            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();

        }
        
        /// <summary>
        /// 导出成c#
        /// </summary>
        public void ExportToCSHARP()
        {
            string csharpFile = "///本代码由路径编辑器自动生成, 请勿手动修改! by : Fan Zheng Yong\r\n" +
                "using Game.MiniGame;\r\n" +
                "using System.Collections.Generic;\r\n" +
                "using UFrame.Common;\r\n" +
                "using UFrame.Path.StraightLine;\r\n" +
                "using UnityEngine;\r\n" +
                "namespace Game.Path.StraightLine\r\n" +
                "{\r\n" +
                "    public partial class PathManager : Singleton<PathManager>, ISingleton\r\n" +
                "    {\r\n" +
                "        public void AddAllPath()\r\n" +
                "        {\r\n";

            
            Dictionary<string, List<Vector3>> dic = new Dictionary<string, List<Vector3>>();
            for (int i = 0; i < showPathLines.transform.childCount; i++)
            {
                var path = showPathLines.transform.GetChild(i);
                //list名
                string pathListName = "path_" + path.name;
                //list声明
                string pathListST = string.Format("\r\n            var {0} = new List<Vector3>();\r\n", pathListName);

                bool isYValid = path.GetComponent<ShowPathLine>().isYValid;

                List<Vector3> posList = new List<Vector3>();
                //list插入
                string pathListInsert = "";

                for (int j = 0; j < path.childCount; j++)
                {
                    var pathPos = path.GetChild(j);
                    Vector3 pos = pathPos.transform.position;
                    if (!isYValid)
                    {
                        pos.y = 0f;
                    }
                    posList.Add(pos);
                    //Debug.LogError(path.name + " " + pos);
                    pathListInsert += string.Format("            {0}.Add(new Vector3({1}f, {2}f, {3}f));\r\n",
                        pathListName, pos.x, pos.y, pos.z);
                    
                }
                dic.Add(path.name, posList);
                //Add to Manager
                string addToManager = "";
                addToManager = string.Format("            AddPath($__pathname__$, {0});\r\n", pathListName);
                addToManager = addToManager.Replace("$__pathname__$", "\"" + path.name + "\"");

                csharpFile += pathListST;
                csharpFile += pathListInsert;
                csharpFile += addToManager;
            }
            csharpFile += "        }\r\n";
            csharpFile += "    }\r\n";
            csharpFile += "}\r\n";

            FileUtil.CreateDir("./EditorConfig", true);
            string filepath = "./EditorConfig/PathManager_AddAll.cs";
            File.WriteAllText(filepath, csharpFile, Encoding.UTF8);
            AssetDatabase.Refresh();
            UnityEditor.EditorApplication.isPlaying = false;

        }


        /// <summary>
        /// 为laya项目导出，格式为json，并且坐标换成右手坐标
        /// </summary>
        void ExportToLaya()
        {
            showPathLines.ProtecteRoot();
            showPathLines.gameObject.SetActive(false);
            var scene = EditorSceneManager.GetActiveScene();
            
            string filepath = Application.dataPath + @"/StreamingAssets/" + scene.name + "_Path.json";
            FileInfo t = new FileInfo(filepath);
            if (!File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            StreamWriter sw = t.CreateText();

            StringBuilder sb = new StringBuilder();
            JsonWriter writer = new JsonWriter(sb);
            writer.WriteObjectStart();
            writer.WritePropertyName("Path_Root");
            writer.WriteArrayStart();
            for (int i = 0; i < showPathLines.transform.childCount; i++)
            {

                var path = showPathLines.transform.GetChild(i);
                bool isYValid = path.GetComponent<ShowPathLine>().isYValid;
                //Debug.LogError(path.name);
                writer.Write(path.name);
                writer.WriteArrayStart();
                for (int j = 0; j < path.childCount; j++)
                {
                    var pathPos = path.GetChild(j);
                    string x = (-pathPos.transform.position.x).ToString("F4");
                    string y = "0";
                    if (isYValid)
                    {
                        y = pathPos.transform.position.y.ToString("F4");
                    }
                    string z = pathPos.transform.position.z.ToString("F4");
                    writer.Write(x + "," + y + "," + z);
                }
                writer.WriteArrayEnd();
            }
            writer.WriteArrayEnd();
            writer.WriteObjectEnd();

            sw.WriteLine(sb.ToString());
            sw.Close();
            sw.Dispose();
            AssetDatabase.Refresh();
        }

        public static void ApplyPrefab(GameObject instanceRoot)
        {
            if (!Application.isPlaying)
            {
                PrefabUtility.ApplyPrefabInstance(instanceRoot, InteractionMode.AutomatedAction);
                return;
            }
            LogWarp.Log(instanceRoot.name);
            bool retCode = false;
            string savePath = string.Format("{0}/Resources/prefabs/{1}.prefab", Application.dataPath, instanceRoot.name);
            PrefabUtility.SaveAsPrefabAsset(instanceRoot, savePath, out retCode);
        }

        public static void RemovePrefab(Object obj)
        {
            GameObject.DestroyImmediate(obj, true);
        }

        void ExportToScriptableObject()
        {
            string savePath = "Assets/Resources/path_root.asset";
            PathRoot scriptableObj = ScriptableObject.CreateInstance<PathRoot>();
            UnityEditor.AssetDatabase.CreateAsset(scriptableObj, savePath);
            Dictionary<string, List<Vector3>> dic = new Dictionary<string, List<Vector3>>();

            for (int i = 0; i < showPathLines.transform.childCount; i++)
            {
                var path = showPathLines.transform.GetChild(i);
                bool isYValid = path.GetComponent<ShowPathLine>().isYValid;
                List<Vector3> posList = new List<Vector3>();
                for (int j = 0; j < path.childCount; j++)
                {
                    var pathPos = path.GetChild(j);
                    Vector3 pos = pathPos.transform.position;
                    if (!isYValid)
                    {
                        pos.y = 0f;
                    }
                    posList.Add(pos);
                }
                dic.Add(path.name, posList);
            }
            scriptableObj.Target = dic;

            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
        }



        void SaveScene()
        {
            //var scene = EditorSceneManager.GetActiveScene();
            //EditorSceneManager.SaveScene(scene);
            //EditorSceneManager.SaveOpenScenes();
        }

        public void LoadScriptableObject()
        {
            PathRoot pathRoot = Resources.Load<PathRoot>("path_root");

            Dictionary<string, List<Vector3>> dic = pathRoot.Target;

            foreach(var kv in dic)
            {
                Debug.Log(kv.Key + "begin");
                for(int i = 0; i < kv.Value.Count; i++)
                {
                    Debug.Log(kv.Value[i]);
                }
                Debug.Log(kv.Key + "end");
            }
        }
    }
}
