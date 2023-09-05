using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BuilderGame.EndingPhase {
    public class CompetitiveModeUnlocker : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textField;
        [SerializeField] private string _unlockText;
        void Start()
        {
            FindObjectOfType<EndNotifier>().GameEnd += OnGameEnd;
        }

        private void OnGameEnd()
        {
            int previousMode = PlayerPrefs.GetInt("CompetitiveMode", 0);
            if (previousMode == 0) {
                _textField.text = _unlockText;
            }
            PlayerPrefs.SetInt("CompetitiveMode", 1);
        }
    }
}