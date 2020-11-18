using System.Collections.Generic;
using UnityEngine;

public class SingletonParameters : MonoBehaviour
{
    ///////////////////////////////
    /// Параметры одного рыцаря (любого)
    public int NumberChoseKnight { get; set; }
    [Header("Knight parameters")]
    [SerializeField] private float runningSpeedKnight;

    public float RunningSpeedKnight
    {
        get => runningSpeedKnight;
        set => runningSpeedKnight = value;
    }
    [SerializeField] private float forceJumpKnight;

    public float ForceJumpKnight
    {
        get => forceJumpKnight;
        set => forceJumpKnight = value;
    }
    [SerializeField] private float speedStairKnight;

    public float SpeedStairKnight
    {
        get => speedStairKnight;
        set => speedStairKnight = value;
    }
    [SerializeField] private float secondsToWaitAnimationKnight;

    public float SecondsToWaitAnimationKnight
    {
        get => secondsToWaitAnimationKnight;
        set => secondsToWaitAnimationKnight = value;
    }
    
    ///////////////////////////////
    /// Информация обо всех рыцарях
    [Header("All knights")]
    [Space]
    [SerializeField]
    private List<GameObject> knights = new List<GameObject>();
    
    public List<GameObject> Knights => knights;
    
    [SerializeField] private float coinsKnight;

    public float CoinsKnight
    {
        get => coinsKnight;
        set => coinsKnight = value;
    }
}
