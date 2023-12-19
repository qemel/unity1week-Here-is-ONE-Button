using App.Scripts.Models.Statics;
using TMPro;
using UnityEngine;

namespace App.Scripts.Views
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class StageText : MonoBehaviour
    {
        private TextMeshProUGUI _textMeshProUgui;
        private void Awake()
        {
            _textMeshProUgui = GetComponent<TextMeshProUGUI>();
            _textMeshProUgui.text = "Stage " + (StaticLevel.CurrentLevel.Value);
        }
    }
}