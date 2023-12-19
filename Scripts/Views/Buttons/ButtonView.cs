using AnnulusGames.LucidTools.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Views.Buttons
{
    [RequireComponent(typeof(Image))]
    public class ButtonView : MonoBehaviour
    {
        private Image _image;
        [SerializeField] private Sprite _fromSprite;
        [SerializeField] private Sprite _toSprite;
        [SerializeField] private AudioClip _pushedSE;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _image.sprite = _fromSprite;
        }

        public void SetImage(bool isPushedSprite)
        {
            _image.sprite = isPushedSprite ? _toSprite : _fromSprite;
            if (isPushedSprite) LucidAudio.PlaySE(_pushedSE).SetTimeSamples();
        }
    }
}