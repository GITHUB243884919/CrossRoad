using System.IO;
using System.Linq;
//using Assets.Scripts.Common.Util;
using UnityEditor;
using UnityEngine;
//using FileUtil = Assets.Scripts.Common.Util.FileUtil;


namespace Assets.Scripts.Editor.MenuItemEx
{
    public class OptimizeFBX
    {
        private const float AnimationPositionError = 0.2f;
        private const float AnimationRotationError = 0.1f;
        private const ModelImporterAnimationCompression Compression = ModelImporterAnimationCompression.Optimal;
        private const int DecimalAccuracy = 10000;


        private static string _fbxPath;


        [MenuItem("Assets/Optimize/Optimize FBX", false, 1000)]
        private static void OptimizeFbx()
        {
            EditorSettings.serializationMode = SerializationMode.ForceText;
            var objs = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            var count = objs.Length;
            var index = 0;
            foreach (var obj in objs)
            {
                var isCancel = EditorUtility.DisplayCancelableProgressBar("优化FBX文件",
                  string.Format("正在优化中...{0}/{1}", ++index, count), (float)index / count);
                if (isCancel)
                {
                    EditorUtility.ClearProgressBar();
                    return;
                }
                OptimizeObject(obj);
            }
            EditorUtility.ClearProgressBar();
        }


        public static void OptimizeObject(Object obj)
        {
            var path = AssetDatabase.GetAssetPath(obj);
            if (path != null && Path.GetExtension(path).ToLower() == ".fbx")
            {
                _fbxPath = path;
                OptimizeModleImporter(path);
                OptimizeAnimationClip(path);
            }
        }


        /// <summary>
                /// 设置fbx动画导入格式，默认AnimationPositionError = 0.2，AnimationRotationError = 0.1，根据项目调整，值越低优化压缩越少。
                /// </summary>
                /// <param name="fbxPath"></param>
        private static void OptimizeModleImporter(string fbxPath)
        {
            var modelImporter = AssetImporter.GetAtPath(fbxPath) as ModelImporter;
            if (modelImporter != null)
            {
                var isChange = false;
                if (Compression != modelImporter.animationCompression)
                {
                    isChange = true;
                    modelImporter.animationCompression = Compression;
                    modelImporter.animationPositionError = AnimationPositionError;
                    modelImporter.animationRotationError = AnimationRotationError;
                    modelImporter.resampleCurves = false;
                }
                else
                {
                    if (!Mathf.Approximately(modelImporter.animationPositionError, AnimationPositionError))
                    {
                        isChange = true;
                        modelImporter.animationPositionError = AnimationPositionError;
                    }
                    if (!Mathf.Approximately(modelImporter.animationRotationError, AnimationRotationError))
                    {
                        isChange = true;
                        modelImporter.animationRotationError = AnimationRotationError;
                    }
                    if (modelImporter.resampleCurves)
                    {
                        isChange = true;
                        modelImporter.resampleCurves = false;
                    }
                }
                if (isChange)
                {
                    modelImporter.SaveAndReimport();
                    AssetDatabase.Refresh();
                }
            }
        }


        private static void OptimizeAnimationClip(string fbxPath)
        {
            var objs = AssetDatabase.LoadAllAssetsAtPath(fbxPath);
            var index = 0;
            var folderPath = string.Empty;
            foreach (var o in objs)
            {
                if (o is AnimationClip)
                {
                    if (o.name == "__preview__Take 001")
                        continue;
                    if (index++ == 0) //只在第一次创建文件夹
                    {
                        folderPath = _fbxPath.Substring(0, _fbxPath.LastIndexOf('.'));
                        //FileUtil.RecreateDirectory(PathUtil.AssetPathToAbsolutePath(folderPath)); //删除原来文件夹，重新创建新文件夹
                    }
                    OptimizeAnimationCurveData(o as AnimationClip, folderPath);
                }
            }
        }


        /// <summary>
                /// 优化动画片段，删除不需要的序列帧，并降低帧信息的精度
                /// </summary>
                /// <param name="clip"></param>
                /// <param name="folderPath"></param>
                /// <returns></returns>
        public static bool OptimizeAnimationCurveData(AnimationClip clip, string folderPath)
        {
            if (clip == null)
            {
                Debug.LogError("No Clip error, please tell zhanglingyun" + "  " + _fbxPath);
                return false;
            }


            var curveDatas = AnimationUtility.GetAllCurves(clip, true);
            if (curveDatas == null || curveDatas.Length == 0)
            {
                Debug.LogError("No AnimationClipCurveData error, please tell zhanglingyun!" + "  " + _fbxPath + "  " +
                       clip.name);
                return false;
            }


            var newClip = new AnimationClip();
            EditorUtility.CopySerialized(clip, newClip);


            newClip.name = clip.name;
            newClip.ClearCurves();


            foreach (var dt in curveDatas)
            {
                var nodeName = dt.path.ToLower().Split('/').Last();
                // 进行过滤
                if (IsFilterCurveData(dt, nodeName))
                    continue;


                var keys = dt.curve.keys;
                for (var i = 0; i < keys.Length; i++)
                {
                    keys[i].time = Mathf.Round(keys[i].time * DecimalAccuracy) / DecimalAccuracy;
                    keys[i].value = Mathf.Round(keys[i].value * DecimalAccuracy) / DecimalAccuracy;
                    keys[i].outTangent = Mathf.Round(keys[i].outTangent * DecimalAccuracy) / DecimalAccuracy;
                    keys[i].inTangent = Mathf.Round(keys[i].inTangent * DecimalAccuracy) / DecimalAccuracy;
                }


                //过滤位移值没有变化的帧动画
                //因为帧信息有初始位置，所有要保留头尾两帧，如果全部删除会出现初始位置为默认值的问题
                if (IsFilterApproximateKeyFrame(ref keys))
                {
                    var newKeys = new Keyframe[2];
                    newKeys[0] = keys[0];
                    newKeys[1] = keys[keys.Length - 1];
                    keys = newKeys;
                }
                dt.curve.keys = keys;
                //设置新数据
                newClip.SetCurve(dt.path, dt.type, dt.propertyName, dt.curve);
            }


            AssetDatabase.CreateAsset(newClip, folderPath + @"/" + newClip.name + ".anim");
            AssetDatabase.Refresh();


            return true;
        }


        /// <summary>
                /// 过滤值一样的序列帧
                /// </summary>
                /// <param name="keys"></param>
                /// <returns></returns>
        private static bool IsFilterApproximateKeyFrame(ref Keyframe[] keys)
        {
            for (var i = 0; i < keys.Length - 1; i++)
            {
                if (Mathf.Abs(keys[i].value - keys[i + 1].value) > 0 ||
                  Mathf.Abs(keys[i].outTangent - keys[i + 1].outTangent) > 0
                  || Mathf.Abs(keys[i].inTangent - keys[i + 1].inTangent) > 0)
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
                /// 动画默认不导出Scale序列帧，除非该节点包含scale关键词(加scale关键词表示该节点需要进行scale变换)
                /// </summary>
                /// <param name="dt"></param>
                /// <returns></returns>
        private static bool IsFilterCurveData(AnimationClipCurveData dt, string nodeName)
        {
            if (dt.propertyName.ToLower().Contains("scale") && !nodeName.Contains("scale"))
                return true;
            return false;
        }
    }
}
