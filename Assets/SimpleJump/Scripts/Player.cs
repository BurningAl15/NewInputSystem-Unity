/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using UnityEngine;
using V_AnimationSystem;
using CodeMonkey.Utils;
using Unity.Mathematics;

/*
 * Simple Jump
 * */
public class Player : MonoBehaviour
{
    public enum Direction
    {
        Nope,
        Left,
        Right
    }

    [SerializeField] Direction _direction;

    [SerializeField] private LayerMask platformsLayerMask;

    // private Player_Base playerBase;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;
    [SerializeField] private float jumpVelocity = 100f;
    [SerializeField] private float moveSpeed = 40f;
    private int airJumpCount;
    private int airJumpCountMax;

    private void Awake()
    {
        // playerBase = gameObject.GetComponent<Player_Base>();
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        airJumpCountMax = 2;
    }

    private void Update()
    {
        if (IsGrounded())
        {
            airJumpCount = 0;
        }
        
    }

    void Animate()
    {
        // Set Animations
        // if (IsGrounded()) {
        //     if (rigidbody2d.velocity.x == 0) {
        //         playerBase.PlayIdleAnim();
        //     } else {
        //         playerBase.PlayMoveAnim(new Vector2(rigidbody2d.velocity.x, 0f));
        //     }
        // } else {
        //     playerBase.PlayJumpAnim(rigidbody2d.velocity);
        // }
    }

    public void Jump(float _jump)
    {
        // if (Input.GetKey(KeyCode.Space))
        if (_jump>0)
        {
            if (IsGrounded())
            {
                rigidbody2d.velocity = Vector2.up * jumpVelocity;
            }
            else
            {
                // if (Input.GetKeyDown(KeyCode.Space))
                if (_jump>.5f)
                    if (airJumpCount < airJumpCountMax)
                    {
                        rigidbody2d.velocity = Vector2.up * jumpVelocity;
                        airJumpCount++;
                    }
            }
        }
    }
    
    private bool IsGrounded()
    {
        var raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down,
            1f, platformsLayerMask);
        return raycastHit2d.collider != null;
    }

    public void HandleMovement_FullMidAirControl(float _moveDir)
    {
        if (GetDirection(_moveDir) == Direction.Left)
        {
            rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
        }
        else
        {
            if (GetDirection(_moveDir) == Direction.Right)
                rigidbody2d.velocity = new Vector2(+moveSpeed, rigidbody2d.velocity.y);
            else // No keys pressed
                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
        }
    }

    private void HandleMovement_SomeMidAirControl(float _moveDir)
    {
        var midAirControl = 3f;
        if (GetDirection(_moveDir) == Direction.Left)
        {
            if (IsGrounded())
            {
                rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
            }
            else
            {
                rigidbody2d.velocity += new Vector2(-moveSpeed * midAirControl * Time.deltaTime, 0);
                rigidbody2d.velocity = new Vector2(Mathf.Clamp(rigidbody2d.velocity.x, -moveSpeed, +moveSpeed),
                    rigidbody2d.velocity.y);
            }
        }
        else
        {
            if (GetDirection(_moveDir) == Direction.Right)
            {
                if (IsGrounded())
                {
                    rigidbody2d.velocity = new Vector2(+moveSpeed, rigidbody2d.velocity.y);
                }
                else
                {
                    rigidbody2d.velocity += new Vector2(+moveSpeed * midAirControl * Time.deltaTime, 0);
                    rigidbody2d.velocity = new Vector2(Mathf.Clamp(rigidbody2d.velocity.x, -moveSpeed, +moveSpeed),
                        rigidbody2d.velocity.y);
                }
            }
            else
            {
                // No keys pressed
                if (IsGrounded()) rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
            }
        }
    }

    private void HandleMovement_NoMidAirControl(float _moveDir)
    {
        if (IsGrounded())
        {
            // Input.GetKey(KeyCode.LeftArrow) 
            if (GetDirection(_moveDir) == Direction.Left)
            {
                rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
            }
            else
            {
                // Input.GetKey(KeyCode.RightArrow
                if (GetDirection(_moveDir) == Direction.Right)
                    rigidbody2d.velocity = new Vector2(+moveSpeed, rigidbody2d.velocity.y);
                else // No keys pressed
                    rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
            }
        }
    }

    public Direction GetDirection(float _moveDir)
    {
        // if (Input.GetAxisRaw("Horizontal") < 0)
        if (_moveDir < 0)
        {
            _direction = Direction.Left;
            Flip(1);
        }
        // else if (Input.GetAxisRaw("Horizontal") > 0)
        else if (_moveDir > 0)
        {
            _direction = Direction.Right;
            Flip(0);
        }
        else
            _direction = Direction.Nope;
        return _direction;
    }

    void Flip(int _direction){
        // transform.localScale=new Vector2(_direction,1);
        transform.rotation=Quaternion.Euler(0,180*_direction,0);
    }
}