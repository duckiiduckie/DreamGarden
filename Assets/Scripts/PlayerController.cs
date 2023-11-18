using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int dir;
    private float moveX;
    private float moveY;
    private float speech = 5f;
    private Animator animator;
    private Vector3 direction;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        direction = new Vector3(moveX, moveY);
        transform.Translate(VectorTranslate());
        AnimateMovement();
    }

    private void AnimateMovement()
    {
        if (animator != null)
        {
            if (direction.magnitude > 0)
            {
                animator.SetBool("isMoving", true);
                animator.SetFloat("horizontal", direction.x);
                animator.SetFloat("vertical", direction.y);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
    }

    private Vector3 VectorTranslate()
    {
        if (moveX == 0 && moveY == 0)
        {
            return Vector3.zero;
        }
        float sqrt = (float)Math.Sqrt(moveY * moveY + moveX * moveX);
        return direction * speech * Time.deltaTime / sqrt;
    }
}
