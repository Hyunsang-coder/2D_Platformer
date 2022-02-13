using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 3f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(20, 20);
    [SerializeField] GameObject bulletPrefabRight;
    [SerializeField] GameObject bulletPrefabLeft;
    [SerializeField] Transform bulletPosition;


    Vector2 moveInput;
    float gravityScaleAtStart;
    bool isAlive = true;
    
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();

        gravityScaleAtStart = myRigidBody.gravityScale;
    }

    void Update()
    {
        if (!isAlive) return;
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }


    void OnMove(InputValue value)
    {
        if (!isAlive) return;
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) return;
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
        if (value.isPressed)
        {
            myRigidBody.velocity += new Vector2(0, jumpSpeed);
            //myRigidBody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }        
    }

    void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            myAnimator.SetTrigger("Shoot");
            if (transform.localScale.x == -1)
            {
                Instantiate(bulletPrefabLeft, bulletPosition.position, bulletPosition.rotation);
            }
            else
            {
                Instantiate(bulletPrefabRight, bulletPosition.position, bulletPosition.rotation);
            }
            
        }
    }

    void Run()
    {
        //myRigidBody.AddForce(Vector2.right * moveInput.x, ForceMode2D.Impulse);
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        // 돈 주고 배우는 부분 ㅋㅋ
        bool playerIsMoving = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerIsMoving);
    }

    private void FlipSprite()
    {
        // Epsilon은 0보다 큰 아주 작은 수 (0 대신 사용) 
        bool playerisMoving = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerisMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1);
        }
    }

    void ClimbLadder()
    {    
        if (!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myRigidBody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, moveInput.y * climbSpeed);
        myRigidBody.velocity = climbVelocity;

        myRigidBody.gravityScale = 0;

        bool playerIsClimbing = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerIsClimbing);
    }

    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Die");
            if (myRigidBody.velocity.x > 0)
            {
                myRigidBody.velocity = new Vector2(-deathKick.x, deathKick.y);
            }
            else
            {
                myRigidBody.velocity = deathKick;
            }
            FindObjectOfType<GameManager>().ProcessPlayerDeath();
        }
        
    }


}
