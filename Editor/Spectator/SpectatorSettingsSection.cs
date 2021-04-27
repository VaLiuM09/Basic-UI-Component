using System;
using Innoactive.Creator.Core.Configuration;
using Innoactive.CreatorEditor.Input;
using UnityEditor;
using UnityEngine;

namespace Innoactive.CreatorEditor.UI
{
    internal class SpectatorSettingsSection : IProjectSettingsSection
    {
        public string Title { get; } = "Spectator Settings";
        public Type TargetPageProvider { get; } = typeof(SpectatorSettingsProvider);
        public int Priority { get; } = 100;

        public void OnGUI(string searchContext)
        {
            EditorGUILayout.Space();
            GUIStyle labelStyle = CreatorEditorStyles.ApplyPadding(CreatorEditorStyles.Paragraph, 0);
            GUILayout.Label("These settings help you to configure the spectator for non-VR users.", labelStyle);
            EditorGUILayout.Space();

            if (GUILayout.Button("Edit key bindings"))
            {
                if (InputEditorUtils.UsesCustomKeyBindingAsset() == false)
                {
                    InputEditorUtils.CopyCustomKeyBindingAsset();
                }
                
                AssetDatabase.OpenAsset(RuntimeConfigurator.Configuration.CurrentInputActionAsset);
            }

            EditorGUILayout.Space(20f);
        }
    }
}
