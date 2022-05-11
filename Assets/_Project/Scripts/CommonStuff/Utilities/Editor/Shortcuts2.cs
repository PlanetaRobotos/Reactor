using System;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Appside
{
    public static class Shortcuts2
    {
        private readonly struct TransformData
        {
            public readonly Vector3 localPosition;
            public readonly Quaternion localRotation;
            public readonly Vector3 localScale;

            public TransformData(Vector3 localPosition, Quaternion localRotation, Vector3 localScale)
            {
                this.localPosition = localPosition;
                this.localRotation = localRotation;
                this.localScale = localScale;
            }
        }

        private static TransformData _data;
        private static Vector3? _dataCenter;

        private static EditorWindow _mouseOverWindow;

        [MenuItem("Custom Shortcuts/Select Inspector under mouse cursor (use hotkey) #&q")]
        private static void SelectLockableInspector()
        {
            if (EditorWindow.mouseOverWindow.GetType().Name == "InspectorWindow")
            {
                _mouseOverWindow = EditorWindow.mouseOverWindow;
                var type = Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.InspectorWindow");
                var findObjectsOfTypeAll = Resources.FindObjectsOfTypeAll(type);
                var indexOf = findObjectsOfTypeAll.ToList().IndexOf(_mouseOverWindow);
                EditorPrefs.SetInt("LockableInspectorIndex", indexOf);
            }
        }

        /*[MenuItem("Custom Shortcuts/Toggle Lock &q")]
        static void ToggleInspectorLock()
        {
            if (_mouseOverWindow == null)
            {
                if (!EditorPrefs.HasKey("LockableInspectorIndex"))
                    EditorPrefs.SetInt("LockableInspectorIndex", 0);
                int i = EditorPrefs.GetInt("LockableInspectorIndex");

                Type type = Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.InspectorWindow");
                Object[] findObjectsOfTypeAll = Resources.FindObjectsOfTypeAll(type);
                _mouseOverWindow = (EditorWindow)findObjectsOfTypeAll[i];
            }

            if (_mouseOverWindow != null && _mouseOverWindow.GetType().Name == "InspectorWindow")
            {
                Type type = Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.InspectorWindow");
                PropertyInfo propertyInfo = type.GetProperty("isLocked");
                bool value = (bool)propertyInfo.GetValue(_mouseOverWindow, null);
                propertyInfo.SetValue(_mouseOverWindow, !value, null);
                _mouseOverWindow.Repaint();
            }
        }
        */

        [MenuItem("Custom Shortcuts/Clear Console #&c")]
        static void ClearConsole()
        {
            Type type = Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditorInternal.LogEntries");
            type.GetMethod("Clear").Invoke(null, null);
        }

        [MenuItem("Custom Shortcuts/Copy Transform Values %#c", false, -101)]
        public static void CopyTransformValues()
        {
            if (Selection.gameObjects.Length == 0) return;
            var selectionTr = Selection.gameObjects[0].transform;
            _data = new TransformData(selectionTr.localPosition, selectionTr.localRotation, selectionTr.localScale);
        }

        [MenuItem("Custom Shortcuts/Paste Transform Values %#v", false, -101)]
        public static void PasteTransformValues()
        {
            foreach (var selection in Selection.gameObjects)
            {
                var selectionTr = selection.transform;
                Undo.RecordObject(selectionTr, "Paste Transform Values");
                selectionTr.localPosition = _data.localPosition;
                selectionTr.localRotation = _data.localRotation;
                selectionTr.localScale = _data.localScale;
            }
        }

        [MenuItem("Custom Shortcuts/Copy Center Position &k", false, -101)]
        public static void CopyCenterPosition()
        {
            if (Selection.gameObjects.Length == 0) return;
            var render = Selection.gameObjects[0].GetComponent<Renderer>();
            if (render == null) return;
            _dataCenter = render.bounds.center;
        }

        [MenuItem("Custom Shortcuts/Paste Center Position &%l", false, -101)]
        public static void PasteCenterPosition()
        {
            if (_dataCenter == null) return;
            foreach (var selection in Selection.gameObjects)
            {
                Undo.RecordObject(selection.transform, "Paste Center Position");
                selection.transform.position = _dataCenter.Value;
            }
        }

        [MenuItem("Custom Shortcuts/Clear Saved Data %u")]
        public static void ClearData()
        {
            Debug.Log("<color=cyan>All data is cleared</color>");
           // SaveManager.DeleteSave();
        }

        [MenuItem("Custom Shortcuts/Disable Raycast Targets And Maskable in Scene")]
        public static void DisableUIRaycasts()
        {
            int rayChangedImagesCount = 0,
                maskChangedImagesCount = 0,
                rayChangedTextCount = 0,
                maskChangedTextCount = 0;

            Image[] images = Object.FindObjectsOfType<Image>();
            var texts = Object.FindObjectsOfType<TMP_Text>();
            foreach (Image image in images)
            {
                if (!image.TryGetComponent<Button>(out _) && image.raycastTarget)
                {
                    rayChangedImagesCount++;
                    image.raycastTarget = false;
                }

                if (image.maskable && image.transform.parent == null || image.maskable && image.transform.parent != null &&
                    !image.transform.parent.TryGetComponent<Mask>(out _))
                {
                    maskChangedImagesCount++;
                    image.maskable = false;
                }
            }

            foreach (var text in texts)
            {
                if (text.maskable)
                {
                    rayChangedTextCount++;
                    text.maskable = false;
                }

                if (text.raycastTarget)
                {
                    maskChangedTextCount++;
                    text.raycastTarget = false;
                }
            }

            Debug.Log(
                $"[IMAGES]: <color=cyan>{rayChangedImagesCount}</color> Rays AND <color=cyan>{maskChangedImagesCount}</color> Masks are disabled \t\t " +
                $"[TMPs]: <color=cyan>{rayChangedTextCount}</color> Rays AND <color=cyan>{maskChangedTextCount}</color> Masks are disabled");
        }
    }
}