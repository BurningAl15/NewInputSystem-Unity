using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player_2d : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private PlayerBase_2d playerBase;
    private Rigidbody2D rgb;
    [SerializeField] private float moveSpeed = 40f;
    [SerializeField] private float jumpVelocity = 10f;
    [SerializeField] private BoxCollider2D boxCollider2D;
    void Awake()
    {
        playerBase = GetComponent<PlayerBase_2d>();
        rgb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        HandleMovement();

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rgb.velocity = Vector2.up * jumpVelocity * 0.01f;
        }

    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f,
            Vector2.down ,.15f, layerMask);

        return raycastHit2D.collider != null;
    }

    void HandleMovement()
    {
        float midAirControl = 1.5f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (IsGrounded())
                rgb.velocity = new Vector2(-moveSpeed, rgb.velocity.y);
            else
            {
                rgb.velocity += new Vector2(-moveSpeed * midAirControl * Time.deltaTime, 0);
                rgb.velocity = new Vector2(Mathf.Clamp(rgb.velocity.x, -moveSpeed, +moveSpeed), rgb.velocity.y);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (IsGrounded())
                    rgb.velocity = new Vector2(moveSpeed, rgb.velocity.y);
                else
                {
                    rgb.velocity += new Vector2(moveSpeed * midAirControl * Time.deltaTime, 0);
                    rgb.velocity = new Vector2(Mathf.Clamp(rgb.velocity.x, -moveSpeed, +moveSpeed), rgb.velocity.y);
                }
            }
            else
            {
                if (IsGrounded())
                    rgb.velocity=new Vector2(0,rgb.velocity.y);
            }
            
        }
    }
}
