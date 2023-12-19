using System;
using App.Scripts.Models;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Views.Buttons
{
    [RequireComponent(typeof(ButtonView))]
    public class GameButtonGreen : MonoBehaviour, IGameButton
    {
        private ButtonView _buttonView;
        private Button _button;

        public IObservable<Unit> OnClickAsObservable =>
            _button.OnClickAsObservable().Where(_ => CanPush).TakeUntilDestroy(this);

        public bool CanPush { get; private set; } = true;
        public bool IsActive => gameObject.activeInHierarchy;
        public ButtonType ButtonType => ButtonType.Green;
        public void Destroy() => Destroy(gameObject);
        public GameObject Entity => gameObject;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _buttonView = GetComponent<ButtonView>();
            OnClickAsObservable.Subscribe(_ =>
            {
                // 1フレーム後に押せなくする
                Observable.TimerFrame(1).Subscribe(_ => SetPush(false)).AddTo(this);
            }).AddTo(this);
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void SetPush(bool canPush)
        {
            CanPush = canPush;
            _buttonView.SetImage(!canPush);
        }
    }
}