using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.MiniGame;
using Game.GlobalData;

namespace Game
{
    public class GameSoundManager : SoundManager
    {

        public override void PlayBGMusic(string clipPath)
        {
            if (CouldPlay())
            {
                base.PlayBGMusic(clipPath);
            }
        }

        public override void PlayBGMusicAsync(string clipPath)
        {
            if (CouldPlay())
            {
                base.PlayBGMusicAsync(clipPath);
            }
        }

        public override void PlaySound(string clipPath)
        {
            if (CouldPlay())
            {
                base.PlaySound(clipPath);
            }
        }

        public override void PlaySoundAsync(string clipPath)
        {
            if (CouldPlay())
            {
                base.PlaySoundAsync(clipPath);
            }
        }

        protected bool CouldPlay()
        {
            return GlobalDataManager.GetInstance().playerData.isSound;
        }
    }
}
