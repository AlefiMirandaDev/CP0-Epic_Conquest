using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimentação")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isFacingRight = true;

    [Header("Verificações")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;

    private float horizontalInput;
    private bool isGrounded;
    private bool isJumping;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }

        Flip();
        Animate();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        if (isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = false;
        }
    }

    void Flip()
    {
        if (horizontalInput > 0 && !isFacingRight || horizontalInput < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    void Animate()
    {
        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
            animator.SetBool("isGrounded", isGrounded);
        }
    }

    // Pode adicionar aqui chamadas para dash, ataque, etc.
}
