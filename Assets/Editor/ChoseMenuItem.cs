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
    private static int _id;
    private static string _name;
    static CreateGameObjects()
    {
        EditorApplication.hierarchyWindowItemOnGUI += Hierarchy;
    }
    static void Hierarchy(int instanceId, Rect rect)
    {
        var obj = EditorUtility.InstanceIDToObject(instanceId);
        if (obj != null)
        {
            if (Selection.instanceIDs.Contains(instanceId))
            {
                _id = obj.GetInstanceID();
            }
        }
    }

    private static GameObject _room;
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
        if (GUILayout.Button("Create room"))
        {
            _room = new GameObject {name = "Room (" + _number + ")"};
            _room.AddComponent<Grid>().cellSize = new Vector3(0.2f,0.2f);
            _room.AddComponent<Container>();
            CreateRoom();
        }
        
        EditorGUILayout.BeginHorizontal();
        _roomName = EditorGUILayout.TextField(_roomName);
        
        
        if (GUILayout.Button("Create"))
        {
            var gameObject = new GameObject {name = _roomName};
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginVertical();
        
        if (GUILayout.Button("Copy element"))
        {
            var obj = EditorUtility.InstanceIDToObject(_id) as GameObject;
            var duplicate =Instantiate(obj, _room.transform, true);
        }
        EditorGUILayout.EndVertical();
        if (GUILayout.Button("Transparency"))
        {
            var obj = EditorUtility.InstanceIDToObject(_id)as GameObject;
            if (!(obj is null)) obj.GetComponent<Tilemap>().color = new Color(1, 1, 1, 0.5f);
        }
    }

    static void CreateRoom()
    {
        var createObjects =new List<GameObject>();
        var colliderPool = new List<GameObject>();
        _number++;

        var backGround = new GameObject {name = "Background"};
        backGround.AddComponent<Tilemap>();
        backGround.AddComponent<TilemapRenderer>().sortingOrder = 0;
        createObjects.Add(backGround);
        var walls = new GameObject {name = "Walls"};
        walls.AddComponent<TilemapRenderer>().sortingOrder = 0;
        createObjects.Add(walls);
        colliderPool.Add(walls);
        var ceiling= new GameObject{name = "Ceiling"};
        ceiling.AddComponent<TilemapRenderer>().sortingOrder = 0;
        createObjects.Add(ceiling);
        colliderPool.Add(ceiling);
        var floor = new GameObject {name = "Floor"};
        floor.AddComponent<TilemapRenderer>().sortingOrder = 0;
        createObjects.Add(floor);
        colliderPool.Add(floor);
        var emptySpace = new GameObject {name = "EmptySpace"};
        emptySpace.AddComponent<Tilemap>();
        emptySpace.AddComponent<TilemapRenderer>().sortingOrder = 0;
        createObjects.Add(emptySpace);
        var platform = new GameObject {name = "Platform"};
        platform.AddComponent<TilemapRenderer>().sortingOrder = 1;
        createObjects.Add(platform);
        colliderPool.Add(platform);
        var stair = new GameObject {name = "Stair"};
        stair.AddComponent<TilemapRenderer>().sortingOrder = 2;
        stair.AddComponent<TilemapCollider2D>();
        createObjects.Add(stair);
        var transparentOverlap = new GameObject {name = "TransparentOverlap"};
        transparentOverlap.AddComponent<Tilemap>().color = new Color(1,1,1,0.5f);
        transparentOverlap.AddComponent<TilemapRenderer>().sortingOrder = 3;
        createObjects.Add(transparentOverlap);
        colliderPool.Add(transparentOverlap);
        var decoration = new GameObject {name = "Decoration"};
        decoration.AddComponent<Tilemap>();
        decoration.AddComponent<TilemapRenderer>().sortingOrder = 3;
        createObjects.Add(decoration);
        var web = new GameObject {name = "Web"};
        web.AddComponent<Tilemap>().color = new Color(1,1,1,0.25f);
        web.AddComponent<TilemapRenderer>().sortingOrder = 4;
        createObjects.Add(web);
        
        foreach (var item in createObjects)
        {
            item.transform.parent = _room.transform;
        }

        foreach (var item in colliderPool)
        {
            item.AddComponent<TilemapCollider2D>().usedByComposite = true;
            item.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            item.AddComponent<CompositeCollider2D>();
        }
        var spawns = new GameObject{name = "Spawns"};
        spawns.transform.parent = _room.transform;
        var beginMap = new GameObject{name =  "BeginMap"};
        beginMap.transform.parent = _room.transform;
    }
}