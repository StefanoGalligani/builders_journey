using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuilderGame.BuildingPhase.Start;
using BuilderGame.BuildingPhase.Tutorial;

namespace BuilderGame.BuildingPhase {
    public abstract class BuildingPhaseUI : MonoBehaviour, ITutorialElement
    {
        [SerializeField] private GameObject _content;
        private void Start()
        {
            FindObjectOfType<StartNotifier>().GameStart += OnGameStart;
            Init();
        }

        protected abstract void Init();

        private void OnGameStart() {
            DoOnGameStart();
            _content.SetActive(false);
        }

        protected virtual void DoOnGameStart(){}

        public void DisableInTutorial() {
            _content.SetActive(false);
        }
        public void EnableInTutorial() {
            _content.SetActive(true);
        }
    }
}
