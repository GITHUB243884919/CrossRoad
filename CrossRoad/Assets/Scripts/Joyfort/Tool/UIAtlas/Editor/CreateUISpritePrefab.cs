using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor.ProjectWindowCallback;
using System.Text.RegularExpressions;
using System.Linq;


public class CreateUISpritePrefab : MonoBehaviour {

	public static string AtlasInPath = "Assets/Res/UIAtlas/";
    public static string outPath = "Resources/UIAtlas/";

	[MenuItem("Joyfort/Tools/图集/生成图集的prefab文件")]
	static void CreateData()
	{
		if (Directory.Exists(AtlasInPath)){  
			FileUtil.DeleteDir (outPath);
			DirectoryInfo direction = new DirectoryInfo(AtlasInPath);  
			FileInfo[] files = direction.GetFiles("*",SearchOption.AllDirectories);
			int Max = files.Length;
			for(int i=0;i<Max;i++){  
				if (!files[i].Name.EndsWith(".meta")){  
					string fullName =  Path.GetFullPath(files[i].FullName);
					fullName =FormatAssetPath(fullName);
//					fullName = fullName.Substring (0,fullName.IndexOf("."));
					var spr = AssetDatabase.LoadAssetAtPath(fullName,typeof(UnityEngine.Sprite))as UnityEngine.Sprite;
					var name = Path.GetFileNameWithoutExtension (files [i].FullName);
					AddAtlasPrefab (spr,name,fullName);
					EditorUtility.DisplayProgressBar("生成图集的prefab文件",name, i /(float)Max);  
//					return;
				}
			}
			EditorUtility.ClearProgressBar();
		}	
		Debug.Log ("所有的图集的prefab文件生成成功");
		AssetDatabase.Refresh();
	}

	private static void AddAtlasPrefab(Sprite spr,string name,string SprAssetPath){
		var go1 = new GameObject(name);
		go1.AddComponent<SpriteRenderer> ().sprite = spr;
		string outPaht = SprAssetPath.Replace ("/Res/", "/Resources/");
		var str = outPaht.Split ('.') [0];
		outPaht = str +".prefab";
		FileUtil.CreateDir (str.Replace("/"+name,""), false);
		PrefabUtility.CreatePrefab (outPaht, go1);
		DestroyImmediate (go1);
	}

	public static string FormatAssetPath(string filePath)
	{
		var newFilePath1 = filePath.Replace("\\", "/");
		var newFilePath2 = newFilePath1.Replace("//", "/").Trim();
		newFilePath2 = newFilePath2.Replace("///", "/").Trim();
		newFilePath2 = newFilePath2.Replace("\\\\", "/").Trim();
		var n = newFilePath2.IndexOf ("Assets/");
		newFilePath2 = newFilePath2.Substring (n);
		return newFilePath2;
	}
}
