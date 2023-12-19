using UnityEngine;

namespace App.Scripts.Views
{
    public class LastStageFlag : MonoBehaviour
    {
        private void Start()
        {
            PlayerPrefs.SetInt("Reached12", 1);
        }
    }
}