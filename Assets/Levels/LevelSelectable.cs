using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BuilderGame.Levels
{
    public class LevelSelectable : MonoBehaviour
    {
        [SerializeField] private Image[] _starsImages;
        [SerializeField] private Image _background;
        [SerializeField] private TextMeshProUGUI _infoText;
        [SerializeField] private Color[] colors;
        private string _sceneName;
        private LevelSelectionManager _selectionManager;
        private bool _clickable;

        internal void Init(LevelInfoScriptableObject levelInfo, int levelStars, LevelState levelState, LevelSelectionManager selectionManager) {
            _selectionManager = selectionManager;

            _infoText.text = levelInfo.LevelName;
            _sceneName = levelInfo.SceneName;

            for (int i=levelStars; i<_starsImages.Length; i++) {
                _starsImages[i].enabled = false;
            }
            _background.color = colors[(int)levelState];
            _clickable = levelState != LevelState.Blocked;
        }

        public void OnClick() {
            if (!_clickable) return;
            _selectionManager.Selection(this, _sceneName);
        }
    }

    public enum LevelState {
        Blocked,
        NotPassed,
        Passed
    }
}