using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Views
{
    public class ScreenSaver : MonoBehaviour
    {
        [SerializeField] private float _duration;

        private async UniTaskVoid Awake()
        {
            await UniTask.WaitForSeconds(_duration);
            Destroy(gameObject);
        }
    }
}