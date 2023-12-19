using AnnulusGames.LucidTools.Audio;
using App.Scripts.Models.Statics;
using App.Scripts.Utils;
using UnityEngine;

namespace App.Scripts.Views
{
    public class GameBGMAudio : Singleton<GameBGMAudio>
    {
        [SerializeField] private AudioClip _gameBGM1;
        [SerializeField] private AudioClip _gameBGM2;
        [SerializeField] private AudioClip _gameBGM2_2;

        [SerializeField] private AudioClip _gameBGMTruth;

        [SerializeField] private AudioClip _gameBGMEnding;


        private const int TruthLevel = 8;

        private void Start()
        {
            switch (StaticLevel.CurrentLevel.Value)
            {
                case 0:
                    break;
                case TruthLevel:
                    FadeBgmTruth();
                    break;
                case > TruthLevel:
                    return;
                default:
                    FadeBgmDown();
                    break;
            }
        }

        public static void FadeBgmUp()
        {
            if (StaticLevel.CurrentLevel.Value >= TruthLevel) return;
            var lowerLevel = StaticLevel.CurrentLevel.Value <= 4;
            if (lowerLevel)
            {
                StaticBgm.BgmFront.FadeVolume(1, 0.5f);
            }
            else
            {
                StaticBgm.BgmFront2.FadeVolume(1, 0.5f);
            }
        }

        public void FadeBgmTruth()
        {
            StaticBgm.BgmBack.FadeVolume(0, 0.5f, () =>
            {
                StaticBgm.BgmBack.Stop();
                StaticBgm.BgmBack = LucidAudio.PlayBGM(_gameBGMTruth).SetLoop();
                StaticBgm.BgmBack.FadeVolume(1, 1f);
            });
            StaticBgm.BgmFront.FadeVolume(0, 0.5f);
            StaticBgm.BgmFront2.FadeVolume(0, 0.5f);
        }

        public void FadeBgmDown()
        {
            StaticBgm.BgmFront.FadeVolume(0, 0.5f);
            StaticBgm.BgmFront2.FadeVolume(0, 0.5f);
        }

        public void GoEnding()
        {
            StaticBgm.BgmBack.FadeVolume(0, 0.5f);
            StaticBgm.BgmFront.FadeVolume(0, 0.5f);
            StaticBgm.BgmFront2.FadeVolume(0, 0.5f);
            StaticBgm.BgmBack = LucidAudio.PlayBGM(_gameBGMEnding).SetLoop();
            StaticBgm.BgmBack.FadeVolume(1, 1f);
        }

        public void GoTitle()
        {
            StaticBgm.BgmBack = LucidAudio.PlayBGM(_gameBGM1).SetLoop();
            StaticBgm.BgmFront = LucidAudio.PlayBGM(_gameBGM2).SetVolume(0).SetLoop();
            StaticBgm.BgmFront2 = LucidAudio.PlayBGM(_gameBGM2_2).SetVolume(0).SetLoop();
            
            StaticBgm.BgmBack.FadeVolume(1, 0.5f);
            StaticBgm.BgmFront.FadeVolume(0, 0.5f);
            StaticBgm.BgmFront2.FadeVolume(0, 0.5f);
        }
    }
}