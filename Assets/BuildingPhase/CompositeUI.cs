using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuilderGame.Effects;

namespace BuilderGame.BuildingPhase {
    public class CompositeUI : BuildingPhaseUI
    {
        [SerializeField] private SubmenuUI[] submenus;
        [SerializeField] private EffectHandler[] _effects;
        protected override void Init()
        {
            OpenMenu(0);
        }

        public void OpenMenu(int index) {
            for (int i=0; i<submenus.Length; i++) {
                submenus[i].ToggleContent(i == index);
            }
        }
    }
}
