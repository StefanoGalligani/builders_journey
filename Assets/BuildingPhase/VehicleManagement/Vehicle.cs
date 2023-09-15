using System.Linq;
using UnityEngine;
using BuilderGame.BuildingPhase.Start;
using BuilderGame.BuildingPhase.Tutorial;
using BuilderGame.PlayPhase;
using BuilderGame.SpecialPieces;

namespace BuilderGame.BuildingPhase.VehicleManagement {
    public class Vehicle : MonoBehaviour, ITutorialElement
    {
        private void Start() {
            FindObjectOfType<StartNotifier>().GameStart += OnGameStart;
        }

        private void OnGameStart() {
            GetComponentsInChildren<Piece>().ToList().ForEach(p => p.PrepareForGame());
            GetComponentInChildren<OutOfBoundsNotifier>().OutOfBounds += OnOutOfBounds;
        }

        private void OnOutOfBounds() {
            GetComponentsInChildren<Piece>().ToList().ForEach(p => p.Interrupt());
            GetComponentsInChildren<SpecialPiece>().ToList().ForEach(p => p.Interrupt());
        }

        void ITutorialElement.DisableInTutorial()
        {
            foreach (Transform child in transform) {
                child.gameObject.SetActive(false);
            }
        }

        void ITutorialElement.EnableInTutorial()
        {
            foreach (Transform child in transform) {
                child.gameObject.SetActive(true);
            }
        }
    }
}
