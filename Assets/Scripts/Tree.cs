using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private int health = 3;
    public GameObject wood;
    public bool canCutted = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canCutted)
            {
                health--;
                canCutted = false;
            }
        }
        if (health == 0)
        {
            Destroy(gameObject);
            GennerateWood();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController controller = collision.GetComponent<PlayerController>();
            if (controller.typeTool == 2)
            {
                canCutted = true;
            }
        }
    }

    private void GennerateWood()
    {
        Instantiate(wood, transform.position, transform.rotation);
    }
}
