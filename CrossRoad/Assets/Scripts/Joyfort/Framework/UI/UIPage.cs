/*******************************************************************
* FileName:     UIPage.cs
* Author:       Fan Zheng Yong
* Date:         2019-10-14
* Description:  以车项目代码为基础, 增加通用按钮点击音效,增加通用点击缩放动效
* other:    
********************************************************************/


using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Object = UnityEngine.Object;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Plugins.Options;
using DG.Tweening.Core;
//using UFrame.MiniGame;
using UnityEngine.EventSystems;
using Game.GlobalData;
using UFrame;
//using Game;
using UFrame.Logger;
using Game.MessageCenter;
using UnityEngine.Events;

public enum UIType
{
    Normal,
    Fixed,
    PopUp,
    Mask,
    None,      //独立的窗口
}

public enum UIMode
{
    DoNothing,
    DoTween,//需要放大显示和缩小显示的动画--
}

public enum UIAlphaBackground
{
    None,
    DoAlpha,
}

public enum UICollider
{
    None,      // 显示该界面不包含碰撞背景
    Normal,    // 碰撞透明背景
    WithBg,    // 碰撞非透明背景
}
public enum UITopMode
{
    None,
    AddTop
}

public enum UITickedMode
{
    None,
    Update,
}

public class UIPageCloseEvent : UnityEvent<int> { };
public class UIPageActionEvent : UnityEvent<int, object> { };

public class UIPage : TickBase
{

    public string name = string.Empty;

    //this page's id
    public int id = -1;

    //this page's type
    public UIType type = UIType.Normal;

    public UITopMode topmode = UITopMode.None;
    //how to show this page.
    public UIMode mode = UIMode.DoNothing;

    //the background collider mode
    public UICollider collider = UICollider.None;

    public UITickedMode tickedMode = UITickedMode.None;

    public UIAlphaBackground alphaBackground = UIAlphaBackground.None;
    //path to load ui
    public string uiPath = string.Empty;

    //this ui's gameobject
    public GameObject gameObject;
    public Transform transform;

    //record this ui load mode.async or sync.
    public bool isAsyncUI = false;

    //this page active flag
    protected bool isActived = false;

    //refresh page 's data.
    public object m_data = null;


    //protected UICurrency uiCurrency;

    private Image bgImg;

    private Transform m_ContentTrans;

    public int closeDetail = -1;
    /// <summary>
    /// View关闭事件
    /// </summary>
    public UIPageCloseEvent onClose = new UIPageCloseEvent();
    /// <summary>
    /// View动作事件
    /// </summary>
    public UIPageActionEvent onViewAction = new UIPageActionEvent();

    ///When Instance UI Ony Once.
    public virtual void Awake(GameObject go)
    {
        if (this.topmode == UITopMode.AddTop)
        {
            GameObject moneyGroup = Resources.Load<GameObject>("UIPrefab/UICurrency");
            var objMoneyGroup = GameObject.Instantiate<GameObject>(moneyGroup, this.transform);
            // Util.SetParent(objMoneyGroup, go);
            //uiCurrency = objMoneyGroup.GetComponent<UICurrency>();
        }
        //LogWarp.LogErrorFormat("Page {0} Awake", this.name);
        if (this.tickedMode == UITickedMode.Update)
        {
            //GameManager.GetInstance().AddPageToTick(this.name, this);
            UIMessage_AddToTick.Send(this);
        }
    }

    ///Show UI Refresh Eachtime.
    public virtual void Refresh()
    {

    }
    TweenerCore<float, float, FloatOptions> tweenShowColor;
    TweenerCore<float, float, FloatOptions> tweenHideColor;
    TweenerCore<Vector3, Vector3, VectorOptions> tweenShowScale;
    TweenerCore<Vector3, Vector3, VectorOptions> tweenShowScale1;

