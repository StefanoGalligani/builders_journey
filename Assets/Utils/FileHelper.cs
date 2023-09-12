using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace BuilderGame.Utils {
    public static class FileHelper {
        public static void Write<T>(T data, string path, out bool success) {
            try {
                FileStream dataStream = new FileStream(path, FileMode.Create);
                BinaryFormatter converter = new BinaryFormatter();
                converter.Serialize(dataStream, data);
                dataStream.Close();
                success = true;
            } catch (Exception e) {
                Debug.LogError("Error writing to file " + path + "\n" + e.StackTrace);
                success = false;
            }
        }

        public static T Read<T>(string path, out bool success) {
            T data = default(T);
            try {
                FileStream dataStream = new FileStream(path, FileMode.Open);
                BinaryFormatter converter = new BinaryFormatter();
                data = (T)converter.Deserialize(dataStream);
                dataStream.Close();
                success = true;
            } catch (Exception e) {
                Debug.LogError("Error reading from file " + path + "\n" + e.StackTrace);
                success = false;
            }
            return data;
        }
    }
}
