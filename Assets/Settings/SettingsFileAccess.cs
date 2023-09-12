using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System;

namespace BuilderGame.Settings {
    public class SettingsFileAccess : MonoBehaviour {
        public Action<SettingsDataSerializable> SettingsUpdated;
        [SerializeField] private string _fileName = "Settings.bin";
        private string _filePath;
        private SettingsDataSerializable _settingsData;
        private bool _fileRead = false;
        internal bool _test;

        private void Awake()
        {
            _filePath = Application.persistentDataPath + "/" + _fileName;
        }

        public void UpdateMusicVolume(float volume) {
            _settingsData.MusicVolume = volume;
            SettingsUpdated?.Invoke(_settingsData);
            WriteToFile();
        }

        public void UpdateSfxVolume(float volume) {
            _settingsData.SfxVolume = volume;
            SettingsUpdated?.Invoke(_settingsData);
            WriteToFile();
        }

        public void UpdateCameraSensitivity(float sensitivity) {
            _settingsData.CameraSensitivity = sensitivity;
            SettingsUpdated?.Invoke(_settingsData);
            WriteToFile();
        }

        public void UpdateTooltipsOn(bool on) {
            _settingsData.TooltipsOn = on;
            SettingsUpdated?.Invoke(_settingsData);
            WriteToFile();
        }

        public void UpdateParticlesOn(bool on) {
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
            _settingsData.CameraSensitivity = 1;
            _settingsData.TooltipsOn = true;
            _settingsData.ParticlesOn = true;

            _fileRead = true;
            WriteToFile();
        }

        private void WriteToFile() {
            if (_test) return;
            if (!_fileRead) {
                Debug.LogWarning("Tried to write to file without having the data");
                return;
            }
            FileStream dataStream = new FileStream(_filePath, FileMode.Create);
            BinaryFormatter converter = new BinaryFormatter();
            converter.Serialize(dataStream, _settingsData);
            dataStream.Close();
        }

        private void ReadFromFile() {
            if (_test) {
                _fileRead = true;
                return;
            }
            if(File.Exists(_filePath)) {
                FileStream dataStream = new FileStream(_filePath, FileMode.Open);

                BinaryFormatter converter = new BinaryFormatter();
                _settingsData = converter.Deserialize(dataStream) as SettingsDataSerializable;

                dataStream.Close();
                _fileRead = true;
            } else {
                Debug.LogError("Could not find file " + _filePath);
            }
        }
    }
}
