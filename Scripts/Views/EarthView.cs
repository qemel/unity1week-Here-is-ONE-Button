using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Views
{
    public class EarthView : MonoBehaviour
    {
        [SerializeField] private Image _image;

        [SerializeField] private Sprite[] _sprites;

        public void ChangeSprite(int index)
        {
            _image.sprite = _sprites[index];
        }
        
        public void DeleteSprite()
        {
            _image.sprite = null;
            _image.color = Color.clear;
        }
    }
}