    TweenerCore<Vector3, Vector3, VectorOptions> tweenHideScale;
    TweenerCore<Vector3, Vector3, VectorOptions> tweenHideScale1;

    ///Active this UI
    public virtual void Active()
    {
        this.transform.SetAsLastSibling();
        this.gameObject.SetActive(true);
        isActived = true;
        GameObject tempObj = null;
        if (m_ContentTrans != null)
        {
            tempObj = m_ContentTrans.gameObject;
        }
        else
        {
            tempObj = this.gameObject;
        }
        if (alphaBackground == UIAlphaBackground.DoAlpha)
        {
            if (bgImg != null)
            {
                if (tweenShowColor != null)
                    tweenShowColor.Kill();
                float p2 = 0;
                tweenShowColor = DOTween.To(() => p2, x => p2 = x, 1, 0.28f);
                tweenShowColor.SetEase(Ease.Linear);
                tweenShowColor.OnUpdate(() =>
                {
                    Color c = bgImg.color;
                    c.a = p2;
                    bgImg.color = c;
                });
            }
        }
        if (this.mode == UIMode.DoTween && tempObj != null)
        {
            if (tweenShowScale != null)
                tweenShowScale.Kill();
            if (tweenShowScale1 != null)
                tweenShowScale1.Kill();
            //				DOTween.To ();
            Vector3 p2 = Vector3.zero;
            tweenShowScale = DOTween.To(() => p2, x => p2 = x, new Vector3(1.15f, 1.15f, 1), 0.12f);
            tweenShowScale.SetEase(Ease.InOutSine);
            tweenShowScale.OnUpdate(() =>
            {
                //Debug.Log(p2);
                tempObj.transform.localScale = p2;
            });
            tweenShowScale.OnComplete(() =>
            {
                tweenShowScale1 = DOTween.To(() => p2, x => p2 = x, Vector3.one, 0.16f);
                tweenShowScale1.SetEase(Ease.InOutSine);
                tweenShowScale1.OnUpdate(() =>
                {
                    //Debug.Log(p2);
                    tempObj.transform.localScale = p2;
                });
            });
        }
        //if (uiCurrency != null)
        //{
        //    uiCurrency.Active();
        //}
    }

    private void InvokeCloseAndRemoveAllListeners()
    {
        onClose.Invoke(closeDetail);

        onViewAction.RemoveAllListeners();
        onClose.RemoveAllListeners();
    }

    /// <summary>
    /// Only Deactive UI wont clear Data.
    /// </summary>
    public virtual void Hide()
    {
        GameObject tempObj = null;
        if (m_ContentTrans != null)
        {
            tempObj = m_ContentTrans.gameObject;
        }
        else
        {
            tempObj = this.gameObject;
        }
        if (alphaBackground == UIAlphaBackground.DoAlpha)
        {
            if (bgImg != null)
            {
                if (tweenHideColor != null)
                    tweenHideColor.Kill();
                float p2 = 1;
                tweenHideColor = DOTween.To(() => p2, x => p2 = x, 0, 0.25f);
                tweenHideColor.SetEase(Ease.Linear);
                tweenHideColor.OnUpdate(() =>
                {
                    Color c = bgImg.color;
                    c.a = p2;
                    bgImg.color = c;
                });
            }
        }
        if (this.mode == UIMode.DoTween && tempObj != null)
        {
            //				DOTween.To ();
            if (tweenHideScale != null)
                tweenHideScale.Kill();
            if (tweenHideScale1 != null)
                tweenHideScale1.Kill();
            Vector3 p2 = Vector3.one;
            tweenHideScale = DOTween.To(() => p2, x => p2 = x, new Vector3(1.15f, 1.15f, 1), 0.08f);
            tweenHideScale.SetEase(Ease.InSine);
            tweenHideScale.OnUpdate(() =>
            {
                //Debug.Log(p2);
                tempObj.transform.localScale = p2;
            });
            tweenHideScale.OnComplete(() =>
            {
                tweenHideScale1 = DOTween.To(() => p2, x => p2 = x, new Vector3(0, 0, 1), 0.17f);
                tweenHideScale1.SetEase(Ease.InOutSine);
                tweenHideScale1.OnUpdate(() =>
                {
                    tempObj.transform.localScale = p2;
                    //Debug.Log(p2);
                });
                tweenHideScale1.OnComplete(() =>
                {
                    this.gameObject.SetActive(false);
                    isActived = false;
                    //set this page's data null when hide.
                    this.m_data = null;
                    InvokeCloseAndRemoveAllListeners();
                });
            });
            return;
        }
        //if (uiCurrency != null)
        //{
        //    uiCurrency.Hide();
        //}

        this.gameObject.SetActive(false);
        isActived = false;
        //set this page's data null when hide.
        this.m_data = null;
        InvokeCloseAndRemoveAllListeners();
    }

