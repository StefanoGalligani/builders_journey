using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BuilderGame.MainMenu {
    public class LevelSelectionManager : MonoBehaviour {
        [SerializeField] private RectTransform _contentRect;
        [SerializeField] private LevelSelectable _levelSelectablePrefab;
        [SerializeField] private LevelInfoScriptableObject[] _levelInfos; //non scriptableobjects ma oggetti di altro tipo recuperati da un'altra classe che si occupa del file
        private List<LevelSelectable> selectables;
        
        private void Start() {
            selectables = new List<LevelSelectable>();
            foreach(LevelInfoScriptableObject pieceInfo in _levelInfos) {
                LevelSelectable pieceSelectable = Instantiate<LevelSelectable>(_levelSelectablePrefab, _contentRect);
                pieceSelectable.Init(pieceInfo, this);
                selectables.Add(pieceSelectable);
            }
        }

        private void OnGameStart() {
            gameObject.SetActive(false);
        }

        public void Selection(LevelSelectable levelSelectable, string sceneName) {
            SceneManager.LoadScene(sceneName);
        }
    }
}