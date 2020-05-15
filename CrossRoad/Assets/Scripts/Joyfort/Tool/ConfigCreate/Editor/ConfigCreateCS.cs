/*******************************************************************
* FileName:     ConfigCreateCS.cs
* Author:       Fan Zheng Yong
* Date:         2019-09-16
* Description:  结构copy的枪项目，支持Unity 2018解析excel从大项目copy
* other:    
********************************************************************/


using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor.ProjectWindowCallback;
using System.Text.RegularExpressions;
using System.Linq;
//using Excel;
using System.Data;
using OfficeOpenXml;
using UFrame.Logger;

public class CreateConfigFile : MonoBehaviour
{
    [MenuItem("Joyfort/Tools/配置表/生成配置表cs文件")]
    static void CreateData()
    {
        //当前目录是c#工程的所在目录
        string fullPath = "./Excel/";
        //#if Dev_Branche
        //        //分支下开发
        //        string fullPath = "./Excel/";
        //#elif Dev_Special_Branche
        //		//特殊分支下开发
        //		string fullPath = "../../trunk/Document/data_Special_Branche/";
        //#else
        //		//主线下开发
        //		string fullPath = "../../../Document/data/";
        //#endif
        if (Directory.Exists(fullPath))
        {
            DirectoryInfo direction = new DirectoryInfo(fullPath);
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
            for (int i = 0, imax = files.Length; i < imax; i++)
            {
                //if (files[i].Name.EndsWith(".xlsx"))
                if (files[i].Name.EndsWith(".xlsx"))
                {
                    string fullName = Path.GetFileNameWithoutExtension(files[i].FullName);
                    //string[,] arr = ReadExcelData (files [i].FullName);
                    try
                    {
                        string[,] arr = ReadXls(files[i].FullName);
                        CreateOne(fullName, arr);
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError("导表出错 " + files[i].Name);
                        Debug.LogError(e.Message);
                    }

                    //break;

                }
            }

            EditorUtility.ClearProgressBar();
        }
        Debug.Log("所有的配置cs类都生成成功");
        AssetDatabase.Refresh();
    }

    [MenuItem("Joyfort/Tools/配置表/Copy编辑器生成配置文件")]
    static void CopyEditorConfig()
    {
        string filepath = "./EditorConfig/";
        string destDir = Application.dataPath + "/Scripts/Joyfort/Config/";
        string destPath = "";
        if (Directory.Exists(filepath))
        {
            DirectoryInfo dir = new DirectoryInfo(filepath);
            foreach (var file in dir.GetFiles())
            {
                LogWarp.Log(file.FullName);
                destPath = Path.GetFileName(file.FullName);
                destPath = destDir + destPath;
                file.CopyTo(destPath, true);
            }
        }
    }
    [MenuItem("Joyfort/Tools/配置表/生成单枚举配置表cs文件")]
    static void CreateEnumData_One()
    {
        //当前目录是c#工程的所在目录

        string fullPath = "./EnumExcel_One/";

        if (Directory.Exists(fullPath))
        {
            DirectoryInfo direction = new DirectoryInfo(fullPath);
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
            for (int i = 0, imax = files.Length; i < imax; i++)
            {
                //if (files[i].Name.EndsWith(".xlsx"))
                if (files[i].Name.EndsWith(".xlsx"))
                {
                    string fullName = Path.GetFileNameWithoutExtension(files[i].FullName);
                    //string[,] arr = ReadExcelData (files [i].FullName);
                    try
                    {
                        string[,] arr = ReadXls(files[i].FullName);
                        CreateEvum_One(fullName, arr);

                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError("导表出错 " + files[i].Name);
                        Debug.LogError(e.Message);
                    }
                }
            }

            EditorUtility.ClearProgressBar();
        }
        Debug.Log("所有的单枚举配置表cs文件都生成成功");
        AssetDatabase.Refresh();
    }
    [MenuItem("Joyfort/Tools/配置表/生成多枚举配置表cs文件")]
    static void CreateEnumData_More()
    {
        string fullPath = "./EnumExcel_More/";

        if (Directory.Exists(fullPath))
        {
            DirectoryInfo direction = new DirectoryInfo(fullPath);
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
            for (int i = 0, imax = files.Length; i < imax; i++)
            {
                //if (files[i].Name.EndsWith(".xlsx"))
                if (files[i].Name.EndsWith(".xlsx"))
                {
                    string fullName = Path.GetFileNameWithoutExtension(files[i].FullName);
                    //string[,] arr = ReadExcelData (files [i].FullName);
                    try
                    {
                        string[,] arr = ReadXls(files[i].FullName);
                        CreateEvum_More(fullName, arr);

                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError("导表出错 " + files[i].Name);
                        Debug.LogError(e.Message);
                    }
                }
            }

            EditorUtility.ClearProgressBar();
        }
        Debug.Log("所有的生成多枚举配置表cs文件都生成成功");
        AssetDatabase.Refresh();
    }
    public static string GetCellValue(ExcelWorksheet sheet, int row, int column)
    {
        object o = sheet.GetValue(row, column);
        if (o != null)
        {
            return o.ToString();
        }
        return null;
    }

