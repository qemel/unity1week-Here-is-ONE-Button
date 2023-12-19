using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Views
{
    [RequireComponent(typeof(Image), typeof(Button))]
    public class SideDoorView : MonoBehaviour
    {
        private Button _button;
        private Image _image;

        [SerializeField] private Sprite _fromSprite;
        [SerializeField] private Sprite _toSprite;

        [SerializeField] private bool _isOpened;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
            _image.sprite = _fromSprite;
        }

        private void Start()
        {
            _image.sprite = _isOpened ? _toSprite : _fromSprite;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(_isOpened);
            }

            _button.onClick.AddListener(() =>
            {
                _isOpened = !_isOpened;
                _image.sprite = _isOpened ? _toSprite : _fromSprite;
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(_isOpened);
                }
            });
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}