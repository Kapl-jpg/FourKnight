using System.Xml.Linq;
using System.IO;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    private CameraTranslate _cameraTranslate;

    public CameraTranslate CameraTranslate
    {
        get => _cameraTranslate;
        set => _cameraTranslate = value;
    }

    private Camera _camera;

    public Camera Camera
    {
        get => _camera;
        set => _camera = value;
    }

    private string _path;

    private XElement _root;

    private int _number;

    public int Number
    {
        get => _number;
        set => _number = value;
    }
    private void Awake()
    {
#if !UNITY_EDITOR
        _path = Path.Combine(Application.persistentDataPath, "Data.xml");
#else
        _path = Path.Combine(Application.dataPath, "Data.xml");
#endif
        LoadGame();
    }

#if !UNITY_EDITOR
    private void OnApplicationPause(bool pauseStatus)
    {
        SaveGame();
    }
#endif
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    void SaveGame()
    {
        _root = new XElement("root");
        _root.Add(new XElement("number", _number));
        XDocument data = new XDocument(_root);
        File.WriteAllText(_path, data.ToString());
    }

    void LoadGame()
    {
        if (!File.Exists(_path))
        {
            _root = XDocument.Parse(File.ReadAllText(_path)).Element("root");
        }
        else
        {
            _root = XDocument.Parse(File.ReadAllText(_path)).Element("root");
        }

        if (_root != null)
        {
            _number = int.Parse(_root.Element("number").Value);
        }
    }
}

