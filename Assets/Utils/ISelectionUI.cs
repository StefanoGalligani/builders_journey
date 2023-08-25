using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Utils {
    public interface ISelectionUI<ISelectable, ISelectionInfo>
    {
        public abstract void Selection(ISelectable selectable, ISelectionInfo info);
    }

    public interface ISelectable {
        public void OnClick();
    }

    public interface ISelectionInfo {
    }
}
