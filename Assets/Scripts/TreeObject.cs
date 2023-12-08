using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObject : MonoBehaviour
{
    private int health = 3;
    [SerializeField] private GameObject wood;
    [SerializeField] private GameObject fruit;
    private bool canCut = false;
    private bool canClamb = false;
    private bool isRipe = false;
    [SerializeField] private Sprite treeRipeSprite;
    [SerializeField] private Sprite treeSprite;
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(RipeCoroutin());
    }
    private void Update()
    {
        if (isRipe)
        {
            sprite.sprite = treeRipeSprite;
        }
        else
        {
            sprite.sprite = treeSprite;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canCut)
            {
                health--;
                canCut = false;
            }
            if (canClamb && isRipe)
            {
                GennerateFruit();
                StartCoroutine(RipeCoroutin());
            }
        }
        if (health == 0)
        {
            Destroy(gameObject);
            if (isRipe)
            {
                GennerateFruit();
            }
            GennerateWood();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController controller = collision.GetComponent<PlayerController>();
            if (controller.TypeTool == 2)
            {
                canCut = true;
            }
            else if (controller.TypeTool == 0)
            {
                canClamb = true;
            }
        }
    }
    private IEnumerator RipeCoroutin()
    {
        isRipe = false;
        yield return new WaitForSeconds(5f);
        isRipe = true;
    }

    private void GennerateWood()
    {
        Instantiate(wood, transform.position, transform.rotation);
    }
    private void GennerateFruit()
    {
        Instantiate(fruit, transform.position + new Vector3(1, 1), transform.rotation);
    }
}
