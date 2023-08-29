using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.BuildingPhase.Tutorial
{
    public class TutorialManager : MonoBehaviour {
        [SerializeField] private TutorialPanel[] _panels;
        [SerializeField] private List<MonoBehaviour> _linkedElements;
        [SerializeField] private bool _enabled;
        private int _currentPanel = 0;
        void Start()
        {
            if (!_enabled) {
                gameObject.SetActive(false);
                return;
            }
            foreach (TutorialPanel panel in _panels) {
                panel.gameObject.SetActive(false);
                foreach (ITutorialElement element in panel.GetLinkedElements()) {
                    element.DisableInTutorial();
                }
            }
            foreach (ITutorialElement element in _linkedElements.AsEnumerable().Select(m => (ITutorialElement)m).ToList()) {
                element.DisableInTutorial();
            }
            ActivatePanel(0);
        }

        public void OnNextPanel() {
            _panels[_currentPanel].gameObject.SetActive(false);
            _currentPanel++;
            if (_currentPanel == _panels.Length) {
                foreach (ITutorialElement element in _linkedElements.AsEnumerable().Select(m => (ITutorialElement)m).ToList()) {
                    element.EnableInTutorial();
                }
                gameObject.SetActive(false);
            } else {
                ActivatePanel(_currentPanel);
            }
        }

        private void ActivatePanel(int index) {
            _panels[index].gameObject.SetActive(true);
            foreach (ITutorialElement element in _panels[index].GetDeactivatedElements()) {
                element.DisableInTutorial();
            }
            foreach (ITutorialElement element in _panels[index].GetLinkedElements()) {
                element.EnableInTutorial();
            }
        }

        private void OnValidate() {
            _linkedElements.RemoveAll(m => !(m is ITutorialElement));
        }
    }
}
