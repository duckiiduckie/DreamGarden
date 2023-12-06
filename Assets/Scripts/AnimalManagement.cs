using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalManagement : MonoBehaviour
{
    public GameObject item;
    public GameObject food;
    public bool isFull;
    public bool canFeed;
    public PlayerManagement player;
    private void Start()
    {
        isFull = false;
        canFeed = false;
    }

    private void Update()
    {
        if (canFeed && Input.GetKeyDown(KeyCode.Space))
        {
            GenerateItem();
            canFeed = false;
            StartCoroutine(FullCoroutine());
        }
    }

    private void GenerateItem()
    {
        Instantiate(item, transform.position, transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isFull)
        {
            for (int i = 0; i < player.inventory.slots.Count; i++)
            {
                if (food.tag == player.inventory.slots[i].type && player.inventory.slots[i].count > 0)
                {
                    player.inventory.slots[i].count--;
                    canFeed = true;
                    return;
                }
            }
        }
    }

    private IEnumerator FullCoroutine()
    {
        isFull = true;
        yield return new WaitForSeconds(120f);
        isFull = false;
    }
}
