using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    public bool collisionDetected;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionDetected = true;
    }
}
