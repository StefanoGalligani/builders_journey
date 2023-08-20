using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BuilderGame.MainMenu.LevelSelection.LevelInfo;

namespace BuilderGame.Levels.FileManagement
{
    public class LevelFileManagerSingleton
    {
        private string _fileName = "Data.bin";
        private string _filePath;
        private LevelsDataSerializable _levelsData;
        private bool _fileRead = false;
        private static LevelFileManagerSingleton _instance;
        public static LevelFileManagerSingleton Instance {get {return (_instance==null ? (_instance = new LevelFileManagerSingleton()) : _instance);} private set{} }

        private LevelFileManagerSingleton()
        {
            _filePath = Application.persistentDataPath + "/" + _fileName;
        }

        public void SetLevelStars(string levelName, int stars) {
            if (!_fileRead) ReadFromFile();
            for (int i=0; i<_levelsData.levelCount; i++) {
                if (levelName == _levelsData.data[i].levelName) {
                    _levelsData.data[i].levelStars = stars;
                    WriteToFile();
                    return;
                }
            }
            Debug.LogWarning("Tried to update level " + levelName + " but it was not found in file");
        }

        public void SetLevelState(string levelName, LevelState state) {
            if (!_fileRead) ReadFromFile();
            for (int i=0; i<_levelsData.levelCount; i++) {
                if (levelName == _levelsData.data[i].levelName) {
                    _levelsData.data[i].levelState = state;
                    WriteToFile();
                    return;
                }
            }
            Debug.LogWarning("Tried to update level " + levelName + " but it was not found in file");
        }
        
        public int GetLevelStars(string levelName) {
            if (!_fileRead) ReadFromFile();
            for (int i=0; i<_levelsData.levelCount; i++) {
                if (levelName == _levelsData.data[i].levelName) {
                    return _levelsData.data[i].levelStars;
                }
            }
            Debug.LogWarning("Tried to read from level " + levelName + " but it was not found in file");
            return 0;
        }

        public LevelState GetLevelState(string levelName) {
            if (!_fileRead) ReadFromFile();
            for (int i=0; i<_levelsData.levelCount; i++) {
                if (levelName == _levelsData.data[i].levelName) {
                    return _levelsData.data[i].levelState;
                }
            }
            Debug.LogWarning("Tried to read from level " + levelName + " but it was not found in file");
            return LevelState.Blocked;
        }

        public void CreateFileIfNotExists(LevelInfoScriptableObject[] levelInfos) {
            if(File.Exists(_filePath)) {
                ReadFromFile();
                if (levelInfos.Length > _levelsData.levelCount) {
                    AddNewLevels(levelInfos);
                    WriteToFile();
                }
            } else {
                CreateFile(levelInfos);
            }
        }

        private void AddNewLevels(LevelInfoScriptableObject[] levelInfos) {
            LevelsDataSerializable newLevelsData = new LevelsDataSerializable();
            int n = levelInfos.Length;
            newLevelsData = new LevelsDataSerializable();
            _levelsData.levelCount = n;
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
    }
}
