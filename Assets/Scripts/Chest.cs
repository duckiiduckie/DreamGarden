using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator animator;
    private bool isOpen;
    public bool isCollect;
    [SerializeField] private GameObject book;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        isOpen = false;
        isCollect = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isOpen && !isCollect)
        {
            Instantiate(book, transform.position, transform.rotation);
            isCollect = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            animator.SetBool("IsOpen", true);
            if (!isOpen) { isOpen = true; }
        }
    }

}
