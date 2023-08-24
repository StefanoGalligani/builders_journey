using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.BuildingPhase {
    public abstract class SubmenuUI : MonoBehaviour
    {
        public event Action<bool> OnToggled;
        [SerializeField] private GameObject _content;
        public void ToggleContent(bool on) {
            _content.SetActive(on);
            OnToggled?.Invoke(on);
        }
    }
}
