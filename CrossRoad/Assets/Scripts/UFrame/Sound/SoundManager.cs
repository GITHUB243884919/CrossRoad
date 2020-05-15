/*******************************************************************
* FileName:     SoundManager.cs
* Author:       Fan Zheng Yong
* Date:         2019-9-29
* Description:  
* other:    
********************************************************************/


using System.Collections;
using System.Collections.Generic;
using UFrame.Common;
using UnityEngine;
using UnityEngine.Networking;

namespace UFrame.MiniGame
{
    public class SoundManager : SingletonMono<SoundManager>
    {
        AudioSource BGMusicSource = null;
        AudioSource normalSource = null;
        public override void Awake()
        {
            base.Awake();
            BGMusicSource = gameObject.AddComponent<AudioSource>();
            BGMusicSource.loop = true;

            normalSource = gameObject.AddComponent<AudioSource>();
        }

        public virtual void PlayBGMusic(string clipPath)
        {
            BGMusicSource.clip = ResourceManager.GetInstance().LoadObject<AudioClip>(clipPath);
            BGMusicSource.Play();
        }

        public virtual void PlayBGMusicAsync(string clipPath)
        {
            ResourceManager.GetInstance().LoadObjectAsync(clipPath, OnPlayBGMusicAsyncCallback);
        }

        protected void OnPlayBGMusicAsyncCallback(Object obj)
        {
            BGMusicSource.clip = obj as AudioClip;
            BGMusicSource.Play();
        }

        public void StopBGMusic()
        {
            BGMusicSource.Stop();
        }

        public virtual void PlaySound(string clipPath)
        {
            var clip = ResourceManager.GetInstance().LoadObject<AudioClip>(clipPath);
            normalSource.PlayOneShot(clip);
        }

        public virtual void PlaySoundAsync(string clipPath)
        {
            ResourceManager.GetInstance().LoadObjectAsync(clipPath, OnPlaySoundAsyncCallback);
        }

        protected void OnPlaySoundAsyncCallback(Object obj)
        {
            var clip = obj as AudioClip;
            normalSource.PlayOneShot(clip);
        }

        public void StopSound()
        {
            normalSource.Stop();
        }

    }
}

