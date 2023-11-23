using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManagement : MonoBehaviour
{
    public GameObject item;
    public bool isFull;
    private void Start()
    {
        isFull = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GenerateItem();
            isFull = true;
        }
        else isFull = false;
    }

    private void GenerateItem()
    {
        Instantiate(item, transform.position, transform.rotation);
    }
}
