using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject seed;
    private float moveX;
    private float moveY;
    private float speed;
    private Animator animator;
    private Vector3 direction;
    private int typeTool;
    private bool isUseTool = false;

    public int TypeTool { get { return typeTool; } }

    private void Start()
    {
        typeTool = 0;
        speed = 1.25f;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isUseTool)
        {
            if (Input.anyKeyDown)
            {
                isUseTool = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (typeTool > 0)
                StartCoroutine(ToolCoroutine());
            if (typeTool == 3)
            {
                Plow();
            }
            if (typeTool == 0)
            {
                PlantOrClam();
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isUseTool)
            {
                isUseTool = false;
            }
            typeTool++;
            typeTool %= 4;
            animator.SetInteger("typeTool", typeTool);
        }
        animator.SetBool("isUseTool", isUseTool);
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        direction = new Vector3(moveX, moveY);
        AnimateMovement();
    }

    private void FixedUpdate()
    {
        transform.position += VectorTranslate();
    }

    private void AnimateMovement()
    {
        if (animator != null)
        {
            if (direction.magnitude > 0)
            {
                animator.SetBool("isMoving", true);
                animator.SetFloat("horizontal", direction.x);
                animator.SetFloat("vertical", direction.y);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
    }


    private Vector3 VectorTranslate()
    {
        if (moveX == 0 && moveY == 0)
        {
            return Vector3.zero;
        }
        float sqrt = (float)Math.Sqrt(moveY * moveY + moveX * moveX);
        return speed * Time.deltaTime * direction / sqrt;
    }

    private IEnumerator ToolCoroutine()
    {
        isUseTool = true;
        yield return new WaitForSeconds(0.75f);
        isUseTool = false;
    }

    private void PlantOrClam()
    {
        Vector3Int position = new Vector3Int((int)Math.Round(transform.position.x), (int)Math.Round(transform.position.y), 0);
        if (GameManager.instance.tileManager.IsInteractable(position))
        {
            Vector3 plantPosition = GameManager.instance.tileManager.CordinateTile(position) + new Vector3(0.5f, 0.5f, 0);
            //Plant
            if (!GameManager.instance.tileManager.dictVectorUse.ContainsKey(plantPosition))
            {
                if (seed != null)
                {
                    GameObject gameObject = Instantiate(seed, plantPosition, Quaternion.identity);
                    GameManager.instance.tileManager.dictVectorUse.Add(plantPosition, gameObject);
                }
            }
            //Clam
            else
            {
                GameObject item = GameManager.instance.tileManager.dictVectorUse.GetValueOrDefault(plantPosition);
                if (item != null)
                {
                    Growth growth = item.GetComponent<Growth>();
                    if (growth.CanClam)
                    {
                        growth.Clamed();
                        GameManager.instance.tileManager.dictVectorUse.Remove(plantPosition);
                    }
                }
            }
        }
    }

    private void Plow()
    {
        Vector3Int position = new Vector3Int((int)Math.Round(transform.position.x), (int)Math.Round(transform.position.y), 0);
        if (GameManager.instance.tileManager.IsInteractable(position))
        {
            Vector3 plantPosition = GameManager.instance.tileManager.CordinateTile(position) + new Vector3(0.5f, 0.5f, 0);
            GameObject item = GameManager.instance.tileManager.dictVectorUse.GetValueOrDefault(plantPosition);
            if (item != null)
            {
                Destroy(item);
            }
        }
    }
}
