using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Rigidbody2D enemyRB;
    BoxCollider2D enemyBoxcollider;
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemyBoxcollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        enemyRB.velocity = new Vector2(moveSpeed, 0);
        
    }

    // rigidbody type을 kinematic으로 설정해서 isTouchingLayer 사용 불가 
    // 강의에서는 OnTriggerExit2D를 사용했지만, 나는 더 간단하게 OnTriggerEnter2D 사용
    private void OnTriggerEnter2D(Collider2D collision)
    {
        moveSpeed = -moveSpeed;
        EnemyFlipping();

    }

    private void EnemyFlipping()
    {
        transform.localScale = new Vector2(-Mathf.Sign(enemyRB.velocity.x), 1);
    }
}
