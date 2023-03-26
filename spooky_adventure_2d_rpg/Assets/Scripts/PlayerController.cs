using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;

    public bool isMoving;

    public Vector2 input;

    private Animator animator;

    public LayerMask solidObjectsLayer;

    private void Awake() // When loading player
    {
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");


            if (input.x != 0) input.y = 0; // No diagonal movements

            if(input != Vector2.zero)
            {
                Debug.Log("Input.x: " + input.x);
                Debug.Log("Input.y: " + input.y);

                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;


                if (IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));  // Check what this does
                }


            }
        }

        animator.SetBool("isMoving", isMoving);
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon) // I'm not sure why this is important
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if(Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null)
        {
            return false;
        }

        return true;
    }
}
