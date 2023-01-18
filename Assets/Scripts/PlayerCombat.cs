using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Timeline;

public class PlayerCombat : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float attackRange;
    [SerializeField] private int damage = 40;
    [SerializeField] private float attackRate = 2f;
    private float _nextAttackTime = 0f;
    private AnimatorController _animator;
    public Transform AttackPoint => attackPoint;

    private void Start()
    {
        _animator = new AnimatorController(animator);
    }

    void Update()
    {
        if (Time.time >=_nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                _nextAttackTime = Time.time + 1f / attackRate;
            }    
        }
       
    }

    private void Attack()
    {
        _animator.OnAttack();
        Collider2D[] _hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

       foreach (Collider2D enemy in _hitEnemies)
       {
           Damage(damage: damage);
           enemy.GetComponent<Enemy>().Damage(damage);
       }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    
    public void Damage(int damage)
    {
    }
}
