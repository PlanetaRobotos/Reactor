using System;
using UnityEditor;
using UnityEngine;

namespace LazyShortcuts
{
    public class LazyShortcutsEditor : EditorWindow
    {
        private const int MinWidth = 140;

        [MenuItem("Tools/LazyShortcuts/Settings", false, 0)]
        public static void ShowWindow()
        {
            EditorWindow[] editorWindows = Resources.FindObjectsOfTypeAll<EditorWindow>();
            var editor = typeof(Editor).Assembly;
            Type inspector = editor.GetType("UnityEditor.InspectorWindow");
            LazyShortcutsEditor window =
                GetWindow<LazyShortcutsEditor>("LazyShortcutsSettings", true, inspector);
            window.minSize = new Vector2(275, 50);
        }

        private void OnGUI()
        {
            DisplayBlock("Reset Transform", LazyShortcuts.enableResetTransform);
            DisplayBlock("Reset Name", LazyShortcuts.enableResetName);
            DisplayBlock("Revert Prefab", LazyShortcuts.enableRevertPrefab);
            DisplayBlock("Save Changes To Prefab", LazyShortcuts.enableSaveChangesToPrefab);
            DisplayBlock("Toggle Debug Normal View", LazyShortcuts.enableToggleDebugNormalView);
            DisplayBlock("Toggle Lock Unlock Inspector", LazyShortcuts.enableToggleLockUnlockInspector);
            DisplayBlock("Toggle Maximize on Play", LazyShortcuts.enableMaxPlay);
            DisplayBlock("Toggle Mute on Play", LazyShortcuts.enableMutePlay);
            DisplayBlock("Toggle Stats", LazyShortcuts.enableStats);
        }

        private static void DisplayBlock(string label, bool enable)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.MinWidth(MinWidth));
            LazyShortcuts.enableResetTransform = EditorGUILayout.Toggle(enable);
            EditorGUILayout.EndHorizontal();
        }
    }
}