using UnityEngine;

namespace BuilderGame.Utils
{
    public class Singleton : MonoBehaviour {
        private static Singleton _instance;

        public static Singleton Instance
        {
            get {
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null) {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            } else {
                Destroy(gameObject);
            }
        }
    }
}
