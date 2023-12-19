using AnnulusGames.LucidTools.Audio;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Views
{
    [RequireComponent(typeof(Image), typeof(Button))]
    public class LastDoorView : MonoBehaviour
    {
        private Image _image;
        private Button _button;
        [SerializeField] private Sprite _closeSprite;
        [SerializeField] private Sprite _openSprite;
        [SerializeField] private AudioClip _walkSE;
        [SerializeField] private AudioClip _doorOpenSE;

        [SerializeField] private string _toSceneName;


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
            if (_isOpened) return;
            _isOpened = true;
            LucidAudio.PlaySE(_doorOpenSE).SetTimeSamples();
            _image.sprite = _openSprite;

            _button.OnClickAsObservable().First().Subscribe(_ =>
            {
                LucidAudio.PlaySE(_walkSE).SetTimeSamples();
                FadeManager.Instance.LoadScene(_toSceneName, 3f);
            }).AddTo(this);
        }

        public void ChangeToSceneName(string sceneName)
        {
            _toSceneName = sceneName;
        }
    }
}