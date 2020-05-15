using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour {
    
    private float f_UpdateInterval = 0.5f;
    private float f_LastInterval;
    private int i_Frames = 0;
    public float fps;
    private string time;
    private string htmlColorTag;
    private string m_FPS;

    void Awake()
    {
       // Application.targetFrameRate = 30;
    }

    // Use this for initialization
    void Start()
    {
        f_LastInterval = Time.realtimeSinceStartup;
        i_Frames = 0;
    }

    // Update is called once per frame
    void Update()
    {
        i_Frames += 1;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > f_LastInterval + f_UpdateInterval)
        {
            // Debug.Log("update:"+i_Frames);
            time = System.DateTime.Now.ToLongTimeString().ToString();
            fps = i_Frames / (timeNow - f_LastInterval);
            float ms = 1000.0f / Mathf.Max(fps, 0.00001f);

            if (fps < 30)
                htmlColorTag = "<color=red>";
            else if (fps < 50)
                htmlColorTag = "<color=yellow>";
            else
                htmlColorTag = "<color=#00ff00>";

            string format = System.String.Format(htmlColorTag + time + "\nFPS:{0:F2} </color>\n" + htmlColorTag + "M S:{1:F2}</color>", fps, ms);
            m_FPS = format;

            i_Frames = 0;
            f_LastInterval = timeNow;
        }
    }

    void OnGUI()
    {
        GUIStyle fontStyle = new GUIStyle();
        fontStyle.fontSize = 40;       //字体大小
        GUI.Label(new Rect(Screen.width / 2, 100, 250, 250), "FPS: " + m_FPS, fontStyle);
    }

}
