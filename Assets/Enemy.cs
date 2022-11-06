using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Arrow")
        {
            transform.localScale = new Vector3(1.0f, -1.0f, 1.0f);
            Destroy(gameObject, 1f);
        }
    }
}
