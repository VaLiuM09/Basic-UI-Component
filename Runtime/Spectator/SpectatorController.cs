using System;
using Innoactive.Creator.Core.Input;
using UnityEngine.InputSystem;

namespace Innoactive.Creator.UX
{
    /// <summary>
    /// Controller for a spectator to toggle UI visibility.
    /// </summary>
    public class SpectatorController : InputActionListener
    {
        /// <summary>
        /// Event fired when UI visibility is toggled.
        /// </summary>
        public event EventHandler ToggleUIOverlayVisibility;

        protected void OnEnable()
        {
            RegisterInputEvent(ToggleOverlay);
        }
        
        protected void OnDisable()
        {
            UnregisterInputEvent(ToggleOverlay);
        }

        protected void ToggleOverlay(InputAction.CallbackContext value)
        {
            ToggleUIOverlayVisibility?.Invoke(this, new EventArgs());
        }
    }
}
