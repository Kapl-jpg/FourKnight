using UnityEditor;
using UnityEditor.SceneManagement;

public class ChoseMenuItem : Editor
{
    [MenuItem("ChoseScenes/Start Game")]
    private static void ChoseStartScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/StartGame.unity");
    }

    [MenuItem("ChoseScenes/Home")]
    private static void ChoseHomeScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/HomeScene.unity");
    }

    [MenuItem("ChoseScenes/Level Scene")]
    private static void ChoseLevelScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Parts.unity");
    }
}
