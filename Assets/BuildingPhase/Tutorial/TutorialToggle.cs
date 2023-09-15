using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BuilderGame.BuildingPhase.Tutorial {
    public class TutorialToggle : MonoBehaviour {
        [SerializeField] internal Toggle toggle;

        internal void Awake() {
            int tutorialEnabled = PlayerPrefs.GetInt("TutorialEnabled", 1);
            toggle.isOn = tutorialEnabled == 1;
            toggle.onValueChanged.AddListener(_ => OnToggle());

            PlayerPrefs.SetInt("CurrentTutorialEnabled", 1);
        }

        private void OnToggle() {
            PlayerPrefs.SetInt("TutorialEnabled", toggle.isOn ? 1 : 0);
        }
    }
}
