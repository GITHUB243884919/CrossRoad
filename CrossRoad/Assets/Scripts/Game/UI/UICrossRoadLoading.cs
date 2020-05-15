using Game;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
//using Tools;
using UnityEngine;
using UnityEngine.UI;

public class UICrossRoadLoading : UIPage
{
    private Slider m_Slider;
    private Text m_AlertText;
    private Text m_ValueText;
    private Image m_TitleImage;
    private int m_CurCount;
    private int m_MaxCount;
    private int TableCount;
    private int SoundCount;
    private int EffectCount;
    private int TextureCount;
    private Text m_VersionText;
    private float sliderValue;

    public UICrossRoadLoading() : base(UIType.PopUp, UIMode.DoNothing, UICollider.None)
    {
        uiPath = "uiprefab/UICrossRoadLoading";
    }

    public override void Awake(GameObject go)
    {
        base.Awake(go);
        this.RegistCompent();
        GetTransPrefabAllTextShow(this.transform);

    }

    private void RegistCompent()
    {
        m_Slider = this.RegistCompent<Slider>("Slider");
        m_Slider.value = 0;
        m_ValueText = this.RegistCompent<Text>("Slider/FillArea/ValueText");

        m_AlertText = this.RegistCompent<Text>("AlertText");
        m_TitleImage = this.RegistCompent<Image>("title");
        m_VersionText = this.RegistCompent<Text>("versions");
    }

    public override void Refresh()
    {
        base.Refresh();
        m_TitleImage.SetNativeSize();
    }

    public override void Active()
    {
        base.Active();
    }

    public override void Hide()
    {
        base.Hide();
    }

    public void SliderValueLoading(float value)
    {
        sliderValue += value;
        if (sliderValue>1)
        {
            sliderValue = 1;
        }
        m_Slider.value = sliderValue;

        int number = (int)(sliderValue * 100f);
        m_ValueText.text = number + "%";
        //Logger.LogWarp.LogError("测试： 场景加载  value="+ sliderValue);
    }
}
