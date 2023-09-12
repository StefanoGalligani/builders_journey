using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System;
using BuilderGame.Utils;

[assembly: InternalsVisibleToAttribute("SettingsTests")]
namespace BuilderGame.Settings {
    public class SettingsFileAccess : MonoBehaviour {
        public Action<SettingsDataSerializable> SettingsUpdated;
        [SerializeField] private string _fileName = "Settings.bin";
        private string _filePath;
        private SettingsDataSerializable _settingsData;
        private bool _fileRead = false;
        internal bool _test;

        internal void Awake() {
            _filePath = Application.persistentDataPath + "/" + _fileName;
            CreateFileIfNotExists();
        }

        internal void UpdateMusicVolume(float volume) {
            _settingsData.MusicVolume = volume;
            SettingsUpdated?.Invoke(_settingsData);
            WriteToFile();
        }

        internal void UpdateSfxVolume(float volume) {
            _settingsData.SfxVolume = volume;
            SettingsUpdated?.Invoke(_settingsData);
            WriteToFile();
        }

        internal void UpdateCameraSensitivity(float sensitivity) {
            _settingsData.CameraSensitivity = sensitivity;
            SettingsUpdated?.Invoke(_settingsData);
            WriteToFile();
        }

        internal void UpdateTooltipsOn(bool on) {
            _settingsData.TooltipsOn = on;
            SettingsUpdated?.Invoke(_settingsData);
            WriteToFile();
        }

        internal void UpdateParticlesOn(bool on) {
            _settingsData.ParticlesOn = on;
            SettingsUpdated?.Invoke(_settingsData);
            WriteToFile();
        }

        public float GetMusicVolume() {
            if (!_fileRead) CreateFileIfNotExists();
            return _settingsData.MusicVolume;
        }

        public float GetSfxVolume() {
            if (!_fileRead) CreateFileIfNotExists();
            return _settingsData.SfxVolume;
        }

        public float GetCameraSensitivity() {
            if (!_fileRead) CreateFileIfNotExists();
            return _settingsData.CameraSensitivity;
        }

        public bool GetTooltipsOn() {
            if (!_fileRead) CreateFileIfNotExists();
            return _settingsData.TooltipsOn;
        }

        public bool GetParticlesOn() {
            if (!_fileRead) CreateFileIfNotExists();
            return _settingsData.ParticlesOn;
        }

        private void CreateFileIfNotExists() {
            if(CheckIfFileExists()) {
                ReadFromFile();
            } else {
                CreateFile();
            }
        }

        private bool CheckIfFileExists() {
            return _test ? _fileRead : File.Exists(_filePath);
        }

        private void CreateFile() {
            _settingsData = new SettingsDataSerializable();
            _settingsData.MusicVolume = 1;
            _settingsData.SfxVolume = 1;
            _settingsData.CameraSensitivity = 0.25f;
            _settingsData.TooltipsOn = true;
            _settingsData.ParticlesOn = true;

            _fileRead = true;
            WriteToFile();
        }

        private bool WriteToFile() {
            if (_test) return true;
            if (!_fileRead) {
                Debug.LogWarning("Tried to write to file without having the data");
                return false;
            }
            bool success;
            FileHelper.Write(_settingsData, _filePath, out success);
            return success;
        }

        private bool ReadFromFile() {
            if (_test) {
                _fileRead = true;
                return true;
            }
            if(File.Exists(_filePath)) {
                bool success;
                _settingsData = FileHelper.Read<SettingsDataSerializable>(_filePath, out success);
                _fileRead = true;
                return success;
            } else {
                Debug.LogError("Could not find file " + _filePath);
                return false;
            }
        }
    }
}
