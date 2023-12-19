using UnityEngine;

namespace App.Scripts.Utils
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T I
        {
            get
            {
                if (instance != null) return instance;
                var t = typeof(T);

                instance = (T)FindObjectOfType(t);
                if (instance == null)
                {
                    Debug.LogError(t + " をアタッチしているGameObjectはありません");
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            // 他のゲームオブジェクトにアタッチされているか調べる
            // アタッチされている場合は破棄する。
            CheckInstance();
        }

        protected virtual void Init()
        {
            
        }

        protected bool CheckInstance()
        {
            if (instance == null)
            {
                instance = this as T;
                return true;
            }

            if (I == this)
            {
                return true;
            }

            Destroy(this);
            return false;
        }
    }
}