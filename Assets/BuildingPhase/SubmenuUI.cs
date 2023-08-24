using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.BuildingPhase {
    public abstract class SubmenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject _content;
        public void ToggleContent(bool on) {
            _content.SetActive(on);
        }
    }
}
