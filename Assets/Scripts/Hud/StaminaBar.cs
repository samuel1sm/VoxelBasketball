using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private CharacterStatus characterStatus;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;
    
    private Slider _slider;
    
    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.value = 1;
    }

    void Start()
    {
        characterStatus.StaminaUpdated += UpdateStaminaBar;
    }

    private void UpdateStaminaBar(float stamina)
    {
        _slider.value = stamina;
        fill.color = gradient.Evaluate(stamina);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
