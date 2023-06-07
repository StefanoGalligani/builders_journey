using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
            for (int i=0; i<_levelsData.levelsNames.Length; i++) {
                if (levelName == _levelsData.levelsNames[i]) {
                    _levelsData.levelsStars[i] = stars;
                    WriteToFile();
                    return;
                }
            }
            Debug.LogWarning("Tried to update level " + levelName + " but it was not found in file");
        }

        public void SetLevelState(string levelName, LevelState state) {
            if (!_fileRead) ReadFromFile();
            for (int i=0; i<_levelsData.levelsNames.Length; i++) {
                if (levelName == _levelsData.levelsNames[i]) {
                    _levelsData.levelsStates[i] = state;
                    WriteToFile();
                    return;
                }
            }
            Debug.LogWarning("Tried to update level " + levelName + " but it was not found in file");
        }
        
        public int GetLevelStars(string levelName) {
            if (!_fileRead) ReadFromFile();
            for (int i=0; i<_levelsData.levelsNames.Length; i++) {
                if (levelName == _levelsData.levelsNames[i]) {
                    return _levelsData.levelsStars[i];
                }
            }
            Debug.LogWarning("Tried to read from level " + levelName + " but it was not found in file");
            return 0;
        }

        public LevelState GetLevelState(string levelName) {
            if (!_fileRead) ReadFromFile();
            for (int i=0; i<_levelsData.levelsNames.Length; i++) {
                if (levelName == _levelsData.levelsNames[i]) {
                    return _levelsData.levelsStates[i];
                }
            }
            Debug.LogWarning("Tried to read from level " + levelName + " but it was not found in file");
            return LevelState.Blocked;
        }

        public void CreateFileIfNotExists(LevelInfoScriptableObject[] levelInfos) {
            if(File.Exists(_filePath)) {
                ReadFromFile();
                if (levelInfos.Length > _levelsData.levelsNames.Length) {
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
            newLevelsData.levelsNames = new string[n];
            newLevelsData.levelsStars = new int[n];
            newLevelsData.levelsStates = new LevelState[n];
            bool lastLevelPassed = false;
            for (int i=0; i<_levelsData.levelsNames.Length; i++) {
                newLevelsData.levelsNames[i] = _levelsData.levelsNames[i];
                newLevelsData.levelsStars[i] = _levelsData.levelsStars[i];
                newLevelsData.levelsStates[i] = _levelsData.levelsStates[i];
                lastLevelPassed = (_levelsData.levelsStates[i] == LevelState.Passed);
            }
            for (int i=_levelsData.levelsNames.Length; i<n; i++) {
                newLevelsData.levelsNames[i] = levelInfos[i].LevelName;
                newLevelsData.levelsStars[i] = 0;
                newLevelsData.levelsStates[i] = (i==_levelsData.levelsNames.Length && lastLevelPassed) ? 
                    LevelState.NotPassed : LevelState.Blocked;
            }
            _levelsData = newLevelsData;
        }

        private void CreateFile(LevelInfoScriptableObject[] levelInfos) {
            int n = levelInfos.Length;
            _levelsData = new LevelsDataSerializable();
            _levelsData.levelsNames = new string[n];
            _levelsData.levelsStars = new int[n];
            _levelsData.levelsStates = new LevelState[n];
            for (int i=0; i<n; i++) {
                _levelsData.levelsNames[i] = levelInfos[i].LevelName;
                _levelsData.levelsStars[i] = 0;
                _levelsData.levelsStates[i] = (i==0)?LevelState.NotPassed:LevelState.Blocked;
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
