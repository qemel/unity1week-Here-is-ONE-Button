using App.Scripts.Models.Statics;
using App.Scripts.Views;
using UnityEngine;

namespace App.Scripts.Presenters
{
    public class InputProvider : MonoBehaviour
    {
        private void Update()
        {
            if (StaticLevel.CurrentLevel.Value == 0) return;
            if (Input.GetKeyDown("r"))
            {
                FadeManager.Instance.LoadScene("Level" + StaticLevel.CurrentLevel.Value, 1f);
                GameBGMAudio.I.FadeBgmDown();
            }
        }
    }
}