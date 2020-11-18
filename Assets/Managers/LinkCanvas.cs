using UnityEngine;

public class LinkCanvas : MonoBehaviour
{
    private Canvas _canvas;

    private void Start()
    {
        var canvas = GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
    }
}
