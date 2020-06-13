using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

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

    static void SaveTheChanges()
    {
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
    }
}
