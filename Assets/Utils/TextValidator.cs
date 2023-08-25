using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;

namespace BuilderGame.Utils {
    [RequireComponent(typeof(TMP_InputField))]
    public class TextValidator : MonoBehaviour
    {
        TMP_InputField _inputField;

        private void Start() {
            _inputField = GetComponent<TMP_InputField>();
        }

        public void ValidateText() {
            string text = _inputField.text;
            text = Regex.Replace(text, @"[^\w\s]", "", RegexOptions.None);
            _inputField.text = text;
        }
    }
}