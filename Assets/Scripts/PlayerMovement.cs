using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private AudioSource jumpAudioSource;

    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private bool hasAirJump = false;

    // Start is called before the first frame update
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (rigidbody.bodyType == RigidbodyType2D.Static) 
        {
            return;
        }
        State currentState = State.Idle;
        float horizontalDirection = Input.GetAxis("Horizontal");
        bool running = horizontalDirection != 0f;
        if (running) 
        {
            rigidbody.velocity = new Vector2(horizontalDirection * moveSpeed, rigidbody.velocity.y);
            spriteRenderer.flipX = horizontalDirection < 0f;
            currentState = State.Running;
        } 
        bool isGrounded = IsGrounded();
        if (isGrounded)
        {
            hasAirJump = true;
        }
        if (Input.GetButtonDown("Jump")) 
        {
            if (isGrounded || hasAirJump)
            {
                rigidbody.velocity = new Vector2(0f, jumpForce);
                jumpAudioSource.Play();
                hasAirJump = isGrounded;
            }
        }
        if (IsJumping()) 
        {
            currentState = State.Jumping;
        } 
        else if (IsFalling()) 
        {
            currentState = State.Falling;
        }
        animator.SetInteger("state", (int) currentState);
    }

    private bool IsJumping()
    {
        return rigidbody.velocity.y > .1f;
    }

    private bool IsFalling()
    {
        return rigidbody.velocity.y < -.1f;
    }

    private bool IsGrounded() 
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, groundLayer, 0f, 0f);
    }

    private enum State 
    {
        Idle,
        Running,
        Jumping,
        Falling
    }

}
