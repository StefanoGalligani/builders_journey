using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

namespace BuilderGame.Utils
{
    public class TextValidatorTest
    {
        [Test]
        public void TestValidate()
        {
            GameObject obj = new GameObject();
            TMP_InputField inputField = obj.AddComponent<TMP_InputField>();
            TextValidator validator = obj.AddComponent<TextValidator>();
            validator.Start();
            inputField.text = "Test@\\/.; 123?!-<>";
            validator.ValidateText();
            Assert.AreEqual(inputField.text, "Test 123");
        }
    }
}
