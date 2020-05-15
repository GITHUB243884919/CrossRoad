/*******************************************************************
* FileName:     GlobalDataManager.cs
* Author:       Fan Zheng Yong
* Date:         2019-8-16
* Description:  
* other:    
********************************************************************/


using UFrame.Logger;
using System;
using System.Collections;
using System.Collections.Generic;
using UFrame;
using UFrame.Common;
using UnityEngine;

namespace Game.GlobalData
{
    public partial class GlobalDataManager : Singleton<GlobalDataManager>, ISingleton
    {
        bool isInit = false;

        public I18N i18n { get; protected set; }

        public PlayerData playerData = null;

        public void Init()
        {
            if (isInit)
            {
                return;
            }
            isInit = true;

            playerData = PlayerData.Load();
        }


        /// <summary>
        /// 不是所有都Release
        /// </summary>
        public void Release()
        {

        }





    }

}
