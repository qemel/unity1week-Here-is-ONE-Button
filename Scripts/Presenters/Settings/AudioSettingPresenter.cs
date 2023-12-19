using System;
using AnnulusGames.LucidTools.Audio;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Presenters.Settings
{
    public class AudioSettingPresenter : MonoBehaviour
    {
        [SerializeField] private Slider _seSlider;
        [SerializeField] private Slider _bgmSlider;

        private async UniTaskVoid Start()
        {
            await UniTask.DelayFrame(1);
            _seSlider.onValueChanged.AddListener(OnSEVolumeChanged);
            _bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
            await UniTask.DelayFrame(1);
            _seSlider.value = PlayerPrefs.GetFloat("SEVolume", 0.3f);
            _bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.3f);
            OnSEVolumeChanged(_seSlider.value);
            OnBGMVolumeChanged(_bgmSlider.value);
        }

        private static void OnSEVolumeChanged(float value)
        {
            if (value < 0f || value > 1f) throw new ArgumentException(nameof(value));
            PlayerPrefs.SetFloat("SEVolume", value);
            LucidAudio.SEVolume = value;
        }

        private static void OnBGMVolumeChanged(float value)
        {
            if (value < 0f || value > 1f) throw new ArgumentException(nameof(value));
            PlayerPrefs.SetFloat("BGMVolume", value);
            LucidAudio.BGMVolume = value;
        }

        private void OnDestroy()
        {
            _seSlider.onValueChanged.RemoveAllListeners();
            _bgmSlider.onValueChanged.RemoveAllListeners();
        }
    }
}