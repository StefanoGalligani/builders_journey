using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

[assembly: InternalsVisibleToAttribute("BindingTests")]
namespace BuilderGame.BuildingPhase.Binding
{
    public class BindingInfo : MonoBehaviour
    {
        public event Action<int> OnRebind;
        [SerializeField] private TextMeshProUGUI _actionNameTxt;
        [SerializeField] private TextMeshProUGUI _bindingKeyTxt;
        private int _index;

        internal void Init(string actionName, string bindingKey, int index) {
            if (_actionNameTxt) _actionNameTxt.text = actionName;
            if (_bindingKeyTxt) _bindingKeyTxt.text = bindingKey;
            _index = index;
        }

        internal void UpdateBindingName(string bindingKey) {
            if (_bindingKeyTxt) _bindingKeyTxt.text = bindingKey;
        }

        public void OnRebindBtnPressed() {
            if (_bindingKeyTxt) _bindingKeyTxt.text = "Press a key...";
            OnRebind?.Invoke(_index);
        }
    }
}
