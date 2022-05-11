using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public static class Shortcuts
{
    private const string FindSourceAssetString = "Custom Shortcuts/GameObject/Find Source Asset %e";
    private const string HideSelectionString = "Custom Shortcuts/GameObject/Hide Selection %h";
    private const string ViewTopDownString = "Custom Shortcuts/View/Toggle View Top-Down &1";
    private const string ViewLeftRightString = "Custom Shortcuts/View/Toggle View Left-Right &2";
    private const string ViewFrontBackString = "Custom Shortcuts/View/Toggle View Front-Back &3";
    private const string ToggleOrthogonalString = "Custom Shortcuts/View/Toggle View Perspective-Orthogonal &`";
    private const string LockInspectorString = "Custom Shortcuts/View/Toggle Inspector Lock &l";


    // Hide/show selected objects

    #region Hide Selection

    [MenuItem(HideSelectionString, true)]
    public static bool ValidateHideSelection()
    {
        var hidden_objects = 0;
        var shown_objects = 0;

        foreach (var obj in Selection.gameObjects)
            if (obj.activeSelf)
                shown_objects++;
            else
                hidden_objects++;

        return hidden_objects != 0 || shown_objects != 0;
    }

    [MenuItem(HideSelectionString)]
    private static void HideSelection()
    {
        var hidden_objects = 0;
        var shown_objects = 0;

        foreach (var obj in Selection.gameObjects)
            if (obj.activeSelf)
                shown_objects++;
            else
                hidden_objects++;

        if (hidden_objects == 0 && shown_objects == 0)
            return;

        var toggle = hidden_objects != 0;
        foreach (var obj in Selection.gameObjects) obj.SetActive(toggle);
    }

    #endregion


    // Find the source asset in the project that is currently selected

    #region Find Source Asset

    [MenuItem(FindSourceAssetString, true)]
    public static bool ValidateFindSourceAsset()
    {
        var gameObject = Selection.activeGameObject;
        if (gameObject == null) return false;

        if (PrefabUtility.GetPrefabParent(gameObject) != null) return true;
        var meshFilter = gameObject.GetComponent<MeshFilter>();

        return meshFilter != null && meshFilter.sharedMesh != null && AssetDatabase.Contains(meshFilter.sharedMesh);
    }

    [MenuItem(FindSourceAssetString)]
    public static void FindSourceAsset()
    {
        var gameObject = Selection.activeGameObject;
        if (gameObject == null)
            return;

        var parent = PrefabUtility.GetPrefabParent(gameObject);
        if (parent != null)
        {
            Selection.activeObject = parent;
            EditorGUIUtility.PingObject(gameObject);
            return;
        }

        var meshFilter = gameObject.GetComponent<MeshFilter>();
        if (meshFilter == null)
            return;

        var mesh = meshFilter.sharedMesh;
        if (mesh == null)
            return;

        Selection.activeObject = mesh;
    }

    #endregion


    // Toggle between top and down view 

    #region Set View Top-Down

    [MenuItem(ViewTopDownString, true)]
    public static bool ValidateSetViewTopDown()
    {
        return SceneView.lastActiveSceneView != null;
    }

    [MenuItem(ViewTopDownString)]
    public static void SetViewTopDown()
    {
        var view = SceneView.lastActiveSceneView;
        if (view == null)
            return;

        var topDirection = kDirectionRotations[1];
        var downDirection = kDirectionRotations[4];
        ToggleBetweenViewDirections(view, topDirection, downDirection);
    }

    #endregion


    // Toggle between left and right view 

    #region Set View Left-Right

    [MenuItem(ViewLeftRightString, true)]
    public static bool ValidateSetViewLeftRight()
    {
        return SceneView.lastActiveSceneView != null;
    }

    [MenuItem(ViewLeftRightString)]
    public static void SetViewLeftRight()
    {
        var view = SceneView.lastActiveSceneView;
        if (view == null)
            return;

        var leftDirection = kDirectionRotations[3];
        var rightDirection = kDirectionRotations[0];
        ToggleBetweenViewDirections(view, leftDirection, rightDirection);
    }

    #endregion


    // Toggle between front and back view 

    #region Set View Front-Back

    [MenuItem(ViewFrontBackString, true)]
    public static bool ValidateSetFrontBack()
    {
        return SceneView.lastActiveSceneView != null;
    }

    [MenuItem(ViewFrontBackString)]
    public static void SetViewFrontBack()
    {
        var view = SceneView.lastActiveSceneView;
        if (view == null)
            return;

        var frontDirection = kDirectionRotations[2];
        var backDirection = kDirectionRotations[5];
        ToggleBetweenViewDirections(view, frontDirection, backDirection);
    }

    #endregion


    // Toggle between perspective and orthogonal view 

    #region Toggle Orthogonal

    [MenuItem(ToggleOrthogonalString, true)]
    public static bool ValidateToggleOrthogonal()
    {
        return SceneView.lastActiveSceneView != null;
    }

    [MenuItem(ToggleOrthogonalString)]
    public static void ToggleOrthogonal()
    {
        var view = SceneView.lastActiveSceneView;
        if (view == null)
            return;

        view.LookAt(view.pivot, view.rotation, view.size, !view.orthographic);
    }

    #endregion


    // Toggle the inspector lock of the first inspector window

    #region Toggle Inspector Lock

    private static void InitToggleInspectorLock()
    {
        inspectorIsLockedPropertyInfo = null;
        inspectorType = Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.InspectorWindow");
        if (inspectorType != null) inspectorIsLockedPropertyInfo = inspectorType.GetProperty("isLocked");

        inspectorInitialized = true;
    }

    private static bool inspectorInitialized;
    private static Type inspectorType;
    private static PropertyInfo inspectorIsLockedPropertyInfo;

    [MenuItem(LockInspectorString, true)]
    private static bool ValidateToggleInspectorLock()
    {
        if (!inspectorInitialized) InitToggleInspectorLock();

        if (inspectorIsLockedPropertyInfo == null)
            return false;
        var allInspectors = Resources.FindObjectsOfTypeAll(inspectorType);
        if (allInspectors.Length == 0)
            return false;
        var inspector = allInspectors[allInspectors.Length - 1] as EditorWindow;
        if (inspector == null)
            return false;
        return true;
    }

    [MenuItem(LockInspectorString)]
    private static void ToggleInspectorLock()
    {
        if (!inspectorInitialized) InitToggleInspectorLock();

        if (inspectorIsLockedPropertyInfo == null)
            return;
        var allInspectors = Resources.FindObjectsOfTypeAll(inspectorType);
        if (allInspectors.Length == 0)
            return;
        var inspector = allInspectors[allInspectors.Length - 1] as EditorWindow;
        if (inspector == null)
            return;

        var value = (bool)inspectorIsLockedPropertyInfo.GetValue(inspector, null);
        inspectorIsLockedPropertyInfo.SetValue(inspector, !value, null);
        inspector.Repaint();
    }

    #endregion

    #region Toggle Between View Directions (helper function)

    private static readonly Quaternion[] kDirectionRotations =
    {
        Quaternion.LookRotation(new Vector3(-1, 0, 0)), // right
        Quaternion.LookRotation(new Vector3(0, -1, 0)), // top
        Quaternion.LookRotation(new Vector3(0, 0, -1)), // front
        Quaternion.LookRotation(new Vector3(1, 0, 0)), // left
        Quaternion.LookRotation(new Vector3(0, 1, 0)), // down
        Quaternion.LookRotation(new Vector3(0, 0, 1)) // back
    };

    private const float kCompareEpsilon = 0.0001f;

    private static void ToggleBetweenViewDirections(SceneView view, Quaternion primaryDirection,
        Quaternion alternativeDirection)
    {
        var direction = primaryDirection * Vector3.forward;
        var dot = Vector3.Dot(view.camera.transform.forward, direction);
        if (dot < 1.0f - kCompareEpsilon)
            view.LookAt(view.pivot, primaryDirection, view.size, view.orthographic);
        else
            view.LookAt(view.pivot, alternativeDirection, view.size, view.orthographic);
    }

    #endregion
}