using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BuilderGame.BuildingPhase.Binding
{
    public class BindingInfo : MonoBehaviour
    {
        public event Action<int> OnRebind;
        [SerializeField] private TextMeshProUGUI _actionNameTxt;
        [SerializeField] private TextMeshProUGUI _bindingKeyTxt;
        private int _index;

        internal void Init(string actionName, string bindingKey, int index) {
            _actionNameTxt.text = actionName;
            _bindingKeyTxt.text = bindingKey;
            _index = index;
        }

        internal void UpdateBindingName(string bindingKey) {
            _bindingKeyTxt.text = bindingKey;
        }

        public void OnRebindBtnPressed() {
            _bindingKeyTxt.text = "Press a key...";
            OnRebind?.Invoke(_index);
        }
    }
}
