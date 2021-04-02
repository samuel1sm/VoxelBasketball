using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterShootHandler : MonoBehaviour
{
    public event Action<float> StartChanceMarker = delegate { };
    public event Func<float, float> StopChanceMarker;
    [SerializeField] private float successChance = 100f;



    private void Awake()
    {
    }

    private float CalculateChanceOfSuccess()
    {
        return successChance;
    }

    public void StartChanceBar()
    {
        StartChanceMarker(CalculateChanceOfSuccess());
    }

    public float StopChanceBar(float extraModifiers)
    {
        // var extraModifiers = hoopDistance * stamina;
        var result = StopChanceMarker(extraModifiers);
        // print(result);
        return result;
    }

    

}