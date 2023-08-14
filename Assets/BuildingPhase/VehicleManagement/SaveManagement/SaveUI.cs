using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BuilderGame.BuildingPhase.VehicleManagement;

namespace BuilderGame.BuildingPhase.VehicleManagement.SaveManagement
{
    public class SaveUI : MonoBehaviour
    {
        [SerializeField] private VehicleSaveManager saveManager;
        [SerializeField] private TMP_InputField fileName;
        [SerializeField] private GameObject content;
        
        private void Start() {
            FindObjectOfType<StartNotifier>().GameStart += OnGameStart;
        }

        public void OnSave() {
            saveManager.SaveOnFile(fileName.text);
        }

        public void OnLoad() {
            saveManager.LoadFromFile(fileName.text);
        }

        private void OnGameStart() {
            content.SetActive(false);
        }
    }
}
