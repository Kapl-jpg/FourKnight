using UnityEngine;

public class LinkCanvas : MonoBehaviour
{
    private GameObject mainCamera;
    private Canvas _canvas;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
        GetCamera();
    }

    public void GetCamera()
    {
        _canvas.renderMode = RenderMode.ScreenSpaceCamera;
        _canvas.worldCamera = Camera.main;
    }
}
