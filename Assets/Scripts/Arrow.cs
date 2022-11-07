using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    [SerializeField] float speed = 5f;
    bool directionSet = false;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        PlayerController.Shoot += ArrowDirection;
    }

    public void ArrowDirection(Vector3 direction)
    {
        if (directionSet) return;
        directionSet = true;
        int z;
        if (direction.y > 0)
            z = 0;
        else
        {
            if (direction.y < 0)
                z = 180;
            else
            {
                if (direction.x > 0)
                    z = -90;
                else
                    z = 90;
            }
        }
        transform.eulerAngles = new Vector3(0, 0, z);
        Shoot();
    }

    public void Shoot()
    {
        rigidbody2d.AddForce(transform.up * speed, ForceMode2D.Impulse);
        Destroy(gameObject, 1f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            rigidbody2d.velocity = Vector2.zero;
            Destroy(gameObject, 0.3f);
        }
    }

    private void OnDestroy()
    {
        PlayerController.Shoot -= ArrowDirection;
    }
}
