using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField] private float staminaRegenerationCountdown = 0.8f;
    [SerializeField] private float passiveStaminaRegeneration = 2f;
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float successChance = 100f;
    public event Action<float> StaminaUpdated = delegate(float f) {  };
    public event Action<float> StartChanceMarker = delegate {  };
    public event Func<float,float> StopChanceMarker; 
    
    private float _actualStamina;
    
    private void Awake()
    {
        _actualStamina = maxStamina;
    }

    private void Start()
    {
        StartCoroutine(StaminaRegen());
    }

    private float CalculateChanceOfSuccess()
    {

        return successChance;
    }

    public void StartChanceBar()
    {
        StartChanceMarker( CalculateChanceOfSuccess());
    }
    
    public float StopChanceBar(float hoopDistance)
    {
        var extraValues = hoopDistance * _actualStamina / maxStamina;
        var result = StopChanceMarker(extraValues);
        // print(result);
        return result;
    }
    

    public void UpdateStamina(float value)
    {
        _actualStamina += value;
        _actualStamina = Mathf.Clamp(_actualStamina, 0, maxStamina);
        
        StaminaUpdated(_actualStamina / maxStamina);
    }

    IEnumerator StaminaRegen()
    {
        while (true)
        {
            yield return new WaitForSeconds(staminaRegenerationCountdown);
            UpdateStamina(passiveStaminaRegeneration);
        }
    }
}

