/*******************************************************************
* FileName:     PathEditor.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-6
* Description:  
* other:    
********************************************************************/


using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using UnityEditor.SceneManagement;
using LitJson;

namespace UFrame.Path.StraightLine
{
    public class PathEditor : Editor
    {
        //[MenuItem("GameObject/ExportPathToLaya")]
        static void ExportPathToLaya()
        {
            //foreach (UnityEditor.EditorBuildSettingsScene S in UnityEditor.EditorBuildSettings.scenes)
            string name = "Assets/ArtResources/Scenes/DWY_02.unity";

            EditorSceneManager.OpenScene(name);
            var obj = GameObject.Find("Path_Root");
            Debug.LogError(obj.name + " " + obj.transform.childCount);
            ShowPathLines spl = obj.GetComponent<ShowPathLines>();
            spl.isShowPath = true;
            spl.enabled = true;

            string filepath = Application.dataPath + @"/StreamingAssets/Path.json";
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
            for (int i = 0; i < obj.transform.childCount; i++)
            {

                var path = obj.transform.GetChild(i);
                //Debug.LogError(path.name);
                writer.Write(path.name);
                writer.WriteArrayStart();
                for (int j = 0; j < path.childCount; j++)
                {
                    var pathPos = path.GetChild(j);
                    Debug.LogError(pathPos.name + " " + pathPos.transform.position);
                    //writer.Write(pathPos.name);
                    var x = (-pathPos.transform.position.x).ToString("F5");
                    var y = pathPos.transform.position.y.ToString("F5");
                    var z = pathPos.transform.position.z.ToString("F5");
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
    }
}