    private UIPage()
    {

    }
    public UIPage(UIType type, UIMode mod, UICollider col, UITickedMode tickedMode = UITickedMode.None)
    {
        this.type = type;
        this.mode = mod;
        this.collider = col;
        this.name = this.GetType().ToString();

        this.tickedMode = tickedMode;

        //when create one page.
        //bind special delegate .
        UIBind.Bind();
        //Debug.LogWarning("[UI] create page:" + ToString());
    }

    /// <summary>
    /// Sync Show UI Logic
    /// </summary>
    public void Show()
    {
        //1:instance UI
        if (this.gameObject == null && string.IsNullOrEmpty(uiPath) == false)
        {
            GameObject go = null;
            if (PageMgr.delegateSyncLoadUI != null)
            {
                Object o = PageMgr.delegateSyncLoadUI(uiPath);
                go = o != null ? GameObject.Instantiate(o) as GameObject : null;
            }
            else
            {
                go = GameObject.Instantiate(Resources.Load(uiPath)) as GameObject;
            }

            //protected.
            if (go == null)
            {
                Debugger.LogError("[UI] Cant sync load your ui prefab.uiPath = " + uiPath);
                return;
            }

            AnchorUIGameObject(go);

            //after instance should awake init.
            Awake(go);

            //mark this ui sync ui
            isAsyncUI = false;
            Transform bgTrans = this.transform.Find("bg");
            if (bgTrans != null)
            {
                bgImg = bgTrans.GetComponent<Image>();
            }
            m_ContentTrans = this.transform.Find("Content");
        }

        //:animation or init when active.
        Active();

        //:refresh ui component.
        Refresh();

        //:popup this node to top if need back.
        PageMgr.PopNode(this);
    }

    /// <summary>
    /// Async Show UI Logic
    /// </summary>
    public void Show(Action callback)
    {
        UIRoot.Instance.StartCoroutine(AsyncShow(callback));
    }

    IEnumerator AsyncShow(Action callback)
    {
        //1:Instance UI
        //FIX:support this is manager multi gameObject,instance by your self.
        if (this.gameObject == null && string.IsNullOrEmpty(uiPath) == false)
        {
            GameObject go = null;
            bool _loading = true;
            PageMgr.delegateAsyncLoadUI(uiPath, (o) =>
                {
                    go = o != null ? GameObject.Instantiate(o) as GameObject : null;
                    AnchorUIGameObject(go);
                    Awake(go);
                    isAsyncUI = true;
                    _loading = false;

                    //:animation active.
                    Active();

                    //:refresh ui component.
                    Refresh();

                    //:popup this node to top if need back.
                    PageMgr.PopNode(this);

                    if (callback != null) callback();
                });

            float _t0 = Time.realtimeSinceStartup;
            while (_loading)
            {
                if (Time.realtimeSinceStartup - _t0 >= 10.0f)
                {
                    Debugger.LogError("[UI] WTF async load your ui prefab timeout!");
                    yield break;
                }
                yield return null;
            }
        }
        else
        {
            //:animation active.
            Active();

            //:refresh ui component.
            Refresh();

            //:popup this node to top if need back.
            PageMgr.PopNode(this);

            if (callback != null) callback();
        }
    }

