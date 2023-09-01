using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.BuildingPhase.Tutorial
{
    public class TutorialElement : MonoBehaviour, ITutorialElement
    {
        void ITutorialElement.DisableInTutorial()
        {
            gameObject.SetActive(false);
        }

        void ITutorialElement.EnableInTutorial()
        {
            gameObject.SetActive(true);
        }
    }
}
