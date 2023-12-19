using AnnulusGames.LucidTools.Audio;
using App.Scripts.Models.Statics;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Views
{
    [RequireComponent(typeof(Image), typeof(Button))]
    public class DoorView : MonoBehaviour
    {
        private Image _image;
        private Button _button;
        [SerializeField] private Sprite _closeSprite;
        [SerializeField] private Sprite _openSprite;
        [SerializeField] private AudioClip _walkSE;
        [SerializeField] private AudioClip _doorOpenSE;
        
        public bool IsOpened => _isOpened;
        private bool _isOpened;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _button = GetComponent<Button>();
            _image.sprite = _closeSprite;
        }

        public void Open()
        {
            if(_isOpened) return;
            _isOpened = true;
            LucidAudio.PlaySE(_doorOpenSE).SetTimeSamples();
            _image.sprite = _openSprite;
            GameBGMAudio.FadeBgmUp();

            _button.OnClickAsObservable().First().Subscribe(_ =>
            {
                LucidAudio.PlaySE(_walkSE).SetTimeSamples();
                StaticLevel.NextLevel();
            }).AddTo(this);
        }
    }
}