    internal bool CheckIfNeedBack()
    {
        if (type == UIType.Fixed || type == UIType.PopUp || type == UIType.None || type == UIType.Mask)
            return false;
        else if (mode == UIMode.DoNothing)
            return false;
        return true;
    }

    protected void AnchorUIGameObject(GameObject ui)
    {
        if (UIRoot.Instance == null || ui == null) return;

        this.gameObject = ui;
        this.transform = ui.transform;

        //check if this is ugui or (ngui)?
        Vector3 anchorPos = Vector3.zero;
        Vector2 sizeDel = Vector2.zero;
        Vector3 scale = Vector3.one;
        RectTransform uiRect = ui.GetComponent<RectTransform>();
        if (uiRect != null)
        {
            anchorPos = uiRect.anchoredPosition;
            sizeDel = uiRect.sizeDelta;
            scale = uiRect.localScale;
        }
        else
        {
            anchorPos = ui.transform.localPosition;
            scale = ui.transform.localScale;
        }

        //Debug.Log("anchorPos:" + anchorPos + "|sizeDel:" + sizeDel);

        if (type == UIType.Fixed)
        {
            ui.transform.SetParent(UIRoot.Instance.fixedRoot);
        }
        else if (type == UIType.Normal)
        {
            ui.transform.SetParent(UIRoot.Instance.normalRoot);
        }
        else if (type == UIType.PopUp)
        {
            ui.transform.SetParent(UIRoot.Instance.popupRoot);
        }
        else if (type == UIType.Mask)
        {
            ui.transform.SetParent(UIRoot.Instance.maskRoot);
        }


        uiRect = ui.GetComponent<RectTransform>();
        if (uiRect != null)
        {
            uiRect.anchoredPosition = anchorPos;
            uiRect.sizeDelta = sizeDel;
            uiRect.localScale = scale;
        }
        else
        {
            ui.transform.localPosition = anchorPos;
            ui.transform.localScale = scale;
        }
    }

    public override string ToString()
    {
        return ">Name:" + name + ",ID:" + id + ",Type:" + type.ToString() + ",ShowMode:" + mode.ToString() + ",Collider:" + collider.ToString();
    }

    public bool isActive()
    {
        //fix,if this page is not only one gameObject
        //so,should check isActived too.
        bool ret = gameObject != null && gameObject.activeSelf;
        return ret || isActived;
    }


    protected T RegistCompent<T>(string path) where T : Component
    {
        T t = null;
        if (this.transform != null)
        {
            var trans = this.transform.Find(path);
            if (trans == null)
            {
                Debugger.LogWarning(path);
                return t;
            }
            t = trans.GetComponent<T>();
        }
        return t;
    }
    /// <summary>
    /// 给按钮添加点击事件
    /// </summary>
    /// <param name="path">按钮路径</param>
    /// <param name="onClick">按钮点击事件</param>
    /// <returns></returns>
    protected Button RegistBtnAndClick(string path, Action<string> onClick, bool isScaleAnim = true)
    {
        Button btn = null;
        if (this.transform != null)
        {
            var go = this.transform.Find(path);
            if (go == null)
            {
                Debugger.LogWarning(path);
                return btn;
            }
            btn = go.GetComponent<Button>();
            if (btn == null)
            {
                return btn;
            }
            btn.onClick.AddListener(delegate
            {
                onClick(go.name);
                if (isScaleAnim)
                {
                    BtnScaleAnim(btn.gameObject, 1.1f, 0.95f);
                }
                PageMgr.PlayButtonSound();
            });
        }
        return btn;
    }

