using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour, IInteractable
{
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer playerRenderer;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private int health;
    [SerializeField] private PlayerCombat playerCombat;

    private float _horizontal;
    private bool _isFacingRight = true;
    private bool _isDead;
    private AnimatorController _animatorController;

    private void Start()
    {
        _animatorController = new AnimatorController(animator);
    }

    void Update()
    {
        if (_isDead)
            return;

        _horizontal = Input.GetAxisRaw("Horizontal");
        Flip();
    }

    private void FixedUpdate()
    {
        if (_isDead)
            return;
        
        Run();
        Jump();
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            var isGrounded = IsGrounded();
            if (isGrounded)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpingPower);
                _animatorController.OnJump();
            }
        }
    }

    private void Run()
    {
        if (_horizontal != 0)
        {
            _animatorController.OnRun(true);
        }
        else
        {
            _animatorController.OnRun(false);
        }

        myRigidbody.velocity = new Vector2(_horizontal * speed, myRigidbody.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
    }

    private void Flip()
    {
        if (_isFacingRight && _horizontal < 0f || !_isFacingRight && _horizontal > 0f)
        {
            
            _isFacingRight = !_isFacingRight;
            playerRenderer.flipX = !_isFacingRight;
            var flipX = _isFacingRight ? 1 : -1;

            playerCombat.AttackPoint.localPosition = new Vector3(flipX, 0.85f, 0);
        }
    }

    public void Damage(int damage)
    {
        health = Math.Clamp(health - damage, min: 0, max: 1000);
        if (health == 0 && !_isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        _isDead = true;
        _animatorController.OnDie(true);
    }
}