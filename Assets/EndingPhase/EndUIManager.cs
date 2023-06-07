using UnityEngine;
using BuilderGame.Levels.FileManagement;

namespace BuilderGame.EndingPhase
{
    public class EndUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _uiPanel;
        private void Start()
        {
            _uiPanel.SetActive(false);
            FindObjectOfType<EndNotifier>().GameEnd += OnEndLevel;
        }

        private void OnEndLevel() {
            _uiPanel.SetActive(true);
            //prende nome del livello e del successore dal singleton che gestisce i riferimenti ai livelli
            //poi li usa al posto delle stringhe qua sotto
            LevelFileManagerSingleton.Instance.SetLevelStars("this", 2);
            LevelFileManagerSingleton.Instance.SetLevelState("this", Levels.LevelState.Passed);
            LevelFileManagerSingleton.Instance.SetLevelState("next", Levels.LevelState.Passed);
        }

        public void OnMenuButtonClick() {

        }
        public void OnNextLevelButtonClick() {
            
        }
    }
}
