using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health), typeof(PlayerInput))]
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _enemyLayers;

    private int _attackDamage = 20;
    private Health _playerHealth;
    private PlayerInput _playerInput;
    private Coroutine _coroutineAttack;

    public event Action Attacked;

    private void Awake()
    {
        _playerHealth = GetComponent<Health>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (_playerInput.IsTryingAttack && _coroutineAttack == null)
        {
            _coroutineAttack = StartCoroutine(AttackCoroutine());
        }
    }

    private void OnEnable()
    {
        _playerHealth.Died += DisableComponentAtDeath;
    }

    private void OnDisable()
    {
        _playerHealth.Died -= DisableComponentAtDeath;
    }

    private void Attack()
    {
        Attacked?.Invoke();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            if(enemy != null)
            {
                if(_attackDamage > 0)
                {
                    enemy.GetComponent<Health>().TakeDamage(_attackDamage);
                }
            }
        }
    }

    private IEnumerator AttackCoroutine()
    {
        float timeToDelay = 1.5f;
        var delay = new WaitForSeconds(timeToDelay);

        Attack();
        yield return delay;

        _coroutineAttack = null;
        _playerInput.DeActivateAttackTrying();
    }

    private void DisableComponentAtDeath()
    {
        enabled = false;
    }
}