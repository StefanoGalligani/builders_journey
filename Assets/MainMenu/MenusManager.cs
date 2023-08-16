using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.MainMenu
{
    public class MenusManager : MonoBehaviour
    {
        private GameObject[] menus;

        private void Start() {
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
        }

        public void Quit() {
            Application.Quit();
        }
    }
}
