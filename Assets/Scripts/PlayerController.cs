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
    private TileManager tileManager;
    public bool hasBook;

    public int TypeTool { get { return typeTool; } }

    private void Start()
    {
        typeTool = 0;
        speed = 1.25f;
        hasBook = false;
        animator = GetComponent<Animator>();
        tileManager = GameManager.instance.tileManager;
    }

    private void Update()
    {
        CancelAnimation();
        ActionInteract();
        ChangeTool();
        Movement();
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
        Vector3Int position = PositionInt();
        if (tileManager.IsInteractable(position))
        {
            Vector3 plantPosition = tileManager.CordinateTile(position) + new Vector3(0.5f, 0.5f, 0);
            //Plant
            if (!tileManager.dictVectorUse.ContainsKey(plantPosition))
            {
                if (seed != null)
                {
                    GameObject gameObject = Instantiate(seed, plantPosition, Quaternion.identity);
                    tileManager.dictVectorUse.Add(plantPosition, gameObject);
                }
            }
            //Clam
            else
            {
                GameObject item = tileManager.dictVectorUse.GetValueOrDefault(plantPosition);
                if (item != null)
                {
                    Growth growth = item.GetComponent<Growth>();
                    if (growth.CanClam)
                    {
                        growth.Clamed();
                        tileManager.dictVectorUse.Remove(plantPosition);
                    }
                }
            }
        }
    }

    private void Plow()
    {
        Vector3Int position = PositionInt();
        if (tileManager.IsInteractable(position))
        {
            Vector3 plantPosition = tileManager.CordinateTile(position) + new Vector3(0.5f, 0.5f, 0);
            GameObject item = tileManager.dictVectorUse.GetValueOrDefault(plantPosition, null);
            if (item != null)
            {
                tileManager.dictVectorUse.Remove(plantPosition);
                Destroy(item);
            }
        }
    }

    private Vector3Int PositionInt()
    {
        return new Vector3Int((int)Math.Round(transform.position.x), (int)Math.Round(transform.position.y), 0);
    }

    private void CancelAnimation()
    {
        if (isUseTool)
        {
            if (Input.anyKeyDown)
            {
                isUseTool = false;
            }
        }
    }

    private void ChangeTool()
    {
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
    }

    private void Movement()
    {
        animator.SetBool("isUseTool", isUseTool);
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        direction = new Vector3(moveX, moveY);
        AnimateMovement();
    }

    private void ActionInteract()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (typeTool > 0)
            {
                StartCoroutine(ToolCoroutine());
                if (typeTool == 3)
                {
                    Plow();
                }
            }
            else
            {
                PlantOrClam();
            }
        }
    }
}
