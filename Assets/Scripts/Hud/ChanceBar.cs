using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ChanceBar : MonoBehaviour
{
    [SerializeField] private GameObject forceBar;
    [SerializeField] private GameObject midCircle;
    // [SerializeField] private RectTransform marker2;
    [SerializeField] private RectTransform marker;
    [SerializeField] private float makerVelocity;
    [SerializeField] private float percentageIncrement = 0.01f;
    [SerializeField] private float despawnDelay = 0.1f;
    [Range(0f,0.5f),SerializeField] private float endedPercentage;

    private Coroutine _coroutine;
    private float _radius;
    private float _centerPercentage;
    private void Awake()
    {
        _radius = Math.Abs(marker.transform.localPosition.x);

        foreach (var status in FindObjectsOfType<CharacterShootHandler>())
        {
            status.StartChanceMarker += StartChanceMovingMarker;
            status.StopChanceMarker += StopMovingMarker;
        }
    }
    

    private Vector3 AnglePosition(float value)
    {
        // image.fillAmount = percentage;
        var angle = value * Mathf.PI * 2f;

        angle -= Mathf.PI / 2; 
        var x =  _radius * Mathf.Sin(angle);
        var y =  _radius * Mathf.Cos(angle);
        
        return  new Vector3(x,y,0);
        
    }

    public void StartChanceMovingMarker(float value)
    {
        
        RandomizeMiddlePosition();

        forceBar.SetActive(true);
        
        _coroutine = StartCoroutine(MoveMarker());
    }

    private void RandomizeMiddlePosition()
    {
        _centerPercentage = Random.Range(0.1f, 0.4f);
        var position = AnglePosition(_centerPercentage);
        // marker2.localPosition = position;

        var angle = Vector3.Angle(Vector3.left, position.normalized);

        midCircle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle * -1 + 27f));
    }

    public float StopMovingMarker(float extraValues)
    {
        if (_coroutine == null) return 0;
        StartCoroutine(DeactivateBar());
        StopCoroutine(_coroutine);
        var percentageDistance = Mathf.Abs(endedPercentage - _centerPercentage) * 2; 
        
        // print((1 - percentageDistance) * extraValues);
        return (1 - percentageDistance) * extraValues;
    }

    IEnumerator MoveMarker()
    {
        var localPercentage = 0f;
        while (true)
        {
            
            localPercentage = localPercentage + percentageIncrement > 1 ? 0 : localPercentage + percentageIncrement;
            
            
            endedPercentage = localPercentage > 0.5 ? 1 - localPercentage : localPercentage; 

            marker.transform.localPosition  = AnglePosition(endedPercentage);

            yield return new WaitForSeconds(makerVelocity);

        }
    }

    IEnumerator DeactivateBar()
    {
        yield return new WaitForSeconds(despawnDelay);
        forceBar.SetActive(false);

    }
}
