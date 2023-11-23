using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveX;
    private float moveY;
    private float speech;
    private Animator animator;
    private Vector3 direction;
    private int typeTool;
    private bool isUseTool = false;

    private void Start()
    {
        typeTool = 0;
        speech = 4f;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ToolCoroutine());
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            typeTool++;
            typeTool %= 4;
            animator.SetInteger("typeTool", typeTool);
        }
        animator.SetBool("isUseTool", isUseTool);
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        direction = new Vector3(moveX, moveY);
        AnimateMovement();
    }

    private void FixedUpdate()
    {
        transform.position += VectorTranslate();
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
        return speech * Time.deltaTime * direction / sqrt;
    }

    private IEnumerator ToolCoroutine()
    {
        isUseTool = true;
        yield return new WaitForSeconds(1f);
        isUseTool = false;
    }
}
