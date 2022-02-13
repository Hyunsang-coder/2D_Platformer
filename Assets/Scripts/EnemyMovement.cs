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

    // rigidbody type�� kinematic���� �����ؼ� isTouchingLayer ��� �Ұ� 
    // ���ǿ����� OnTriggerExit2D�� ���������, ���� �� �����ϰ� OnTriggerEnter2D ���
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
