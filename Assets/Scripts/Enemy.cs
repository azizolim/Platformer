using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IInteractable
{
    [SerializeField] private int maxHealth = 200;
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D collider;
    [SerializeField] private Rigidbody2D rigidbody;
    private AnimatorController _animator;
    private bool _isDead;

    private int _currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = maxHealth;
        _animator = new AnimatorController(animator);
    }

    

    public void Damage(int damage)
    {
        if (_isDead)
            return;
        _currentHealth = Math.Clamp(_currentHealth - damage, min: 0, max: 1000);
        _animator.OnHurt();
        if (_currentHealth == 0 && !_isDead)
        {
            Died();
        }
    }

    private void Died()
    {
        collider.enabled = false;
        
        rigidbody.gravityScale = 0;
        _isDead = true;
        _animator.OnDie(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {       if (_isDead)
            return;
        if (other.gameObject.TryGetComponent(out IInteractable player))
        {
            rigidbody.velocity= new Vector2();
        }
        
    }
}
