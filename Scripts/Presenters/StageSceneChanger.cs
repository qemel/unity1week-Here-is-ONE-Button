using System.Collections.Generic;
using App.Scripts.Views;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Presenters
{
    public class StageSceneChanger : MonoBehaviour
    {
        [SerializeField] private List<StageScene> _stageScenes;

        [CanBeNull] [SerializeField] private Button _buttonBack;
        [CanBeNull] [SerializeField] private Button _buttonNext;

        private int _currentStageSceneIndex = 0;

        private void Start()
        {
            foreach (var stageScene in _stageScenes)
            {
                stageScene.gameObject.SetActive(false);
            }

            _stageScenes[0].gameObject.SetActive(true);

            if (_buttonBack != null)
                _buttonBack.onClick.AddListener(() =>
                {
                    _stageScenes[_currentStageSceneIndex].gameObject.SetActive(false);
                    if (_currentStageSceneIndex == 0) _currentStageSceneIndex = _stageScenes.Count - 1;
                    else _currentStageSceneIndex--;
                    _stageScenes[_currentStageSceneIndex].gameObject.SetActive(true);
                });

            if (_buttonNext != null)
                _buttonNext.onClick.AddListener(() =>
                {
                    _stageScenes[_currentStageSceneIndex].gameObject.SetActive(false);
                    if (_currentStageSceneIndex == _stageScenes.Count - 1) _currentStageSceneIndex = 0;
                    else _currentStageSceneIndex++;
                    _stageScenes[_currentStageSceneIndex].gameObject.SetActive(true);
                });
        }

        private void OnDestroy()
        {
            if (_buttonBack != null) _buttonBack.onClick.RemoveAllListeners();
            if (_buttonNext != null) _buttonNext.onClick.RemoveAllListeners();
        }
    }
}