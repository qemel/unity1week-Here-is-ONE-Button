using System.Collections;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace App.Scripts.Views
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class PressRToRetryText : MonoBehaviour
    {
        private TextMeshProUGUI _textMeshProUgui;

        private void Awake()
        {
            _textMeshProUgui = GetComponent<TextMeshProUGUI>();
            _textMeshProUgui.text = "Press R to Retry";
            _textMeshProUgui.color = new Color(0, 0, 0, 0);
            gameObject.SetActive(false);
        }

        public async UniTaskVoid SetActive(bool isActive = true)
        {
            gameObject.SetActive(isActive);
            await UniTask.WaitForSeconds(1f);
            _textMeshProUgui.DOFade(isActive ? 1f : 0, 0.5f);
        }
    }
}