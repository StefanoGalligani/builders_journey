using System;
using UnityEngine;

namespace BuilderGame.Settings {
    [Serializable]
    public class SettingsDataSerializable {
        public float MusicVolume;
        public float SfxVolume;
        public float CameraSensitivity;
        public bool TooltipsOn;
        public bool ParticlesOn;
    }
}
