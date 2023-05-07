using System;

namespace BuilderGame.BuildingPhase.Builder {
    public class StartManagerSingleton
    {
        private static StartManagerSingleton _instance;
        public static StartManagerSingleton Instance {get {return (_instance==null ? (_instance = new StartManagerSingleton()) : _instance);} private set{} }
        public event Action GameStart;

        internal void StartGame() {
            GameStart?.Invoke();
        }
    }
}
