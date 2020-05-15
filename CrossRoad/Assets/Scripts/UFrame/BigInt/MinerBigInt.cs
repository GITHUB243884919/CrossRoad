/*******************************************************************
* FileName:     MinerBigInt.cs
* Author:       Fan Zheng Yong
* Date:         2019-9-6
* Description:  
* other:    
********************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

namespace UFrame
{
    /// <summary>
    /// 实现“采矿大亨”这款游戏的大数据显示规则
    /// </summary>
    public class MinerBigInt
    {
        /// <summary>
        /// 大数后缀
        /// </summary>
        public static char[] bigSuffix = new char[] { ' ', 'K', 'M', 'B', 'T' };

        /// <summary>
        /// 超大数的后缀
        /// </summary>
        public static char[] hugeSuffix = new char []{
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};

        /// <summary>
        /// 有效位数
        /// </summary>
        public static int effectiveLen = 3;

        public static string ToDisplay(BigInteger bigInteger)
        {
            if (bigInteger < 0)
            {
                return "0";
            }

            return ToDisplay(bigInteger.ToString());
        }

        public static string ToDisplay(string strBigInt)
        {
            int len = strBigInt.Length;

            if (len <= effectiveLen)
            {
                return strBigInt;
            }

            //3的倍数
            int timesEffectiveLen = len / effectiveLen;

            //3的余数
            int remainerEffectiveLen = len % effectiveLen;

            string effectiveNum = strBigInt.Substring(0, effectiveLen);
            if (remainerEffectiveLen != 0)
            {
                string effectiveNumInt = effectiveNum.Substring(0, remainerEffectiveLen);
                string effectiveNumFloat = effectiveNum.Substring(remainerEffectiveLen);
                effectiveNum = string.Format("{0}.{1}", effectiveNumInt, effectiveNumFloat);
            }

            string whole = "";
            int upMod = (len + effectiveLen - 1) / effectiveLen;
            //Logger.LogWarp.LogFormat("{0}, {1}, {2}", strBigInt, len, upMod3);
            if (upMod <= bigSuffix.Length)
            {
                whole = string.Format("{0}{1}", effectiveNum, bigSuffix[upMod - 1]);
                return whole;
            }

            int leftTimes = upMod - bigSuffix.Length - 1;
            //Debug.LogErrorFormat("{0}, {1}, {2}, {3}", leftTimes, times3, bigSuffix.Length, len);
            int timesHugeSuffixLen = leftTimes / hugeSuffix.Length;
            int remainerHugeSuffixLen = leftTimes % hugeSuffix.Length;
            whole = string.Format("{0}{1}{2}", effectiveNum, hugeSuffix[timesHugeSuffixLen], 
                hugeSuffix[remainerHugeSuffixLen]);
            return whole;
        }

    }
}

