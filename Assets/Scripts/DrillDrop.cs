using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillDrop : MonoBehaviour
{
    Rigidbody2D rb;
    PolygonCollider2D polygon;
    //[SerializeField] GameObject drop;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        polygon = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Die();
        if(GetComponentInChildren<Drop>().CallDrop())
        {
            rb.velocity = new Vector2(rb.velocity.x, -5f);
        }
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }*/

    void Die()
    {
        if(polygon.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Destroy(gameObject);
        }
    }

}
