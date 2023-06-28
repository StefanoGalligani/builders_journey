using UnityEngine;
using UnityEngine.SceneManagement;

namespace BuilderGame.Levels {
    public class LevelRestartManager : MonoBehaviour {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}