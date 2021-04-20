using Innoactive.Creator.Core.Runtime.Utils;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using System.IO;
using UnityEditor;
using UnityEngine.InputSystem;
#endif

namespace Innoactive.Creator.UX
{
    /// <summary>
    /// Settings to control the spectator.
    /// </summary>
    [CreateAssetMenu(fileName = "SpectatorSettings", menuName = "Innoactive/SpectatorSettings", order = 1)]
    public class SpectatorSettings : SettingsObject<SpectatorSettings>
    {
        private static SpectatorSettings settings;
        public readonly string KeyBindingsPath = "Resources/KeyBindings";
        public readonly string KeyBindingsAssetName = "SpectatorKeyBindings.inputactions";

        private void OnEnable()
        {
#if ENABLE_INPUT_SYSTEM && UNITY_EDITOR
            if (KeyBindingSettings == null && !Application.isPlaying)
            {
                KeyBindingSettings = DuplicateAndSetKeyBindings(KeyBindingsAssetName, KeyBindingsPath);
            }
#endif
        }

#if ENABLE_INPUT_SYSTEM && UNITY_EDITOR
        /// <summary>
        /// Duplicates and returns an InputActionAsset in the Resources of the project.
        /// </summary>
        /// <param name="originalAssetName">Name of the file to duplicate.</param>
        /// <param name="targetPath">Destination path.</param>
        /// <param name="targetAssetName">Optional destination name.</param>
        /// <returns>The copied InputActionAsset.</returns>
        public static InputActionAsset DuplicateAndSetKeyBindings(string originalAssetName, string targetPath, string targetAssetName = null)
        {
            if (targetAssetName == null)
            {
                targetAssetName = originalAssetName;
            }

            string assetName = originalAssetName.Split('.')[0];
            InputActionAsset inputActionAsset = Resources.Load<InputActionAsset>(assetName);

            string path = Path.Combine(Application.dataPath, targetPath);
            string assetPath = Path.Combine("Assets", targetPath, targetAssetName);

            Directory.CreateDirectory(path);
            if (AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(inputActionAsset), assetPath))
            {
                return AssetDatabase.LoadAssetAtPath<InputActionAsset>(assetPath);
            }
            else
            {
                Debug.LogError("Could not create default key bindings. Set them manually in the Spectator Settings.");
                return null;
            }
        }
#endif

#if ENABLE_INPUT_SYSTEM
        [Tooltip("Using the new input system requires to change the input actions.")]
        public InputActionAsset KeyBindingSettings;
#elif ENABLE_LEGACY_INPUT_MANAGER
        /// <summary>
        /// Hotkey to toggle UI overlay.
        /// </summary>
        public KeyCode ToggleOverlay = KeyCode.F9;
#endif
    }
}
