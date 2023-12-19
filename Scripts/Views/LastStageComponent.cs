using System;
using System.Collections.Generic;
using System.Linq;
using AnnulusGames.LucidTools.Audio;
using App.Scripts.Models;
using JetBrains.Annotations;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Views
{
    public class LastStageComponent : MonoBehaviour
    {
        [SerializeField] private LastDoorView _doorView;
        [CanBeNull] [SerializeField] private PressRToRetryText _pressRToRetryText;

        [SerializeField] private AudioClip _healSE;
        [SerializeField] private AudioClip _vanishSE;
        [SerializeField] private AudioClip _transformSE;

        [SerializeField] private EarthView _earthView;

        [SerializeField] private Image _screenSaver;


        private Dictionary<ButtonType, List<IGameButton>> _gameButtonViewsDictionary;

        private void Awake()
        {
            _screenSaver.gameObject.SetActive(false);
            var allButtons = new List<IGameButton>(GetComponentsInChildren<IGameButton>());
            Debug.Log(allButtons.Count);

            // gamebuttonviews に対して、ボタンの種類ごとに分けていく
            _gameButtonViewsDictionary = new Dictionary<ButtonType, List<IGameButton>>();
            foreach (var gameButtonView in allButtons)
            {
                if (!_gameButtonViewsDictionary.ContainsKey(gameButtonView.ButtonType))
                {
                    _gameButtonViewsDictionary.Add(gameButtonView.ButtonType, new List<IGameButton>());
                }

                _gameButtonViewsDictionary[gameButtonView.ButtonType].Add(gameButtonView);
            }

            MessageBroker.Default.Receive<LastEvent>().Subscribe(x =>
            {
                switch (x)
                {
                    case LastEvent.Normal:
                        _doorView.ChangeToSceneName("EndNormal");
                        break;
                    case LastEvent.Earth:
                        _doorView.ChangeToSceneName("EndEarth");
                        break;
                    case LastEvent.White:
                        _doorView.ChangeToSceneName("EndWhite");
                        break;
                    case LastEvent.Red:
                        _doorView.ChangeToSceneName("EndRed");
                        break;
                }
            }).AddTo(this);
        }

        private void Start()
        {
            foreach (var dic in _gameButtonViewsDictionary)
            {
                foreach (var button in dic.Value)
                {
                    button.OnClickAsObservable.Subscribe(_ =>
                    {
                        if (button.ButtonType == ButtonType.Blue) return;
                        if (CanOpen())
                        {
                            GameBGMAudio.I.GoEnding();
                            var lastEvent = button.ButtonType switch
                            {
                                ButtonType.White => LastEvent.White,
                                ButtonType.Green => LastEvent.Normal,
                                ButtonType.Red => LastEvent.Red,
                                ButtonType.Earth => LastEvent.Earth,
                                ButtonType.Blue => LastEvent.Normal,
                                _ => throw new ArgumentOutOfRangeException()
                            };
                            if (lastEvent is LastEvent.White) _earthView.ChangeSprite(1);
                            else if (lastEvent is LastEvent.Red) _earthView.ChangeSprite(2);
                            MessageBroker.Default.Publish(lastEvent);
                            _doorView.Open();
                            DontTouchAnymore();
                        }
                    }).AddTo(this);
                }
            }

            if (_gameButtonViewsDictionary.TryGetValue(ButtonType.White, out var whiteValue))
            {
                foreach (var white in whiteValue)
                {
                    white.OnClickAsObservable.Subscribe(_ => OnPressWhite()).AddTo(this);
                }
            }

            if (_gameButtonViewsDictionary.TryGetValue(ButtonType.Green, out var greenValue))
            {
                foreach (var green in greenValue)
                {
                    green.OnClickAsObservable.Subscribe(_ => OnPressGreen()).AddTo(this);
                }
            }

            if (_gameButtonViewsDictionary.TryGetValue(ButtonType.Blue, out var blueValue))
            {
                foreach (var blue in blueValue)
                {
                    blue.OnClickAsObservable.Subscribe(_ => OnPressBlue(blue)).AddTo(this);
                }
            }
        }

        private void Update()
        {
            if (_doorView.IsOpened) return;
            foreach (var dic in _gameButtonViewsDictionary)
            {
                if (dic.Value.Count == 0) continue;
                foreach (var button in dic.Value)
                {
                    if (button.CanPush) return;
                }
            }

            if (_pressRToRetryText != null) _pressRToRetryText.SetActive().Forget();
        }

        private void OnPressWhite()
        {
            _gameButtonViewsDictionary.TryGetValue(ButtonType.Earth, out var list);
            var isVanish = list != null && list.Count(x => x.IsActive) == 2;

            if (!isVanish) return;

            LucidAudio.PlaySE(_vanishSE).SetTimeSamples();
            MessageBroker.Default.Publish(LastEvent.Vanish);
            _earthView.DeleteSprite();
            _gameButtonViewsDictionary[ButtonType.Earth].ForEach(x => x.Destroy());
            GameBGMAudio.I.GoEnding();
            DontTouchAnymore();
            FadeManager.Instance.LoadScene("EndVanish", 3f);
        }

        private void OnPressGreen()
        {
            var flag = false;
            foreach (var dic in _gameButtonViewsDictionary)
            {
                if (dic.Key == ButtonType.Green) continue;
                foreach (var button in dic.Value)
                {
                    if (!button.IsActive) continue;
                    button.SetPush(true);
                    flag = true;
                }
            }

            if (flag) LucidAudio.PlaySE(_healSE).SetTimeSamples();
        }

        private void OnPressBlue(IGameButton blue)
        {
            var tr = blue.Entity.transform;
            var rot = tr.rotation.eulerAngles.z;
            var ray = new Ray(tr.position, GetDirection(rot));
            // raycast2dでhitしたオブジェクトを取得
            var hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider == null) return;
            var gameButton = hit.collider.GetComponent<IGameButton>();
            if (gameButton == null) return;

            var init = Instantiate(gameButton.Entity, tr.position, tr.rotation, tr.parent);
            var initButton = init.GetComponent<IGameButton>();
            _gameButtonViewsDictionary[gameButton.ButtonType].Add(initButton);
            LucidAudio.PlaySE(_transformSE).SetTimeSamples();
            if (initButton.ButtonType is ButtonType.Earth)
            {
                initButton.OnClickAsObservable.Subscribe(_ =>
                {
                    if (!CanOpen()) return;
                    GameBGMAudio.I.GoEnding();
                    MessageBroker.Default.Publish(LastEvent.Earth);
                    _doorView.Open();
                }).AddTo(this);
            }

            _gameButtonViewsDictionary[ButtonType.Blue].Remove(blue);
            Destroy(tr.gameObject);
        }

        private bool CanOpen()
        {
            var count = 0;
            foreach (var dic in _gameButtonViewsDictionary)
            {
                if (dic.Value.Count == 0) continue;
                count += dic.Value.Count(x => x.IsActive);
            }

            return count == 2;
        }

        private void DontTouchAnymore()
        {
            _screenSaver.gameObject.SetActive(true);
        }

        private static Vector2 GetDirection(float rotationZ)
        {
            if (Mathf.Approximately(rotationZ, 0f)) return Vector2.up;
            if (Mathf.Approximately(rotationZ, 90f)) return Vector2.right;
            if (Mathf.Approximately(rotationZ, 180f)) return Vector2.down;
            if (Mathf.Approximately(rotationZ, 270f)) return Vector2.left;

            return Vector2.zero;
        }
    }
}