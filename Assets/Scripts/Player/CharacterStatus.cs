using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterStatus : MonoBehaviour
{
    [Header("Geral")] [SerializeField] public bool isAI;
    public int CharacterID;
    public bool isTeamOne;

    [Header("Stamina")] [SerializeField] private float staminaRegenerationCountdown = 0.8f;
    [SerializeField] private float passiveStaminaRegeneration = 2f;
    [SerializeField] private float maxStamina = 100f;

    [Header("Dash")] [SerializeField] private bool _hasTheBall;
    [SerializeField] private LayerMask _hitMask;
    [SerializeField] private float dashRadius = 1.5f;
    [SerializeField] private bool hasDashed = false;
    [SerializeField] private float dashDelay = 0.9f;

    private static int IdCount = 0;
    public event Action<CharacterStatus> OnCatchTheBall = delegate(CharacterStatus f) { };

    public event Action<bool> OnDashStatus = delegate(bool b) { };
    public event Action OnBallStollen = delegate { };
    public event Action<float> StaminaUpdated = delegate(float f) { };


    private float _actualStamina;

    private void Awake()
    {
        CharacterID = IdCount;
        IdCount++;
        _actualStamina = maxStamina;
    }

    private void Start()
    {
        StartCoroutine(StaminaRegen());
    }

    #region dash

    public bool GetHasDash()
    {
        return hasDashed;
    }

    public void UpdateHasCharged()
    {
        hasDashed = true;
        OnDashStatus(true);
        StartCoroutine(ReloadDash());
    }

    IEnumerator ReloadDash()
    {
        var delay = dashDelay;
        while (delay > 0)
        {
            yield return new WaitForSeconds(0.01f);
            delay -= 0.01f;
        }

        hasDashed = false;
        OnDashStatus(hasDashed);
    }

    #endregion


    #region Stamina

    public float GetStaminaPercentage()
    {
        return _actualStamina / maxStamina;
    }
    
    public void UpdateStamina(float value)
    {
        _actualStamina += value;
        _actualStamina = Mathf.Clamp(_actualStamina, 0, maxStamina);

        StaminaUpdated(_actualStamina / maxStamina);
    }

    private IEnumerator StaminaRegen()
    {
        while (true)
        {
            yield return new WaitForSeconds(staminaRegenerationCountdown);
            UpdateStamina(passiveStaminaRegeneration);
        }
    }

    #endregion


    #region Ball

    public void UpdateHasTheBall()
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

    public bool GetHasTheBall()
    {
        return _hasTheBall;
    }

    #endregion

    public void CastDash()
    {
        RaycastHit[] results = new RaycastHit[4];
        var size = Physics.SphereCastNonAlloc(transform.position, dashRadius, transform.forward,
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
        Gizmos.DrawWireSphere(transform.position, dashRadius);
    }
}