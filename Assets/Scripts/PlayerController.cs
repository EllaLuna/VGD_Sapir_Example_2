using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Animator animator;
    Rigidbody2D rigidbody2d;
    float speed;
    [SerializeField] float defaultSpeed = 5f;
    Vector2 direction = new Vector2(0, 0);
    Vector2 delta = new Vector2(0, 0);
    [SerializeField] GameObject projectile;
    bool canAttack = true;
    [SerializeField] Transform firePoint;
    [SerializeField] Transform frontFirePoint;
    Transform chosenFirePoint;
    public static event Action<Vector3> Shoot;

    // Start is called before the first frame update
    void Start()
    {
        speed = defaultSpeed;
        chosenFirePoint = frontFirePoint;
        animator = GetComponent<Animator>();
        if (animator is null)
        {
            Debug.LogError("Animator is null, please assign it");
        }
        rigidbody2d = GetComponent<Rigidbody2D>();
        if (rigidbody2d is null)
        {
            Debug.LogError("Rigidbody is null, please assign it");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            canAttack = false;
            speed = 0f;
            animator.SetTrigger("Attack");
            StartCoroutine(WaitForAnimation(0.5f));
            if (!Mathf.Approximately(delta.x, 0.0f))
            {
                transform.localScale = new Vector3(Mathf.Sign(delta.x), 1.0f, 1.0f);
            }
            Instantiate(projectile, chosenFirePoint.position, Quaternion.identity);
            Shoot?.Invoke(direction);
        }
    }

    IEnumerator WaitForAnimation(float sec)
    {
        yield return new WaitForSeconds(sec);
        speed = defaultSpeed;
        canAttack = true;
    }

    private void FixedUpdate()
    {
        delta = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            direction = new Vector2(delta.x, 0);
            rigidbody2d.velocity = direction;
            animator.SetBool("Walking", true);
            animator.SetFloat("PosX", delta.x);
            animator.SetFloat("PosY", 0);
            if (!Mathf.Approximately(delta.x, 0.0f))
            {
                transform.localScale = new Vector3(Mathf.Sign(delta.x), 1.0f, 1.0f);
            }
            chosenFirePoint = firePoint;
        }
        else
        {
            if (Mathf.Abs(delta.y) > Mathf.Abs(delta.x))
            {
                direction = new Vector2(0, delta.y);
                rigidbody2d.velocity = direction;
                animator.SetBool("Walking", true);
                animator.SetFloat("PosX", 0);
                animator.SetFloat("PosY", delta.y);
                frontFirePoint.localPosition = new Vector3(0, delta.y > 0 ? 0.5f : -1, 0);
                chosenFirePoint = frontFirePoint;
            }
            else
            {
                rigidbody2d.velocity = Vector2.zero;
                animator.SetBool("Walking", false);
            }
        }
    }

}
