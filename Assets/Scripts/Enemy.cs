using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float lives = 5;
    [SerializeField] float moveSpeed = -1f;
    protected Rigidbody2D myRigidbody;
    [SerializeField] GameObject effectDie;
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //myRigidbody.velocity = new Vector2(moveSpeed, 0f);
    }

    private void FixedUpdate()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            moveSpeed = -moveSpeed;
            flipEnemyFacing();
        }
    }

    public void flipEnemyFacing()
    {
        transform.localScale = new Vector2((Mathf.Sign(myRigidbody.velocity.x)), 1f);
    }

    public void Die()
    {
        if (lives <= 0)
        {
            Instantiate(effectDie, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void TakeDamage()
    {
        lives--;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage();
            Die();
            Destroy(collision.gameObject);
        }
    }

}