    private static string[,] ReadXls(string path)
    {
        ExcelPackage package = new ExcelPackage(new FileStream(path, FileMode.Open));
        //Debug.LogError("package.Workbook.Worksheets.Count " + (package.Workbook.Worksheets.Count));

        if (package.Workbook.Worksheets.Count <= 0)
        {
            Debug.LogErrorFormat("{0}第没有sheet", path);
            return null;
        }
        ExcelWorksheet sheet = package.Workbook.Worksheets[1];
        //Debug.LogError("sheet != null" + (sheet != null));
        int columns = sheet.Dimension.End.Column;//获取列数
        int rows = sheet.Dimension.End.Row;//获取行数

        //Debug.LogError(path + " " + rows + " " + columns);

        HashSet<string> hashVal = new HashSet<string>();
        for (int i = 1; i <= rows; i++)
        {
            string val = GetCellValue(sheet, i, 1);
            if ((val == "Null" || string.IsNullOrEmpty(val)) && (i > 3))
            {
                rows = i - 1;
                break;
            }
            if (!hashVal.Add(val) && i > 3)
            {
                Debug.LogErrorFormat("{0}第{1}行有重复的key", path, i);
            }
        }

        for (int i = 1; i <= columns; i++)
        {
            string val = GetCellValue(sheet, 1, i);
            if (val == "Null" || string.IsNullOrEmpty(val))
            {
                columns = i - 1;
                break;
            }
        }

        string[,] arr = new string[rows, columns];
        for (int i = 1; i <= rows; i++)
        {
            for (int j = 1; j <= columns; j++)
            {
                arr[i - 1, j - 1] = GetCellValue(sheet, i, j);
            }
        }
        package.Dispose();
        package = null;
        return arr;

    }
    //[MenuItem("Joyfort/Tools/配置表/clearAll")]
    static void DeleteData()
    {

        string fullPath = "Assets/Scripts/Joyfort/Config/";

        FileUtil.DeleteDir(fullPath);
        Debug.Log("所有的配置cs类都删除完毕");
        AssetDatabase.Refresh();
    }

    static void CreateOne(string className, string[,] excelarr)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("using UnityEngine;\nusing System;\nusing System.Security;\nusing System.Collections.Generic;\n");
        sb.Append("namespace Config\n");
        sb.Append("{\n");//namespace
                         //========================================================================
        sb.Append(string.Format("\tpublic class {0}Config\n", className));
        sb.Append("\t{\n");//class
        sb.Append("\t\tprivate " + className + "Config(){ \n");
        sb.Append("\t\t}\n");
        sb.Append(string.Format("\t\tprivate static {0}Config _inst;\n", className));
        sb.Append("\t\tpublic static " + className + "Config getInstace(){\n");
        sb.Append("\t\t\tif (_inst != null) {\n");
        sb.Append("\t\t\t\treturn _inst;\n");
        sb.Append("\t\t\t}\n");
        sb.Append("\t\t\t_inst = new " + className + "Config ();\n");

