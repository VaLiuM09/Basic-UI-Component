using Innoactive.Creator.Core.Runtime.Utils;
using UnityEngine;

namespace Innoactive.Creator.UX
{
    /// <summary>
    /// Settings to control the spectator.
    /// </summary>
    [CreateAssetMenu(fileName = "SpectatorSettings", menuName = "Innoactive/SpectatorSettings", order = 1)]
    public class SpectatorSettings : SettingsObject<SpectatorSettings>
    {
        private static SpectatorSettings settings;
        /// <summary>
        /// Hotkey to toggle UI overlay.
        /// </summary>
        public KeyCode ToggleOverlay = KeyCode.W;
    }
}
