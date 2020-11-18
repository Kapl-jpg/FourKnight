using System.Xml.Linq;
using System.IO;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    [SerializeField] private SingletonParameters singletonParameters;
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

    public int number;

    private string _path;

    private XElement _root;

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
        SaveNumber(number);
    }

    public void SaveNumber(int numberKnight)
    {
        number = numberKnight;
        _root = new XElement("root");
        _root.Add(new XElement("number", number));
        XDocument data = new XDocument(_root);
        File.WriteAllText(_path, data.ToString());
    }

    private void LoadGame()
    {
        _root = XDocument.Parse(File.ReadAllText(_path)).Element("root");

        if (_root != null)
        {
            number = int.Parse(_root.Element("number")?.Value ?? string.Empty);
        }
        else
        {
            number = Random.Range(0, singletonParameters.Knights.Count);
        }

        singletonParameters.NumberChoseKnight = number;
    }
}

