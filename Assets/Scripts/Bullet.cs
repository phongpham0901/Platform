using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    MoblieMove moblieMove;
    float xSpeed;
    float bulletSpeed = 8f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moblieMove = FindObjectOfType<MoblieMove>();
        xSpeed = moblieMove.transform.rotation.y * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Mathf.Sign(xSpeed)* 10f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
                Destroy(gameObject);
        }
    }
}
