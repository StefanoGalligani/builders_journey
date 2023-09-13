using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using BuilderGame.Effects;

[assembly: InternalsVisibleToAttribute("MainMenuTests")]
namespace BuilderGame.MainMenu {
    public class MenusManager : MonoBehaviour {
        [SerializeField] private EffectContainer _effects;
        private GameObject[] menus;

        internal void Start() {
            menus = new GameObject[transform.childCount];
            for (int i=0; i<transform.childCount; i++) {
                menus[i] = transform.GetChild(i).gameObject;
                menus[i].SetActive(false);
            }
        }

        public void OpenMenu(int index) {
            for (int i=0; i<menus.Length; i++) {
                menus[i].SetActive(index == i);
            }
            if (_effects != null) {
                _effects.StartEffects();
            }
        }

        public void Quit() {
            Application.Quit();
        }
    }
}
