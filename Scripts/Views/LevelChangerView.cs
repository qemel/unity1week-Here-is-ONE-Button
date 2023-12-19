using System;
using App.Scripts.Models.Statics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Views
{
    public class LevelChangerView : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _tmpro;
        [SerializeField] private Button _toLastButton;

        private void Start()
        {
            _toLastButton.onClick.AddListener(() =>
            {
                GameBGMAudio.I.FadeBgmTruth();
                StaticLevel.SetLevel(12);
            });

            var reached = PlayerPrefs.GetInt("Reached12", 0) == 1;
            _toLastButton.gameObject.SetActive(reached);
        }

        private void Update()
        {
            if (_tmpro.text != null && _tmpro.text.Length == 1)
            {
                var level = Convert.ToInt32(_tmpro.text);
                StaticLevel.SetLevel(level);
            }
        }
    }
}