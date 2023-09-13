using System;
using UnityEngine;

namespace BuilderGame.Effects {
    [Serializable]
    public class EffectContainer {
        [SerializeField] private EffectHandler[] _effects;
        public void StartEffects() {
            if (_effects != null)
                foreach(EffectHandler effect in _effects) effect.StartEffect();
        }
        public void StopEffects() {
            if (_effects != null)
                foreach(EffectHandler effect in _effects) effect.StopEffect();
        }
    }
}
