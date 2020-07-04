using System;
using UnityEditor;
using UnityEditor.Profiling;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

public class ChoseMenuItem : Editor
{
    
    [MenuItem("ChoseScenes/Start Game")]
    private static void ChoseStartScene()
    {
        SaveTheChanges();
        EditorSceneManager.OpenScene("Assets/Scenes/StartGame.unity");
    }

    [MenuItem("ChoseScenes/Home")]
    private static void ChoseHomeScene()
    {
        SaveTheChanges();
        EditorSceneManager.OpenScene("Assets/Scenes/HomeScene.unity");
    }

    [MenuItem("ChoseScenes/Level Scene")]
    private static void ChoseLevelScene()
    {
        SaveTheChanges();
        EditorSceneManager.OpenScene("Assets/Scenes/Parts.unity");
    }

    private static void SaveTheChanges()
    {
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
    }
}

public class CustomHierarchy : EditorWindow
{
    static CustomHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI += Hierarchy;
    }

    private static int id;

    static void Hierarchy(int instanceId, Rect rect)
    {
        Object o = EditorUtility.InstanceIDToObject(instanceId);
        Debug.Log(instanceId);
    }

    [MenuItem("Example/ID To Name")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        CustomHierarchy window = (CustomHierarchy)EditorWindow.GetWindow(typeof(CustomHierarchy));
        window.Show();
    }

    void OnGUI()
    {
        id = EditorGUILayout.IntField("Instance ID:", id);
        if (GUILayout.Button("Find Name"))
        {
            Object obj = EditorUtility.InstanceIDToObject(id);
            if (!obj)
                Debug.LogError("No object could be found with instance id: " + id);
            else
                Debug.Log("Object's name: " + obj.name);
        }
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }
}
