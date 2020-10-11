using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
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
    private static int _id;
    private static string _name;
    private static TilemapRenderer _tilemapRenderer;

    static void Hierarchy(int instanceId, Rect rect)
    {
        var obj = EditorUtility.InstanceIDToObject(instanceId);
        if (obj != null)
        {
            if (Selection.instanceIDs.Contains(instanceId))
            {
                _id = obj.GetInstanceID();
                _tilemapRenderer = (obj as GameObject)?.GetComponent<TilemapRenderer>();
            }
        }
    }

    [MenuItem("Custom Hierarchy/Rename objects")]
    static void Init()
    {
        CustomHierarchy window = (CustomHierarchy)GetWindow(typeof(CustomHierarchy));
        window.Show();
    }

    void OnGUI()
    {
        if (GUILayout.Button("Roof"))
        {
            Object obj = EditorUtility.InstanceIDToObject(_id);
            _tilemapRenderer.sortingOrder = 0;
            obj.name = "Roof";
        }

        if (GUILayout.Button("Stair"))
        {
            Object obj = EditorUtility.InstanceIDToObject(_id);
            _tilemapRenderer.sortingOrder = 1;
            obj.name = "Stair";
        }

        if (GUILayout.Button("Web"))
        {
            Object obj = EditorUtility.InstanceIDToObject(_id);
            _tilemapRenderer.sortingOrder = 1;
            obj.name = "Web";
        }

        if (GUILayout.Button("Flames"))
        {
            Object obj = EditorUtility.InstanceIDToObject(_id);
            _tilemapRenderer.sortingOrder = 1;
            obj.name = "Flames";
        }

        if (GUILayout.Button("Background"))
        {
            Object obj = EditorUtility.InstanceIDToObject(_id);
            _tilemapRenderer.sortingOrder = 0;
            obj.name = "Background";
        }

        if (GUILayout.Button("Floor"))
        {
            Object obj = EditorUtility.InstanceIDToObject(_id);
            _tilemapRenderer.sortingOrder = 0;
            obj.name = "Floor";
        }

        if (GUILayout.Button("Walls"))
        {
            Object obj = EditorUtility.InstanceIDToObject(_id);
            _tilemapRenderer.sortingOrder = 1;
            obj.name = "Walls";
        }

        if (GUILayout.Button("Platform"))
        {
            Object obj = EditorUtility.InstanceIDToObject(_id);
            _tilemapRenderer.sortingOrder = 1;
            obj.name = "Platform";
        }

        if (GUILayout.Button("Decoration"))
        {
            Object obj = EditorUtility.InstanceIDToObject(_id);
            _tilemapRenderer.sortingOrder = 1;
            obj.name = "Decoration";
        }

        if (GUILayout.Button("TransparentOverlap"))
        {
            Object obj = EditorUtility.InstanceIDToObject(_id);
            _tilemapRenderer.sortingOrder = 2;
            obj.name = "TransparentOverlap";
        }

        if (GUILayout.Button("Spawns"))
        {
            Object obj = EditorUtility.InstanceIDToObject(_id);
            obj.name = "Spawns";
        }

        if (GUILayout.Button("ZombieSpawn"))
        {
            Object obj = EditorUtility.InstanceIDToObject(_id);
            obj.name = "ZombieSpawn";
        }

        if (GUILayout.Button("WizardSpawn"))
        {
            Object obj = EditorUtility.InstanceIDToObject(_id);
            obj.name = "WizardSpawn";
        }

        if (GUILayout.Button("SwardSpawn"))
        {
            Object obj = EditorUtility.InstanceIDToObject(_id);
            obj.name = "SwardSpawn";
        }

        if (GUILayout.Button("BatSpawn"))
        {
            Object obj = EditorUtility.InstanceIDToObject(_id);
            obj.name = "BatSpawn";
        }

        if (GUILayout.Button("Bat"))
        {
            Object obj = EditorUtility.InstanceIDToObject(_id);
            obj.name = "Bat";
        }

        if (GUILayout.Button("Sward"))
        {
            Object obj = EditorUtility.InstanceIDToObject(_id);
            obj.name = "Sward";
        }

        if (GUILayout.Button("Zombie"))
        {
            Object obj = EditorUtility.InstanceIDToObject(_id);
            obj.name = "Zombie";
        }

        if (GUILayout.Button("Wizard"))
        {
            Object obj = EditorUtility.InstanceIDToObject(_id);
            obj.name = "Wizard";
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("GatesLeft"))
        {
            Object obj = EditorUtility.InstanceIDToObject(_id);
            obj.name = "GatesLeft";
        }

        if (GUILayout.Button("GatesRight"))
        {
            Object obj = EditorUtility.InstanceIDToObject(_id);
            obj.name = "GatesRight";
        }

        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("EmptySpace"))
        {
            Object obj = EditorUtility.InstanceIDToObject(_id);
            obj.name = "EmptySpace";
        }
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }
}

public class CreateGameObjects : EditorWindow
{
    private static List<GameObject> _createObjects = new List<GameObject>();
    private static string _roomName;
    private static int _number;
    private int index;

    [MenuItem("Custom Hierarchy/Create new level template")]
    private static void CallWindowCreateObjects()
    {
        CreateGameObjects window = (CreateGameObjects)GetWindow(typeof(CreateGameObjects));
        window.Show();
    }

    private void OnInspectorUpdate()
    {
        Repaint();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Create level"))
        {
            CreateLevel();
        }
        
        EditorGUILayout.BeginHorizontal();
        _roomName = EditorGUILayout.TextField(_roomName);
        
        
        if (GUILayout.Button("Create"))
        {
            var gameObject = new GameObject {name = _roomName};
        }
        EditorGUILayout.EndHorizontal();
    }

    static void CreateLevel()
    {
        _number++;
        GameObject level = new GameObject {name = "Level (" + _number + ")"};
        level.AddComponent<Grid>();
        level.GetComponent<Grid>().cellSize = new Vector3(0.2f,0.2f);
        level.AddComponent<Container>();
        _createObjects.Add(new GameObject{name = "Background"});
        _createObjects.Add(new GameObject{name = "Walls"});
        _createObjects.Add(new GameObject{name = "Roof"});
        _createObjects.Add(new GameObject{name = "Floor"});
        _createObjects.Add(new GameObject{name =  "EmptySpace"});
        _createObjects.Add(new GameObject{name = "Platform"});
        _createObjects.Add(new GameObject{name = "Stair"});
        _createObjects.Add(new GameObject{name = "TransparentOverlap"});
        _createObjects.Add(new GameObject{name = "GatesLeft"});
        _createObjects.Add(new GameObject{name = "GatesRight"});
        _createObjects.Add(new GameObject{name = "Decoration"});
        _createObjects.Add(new GameObject{name = "Web"});
        _createObjects.Add(new GameObject{name = "Flames"});
        
        for (int i = 0; i < _createObjects.Count; i++)
        {
            _createObjects[i].transform.parent = level.transform;
            _createObjects[i].AddComponent<Tilemap>();
            _createObjects[i].AddComponent<TilemapRenderer>();
        }
        var spawns = new GameObject{name = "Spawns"};
        spawns.transform.parent = level.transform;
        var beginMap = new GameObject{name =  "BeginMap"};
        beginMap.transform.parent = level.transform;
        var endMap = new GameObject{name =  "EndMap"};
        endMap.transform.parent = level.transform;
    }
}