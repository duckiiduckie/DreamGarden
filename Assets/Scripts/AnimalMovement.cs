using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    public float xRangeLeft;
    public float xRangeRight;
    public float yRangeUp;
    public float yRangeDown;
    private Animator animator;
    private Rigidbody2D rb;
    private float speech;
    private Vector3 newPosition;
    private SpriteRenderer sprite;

    private void Start()
    {
        speech = 0.5f;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        InvokeRepeating(nameof(UpdateNewPosition), 0f, 15f);
    }


    private void FixedUpdate()
    {
        transform.Translate(speech * Time.deltaTime * (newPosition - transform.position));
        if (Math.Abs(newPosition.x - transform.position.x) < 0.05f)
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void UpdateNewPosition()
    {
        newPosition = NewPosition();
        if (newPosition.x < transform.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
        animator.SetBool("isMoving", true);
    }

    private Vector3 NewPosition()
    {
        return new Vector3(UnityEngine.Random.Range(xRangeLeft, xRangeRight), UnityEngine.Random.Range(yRangeDown, yRangeUp));
    }
}