    protected Button RegistBtnAndClick(string path, Action<Button> onClick, bool isScaleAnim = true)
    {
        Button btn = null;
        if (this.transform != null)
        {
            var go = this.transform.Find(path);
            if (go == null)
            {
                Debugger.LogWarning(path);
                return btn;
            }
            btn = go.GetComponent<Button>();
            if (btn == null)
            {
                return btn;
            }
            btn.onClick.AddListener(delegate
            {
                onClick(btn);
                if (isScaleAnim)
                {
                    BtnScaleAnim(btn.gameObject, 1.1f, 0.95f);
                }
                PageMgr.PlayButtonSound();
            });
        }
        return btn;
    }


    /// <summary>
    /// 重写  不需要路径
    /// </summary>
    /// <param name="go"></param>
    /// <param name="onClick"></param>
    /// <param name="isScaleAnim"></param>
    /// <returns></returns>
    protected Button RegistBtnAndClick(Transform go, Action<string> onClick, bool isScaleAnim = true)
    {
        Button btn = null;
        if (this.transform != null)
        {
            btn = go.GetComponent<Button>();
            if (btn == null)
            {
                return btn;
            }
            btn.onClick.AddListener(delegate
            {
                onClick(go.name);
                if (isScaleAnim)
                {
                    BtnScaleAnim(btn.gameObject, 1.1f, 0.95f);
                }
                PageMgr.PlayButtonSound();
            });
        }
        return btn;
    }
    /// <summary>
    /// 给对象添加组件
    /// </summary>
    /// <typeparam name="T">需要添加组件类型</typeparam>
    /// <param name="t">组件</param>
    /// <param name="path">路径</param>
    /// <returns></returns>
    protected T AddCompentInChildren<T>(T t, string path) where T : MonoBehaviour
    {
        T btn = null;
        if (this.transform != null)
        {
            var go = this.transform.Find(path);
            if (go != null)
            {
                btn = go.gameObject.AddComponent<T>();
            }
        }
        return btn;
    }

    /// <summary>
    /// UItest文本显示函数   算百分比
    /// </summary>
    /// <param name="number">小数值</param>
    /// <param name="number1">大数值</param>
    /// <returns></returns>
    public float AddPercentage(int number, int number1)
    {
        decimal numberD = number;
        decimal number1D = number1;
        decimal percentage = decimal.Parse((numberD / number1D).ToString("0.000"));

        return (float)percentage;
    }


    public static void BtnScaleAnim(GameObject btnGo, float bigScale, float smallScale)
    {
        Transform trans = btnGo.transform;
        Vector3 scaleOrg = Vector3.one;
        Vector3 scale = scaleOrg;
        Tween twChangeBig = DOTween.To(() => scale, x => scale = x, scaleOrg * bigScale, 0.1f);
        twChangeBig.SetEase(Ease.InSine);
        twChangeBig.OnUpdate(() =>
        {
            trans.localScale = scale;
        });

        twChangeBig.OnComplete(() =>
        {
            Tween twChangeSmall = DOTween.To(() => scale, x => scale = x, scaleOrg * smallScale, 0.1f);
            twChangeSmall.SetEase(Ease.InSine);

            twChangeSmall.OnUpdate(() =>
            {
                trans.localScale = scale;
            });

            twChangeSmall.OnComplete(() =>
            {
                trans.localScale = scaleOrg;
            });
        });
    }

    public static bool IsActivePage()
    {

        foreach (var item in PageMgr.allPages.Values)
        {
            if (item.isActive() == true)
            {
                if (item.name == "UIMainPage")
                {
                    break;
                }
                else
                {
                    return false;
                }   
            }
        }
        return true;   
    }

