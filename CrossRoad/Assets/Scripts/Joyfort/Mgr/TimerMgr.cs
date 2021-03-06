/*
 * @Summary:定时管理器（处理定时任务）
 */

using UnityEngine;
using System.Collections.Generic;
using System;

public class TimerMgr : MonoBehaviour
{
    /// <summary>
    /// 全局实例
    /// </summary>
    private static TimerMgr _Instance = null;

    /// <summary>
    /// 定时器字典
    /// </summary>
    private List<Timer> m_TimerList = new List<Timer>();

    /// <summary>
    ///   增加队列
    /// </summary>
    private List<Timer> m_AddTimerList = new List<Timer>();

    /// <summary>
    ///   销毁队列
    /// </summary>
    private List<Timer> m_DestroyTimerList = new List<Timer>();

    public delegate void TimerManagerHandler();

    public delegate void TimerManagerHandlerArgs(params object[] args);

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// 全局实例
    /// </summary>
    /// -----------------------------------------------------------------------------
    public static TimerMgr Instance
    {
        get
        {
            if (_Instance == null)
            {

                _Instance = FindObjectOfType(typeof(TimerMgr)) as TimerMgr;

            }

            return _Instance;
        }
    }

    // Use this for initialization
    void Awake()
    {
        if (TimerMgr.Instance != null && TimerMgr.Instance != this)
        {
            UnityEngine.Object.Destroy(this);
            return;
        }

        _Instance = this;
    }
    //Update duratiaon value
    public void setTimerDuration(string key, float duratiaon)
    {
        Timer timer = m_TimerList.Find(p=>p.m_Name == key);
        if(timer != null)
        {
            timer.Duration = duratiaon;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_DestroyTimerList.Count > 0)
        {
            ///>从销毁队列中销毁指定内容
            ///
            for (int i = 0; i < m_DestroyTimerList.Count; i++)
            {
                m_TimerList.Remove(m_DestroyTimerList[i]);
            }
            //foreach (var i in m_DestroyTimerList)
            //{
            //    m_TimerList.Remove(i);
            //}

            //清空
            m_DestroyTimerList.Clear();
        }

        if (m_AddTimerList.Count > 0)
        {
            for (int i = 0; i < m_AddTimerList.Count; i++)
            {
                
                if (m_AddTimerList[i] == null)
                {
                    continue;
                }

                if (m_TimerList.Contains(m_AddTimerList[i]))
                {
                    m_TimerList.Remove(m_AddTimerList[i]);

                }
                m_TimerList.Add(m_AddTimerList[i]);
            }
            ///>从增加队列中增加定时器
            //foreach (var i in m_AddTimerList)
            //{
            //    if (i == null)
            //    {
            //        continue;
            //    }

            //    if (m_TimerList.Contains(i))
            //    {
            //        m_TimerList.Remove(i);
                    
            //    }
            //    m_TimerList.Add(i);
            //}

            //清空
            m_AddTimerList.Clear();
        }

        if (m_TimerList.Count > 0)
        {
            for (int i = 0; i < m_TimerList.Count; i++)
            {
                if (m_TimerList[i] == null || m_TimerList[i].Duration == 0)
                {
                    return;
                }

                m_TimerList[i].Run();
            }
            //响应定时器
            //foreach (Timer timer in m_TimerList)
            //{
            //    if (timer == null || timer.Duration == 0)
            //    {
            //        return;
            //    }

            //    timer.Run();
            //}
        }
    }

    internal void AddTimerRepeat(string v1, object timeShow, int v2)
    {
        throw new NotImplementedException();
    }

    void LateUpdate()
    {
        if (m_TimerList.Count > 0)
        {
            //响应定时器
            //foreach (Timer timer in m_TimerList)
            //{
            //    if (timer != null && timer.Duration == 0)
            //    {
            //        // JLog.LogError("timer.Duration" + timer.Duration);
            //        timer.Run();
            //    }
            //}
            for (int i = 0; i < m_TimerList.Count; i++)
            {
                if (m_TimerList[i] != null || m_TimerList[i].Duration == 0)
                {
                    m_TimerList[i].Run();
                }
            }
        }
    }

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// 增加定时器
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    /// -----------------------------------------------------------------------------
    public bool AddTimer(string key, float duration, TimerManagerHandler handler)
    {
        return Internal_AddTimer(key, TIMER_MODE.NORMAL, duration, handler);
    }

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// 增加持续定时器
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    /// -----------------------------------------------------------------------------
    public bool AddTimerRepeat(string key, float duration, TimerManagerHandler handler)
    {
        return Internal_AddTimer(key, TIMER_MODE.REPEAT, duration, handler);
    }

    public bool AddTimer(string key, float duration, TimerManagerHandlerArgs handler, params object[] args)
    {
        return Internal_AddTimer(key, TIMER_MODE.NORMAL, duration, handler, 0, args);
    }

