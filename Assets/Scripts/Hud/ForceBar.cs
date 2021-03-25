using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceBar : MonoBehaviour
{
    
    [SerializeField] private Image image;
    [SerializeField] private RectTransform marker;

    [Range(0f,0.5f),SerializeField] private float percentage;

    private float radius;

    private void Awake()
    {
        radius = Math.Abs(marker.transform.localPosition.x);
    }

    private void Update()
    {
        UpdateBar();
    }

    private void UpdateBar()
    {
        // image.fillAmount = percentage;
        var angle = percentage * Mathf.PI * 2f;

        angle -= Mathf.PI / 2; 
        var x =  radius * Mathf.Sin(angle);
        var y =  radius * Mathf.Cos(angle);
        
        
        marker.transform.localPosition = new Vector3(x,y,0);
    }
    

    // IEnumerator 
}