        //=========================================================================
        if (className.ToLower() == "global")
        {
            sb.Append("\t\t\treturn _inst;\n");
            sb.Append("\t\t}\n");
            for (int i = 2, Maxi = excelarr.GetLength(0); i < Maxi; i++)
            {
                string krr = excelarr[i, 2] + " " + excelarr[i, 0];
                sb.Append("\t\t///<summary>\n");
                sb.Append("\t\t///" + excelarr[i, 3] + "\n");
                sb.Append("\t\t///<summary>\n");
                sb.Append("\t\tpublic readonly " + krr + "= " + CfgValue(excelarr[i, 2], excelarr[i, 1]) + ";  \n");
            }
            sb.Append("} \n");
            sb.Append("}");//namespace
        }
        else
        {
            sb.Append("\t\t\t_inst.InitData ();\n");
            sb.Append("\t\t\treturn _inst;\n");
            sb.Append("\t\t}\n");
            //sb.Append (string.Format ("\t\tprivate Dictionary<string,{0}Cell> AllData;\n", className));
            sb.Append(string.Format("\t\tpublic Dictionary<string,{0}Cell> AllData;\n", className));
            sb.Append("\t\tpublic " + className + "Cell getCell(string key){\n");
            sb.Append(string.Format("\t\t\t{0}Cell t = null;\n", className));
            sb.Append("\t\t\tthis.AllData.TryGetValue (key, out t);\n");
            sb.Append("\t\t\treturn t;\n");
            sb.Append("\t\t}\n");
            sb.Append("\t\tpublic " + className + "Cell getCell(int key){\n");
            sb.Append(string.Format("\t\t\t{0}Cell t = null;\n", className));
            sb.Append("\t\t\tthis.AllData.TryGetValue (key.ToString(), out t);\n");
            sb.Append("\t\t\treturn t;\n");
            sb.Append("\t\t}\n");

            sb.Append("\t\tpublic readonly int RowNum = " + (excelarr.GetLength(0) - 3).ToString() + ";\n");

            sb.Append("\t\tprivate void InitData(){\n");
            sb.Append(string.Format("\t\t\tthis.AllData = new Dictionary<string,{0}Cell> ();\n", className));
            for (int i = 3, Maxi = excelarr.GetLength(0); i < Maxi; i++)
            {
                string temp = string.Format("\t\t\tthis.AllData.Add(\"{0}\",new {1}Cell(", excelarr[i, 0], className);
                for (int j = 1, Maxj = excelarr.GetLength(1); j < Maxj; j++)
                {
                    temp += CfgValue(excelarr[1, j], excelarr[i, j]);
                    if (j < Maxj - 1)
                    {
                        temp += ",";
                    }
                }
                temp += "));\n";
                sb.Append(temp);
            }
            sb.Append("\t\t}\n");


            sb.Append("\t}\n");//class
                               //========================================================================

            //========================================================================
            sb.Append(string.Format("\tpublic class {0}Cell\n", className));
            sb.Append("\t{\n");//class
            string temprr = "\t\tpublic " + className + "Cell(";
            string temparr = "";
            for (int i = 1, Maxi = excelarr.GetLength(1); i < Maxi; i++)
            {
                string krr = excelarr[1, i] + " " + excelarr[2, i];
                sb.Append("\t\t///<summary>\n");
                sb.Append("\t\t///" + excelarr[0, i] + "\n");
                sb.Append("\t\t///<summary>\n");
                sb.Append("\t\tpublic readonly " + krr + ";\n");
                temprr += krr;
                if (i < Maxi - 1)
                {
                    temprr += ",";
                }
                temparr += "\t\t\tthis." + excelarr[2, i] + " = " + excelarr[2, i] + ";\n";
            }
            temprr += "){\n";
            sb.Append(temprr);
            sb.Append(temparr);
            sb.Append("\t\t}\n");

            sb.Append("\t}\n");//class
                               //========================================================================
            sb.Append("}");//namespace
        }
        //输出的路径
        string path = Application.dataPath + "/Scripts/Joyfort/Config/" + className + "Config.cs";
        FileUtil.WriteFile(path, sb.ToString());
    }


    static string CfgValue(string valueType, string str)
    {
        string s = "";
        if (valueType == "string")
        {
            s = "\"" + str + "\"";
        }
        else if (valueType == "int")
        {
            s = string.IsNullOrEmpty(str) ? "-1" : str;
        }
        else if (valueType == "bool")
        {
            s = string.IsNullOrEmpty(str) ? "false" : str;
        }
        else if (valueType == "double" || valueType == "float")
        {
            s = string.IsNullOrEmpty(str) ? "-1f" : str + "f";
        }
        else if (valueType == "List<int>" || valueType == "List<float>" || valueType == "List<bool>" || valueType == "int[]" || valueType == "float[]" || valueType == "bool[]")
        {
            if (string.IsNullOrEmpty(str))
            {
                throw (new System.Exception("数组类型不能为空"));
            }
            string[] arr = str.Split('|');
            s = "new " + valueType + "{";
            for (var i = 0; i < arr.Length; i++)
            {
                s += arr[i];
                if (valueType == "List<float>" || valueType == "float[]")
                {
                    s += "f";
                }
                if (i < arr.Length - 1)
                {
                    s += ",";
                }
            }
            s += "}";
        }
        else if (valueType == "List<string>")
        {
            if (string.IsNullOrEmpty(str))
            {
                throw (new System.Exception("数组类型不能为空"));
            }
            string[] arr = str.Split('|');
            s = "new List<string>{";
            for (var i = 0; i < arr.Length; i++)
            {
                s += "\"" + arr[i] + "\"";
                if (i < arr.Length - 1)
                {
                    s += ",";
                }
            }
            s += "}";
        }
        else if (valueType == "string[]")
        {
            if (string.IsNullOrEmpty(str))
            {
                throw (new System.Exception("数组类型不能为空"));
            }
            string[] arr = str.Split('|');
            s = "new string[]{";
            for (var i = 0; i < arr.Length; i++)
            {
                s += "\"" + arr[i] + "\"";
                if (i < arr.Length - 1)
                {
                    s += ",";
                }
            }
            s += "}";
        }
        else
        {
            Debug.LogError("所填的配置表 类型暂时无法识别 [" + valueType + "][" + str + "]");
        }
        return s;
    }
    //static string[,] ReadExcelData(string path){
    //	FileStream stream = File.Open (path, FileMode.Open, FileAccess.Read);
    //	IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

    //	DataSet result = excelReader.AsDataSet();

    //	int columns = result.Tables[0].Columns.Count;//获取列数
    //	int rows = result.Tables[0].Rows.Count;//获取行数
    //	List<string> list = new List<string>();
    //	for(int i=0;i<columns;i++){
    //		string k = result.Tables [0].Rows [0] [i].ToString ();
    //		if (k == "") {
    //			columns = i;
    //			break;
    //		}
    //		if (list.IndexOf (k) != -1) {
    //			Debug.LogError (path + "  i行  有重复的key");
    //		}
    //	}
    //	list = new List<string> ();
    //	for (int i = 1; i < rows; i++) {
    //		string k = result.Tables [0].Rows [i][0].ToString ();
    //		if ( k == "") {
    //			rows = i;
    //			break;
    //		}
    //		if (list.IndexOf (k) != -1) {
    //			Debug.LogError (path + "  i列  有重复的key");
    //		}
    //	}

    //	string[,] arr = new string[rows, columns];
    //	for (int i = 0; i < rows; i++)
    //	{
    //		for (int j = 0; j < columns; j++)
    //		{
    //			arr[i,j] = result.Tables[0].Rows[i][j].ToString();
    //		}
    //	}
    //	return arr;
    //}
    static void CreateEvum_One(string className, string[,] excelarr)
    {
        Debug.Log("CreateEvum_One");

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("using UnityEngine;\nusing System;\nusing System.Security;\nusing System.Collections.Generic;\n");
        sb.Append("namespace Config\n");
        sb.Append("{\n");//namespace
                         //========================================================================
        sb.Append(string.Format("\tpublic enum {0}EnumConfig\n", className));
        sb.Append("\t{\n");//class

        for (int i = 3, Maxi = excelarr.GetLength(0); i < Maxi; i++)
        {

            string temp = string.Format("\t\t{0}={1},\n ", excelarr[i, 0], excelarr[i, 1]);
            //for (int j = 1, Maxj = excelarr.GetLength(1); j < Maxj; j++)
            //{
            //    sss = excelarr[i, j];
            //}
            sb.Append("\t\t///<summary>\n");
            sb.Append("\t\t///" + excelarr[i, 2] + "\n");
            sb.Append("\t\t///<summary>\n");

            sb.Append(temp);
        }
        sb.Append("\t}\n");//class
                           //========================================================================
        sb.Append("}");//namespace


        //输出的路径
        string path = Application.dataPath + "/Scripts/Joyfort/Config/" + className + "Config.cs";
        FileUtil.WriteFile(path, sb.ToString());
    }
    static void CreateEvum_More(string className, string[,] excelarr)
    {
        Debug.Log("CreateEvum_More");

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("using UnityEngine;\nusing System;\nusing System.Security;\nusing System.Collections.Generic;\n");
        sb.Append("namespace Config\n");
        sb.Append("{\n");//namespace
        //========================================================================

        for (int i = 3, Maxi = excelarr.GetLength(0); i < Maxi; i++)
        {
            sb.Append(string.Format("\tpublic enum {0}EnumConfig\n", excelarr[i, 0]));
            List<String> list = new List<String>();
            sb.Append("\t{\n");//class
            for (int j = 1, Maxj = excelarr.GetLength(1); j < Maxj; j = j + 2)
            {
                if (!list.Exists(t => t == excelarr[i, j]))
                {
                    list.Add(excelarr[i, j]);
                    string temp = string.Format("\t\t{0},\n ", excelarr[i, j]);
                    sb.Append("\t\t///<summary>\n");
                    sb.Append("\t\t///" + excelarr[i, j + 1] + "\n");
                    sb.Append("\t\t///<summary>\n");
                    sb.Append(temp);
                }
                else
                {
                    Debug.LogErrorFormat("导表出错:{0}表中{1}枚举元素出现重复{2} ", className, excelarr[i, 0], excelarr[i, j]);
                }
            }
            list.Clear();
            sb.Append("\t}\n");//class
        }
        //========================================================================
        sb.Append("}");//namespace


        //输出的路径
        string path = Application.dataPath + "/Scripts/Joyfort/Config/" + className + "Config.cs";
        FileUtil.WriteFile(path, sb.ToString());
    }
}