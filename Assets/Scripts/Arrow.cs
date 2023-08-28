using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    [SerializeField] float speed = 5f;
    int z;
    [SerializeField] ShootingChannel shootingChannel;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        var beacon = FindObjectOfType<BeaconSO>();
        shootingChannel = beacon.shootingChannel;
        shootingChannel.Shoot += ArrowDirection;
    }

    private int ReturnZPosition(string arg)
    {
        Debug.Log($"{arg}: {z}");
        return z;
    }

    public void ArrowDirection(Vector3 direction)
    {
        z = direction switch
        {
            _ when direction.y > 0 => 0,
            _ when direction.y < 0 => 180,
            _ when direction.x > 0 => -90,
            _ when direction.x < 0 => 90,
            _ => 0,
        };
        //Does the same as above
        //if (direction.y > 0)
        //{
        //    z = 0;
        //}
        //else
        //{
        //    if (direction.y < 0)
        //    {
        //        z = 180;
        //    }
        //    else
        //        if (direction.x > 0)
        //    {
        //        z = -90;
        //    }
        //    else
        //    {
        //        z = 90;
        //    }
        //}
        transform.eulerAngles = new Vector3(0, 0, z);
        Shoot();
    }

    public void Shoot()
    {
        rigidbody2d.AddForce(transform.up * speed, ForceMode2D.Impulse);
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            rigidbody2d.velocity = Vector2.zero;
            Destroy(gameObject, 0.3f);
        }
    }

    private void OnDestroy()
    {
        shootingChannel.Shoot -= ArrowDirection;
    }
}
