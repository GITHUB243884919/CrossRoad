using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GlobalData
{
    /// <summary>
    /// internationalization 国际化
    /// </summary>
    public class I18N
    {
        public SystemLanguage currLanguage;
        public I18N()
        {
            Init();
        }

        protected void Init()
        {
            if (LoadingMgr.Inst.isUsedlanguage)
            {
                currLanguage = LoadingMgr.Inst.language;
                if (currLanguage == SystemLanguage.Chinese)
                {
                    currLanguage = SystemLanguage.ChineseSimplified;
                }

                return;
            }
            //打包;   除了系统语言是中文，显示中文外，其他系统语言全显示英语
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                    currLanguage = SystemLanguage.ChineseSimplified;
                    break;
                default:
                    currLanguage = SystemLanguage.English;
                    break;
            }
            //switch (Application.systemLanguage)
            //{
            //    case SystemLanguage.Chinese: 
            //    case SystemLanguage.ChineseSimplified:
            //        currLanguage = SystemLanguage.ChineseSimplified;
            //        break;
            //    case SystemLanguage.ChineseTraditional:
            //        currLanguage = SystemLanguage.ChineseTraditional;
            //        break;
            //    case SystemLanguage.French:
            //        currLanguage = SystemLanguage.French;
            //        break;
            //    case SystemLanguage.German:
            //        currLanguage = SystemLanguage.German;
            //        break;
            //    case SystemLanguage.Russian:
            //        currLanguage = SystemLanguage.Russian;
            //        break;
            //    case SystemLanguage.Japanese:
            //        currLanguage = SystemLanguage.Japanese;
            //        break;
            //    case SystemLanguage.Korean:
            //        currLanguage = SystemLanguage.Korean;
            //        break;
            //    default:
            //        currLanguage = SystemLanguage.English;
            //        break;
            //}
        }

        /// <summary>
        /// 本地化接口
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetL10N(string key)
        {
            var cell = Config.translateConfig.getInstace().getCell(key);
#if UNITY_EDITOR
            if (cell == null)
            {
                string e = string.Format("警告：ID={0} 在语言表translate中不存在", key);
                throw new System.Exception(e);
            }
#else
            if (cell == null)
            {
                return null;
            }
#endif
            switch (currLanguage)
            {
                case SystemLanguage.ChineseSimplified:
                    return cell.zh_CN;
                case SystemLanguage.ChineseTraditional:
                    return cell.zh_TW;
                case SystemLanguage.French:
                    return cell.fr_FR;
                case SystemLanguage.German:
                    return cell.de_DE;
                case SystemLanguage.Russian:
                    return cell.ru_RU;
                case SystemLanguage.Japanese:
                    return cell.jpn_JPN;
                case SystemLanguage.Korean:
                    return cell.kr_KR;
                default:
                    return cell.en_US;
            }
        }
    }


}

