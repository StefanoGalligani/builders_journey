using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

[assembly: InternalsVisibleToAttribute("UtilsTests")]
namespace BuilderGame.Utils {
    [RequireComponent(typeof(TMP_InputField))]
    public class TextValidator : MonoBehaviour
    {
        private TMP_InputField _inputField;

        internal void Start() {
            _inputField = GetComponent<TMP_InputField>();
        }

        public void ValidateText() {
            string text = _inputField.text;
            text = Regex.Replace(text, @"[^\w\s]", "", RegexOptions.None);
            _inputField.text = text;
        }
    }
}