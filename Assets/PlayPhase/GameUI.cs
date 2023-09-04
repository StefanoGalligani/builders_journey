using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuilderGame.BuildingPhase.Start;
using BuilderGame.EndingPhase;

namespace BuilderGame.PlayPhase
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private GameObject _content;

        private void Start() {
            _content.SetActive(false);
            FindObjectOfType<StartNotifier>().GameStart += OnGameStart;
            FindObjectOfType<EndNotifier>().GameEnd += OnGameEnd;
        }

        private void OnGameStart() {
            _content.SetActive(true);
        }

        private void OnGameEnd() {
            _content.SetActive(false);
        }
    }
}
