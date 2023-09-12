using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace BuilderGame.Settings {
    public class SettingsFileAccessTest {
        private GameObject obj;
        private SettingsFileAccess settingsFileAccess;
        private SettingsDataSerializable settingsData;

        [SetUp]
        public void SetUp() {
            obj = new GameObject();
            settingsFileAccess = obj.AddComponent<SettingsFileAccess>();
            settingsFileAccess._test = true;
            settingsFileAccess.Awake();
        }

        [Test]
        public void TestDefaultSettings() {
            Assert.AreEqual(1, settingsFileAccess.GetMusicVolume());
            Assert.AreEqual(1, settingsFileAccess.GetSfxVolume());
            Assert.AreEqual(0.25f, settingsFileAccess.GetCameraSensitivity());
            Assert.AreEqual(true, settingsFileAccess.GetTooltipsOn());
            Assert.AreEqual(true, settingsFileAccess.GetParticlesOn());
        }

        [Test]
        public void TestUpdatedSettings() {
            settingsFileAccess.UpdateMusicVolume(0.2f);
            settingsFileAccess.UpdateSfxVolume(0.3f);
            settingsFileAccess.UpdateCameraSensitivity(0.4f);
            settingsFileAccess.UpdateTooltipsOn(false);
            settingsFileAccess.UpdateParticlesOn(false);
            
            Assert.AreEqual(0.2f, settingsFileAccess.GetMusicVolume());
            Assert.AreEqual(0.3f, settingsFileAccess.GetSfxVolume());
            Assert.AreEqual(0.4f, settingsFileAccess.GetCameraSensitivity());
            Assert.AreEqual(false, settingsFileAccess.GetTooltipsOn());
            Assert.AreEqual(false, settingsFileAccess.GetParticlesOn());
        }

        [Test]
        public void TestUpdateEvent() {
            settingsFileAccess.SettingsUpdated += data => settingsData = data;

            settingsFileAccess.UpdateMusicVolume(0.2f);
            Assert.AreEqual(0.2f, settingsData.MusicVolume);

            settingsFileAccess.UpdateSfxVolume(0.3f);
            Assert.AreEqual(0.3f, settingsData.SfxVolume);

            settingsFileAccess.UpdateCameraSensitivity(0.4f);
            Assert.AreEqual(0.4f, settingsData.CameraSensitivity);

            settingsFileAccess.UpdateTooltipsOn(false);
            Assert.AreEqual(false, settingsData.TooltipsOn);

            settingsFileAccess.UpdateParticlesOn(false);
            Assert.AreEqual(false, settingsData.ParticlesOn);
        }

        [TearDown]
        public void TearDown() {
            settingsData = null;
            settingsFileAccess = null;
            GameObject.DestroyImmediate(obj);
        }
    }
}
