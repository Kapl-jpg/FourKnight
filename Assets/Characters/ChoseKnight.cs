using System.Collections.Generic;
using UnityEngine;

public class ChoseKnight : MonoBehaviour
{
    private GeneralInformation _generalInformation;
    private int _numberKnight;
    [SerializeField] private float targetRange;
    [SerializeField] private bool createAfkKnights;
    [SerializeField] private GameObject[] knights;
    private GameObject dontDestroy;
    
    private GameObject _nearbyKnight;
    private List<GameObject> _afkKnights = new List<GameObject>();

    private int _activeKnightNumber;
    private int _afkKnightNumber;
    
    private Vector2 _centerActiveKnight;
    private Vector2 _getPosition;

    private void GetComponents()
    {
        _generalInformation = gameObject.GetComponent<GeneralInformation>();
        dontDestroy = _generalInformation.DontDestroyManager;
        _numberKnight = _generalInformation.SaveLoad.Number;
    }

    private void Start()
    {
        GetComponents();
        ChoseTheMainKnight(_numberKnight, _generalInformation.SpawnPointMainKnight.position);
        if (createAfkKnights)
        {
            for (int i = 0; i < knights.Length; i++)
            {
                if (i != _numberKnight)
                    NeedAfkKnight(i);
            }
        }
    }

    private void NeedAfkKnight(int needKnights)
    {
        var afkKnight = Instantiate(knights[needKnights], transform.position, Quaternion.identity);
        _afkKnights.Add(afkKnight);
        afkKnight.GetComponent<KnightController>().GeneralInformation = _generalInformation;
        afkKnight.GetComponent<KnightController>().Afk = true;
        afkKnight.GetComponent<KnightController>().MyNumber = needKnights;
    }

    void ChoseTheMainKnight(int numberMainCharacter, Vector2 spawn)
    {
        if (_generalInformation.ActiveKnight != null)
        {
            Destroy(_generalInformation.ActiveKnight);
        }

        var mainCharacter = Instantiate(knights[numberMainCharacter], spawn, Quaternion.identity);
        _generalInformation.ActiveKnight = mainCharacter;
        _generalInformation.KnightController = mainCharacter.GetComponent<KnightController>();
        _generalInformation.KnightController.GeneralInformation = _generalInformation;
        _generalInformation.KnightController.MyNumber = numberMainCharacter;
        _generalInformation.CameraTranslate.GetTransform();
        _generalInformation.ButtonControl.KnightControl = _generalInformation.KnightController;
        _generalInformation.KnightController.HealthAndArmor = dontDestroy.GetComponent<HealthAndArmor>();
        dontDestroy.GetComponent<ParametersCharacter>().SetValue();
    }

    private void Update()
    {
        if(createAfkKnights){
            if (ClosetObj() != null)
            {
                float distance =
                    Vector2.Distance(_generalInformation.KnightController.GetComponent<BoxCollider2D>().bounds.center,
                        _nearbyKnight.transform.position);

                if (distance < targetRange)
                {
                    _generalInformation.KnightController.Trigger = true;
                    if (_generalInformation.KnightController.ClickMark)
                    {
                        _getPosition = _generalInformation.ActiveKnight.transform.position;
                        _activeKnightNumber = _generalInformation.KnightController.MyNumber;
                        _afkKnightNumber = _nearbyKnight.GetComponent<KnightController>().MyNumber;
                        Destroy(_generalInformation.ActiveKnight);
                        _afkKnights.Remove(_nearbyKnight);
                        Destroy(_nearbyKnight);
                        NeedAfkKnight(_activeKnightNumber);
                        ChoseTheMainKnight(_afkKnightNumber, _getPosition);
                    }
                }
                else
                {
                    _generalInformation.KnightController.Trigger = false;
                }
            }
        }

        GameObject ClosetObj()
        {
            foreach (GameObject go in _afkKnights)
            {
                float distance =
                    Vector2.Distance(_generalInformation.KnightController.GetComponent<BoxCollider2D>().bounds.center,
                        go.transform.position);
                if (distance < targetRange)
                {
                    _nearbyKnight = go;
                }
            }

            return _nearbyKnight;
        }
    }
}