using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Views
{
    [RequireComponent(typeof(Image), typeof(Button))]
    public class ChangeImageOnClick : MonoBehaviour
    {
        private Button _button;
        private Image _image;
        [SerializeField] private Sprite _fromSprite;
        [SerializeField] private Sprite _toSprite;


        private void Awake()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
            _image.sprite = _fromSprite;
        }
        
        private void Start()
        {
            _button.onClick.AddListener(() =>
            {
                _image.sprite = _toSprite;
            });
        }
        
        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}