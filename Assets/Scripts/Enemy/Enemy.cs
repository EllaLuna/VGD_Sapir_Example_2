using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Arrow"))
        {
            transform.localScale = new Vector3(1.0f, -1.0f, 1.0f);
            Destroy(gameObject, 1f);
        }
    }
}
