using System.Collections.Generic;
using UnityEngine;

public class ChoseKnight : MonoBehaviour
{
    [SerializeField] private ButtonControl buttonControl;
    [SerializeField] private SingletonParameters singletonParameters;
    [SerializeField] private GeneralInformation generalInformation;
    [SerializeField] private CameraTranslate cameraTranslate;
    [SerializeField] private CollectionsOfContainer collectionsOfContainer;
    private int numberKnight;
    [SerializeField] private float targetRange;
    [SerializeField] private bool createAfkKnights;
    private List<GameObject> knights = new List<GameObject>();
    private GameObject dontDestroy;
    
    private GameObject _nearbyKnight;
    private List<GameObject> afkKnights = new List<GameObject>();

    private int _activeKnightNumber;
    private int _afkKnightNumber;
    
    private Vector2 _centerActiveKnight;
    private Vector2 _getPosition;

    private void GetComponents()
    {
        numberKnight = singletonParameters.NumberChoseKnight;
    }

    private void Start()
    {
        
        knights = singletonParameters.Knights;
        GetComponents();
        ChoseTheMainKnight(numberKnight, generalInformation.SpawnPointMainKnight.position);
        if (createAfkKnights)
        {
            for (int i = 0; i < knights.Count; i++)
            {
                if (i != numberKnight)
                    NeedAfkKnight(i);
            }
        }
    }
    
    private void NeedAfkKnight(int needKnights)
    {
        var afkKnight = Instantiate(knights[needKnights], transform.position, Quaternion.identity);
        afkKnights.Add(afkKnight);
        var afkKnightController = afkKnight.GetComponent<KnightController>();
        afkKnightController.Afk = true;
        afkKnightController.MyNumber = needKnights;
        afkKnightController.CollectionsOfContainer = collectionsOfContainer;
    }

    private void ChoseTheMainKnight(int numberMainCharacter, Vector2 spawn)
    {
        if (generalInformation.ActiveKnight != null)
        {
            Destroy(generalInformation.ActiveKnight);
        }

        var mainCharacter = Instantiate(knights[numberMainCharacter], spawn, Quaternion.identity);
        cameraTranslate.ActiveKnight = mainCharacter;
        var knightController = mainCharacter.GetComponent<KnightController>();
        knightController.Speed = singletonParameters.RunningSpeedKnight;
        knightController.SpeedStair = singletonParameters.SpeedStairKnight;
        knightController.JumpForce = singletonParameters.ForceJumpKnight;
        knightController.SecondsToWaitAnimation = singletonParameters.SecondsToWaitAnimationKnight;
        knightController.CollectionsOfContainer = collectionsOfContainer;
        buttonControl.KnightControl = knightController;
    }

    private void ReElection()
    {
        
    }
    
    private void Update()
    {/*
        if(createAfkKnights){
            if (ClosetObj() != null)
            {
                float distance =
                    Vector2.Distance(generalInformation.KnightController.GetComponent<BoxCollider2D>().bounds.center,
                        _nearbyKnight.transform.position);

                if (distance < targetRange)
                {
                    generalInformation.KnightController.Trigger = true;
                    if (generalInformation.KnightController.ClickMark)
                    {
                        _getPosition = generalInformation.ActiveKnight.transform.position;
                        _activeKnightNumber = generalInformation.KnightController.MyNumber;
                        _afkKnightNumber = _nearbyKnight.GetComponent<KnightController>().MyNumber;
                        Destroy(generalInformation.ActiveKnight);
                        afkKnights.Remove(_nearbyKnight);
                        Destroy(_nearbyKnight);
                        NeedAfkKnight(_activeKnightNumber);
                        ChoseTheMainKnight(_afkKnightNumber, _getPosition);
                    }
                }
                else
                {
                    generalInformation.KnightController.Trigger = false;
                }
            }
        }

        GameObject ClosetObj()
        {
            foreach (GameObject item in afkKnights)
            {
                float distance = Vector2.Distance(generalInformation.KnightController.GetComponent<BoxCollider2D>().bounds.center,
                        item.transform.position);
                if (distance < targetRange)
                {
                    _nearbyKnight = item;
                }
            }
            
            return _nearbyKnight;
        }*/
    }
}