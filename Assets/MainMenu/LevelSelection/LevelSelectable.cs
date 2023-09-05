using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BuilderGame.Levels;
using BuilderGame.Utils;
using BuilderGame.MainMenu.LevelSelection.LevelInfo;

[assembly: InternalsVisibleToAttribute("MainMenuTests")]
namespace BuilderGame.MainMenu.LevelSelection
{
    public class LevelSelectable : MonoBehaviour, Utils.ISelectable
    {
        [SerializeField] private Image[] _starsImages;
        [SerializeField] private Image _background;
        [SerializeField] private TextMeshProUGUI _infoText;
        [SerializeField] private Color[] colors;
        private ISelectionUI<LevelSelectable, LevelInfoScriptableObject> _selectionUI;
        private bool _clickable;
        private LevelInfoScriptableObject _levelInfo;

        internal void Init(LevelInfoScriptableObject levelInfo, int levelStars, LevelState levelState, ISelectionUI<LevelSelectable, LevelInfoScriptableObject> selectionUI) {
            _levelInfo = levelInfo;
            _selectionUI = selectionUI;
            _clickable = levelState != LevelState.Blocked;

            bool starsActive = PlayerPrefs.GetInt("CompetitiveMode", 0) == 1;

            if(_infoText) _infoText.text = levelInfo.LevelName;
            if (_starsImages != null)
                for (int i=0; i<_starsImages.Length; i++) {
                    _starsImages[i].enabled = i<levelStars && starsActive;
                }

            if(_background) _background.color = colors[(int)levelState];
        }

        public void OnClick() {
            if (!_clickable) return;
            _selectionUI.Selection(this, _levelInfo);
        }
    }
}