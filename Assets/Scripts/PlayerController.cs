using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float hInput;
    [SerializeField] float moveSpeed = 3f;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMove();
    }

    private void HorizontalMove()
    {
        hInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * hInput * moveSpeed * Time.deltaTime);
        if (hInput != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        

    }
}
