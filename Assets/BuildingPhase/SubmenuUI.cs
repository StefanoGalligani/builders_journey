using System;
using System.Collections;
using System.Collections.Generic;
using BuilderGame.BuildingPhase.Tutorial;
using UnityEngine;

namespace BuilderGame.BuildingPhase {
    public abstract class SubmenuUI : MonoBehaviour, ITutorialElement
    {
        public event Action<bool> OnToggled;
        [SerializeField] private GameObject _content;
        public void ToggleContent(bool on) {
            _content.SetActive(on);
            OnToggled?.Invoke(on);
        }

        public void DisableInTutorial() {
            ToggleContent(false);
        }
        public void EnableInTutorial() {
            ToggleContent(true);
        }
    }
}
