using System;
using UniRx;
using UnityEngine;

namespace App.Scripts.Models
{
    public interface IGameButton
    {
        ButtonType ButtonType { get; }
        IObservable<Unit> OnClickAsObservable { get; }
        bool IsActive { get; }
        bool CanPush { get; }
        void SetPush(bool canPush);
        void Destroy();
        void SetActive(bool isActive);
        GameObject Entity { get; }
    }
}