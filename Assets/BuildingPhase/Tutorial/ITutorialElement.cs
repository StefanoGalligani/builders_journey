using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.BuildingPhase.Tutorial
{
    public interface ITutorialElement
    {
        void DisableInTutorial();
        void EnableInTutorial();
    }
}
