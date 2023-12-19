using App.Scripts.Models.Statics;
using UnityEngine;

namespace App.Scripts.Views
{
    public class Force0Level : MonoBehaviour
    {
        private void Awake()
        {
            StaticLevel.SetLevel(0);
        }
    }
}