    public bool AddTimerRepeat(string key, float duration, TimerManagerHandlerArgs handler, params object[] args)
    {
        return Internal_AddTimer(key, TIMER_MODE.REPEAT, duration, handler, 0, args);
    }

    public bool AddTimerRepeat(string key, float duration, TimerManagerHandlerArgs handler, int times, params object[] args)
    {
        return Internal_AddTimer(key, TIMER_MODE.REPEAT, duration, handler, times, args);
    }

    /// <summary>
    /// 暂停带有前缀的所有定时器
    /// </summary>
    /// <param name="prefix"></param>
    public void BreakTimerWithPrefix(string prefix)
    {
        if (m_TimerList != null && m_TimerList.Count > 0)
        {
            var list = m_TimerList.FindAll(p => p.m_Name.StartsWith(prefix));

            if(list != null && list.Count > 0)
            {
                foreach(var item in list)
                {
                    BreakTimer(item.m_Name);
                }
            }
        }
    }

    /// <summary>
    /// 暂停计时器
    /// </summary>
    public void BreakTimer(string key)
    {
        Timer timer = m_TimerList.Find(p=>p.m_Name == key);
        if(timer != null)
        timer.Break();
    }

    /// <summary>
    /// 重启带有前缀的所有定时器
    /// </summary>
    /// <param name="prefix"></param>
    public void ResumeTimerWithPrefix(string prefix)
    {
        if (m_TimerList != null && m_TimerList.Count > 0)
        {
            var list = m_TimerList.FindAll(p => p.m_Name.StartsWith(prefix));

            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    ResumeTimer(item.m_Name);
                }
            }
        }
    }


    /// <summary>
    /// 重启计时器
    /// </summary>
    public void ResumeTimer(string key)
    {
        Timer timer = m_TimerList.Find(p=>p.m_Name == key);
        if(timer != null)
        timer.Resume();
    }

    /// <summary>
    /// 销毁带有前缀的所有定时器
    /// </summary>
    /// <param name="prefix"></param>
    public void ClearTimerWithPrefix(string prefix)
    {
        if (m_TimerList != null && m_TimerList.Count > 0)
        {
            foreach (var timerKey in m_TimerList)
            {
                if (timerKey.m_Name.StartsWith(prefix))
                {
                    Destroy(timerKey.m_Name);
                }
            }
        }
    }

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// 销毁指定定时器
    /// </summary>
    /// <param name="key">标识符</param>
    /// <returns></returns>
    /// -----------------------------------------------------------------------------
    public bool Destroy(string key)
    {
        var timer = m_TimerList.Find(p => p.m_Name == key);
        if(timer == null)
        {
            return false;
        }

        if (m_DestroyTimerList != null && !m_DestroyTimerList.Contains(timer))
        {
            m_DestroyTimerList.Add(timer);
        }

        return true;
    }

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// 增加定时器
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    /// -----------------------------------------------------------------------------
    private bool Internal_AddTimer(string key, TIMER_MODE mode, float duration, TimerManagerHandler handler)
    {
        if (string.IsNullOrEmpty(key))
        {
            return false;
        }

        if (duration < 0.0f)
        {
            return false;
        }

        var oldtimer = m_AddTimerList.Find(p => p.m_Name == key);
        if (oldtimer != null)
        {
            oldtimer.Copy(key, mode, Time.time, duration, handler, this);
        }
        else
        {
            Timer timer = new Timer(key, mode, Time.time, duration, handler, this);
            m_AddTimerList.Add(timer);
        }
       

        return true;
    }

    private bool Internal_AddTimer(string key, TIMER_MODE mode, float duration, TimerManagerHandlerArgs handler, int times = 0, params object[] args)
    {
        if (string.IsNullOrEmpty(key))
        {
            return false;
        }

        if (duration < 0.0f)
        {
            return false;
        }

        var olderTimer = m_AddTimerList.Find(p => p.m_Name == key);
        if (olderTimer != null)
        {
            olderTimer.Copy(key, mode, Time.time, duration, handler, times, this, args);
        }
        else
        {
            Timer timer = new Timer(key, mode, Time.time, duration, handler, times, this, args);
            m_AddTimerList.Add(timer);
        }
        

        return true;
    }

    public bool IsRunning(string key)
    {
        return m_TimerList.Find(p=>p.m_Name == key) != null;
    }

    /// -----------------------------------------------------------------------------
    /// <summary>
    ///  定时器模式
    /// </summary>
    /// -----------------------------------------------------------------------------
    private enum TIMER_MODE
    {
        NORMAL,
        REPEAT,
    }

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// 获取指定定时器剩余时间
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    /// -----------------------------------------------------------------------------
    public float GetTimerLeft(string key)
    {
        var timer = m_TimerList.Find(p => p.m_Name == key);
        if (timer == null)
        {
            return 0.0f;
        }
        return timer.TimeLeft;
    }

    /// <summary>
    /// 获取带有前缀的定时器剩余时间
    /// </summary>
    /// <param name="prefix"></param>
    public float GetTimerLeftWithPrefix(string prefix)
    {
        if (m_TimerList != null && m_TimerList.Count > 0)
        {
            var list = m_TimerList.FindAll(p => p.m_Name.StartsWith(prefix));

            if (list != null && list.Count > 0)
            {
                foreach(var item in list)
                {
                    if(item.m_Name.StartsWith(prefix))
                    {
                        return GetTimerLeft(item.m_Name);
                    }
                }
            }
        }

        return 0.0f;
    }

    private class Timer
    {
        /// <summary>
        ///   名称
        /// </summary>
        public string m_Name;

        /// <summary>
        ///   模式
        /// </summary>
        private TIMER_MODE m_Mode;

        /// <summary>
        ///   开始时间
        /// </summary>
        private float m_StartTime;

        /// <summary>
        ///   时长
        /// </summary>
        private float m_duration;

        /// <summary>
        ///  中断
        /// </summary>
        private bool m_Break = false;

        /// <summary>
        ///  中断开始时间
        /// </summary>
        private float m_BreakStart;

        /// <summary>
        ///  中断开始时间
        /// </summary>
        private float m_BreakDuration = 0;

        /// <summary>
        ///   定时器委托
        /// </summary>
        private TimerManagerHandler m_TimerEvent;

        private TimerManagerHandlerArgs m_TimerArgsEvent;

        private TimerMgr m_Manger;

        private object[] m_Args = null;
        /// <summary>
        /// 作用次数
        /// </summary>
        private int m_Times;
        /// <summary>
        /// 当前作用次数
        /// </summary>
        private int m_CurrentTimes;

        public int Times
        {
            get { return m_Times; }
            set { m_Times = value; }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// 开始时间
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// -----------------------------------------------------------------------------
        public float StartTime
        {
            get
            {
                return m_StartTime;
            }
            set
            {
                m_StartTime = value;
            }
        }

        public float Duration
        {
            get
            {
                return m_duration;
            }
            set
            {
                m_duration = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// 剩余时间
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// -----------------------------------------------------------------------------
        public float TimeLeft
        {
            get
            {
                return Mathf.Max(0.0f, m_duration - (Time.time - m_StartTime) + m_BreakDuration);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// -----------------------------------------------------------------------------
        public Timer(string name, TIMER_MODE mode, float startTime, float duration, TimerManagerHandler handler, TimerMgr manager)
        {
            m_Name = name;
            m_Mode = mode;
            m_StartTime = startTime;
            this.Duration = duration;
            m_TimerEvent = handler;
            m_Manger = manager;
        }

        public Timer(string name, TIMER_MODE mode, float startTime, float duration, TimerManagerHandlerArgs handler, int times, TimerMgr manager, params object[] args)
        {
            m_Name = name;
            m_Mode = mode;
            m_StartTime = startTime;
            this.Duration = duration;
            m_TimerArgsEvent = handler;
            m_Manger = manager;
            m_Args = args;
            m_Times = times;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// 运行事件
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// -----------------------------------------------------------------------------
        public void Run()
        {
            if (m_Break)
            {
                return;
            }

            if (this.TimeLeft > 0.0f)
            {
                return;
            }

            if (this.m_TimerEvent != null)
            {
                this.m_TimerEvent();
            }

            if (this.m_TimerArgsEvent != null)
            {
                this.m_TimerArgsEvent(m_Args);
            }

            if (m_Mode == TIMER_MODE.NORMAL)
            {
                m_Manger.Destroy(this.m_Name);
            }
            else
            {
                m_StartTime = Time.time;
                m_BreakDuration = 0;

                if (Times > 0)
                {
                    m_CurrentTimes++;
                    if (m_CurrentTimes >= Times)
                    {
                        m_Manger.Destroy(this.m_Name);
                        m_CurrentTimes = 0;
                    }
                }

            }
            return;
        }

        public void Break()
        {
            if (m_Break)
            {
                return;
            }

            m_Break = true;
            m_BreakStart = Time.time;
        }

        public void Resume()
        {
            if (!m_Break)
            {
                return;
            }

            m_BreakDuration += (Time.time - m_BreakStart);
            m_Break = false;
        }

        public void Copy(string name, TIMER_MODE mode, float startTime, float duration, TimerManagerHandler handler, TimerMgr manager)
        {
            m_Name = name;
            m_Mode = mode;
            m_StartTime = startTime;
            this.Duration = duration;
            m_TimerEvent = handler;
            m_Manger = manager;
        }
        public void Copy(string name, TIMER_MODE mode, float startTime, float duration, TimerManagerHandlerArgs handler, int times, TimerMgr manager, params object[] args)
        {
            m_Name = name;
            m_Mode = mode;
            m_StartTime = startTime;
            this.Duration = duration;
            m_TimerArgsEvent = handler;
            m_Manger = manager;
            m_Args = args;
            m_Times = times;
        }
    }
}
