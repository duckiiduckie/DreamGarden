using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (animator.GetInteger("check") == 1)
            {
                animator.SetInteger("check", 2);
            }
            else if (animator.GetInteger("check") == 2)
            {
                animator.SetInteger("check", 1);
            }
        }
    }
}
