using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.BuildingPhase.Tutorial
{
    public class TutorialPanel : MonoBehaviour
    {
        [SerializeField] private List<MonoBehaviour> _linkedElements;
        [SerializeField] private List<MonoBehaviour> _deactivatedElements;

        internal void OnValidate() {
            if(_linkedElements != null) _linkedElements.RemoveAll(m => !(m is ITutorialElement));
            if(_deactivatedElements != null) _deactivatedElements.RemoveAll(m => !(m is ITutorialElement));
        }

        internal List<ITutorialElement> GetLinkedElements() {
            return _linkedElements.AsEnumerable().Select(m => (ITutorialElement)m).ToList();
        }

        internal List<ITutorialElement> GetDeactivatedElements() {
            return _deactivatedElements.AsEnumerable().Select(m => (ITutorialElement)m).ToList();
        }

        //FOR TESTING
        internal void SetLists(List<MonoBehaviour> linkedElements, List<MonoBehaviour> deactivatedElements) {
            _linkedElements = linkedElements;
            _deactivatedElements = deactivatedElements;
        }
    }
}
