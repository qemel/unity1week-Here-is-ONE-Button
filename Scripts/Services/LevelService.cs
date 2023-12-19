using App.Scripts.Models.Statics;
using UniRx;
using UnityEngine;

namespace App.Scripts.Services
{
    public class LevelService : MonoBehaviour
    {
        private void Start()
        {
            StaticLevel.CurrentLevel.Skip(1).DistinctUntilChanged(x => x).Take(1).Subscribe(level =>
            {
                FadeManager.Instance.LoadScene("Level" + level, 0.8f + (level >= 8 ? level / 8.0f : 0f));
            }).AddTo(this);
        }
    }
}