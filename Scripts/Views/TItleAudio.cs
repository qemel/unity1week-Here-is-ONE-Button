using System;
using UnityEngine;

namespace App.Scripts.Views
{
    public class TItleAudio : MonoBehaviour
    {
        private void Awake()
        {
            GameBGMAudio.I.GoTitle();
        }
    }
}