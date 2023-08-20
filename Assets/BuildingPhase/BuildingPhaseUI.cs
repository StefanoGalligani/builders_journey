using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuilderGame.BuildingPhase.Start;

namespace BuilderGame.BuildingPhase {
    public abstract class BuildingPhaseUI : MonoBehaviour
    {
        [SerializeField] private GameObject _content;
        private void Start()
        {
            FindObjectOfType<StartNotifier>().GameStart += OnGameStart;
            Init();
        }

        protected abstract void Init();

        private void OnGameStart() {
            _content.SetActive(false);
        }
    }
}
