using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Effects {
    internal abstract class EffectHandler : MonoBehaviour {
        internal abstract void StartEffect();
        internal abstract void StopEffect();
    }
}
