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
    [SerializeField] private LayerMask _hitMask;
    [SerializeField] public bool isAI;
    [SerializeField] private float attackRadius = 1.5f;


    private bool _hasTheBall;

    public bool isTeamOne;
    public event Action<CharacterStatus> OnCatchTheBall = delegate(CharacterStatus f) { };

    public event Action OnBallStollen = delegate { };
    public event Action<float> StaminaUpdated = delegate(float f) { };
    public event Action<float> StartChanceMarker = delegate { };
    public event Func<float, float> StopChanceMarker;

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
        StartChanceMarker(CalculateChanceOfSuccess());
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

    public bool GetHasTheBall()
    {
        return _hasTheBall;
    }

    public void UpdateIsAttacking()
    {
        _hasTheBall = !_hasTheBall;
    }


    public void TookTheBall()
    {
        _hasTheBall = true;
        OnCatchTheBall(this);
    }

    public void LooseTheBall()
    {
        _hasTheBall = false;
        OnBallStollen();
    }

    public void CastAttack()
    {
        RaycastHit[] results = new RaycastHit[4];
        var size = Physics.SphereCastNonAlloc(transform.position, attackRadius, transform.forward,
            results, 0, _hitMask);
        Debug.DrawRay(transform.position, transform.up * 2, Color.blue, 2);
        if (size > 1)
        {
            for (int i = 0; i < size; i++)
            {
                // print(results[i].transform.gameObject.name);
                var status = results[i].transform.GetComponent<CharacterStatus>();
                if (isTeamOne == status.isTeamOne) continue;

                if (status.GetHasTheBall())
                {
                    TookTheBall();
                    BallManager.Instance.StateChanged(BallState.WasCollected, transform, true);
                    status.LooseTheBall();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && !_hasTheBall)
        {
            TookTheBall();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}