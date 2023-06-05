using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BuilderGame.MainMenu
{
    public class LevelSelectable : MonoBehaviour
    {
        [SerializeField] private Image[] _starsImages;
        [SerializeField] private TextMeshProUGUI _infoText;
        private string _sceneName;
        private LevelSelectionManager _selectionManager;

        internal void Init(LevelInfoScriptableObject levelInfo, LevelSelectionManager selectionManager) {
            _selectionManager = selectionManager;

            _infoText.text = levelInfo.Name;
            _sceneName = levelInfo.SceneName;
        }

        public void OnClick() {
            _selectionManager.Selection(this, _sceneName);
        }
    }
}