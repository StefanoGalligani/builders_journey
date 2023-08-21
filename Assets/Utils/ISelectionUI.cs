using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Utils {
    public interface ISelectionUI<ISelectable, IInfoScriptableObject>
    {
        public abstract void Selection(ISelectable selectable, IInfoScriptableObject info);
    }

    public interface ISelectable {
        public void OnClick();
    }

    public interface IInfoScriptableObject {
    }
}
