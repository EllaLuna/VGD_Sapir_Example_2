using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Animator animator;
    Rigidbody2D rigidbody2d;
    [SerializeField] float speed = 5f;
    [SerializeField] float defaultSpeed = 5f;
    Vector2 delta = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        if (rigidbody2d is null)
        {
            Debug.Log("Rigidbody is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2d.velocity = Vector2.zero;
            animator.SetTrigger("Attack");
            speed = 0f;
        }

    }

    private void FixedUpdate()
    {
        delta = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            rigidbody2d.velocity = new Vector2(delta.x, 0);
            animator.SetBool("Walking", true);
            animator.SetFloat("PosX", delta.x);
            animator.SetFloat("PosY", 0);
        }
        else
        {
            if (Mathf.Abs(delta.y) > Mathf.Abs(delta.x))
            {
                rigidbody2d.velocity = new Vector2(0, delta.y);
                animator.SetBool("Walking", true);
                animator.SetFloat("PosX", 0);
                animator.SetFloat("PosY", delta.y);
            }
            else
            {
                rigidbody2d.velocity = Vector2.zero;
                animator.SetBool("Walking", false);
            }
        }
    }

    private void ReturnDefaultSpeed()
    {
        speed = 5f;
        Debug.Log(speed);
    }
   private void SetSpeed()
    {
        speed = 5f;
        Debug.Log(speed);
    }
}
