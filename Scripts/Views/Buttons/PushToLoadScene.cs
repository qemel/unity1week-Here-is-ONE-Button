using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace App.Scripts.Views.Buttons
{
    [RequireComponent(typeof(Button))]
    public class PushToLoadScene : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private string _sceneName;
        private Button _button;


        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() =>
            {
                if (_duration <= 0f)
                {
                    SceneManager.LoadScene(_sceneName);
                    return;
                }

                FadeManager.Instance.LoadScene(_sceneName, _duration);
            });
        }
        
        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}