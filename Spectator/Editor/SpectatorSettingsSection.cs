using System;
using Innoactive.Creator.UX;
using UnityEditor;
using UnityEngine;

namespace Innoactive.CreatorEditor.UI
{
    internal class SpectatorSettingsSection : IProjectSettingsSection
    {
        public string Title { get; } = "Spectator Settings";
        public Type TargetPageProvider { get; } = typeof(SpectatorSettingsProvider);
        public int Priority { get; } = 100;

        private Editor editor;

        public SpectatorSettingsSection()
        {
            editor = Editor.CreateEditor(SpectatorSettings.Instance);
        }

        public void OnGUI(string searchContext)
        {
            EditorGUILayout.Space();
            GUIStyle labelStyle = CreatorEditorStyles.ApplyPadding(CreatorEditorStyles.Paragraph, 0);
            GUILayout.Label("These settings help you to configure the spectator for non-VR users.", labelStyle);
            EditorGUILayout.Space();

            editor.OnInspectorGUI();

            EditorGUILayout.Space(20f);
        }
    }
}
