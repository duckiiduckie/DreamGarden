using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    void Start()
    {
        transform.position = player.transform.position + new Vector3(0, 0, -1);
    }

    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 0, -1);
    }
}
