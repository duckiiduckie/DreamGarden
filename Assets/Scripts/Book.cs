using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.hasBook = true;
            Destroy(gameObject);
        }
    }
}
