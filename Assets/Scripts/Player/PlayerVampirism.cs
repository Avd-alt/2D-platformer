using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health), typeof(PlayerInput))]
public class PlayerVampirism : MonoBehaviour
{
    [SerializeField] private Transform _pointAura;
    [SerializeField] private LayerMask _layerMaskEnemy;

    private Health _playerHealth;
    private PlayerInput _playerInput;
    private Coroutine _vampirismCoroutine;
    private float _radiusAura = 4f;
    private float _damage = 10f;
    private bool _isRecharging;
    private float _timeToDelay = 1f;
    private float _oneSecondTimeAbility = 1f;

    public event Action ChangedValueHealth;

    public float Duration { get; private set; } = 0;
    public float DurationSpell { get; private set; } = 6f;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerHealth = GetComponent<Health>();
    }

    private void Start()
    {
        Duration = DurationSpell;
        _isRecharging = false;
    }

    private void Update()
    {
        if (_playerInput.IsTryingVampirism && _vampirismCoroutine == null && !_isRecharging)
        {
            _vampirismCoroutine = StartCoroutine(LaunchingAbility());
        }

        ChangedValueHealth?.Invoke();
    }

    private void OnEnable()
    {
        _playerHealth.Died += DisableComponentAtDeath;
    }

    private void OnDisable()
    {
        _playerHealth.Died -= DisableComponentAtDeath;
    }

    private void Vampirism()
    {
        Collider2D nearestEnemy = null;
        float shortDistance = float.MaxValue;

        Collider2D [] hitEnemies = Physics2D.OverlapCircleAll(_pointAura.position, _radiusAura, _layerMaskEnemy);

        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy != null)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);

                if(distance < shortDistance)
                {
                    nearestEnemy = enemy;
                    shortDistance = distance;
                }
            }
        }

        if (nearestEnemy != null && _damage > 0)
        {
            Health enemyHealth = nearestEnemy.GetComponent<Health>();

            if (enemyHealth != null)
            {
                float damageDealt = Mathf.Min(_damage, enemyHealth.CurrentHealth);

                enemyHealth.TakeDamage(damageDealt);

                _playerHealth.Heal(damageDealt);
            }
        }
    }

    private IEnumerator LaunchingAbility()
    {
        float timeToStopSkill = 0;
        var delay = new WaitForSeconds(_timeToDelay);

        while (Duration > timeToStopSkill)
        {
            Vampirism();
            Duration -= _oneSecondTimeAbility;

            yield return delay;
        }

        _playerInput.DeactivateVampirismTrying();

        yield return StartCoroutine(ResettingDurationAbility());

        _vampirismCoroutine = null;
    }

    private IEnumerator ResettingDurationAbility()
    {
        var delay = new WaitForSeconds(_timeToDelay);

        while (Duration < DurationSpell)
        {
            _isRecharging = true;
            Duration += _oneSecondTimeAbility;
            yield return delay;
        }

        _isRecharging = false;
    }

    private void DisableComponentAtDeath()
    {
        enabled = false;
    }
}