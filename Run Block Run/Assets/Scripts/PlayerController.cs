using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float groundDistance = 0.3f;
    [SerializeField] private float jumpTime = 0.3f;
    [SerializeField] private Transform graphics;
    [SerializeField] private float crouchHeight = 0.5f;

    private bool isGrounded = false;
    private bool isJumping = false;
    private float jumpTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ProcessJump();
        ProcessCrouch();
    }

    private void ProcessJump()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer); //Checking if feet are in radius of ground

        //Start jump when button pressed
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
        }

        //apply jump for short time if holding down jump button
        if (isJumping && Input.GetButton("Jump"))
        {
            if (jumpTimer < jumpTime)
            {
                rb.velocity = Vector2.up * jumpForce;

                jumpTimer += Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        //Stop applying jump force when button no longer pressed
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            jumpTimer = 0;
        }
    }

    private void ProcessCrouch()
    {
        if(isGrounded && Input.GetButtonDown("Crouch"))
        {
            graphics.localScale = new Vector3(graphics.localScale.x, crouchHeight, graphics.localScale.z);
        };

        if (Input.GetButtonUp("Crouch"))
        {
            graphics.localScale = new Vector3(graphics.localScale.x, 1f, graphics.localScale.z);
        }

        if(isJumping)
        {
            graphics.localScale = new Vector3(graphics.localScale.x, 1f, graphics.localScale.z);
        }
    }
}
