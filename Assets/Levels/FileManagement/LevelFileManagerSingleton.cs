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
        private static LevelFileManagerSingleton _instance;
        public static LevelFileManagerSingleton Instance {get {return (_instance==null ? (_instance = new LevelFileManagerSingleton()) : _instance);} private set{} }

        private LevelFileManagerSingleton()
        {
            _filePath = Application.persistentDataPath + "/" + _fileName;
        }

        public void SetLevelStars(string levelName, int stars) {
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
            for (int i=0; i<_levelsData.levelsNames.Length; i++) {
                if (levelName == _levelsData.levelsNames[i]) {
                    return _levelsData.levelsStars[i];
                }
            }
            Debug.LogWarning("Tried to read from level " + levelName + " but it was not found in file");
            return 0;
        }

        public LevelState GetLevelState(string levelName) {
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
                    //aggiornare i nuovi livelli e solo quelli
                    WriteToFile();
                }
            } else {
                CreateFile(levelInfos);
            }
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
            WriteToFile();
        }

        private void WriteToFile() {
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
            } else {
                Debug.LogError("Could not find file " + _filePath);
            }
        }
    }
}
