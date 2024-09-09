using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    [SerializeField] float speed;
    Transform player;
    [SerializeField] float lineOfSite;
    [SerializeField] float shootingRange;
    float fireRate = 1;
    float nextFireTime;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletParent;
    Rigidbody2D rb;
    [SerializeField] float lives = 4;
    [SerializeField] GameObject effectDie;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPLayer = Vector2.Distance(player.position, transform.position);
        Debug.Log(distanceFromPLayer);

        if (distanceFromPLayer < lineOfSite && distanceFromPLayer > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
            flipSprites();
        }
        else if(distanceFromPLayer <= shootingRange && nextFireTime < Time.time)
        {
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
        
    }

    void flipSprites()
    {
        bool playerIsToTheRight = player.position.x > transform.position.x;
        bool playerHasHorizontalSpeed = Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.x) > Mathf.Epsilon;

        if (playerIsToTheRight && playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
        else if (!playerIsToTheRight && playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
    }

    public void Die()
    {
        if (lives <= 0)
        {
            Destroy(gameObject);
            Instantiate(effectDie, transform.position, Quaternion.identity);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
