using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            PlayerManagement player = collision.GetComponent<PlayerManagement>();
            player.inventory.Add(gameObject.tag, spriteRenderer.sprite);
            Destroy(gameObject);
        }
    }
}
