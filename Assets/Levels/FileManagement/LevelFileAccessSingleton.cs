using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using BuilderGame.MainMenu.LevelSelection.LevelInfo;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine.UIElements;

namespace BuilderGame.Levels.FileManagement
{
    public class LevelFileAccessSingleton
    {
        public static LevelFileAccessSingleton Instance {get {Debug.Log("Getting instance");return (_instance==null ? (_instance = new LevelFileAccessSingleton()) : _instance);} private set{_instance = value;} }
        private static LevelFileAccessSingleton _instance;
        private string _fileName = "Data.bin";
        private string _filePath;
        private LevelsDataSerializable _levelsData;
        private bool _fileRead = false;
        internal bool _test;

        private LevelFileAccessSingleton()
        {
            _filePath = Application.persistentDataPath + "/" + _fileName;
            Debug.Log("Created Instance");
        }

        private void UpdateLevelInfo(string levelName, Action<int> action) {
            if (!_fileRead) ReadFromFile();
            List<int> indices = _levelsData.data.AsEnumerable().Select((l,i) => (l.levelName==levelName) ? i : -1).Except(new int[] {-1}).ToList();
            if (indices.Count > 0) {
                action(indices.First());
                WriteToFile();
                return;
            }
            Debug.LogWarning("Tried to update level " + levelName + " but it was not found in file");
        }

        private T GetLevelInfo<T>(string levelName, Func<SingleLevelData, T> action) {
            if (!_fileRead) ReadFromFile();
            SingleLevelData level = _levelsData.data.AsEnumerable().FirstOrDefault(l => l.levelName==levelName);
            if (level.levelName==levelName) {
                return action(level);
            }
            Debug.LogWarning("Tried to read from level " + levelName + " but it was not found in file");
            return default;
        }

        public void SetLevelStars(string levelName, int stars) {
            UpdateLevelInfo(levelName, i => _levelsData.data[i].levelStars = stars);
        }

        public void SetLevelState(string levelName, LevelState state) {
            UpdateLevelInfo(levelName, i => _levelsData.data[i].levelState = state);
        }
        
        public int GetLevelStars(string levelName) {
            return GetLevelInfo(levelName, l => l.levelStars);
        }

        public LevelState GetLevelState(string levelName) {
            return GetLevelInfo(levelName, l => l.levelState);
        }

        public void CreateFileIfNotExists(LevelInfoScriptableObject[] levelInfos) {
            if(CheckIfFileExists()) {
                ReadFromFile();
                UpdateExistingLevels(levelInfos);
                if (levelInfos.Length > _levelsData.levelCount) {
                    AddNewLevels(levelInfos);
                    WriteToFile();
                }
            } else {
                CreateFile(levelInfos);
            }
        }

        private bool CheckIfFileExists() {
            return _test ? _fileRead : File.Exists(_filePath);
        }

        private void UpdateExistingLevels(LevelInfoScriptableObject[] levelInfos) {
            Debug.Log("Updating levels");
            for (int i=0; i<_levelsData.levelCount; i++) {
                if(levelInfos.Length > i) _levelsData.data[i].levelName = levelInfos[i].LevelName;
            }
        }

        private void AddNewLevels(LevelInfoScriptableObject[] levelInfos) {
            Debug.Log("Adding new levels");
            LevelsDataSerializable newLevelsData = new LevelsDataSerializable();
            int n = levelInfos.Length;
            newLevelsData = new LevelsDataSerializable();
            newLevelsData.levelCount = n;
            newLevelsData.data = new SingleLevelData[n];
            bool lastLevelPassed = false;
            for (int i=0; i<_levelsData.levelCount; i++) {
                newLevelsData.data[i] = _levelsData.data[i];
                lastLevelPassed = (_levelsData.data[i].levelState == LevelState.Passed);
            }
            for (int i=_levelsData.levelCount; i<n; i++) {
                newLevelsData.data[i].levelName = levelInfos[i].LevelName;
                newLevelsData.data[i].levelStars = 0;
                newLevelsData.data[i].levelState = (i==_levelsData.levelCount && lastLevelPassed) ? 
                    LevelState.NotPassed : LevelState.Blocked;
            }
            _levelsData = newLevelsData;
        }

        private void CreateFile(LevelInfoScriptableObject[] levelInfos) {
            Debug.Log("Created file");
            int n = levelInfos.Length;
            _levelsData = new LevelsDataSerializable();
            _levelsData.levelCount = n;
            _levelsData.data = new SingleLevelData[n];
            for (int i=0; i<n; i++) {
                _levelsData.data[i].levelName = levelInfos[i].LevelName;
                _levelsData.data[i].levelStars = 0;
                _levelsData.data[i].levelState = (i==0)?LevelState.NotPassed:LevelState.Blocked;
            }
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
            converter.Serialize(dataStream, _levelsData);
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
                _levelsData = converter.Deserialize(dataStream) as LevelsDataSerializable;

                dataStream.Close();
                _fileRead = true;
            } else {
                Debug.LogError("Could not find file " + _filePath);
            }
        }

        //FOR TESTING
        internal static void DestroyInstance() {
            Instance = null;
        }
    }
}
