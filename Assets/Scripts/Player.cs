using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class Player : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    [Header("Movement Details")]
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 8;
    private float xInput;
    private bool facingLeft = true;


    [Header("Collision Details")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private bool IsGrounded;
    private LayerMask WhatIsGround;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        HandleCollision();
        HandleInput();
        HandleMovement();
        HandleAnimations();
        HandleFlip();

    }

    private void HandleAnimations()
    {
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        anim.SetBool("IsGrounded", IsGrounded);

        

    }

    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

    }

    private void HandleMovement()
    {
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocityY);
           
    }

    private void HandleCollision()

    {
        IsGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance,WhatIsGround);
    }


    private void HandleFlip()
    {
        if (rb.linearVelocity.x > 0 && facingLeft == true)
            Flip();

        else if (rb.linearVelocity.x < 0 && facingLeft == false)
            Flip();
    }

    private void Jump()
    {
        
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
    }

    [ContextMenu("Flip")]
    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingLeft = !facingLeft;     
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
    }
}