    public static void GetTransPrefabAllTextShow(Transform transform, bool includeInactive = false)
    {
        var textTrans = transform.GetComponentsInChildren<Text>(includeInactive);
        foreach (var item in textTrans)
        {
            string a = item.text.Substring(0, 1);
            if (a == "#"||a =="#")
            {
                GetTransPrefabText(item);
            }
        }
    }
    /// <summary>
    /// 获取语言表对应文字
    /// </summary>
    /// <param name="textGB">Text</param>
    /// <returns></returns>
    public static string GetTransPrefabText(Text textGB)
    {
        string textID = textGB.text.Remove(0, 1);   //获取对应ID 
        string str = GetL10NString(textID);//获取对应ID的文本
        textGB.text = str;
        return str;
    }
    /// <summary>
    /// 获取对应ID的文本
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string GetL10NString(string str)
    {
        str = GlobalDataManager.GetInstance().i18n.GetL10N(str);//获取对应ID的文本
        return str;
    }

    /// <summary>
    /// 获取对应ID的文本
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string TAGetL10NString(string str)
    {
        var cell = Config.translateConfig.getInstace().getCell(str);
#if UNITY_EDITOR
        if (cell == null)
        {
            string e = string.Format("警告：ID={0} 在语言表translate中不存在", str);
            throw new System.Exception(e);
        }
#else
            if (cell == null)
            {
                return null;
            }
#endif
        ;

        //获取对应ID的文本
        return cell.zh_CN;
    }
    public override void Tick(int deltaTimeMS)
    {

    }
    /// <summary>
    /// 3D场景飘字
    /// </summary>
    /// <param name="graphic"></param>
    public static void FlyTo(Graphic graphic)
    {
        RectTransform rt = graphic.rectTransform;
        Color c = graphic.color;
        c.a = 0;
        graphic.color = c;
        Sequence mySequence = DOTween.Sequence();
        Tweener move1 = rt.DOMoveY(rt.position.y + 50, 0.5f);
        Tweener move2 = rt.DOMoveY(rt.position.y + 100, 0.5f);
        Tweener alpha1 = graphic.DOColor(new Color(c.r, c.g, c.b, 1), 0.5f);
        Tweener alpha2 = graphic.DOColor(new Color(c.r, c.g, c.b, 0), 0.5f);
        mySequence.Append(move1);
        mySequence.Join(alpha1);
        mySequence.AppendInterval(1);
        mySequence.Append(move2);
        mySequence.Join(alpha2);
    }

    public static void SwitchButtonUnclickable(Button button, bool isBool)
    {
        switch (isBool)
        {
            case true:
                button.transform.Find("ButtonBg_1").gameObject.SetActive(true);
                button.transform.Find("ButtonBg_2").gameObject.SetActive(false);
                button.enabled = true;
                break;
            case false:
                button.transform.Find("ButtonBg_1").gameObject.SetActive(false);
                button.transform.Find("ButtonBg_2").gameObject.SetActive(true);
                button.enabled = false;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 按钮升级后属性升级后的特效播放
    /// </summary>
    /// <param name="transform"></param>
    public void UIButtonEffectSimplePlayer(Transform transform)
    {
        //transform.gameObject.SetActive(true);
        //SimpleParticle particlePlayer = new SimpleParticle();
        //particlePlayer.Init(transform.gameObject);
        ////particlePlayer.Pause();
        //particlePlayer.Play();

        //gameObject.GetCompoment<ParticleSystem>().Play(); 播放
        //gameObject.GetCompoment<ParticleSystem>().Pause(); 暂停
        //gameObject.GetCompoment<ParticleSystem>().Stop(); 停止

        //GameObject go = new GameObject();
        //go.transform.DOMoveZ(0.1f, 0.3f).OnComplete(new TweenCallback(delegate
        //{
        //    transform.gameObject.SetActive(false);
        //}));
    }
    int restrictTime = 0;
    /// <summary>
    /// 限制长按事件调用方法的频率
    /// </summary>
    public bool RestrictLongPressTime() {
        //if (restrictTime > 0)
        //{
        //    restrictTime -= 1;
        //    return false;
        //}
        //restrictTime = 0;
        return true;
    }
}
