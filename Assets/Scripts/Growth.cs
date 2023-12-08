using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growth : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject item;
    private int index;
    private SpriteRenderer spriteRenderer;
    private bool canGrowth;
    private bool canClam;
    public bool CanClam { get { return canClam; } }
    private void Start()
    {
        canClam = false;
        index = 0;
        canGrowth = true;
        StartCoroutine(GrowthCoroutin());
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (index == 3) { canClam = true; }
        if (canGrowth && index < sprites.Length)
        {
            StartCoroutine(GrowthCoroutin());
            index++;
        }
        if (index < sprites.Length)
        {
            spriteRenderer.sprite = sprites[index];
        }
    }

    private IEnumerator GrowthCoroutin()
    {
        canGrowth = false;
        yield return new WaitForSeconds(5f);
        canGrowth = true;
    }

    public void Clamed()
    {
        if (item != null)
        {
            Destroy(gameObject);
            Instantiate(item, transform.position, transform.rotation);
        }
    }
}
