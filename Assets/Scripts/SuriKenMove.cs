using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuriKenMove : MonoBehaviour
{
    [SerializeField] float moveSpeed2 = -1f;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveSpeed2, 0f);
    }

    public void flipEnemyFacing()
    {
        transform.localScale = new Vector2((Mathf.Sign(rb.velocity.x)), 1f);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            moveSpeed2 = -moveSpeed2;
            flipEnemyFacing();
        }
    }
}
