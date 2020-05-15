using System;
using System.Collections;
using System.Collections.Generic;
using UFrame.Logger;
using UnityEngine;

namespace Game.GlobalData
{
    [Serializable]
    public partial class PlayerData
    {
        /// <summary>
        /// 是否首次安装
        /// </summary>
        public bool isFirstInstall = false;

        /// <summary>
        /// 是否开启音效
        /// </summary>
        public bool isSound = true;

        /// <summary>
        /// 重置次数
        /// </summary>
        public int leftNumResetPoker = 10;

        /// <summary>
        /// 提示次数
        /// </summary>
        public int leftNumTips = 5;

        public PlayerData()
        {
            SetDefault();
        }

        protected void SetDefault()
        {
            isFirstInstall = false;
            isSound = true;
            leftNumResetPoker = 10;
            leftNumTips = 5;
        }

        public static PlayerData Load()
        {
            LogWarp.Log("PlayerData.Load");
            var playerData = GlobalDataManager.GetInstance().playerData;
            if (playerData == null)
            {
                LogWarp.Log("PlayerData.LoadFromPlayerPrefs");
                playerData = LoadFromPlayerPrefs();
                GlobalDataManager.GetInstance().playerData = playerData;
            }

            //playerData.leftNumResetPoker = GameConst.Default_LeftNumResetPoker;
            //playerData.leftNumTips = GameConst.Default_LeftNumTips;

            return playerData;
        }

        protected static PlayerData LoadFromPlayerPrefs()
        {
            PlayerData pd = null;
            string str = PlayerPrefs.GetString("PlayerData");
            bool isFirstInstall = false;
            if (string.IsNullOrEmpty(str))
            {
                isFirstInstall = true;
                var playerData = new PlayerData();
                Save(playerData);
                str = PlayerPrefs.GetString("PlayerData");
                if (string.IsNullOrEmpty(str))
                {
                    string e = string.Format("取本地数据PlayerData异常");
                    throw new System.Exception(e);
                }
            }

            pd = JsonUtility.FromJson<PlayerData>(str);
            if (pd == null)
            {
                pd = new PlayerData();
                pd.isFirstInstall = false;
                if (isFirstInstall)
                {
                    pd.isFirstInstall = true;
                }
                return pd;
            }

            pd.isFirstInstall = false;
            if (isFirstInstall)
            {
                pd.isFirstInstall = true;
            }
            return pd;
        }

        public static void Save(PlayerData playerData)
        {
            string str = JsonUtility.ToJson(playerData);
            if (string.IsNullOrEmpty(str))
            {
                string e = string.Format("存本地数据PlayerData异常");
                throw new System.Exception(e);
            }
            PlayerPrefs.SetString("PlayerData", str);
        }
    }
}
