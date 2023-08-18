using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using BuilderGame.Input;

namespace BuilderGame.Utils
{
    public class ChiScrollRect : ScrollRect, IPointerEnterHandler, IPointerExitHandler
    {
        private Controls _actionAsset;
        private bool _swallowMouseWheelScrolls = true;
        private bool _isMouseOver = false;

        protected override void Start() {
            base.Start();
            _actionAsset = new Controls();
            _actionAsset.Enable();
            _actionAsset.defaultmap.Scroll.performed += ctx => DetectScroll(ctx);
        }
    
        public void OnPointerEnter(PointerEventData eventData)
        {
            _isMouseOver = true;
        }
    
        public void OnPointerExit(PointerEventData eventData)
        {
            _isMouseOver = false;
        }
    
        private void DetectScroll(InputAction.CallbackContext context)
        {
            // Detect the mouse wheel and generate a scroll. This fixes the issue where Unity will prevent our ScrollRect
            // from receiving any mouse wheel messages if the mouse is over a raycast target (such as a button).
            if (_isMouseOver)
            {
                PointerEventData pointerData = new PointerEventData(EventSystem.current);
    
                pointerData.scrollDelta = context.ReadValue<Vector2>();
                _swallowMouseWheelScrolls = false;
                OnScroll(pointerData);
                _swallowMouseWheelScrolls = true;
            }
        }
    
        public override void OnScroll(PointerEventData data)
        {
            if (!_swallowMouseWheelScrolls) {
                // Amplify the mousewheel so that it matches the scroll sensitivity.
                if (data.scrollDelta.y < -Mathf.Epsilon)
                    data.scrollDelta = new Vector2(0f, -scrollSensitivity);
                else if (data.scrollDelta.y > Mathf.Epsilon)
                    data.scrollDelta = new Vector2(0f, scrollSensitivity);
    
                base.OnScroll(data);
            }
        }
    }
}
