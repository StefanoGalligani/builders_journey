using System.Runtime.CompilerServices;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[assembly: InternalsVisibleToAttribute("TutorialTests")]
namespace BuilderGame.BuildingPhase.Tutorial
{
    public class TutorialManager : MonoBehaviour {
        [SerializeField] private TutorialPanel[] _panels;
        [SerializeField] private List<MonoBehaviour> _linkedElements;
        private bool _enabled;
        private int _currentPanel = 0;
        internal void Start()
        {
            _enabled = PlayerPrefs.GetInt("TutorialEnabled", 1) == 1 && 
                PlayerPrefs.GetInt("CurrentTutorialEnabled", 1) == 1;
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
                PlayerPrefs.SetInt("CurrentTutorialEnabled", 0);
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

        internal void OnValidate() {
            if(_linkedElements != null) _linkedElements.RemoveAll(m => !(m is ITutorialElement));
        }
        

        //FOR TESTING
        internal List<MonoBehaviour> GetLinkedElements() {
            return _linkedElements;
        }

        internal void SetLinkedElements(List<MonoBehaviour> linkedElements) {
            _linkedElements = linkedElements;
        }

        internal void SetPanels(TutorialPanel[] panels) {
            _panels = panels;
        }
    }
}
