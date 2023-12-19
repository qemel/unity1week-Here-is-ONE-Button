using UnityEngine;

namespace App.Scripts.Views
{
    public class EndingAudio : MonoBehaviour
    {
        private void Awake()
        {
            GameBGMAudio.I.GoEnding();
        }
    }
}