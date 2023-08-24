using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.BuildingPhase {
    public class CompositeUI : BuildingPhaseUI
    {
        [SerializeField] private SubmenuUI[] submenus;
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
