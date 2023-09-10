using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuilderGame.Effects;

namespace BuilderGame.BuildingPhase {
    public class CompositeUI : BuildingPhaseUI
    {
        [SerializeField] private SubmenuUI[] submenus;
        [SerializeField] private EffectHandler[] _effects;
        private bool _playEffects;
        protected override void Init()
        {
            _playEffects = false;
            OpenMenu(0);
            _playEffects = true;
        }

        public void OpenMenu(int index) {
            for (int i=0; i<submenus.Length; i++) {
                submenus[i].ToggleContent(i == index);
            }
            if (_playEffects)
                foreach(EffectHandler effect in _effects) effect.StartEffect();
        }
    }
}
