using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    public string id = Guid.NewGuid().ToString();
    Animator animator;
    [Header("Movement")]
    [SerializeField] float defaultSpeed;
    float speed;
    Rigidbody2D rigidbody2d;
    Vector2 direction = new Vector2(0, 0);
    Vector2 delta = new Vector2(0, 0);
    [Header("Attack")]
    [SerializeField] GameObject projectile;
    [SerializeField] Transform sideFirePoint;
    [SerializeField] Transform frontFirePoint;
    bool canAttack = true;
    Transform chosenFirePoint;

    //[SerializeField] ShootingChannel shootingChannel;
    //InputChannel inputChannel;
    bool shootingPressed = false; 
    public static event Action<Vector3> Shoot;

    void Start()
    {
        //AddListeners();
        AssignDefaults();
    }
    #region Assigns
    private void AssignDefaults()
    {
        speed = defaultSpeed;
        direction = Vector2.down;
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
    #endregion

    #region HandleInputEvents(New Input System)
    //private void AddListeners()
    //{
    //    var beacon = FindObjectOfType<BeaconSO>();
    //    shootingChannel = beacon.shootingChannel;
    //    inputChannel = beacon.inputChannel;
    //    inputChannel.MoveEvent += HandleMovement;
    //    inputChannel.ShootEvent += HandleShooting;
    //    inputChannel.ShootCancelledEvent += HandleShootingCancelled;
    //}
    //private void HandleMovement(Vector2 dir)
    //{
    //    delta = dir * speed;
    //}
    //private void HandleShooting()
    //{
    //    shootingPressed = true;
    //}
    //private void HandleShootingCancelled()
    //{
    //    shootingPressed = false;
    //}
    #endregion

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            shootingPressed = false;
            canAttack = false;
            speed = 0f;
            animator.SetTrigger("Attack");
            StartCoroutine(WaitForAnimation(0.5f));
            if (Mathf.Abs(delta.x) > 0)
                FlipSprite();
            Invoke("HandleShoot", 0.2f);
        }
    }

    private void FixedUpdate()
    {
        delta = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (delta == Vector2.zero)
        {
            rigidbody2d.velocity = Vector2.zero;
            animator.SetBool("Walking", false);
            return;
        }
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            HandleWalking(delta.x, 0);
            FlipSprite();
            chosenFirePoint = sideFirePoint;
            return;
        }
        if (Mathf.Abs(delta.y) > Mathf.Abs(delta.x))
        {
            HandleWalking(0, delta.y);
            frontFirePoint.localPosition = new Vector3(0, delta.y > 0 ? 0.5f : -1.2f, 0);
            chosenFirePoint = frontFirePoint;
        }
    }

    public void HandleShoot()
    {
        Instantiate(projectile, chosenFirePoint.position, Quaternion.identity);
        Shoot?.Invoke(direction);
        //shootingChannel.Shoot?.Invoke(direction);
    }

    private void FlipSprite()
    {
        if (!Mathf.Approximately(delta.x, 0.0f))
        {
            transform.localScale = new Vector3(Mathf.Sign(delta.x), 1.0f, 1.0f);
        }
    }

    IEnumerator WaitForAnimation(float sec)
    {
        yield return new WaitForSeconds(sec);
        speed = defaultSpeed;
        canAttack = true;
    }

    private void HandleWalking(float deltaX, float deltaY)
    {
        direction = new Vector2(deltaX, deltaY);
        rigidbody2d.velocity = direction * speed;
        SetWalkingAnimations(deltaX, deltaY);
    }

    private void SetWalkingAnimations(float deltaX, float deltaY)
    {
        animator.SetBool("Walking", true);
        animator.SetFloat("PosX", deltaX);
        animator.SetFloat("PosY", deltaY);
    }
}