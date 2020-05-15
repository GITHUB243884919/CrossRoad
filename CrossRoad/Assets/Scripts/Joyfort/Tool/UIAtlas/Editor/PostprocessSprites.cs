/*******************************************************************
* FileName:     PostprocessSprites.cs
* Author:       Fan Zheng Yong
* Date:         2019-10-9
* Description:  
* other:    
********************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class PostprocessSprites : AssetPostprocessor
{
    public void OnPostprocessTexture(Texture2D texture)
    {
        if (!assetPath.StartsWith(CreateUISpritePrefab.AtlasInPath))
        {
            return;
        }
        //Debug.LogFormat("Texture2D: name={0}, width={1}, height={2}", Path.GetDirectoryName(this.assetPath), texture.width, texture.height);

        string dir = Path.GetDirectoryName(this.assetPath);
        dir = dir.Replace("\\", "/");
        dir = dir.Substring(dir.LastIndexOf("/") + 1);
        TextureImporter ti = AssetImporter.GetAtPath(this.assetPath) as TextureImporter;
        ti.spritePackingTag = dir;
    }
}
