using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoblieMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool moveLeft;
    private bool moveRight;
    private bool moveUp;
    private bool moveDown;
    private float horizontalMove;
    private float VerticalMove;
    public float speed = 15;
    public float jumpSpeed = 5;
    bool canDoubleJump;
    public float delayBeforeDoubleJump;
    bool facingRight = true;
    CapsuleCollider2D myCap;
    BoxCollider2D myBox;
    [SerializeField] Animator animator;
    public bool isGrounded;
    [SerializeField] AudioSource coinPickUp;
    [SerializeField] GameObject panelPlayAgain;
    // Start is called before the first frame update

    public static MoblieMove instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myCap = GetComponent<CapsuleCollider2D>();
        myBox = GetComponent<BoxCollider2D>();
        moveLeft = false;
        moveRight = false;
        moveUp = false;
        moveDown = false;
        InvokeRepeating("Die", 0, 0.12f);
        //StartCoroutine(Die());
    }

    //I am pressing the left button
    public void PointerDownLeft()
    {
        animator.SetBool("Running", true);
        moveLeft = true;
    }

    //I am not pressing the left button
    public void PointerUpLeft()
    {
        animator.SetBool("Running", false);
        moveLeft = false;
    }

    //Same thing with the right button
    public void PointerDownRight()
    {
        animator.SetBool("Running", true);
        moveRight = true;
    }

    public void PointerUpRight()
    {
        animator.SetBool("Running", false);
        moveRight = false;
    }

    public void PointerDownUp()
    {
        if (!myBox.IsTouchingLayers(LayerMask.GetMask("Ladder"))) { return; }
        animator.SetBool("Climbing", true);
        moveUp = true;
    }

    public void PointerUpUp()
    {
        //if (!myBox.IsTouchingLayers(LayerMask.GetMask("Ladder"))) { return; }
        animator.SetBool("Climbing", false);
        moveUp = false;
    }

    public void PointerDownDown()
    {
        if (!myBox.IsTouchingLayers(LayerMask.GetMask("Ladder"))) { return; }
        animator.SetBool("Climbing", true);
        moveDown = true;
    }

    public void PointerUpDown()
    {
        //if (!myBox.IsTouchingLayers(LayerMask.GetMask("Ladder"))) { return; }
        animator.SetBool("Climbing", false);
        moveDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        MovementPlayer();
        Climb();
    }

    //Now let's add the code for moving
    private void MovementPlayer()
    {
        //If I press the left button

        if (moveLeft)
        {

            horizontalMove = -speed;
        }

        //if i press the right button
        else if (moveRight)
        {

            horizontalMove = speed;
        }

        //if I am not pressing any button
        else
        {
            horizontalMove = 0;
        }

        if (horizontalMove < 0 && facingRight)
        {
            flip();
        }

        if (horizontalMove > 0 && !facingRight)
        {
            flip();
        }
    }

    void Climb()
    {
        if (moveUp && myBox.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            if (myBox.IsTouchingLayers(LayerMask.GetMask("Ladder")))
            {
                rb.gravityScale = 0;
                VerticalMove = 2;
                rb.velocity = new Vector2(rb.velocity.x, VerticalMove);
                animator.SetBool("Jumping", false);
            }
            else
            {
                rb.gravityScale = 2;
                VerticalMove = 0;
                rb.velocity = new Vector2(rb.velocity.x, VerticalMove);
                animator.SetBool("Jumping", true);
            }
        }

        else if (moveDown && myBox.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            if (myBox.IsTouchingLayers(LayerMask.GetMask("Ladder")))
            {
                rb.gravityScale = 0;
                VerticalMove = -2;
                rb.velocity = new Vector2(rb.velocity.x, VerticalMove);
                animator.SetBool("Jumping", false);
            }
            else
            {
                rb.gravityScale = 2;
                VerticalMove = 0;
                rb.velocity = new Vector2(rb.velocity.x, VerticalMove);
                animator.SetBool("Jumping", true);
            }
        }

        else if (myBox.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            rb.gravityScale = 0;
            VerticalMove = 0;
            rb.velocity = new Vector2(rb.velocity.x, VerticalMove);
            animator.SetBool("Jumping", false);
        }

        else if (!myBox.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            rb.gravityScale = 2;
        }
    }

    public void jumpButton()
    {
        animator.SetBool("Jumping", true);

        if (isGrounded && !myBox.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            isGrounded = false;
            rb.velocity = Vector2.up * jumpSpeed;
            Invoke("EnableDoubleJump", delayBeforeDoubleJump);
        }

        if (canDoubleJump)
        {
            animator.SetBool("Jumping", true);
            rb.velocity = Vector2.up * jumpSpeed;
            canDoubleJump = false;
        }
    }

    void EnableDoubleJump()
    {
        canDoubleJump = true;
    }

    //add the movement force to the player
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);

    }

    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            animator.SetBool("Jumping", false);
            canDoubleJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "heath")
        {
            HealthManager.instance.Health(10);
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "Coin")
        {
            coinPickUp.Play();
            Destroy(collision.gameObject);
            GameManager.instance.increment();
        }
    }

    void Die()
    {

        if (myCap.IsTouchingLayers(LayerMask.GetMask("Enemy")) || myBox.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            animator.SetBool("Dying", true);
            animator.SetBool("Jumping", false);
            animator.SetBool("Shooter", false);
            HealthManager.instance.TakeDamage(20);
            if (HealthManager.instance.healthAmount <= 0)
            {
                gameObject.SetActive(false);
                panelPlayAgain.SetActive(true);
            }
        }

        else if (myCap.IsTouchingLayers(LayerMask.GetMask("TrapGai")) || myBox.IsTouchingLayers(LayerMask.GetMask("TrapGai")))
        {
            animator.SetBool("Dying", true);
            animator.SetBool("Jumping", false);
            animator.SetBool("Shooter", false);
            HealthManager.instance.TakeDamage(10);
            if (HealthManager.instance.healthAmount <= 0)
            {
                gameObject.SetActive(false);
                panelPlayAgain.SetActive(true);
            }
        }

        else if (myCap.IsTouchingLayers(LayerMask.GetMask("Drill")) || myBox.IsTouchingLayers(LayerMask.GetMask("Drill")))
        {
            animator.SetBool("Dying", true);
            animator.SetBool("Jumping", false);
            animator.SetBool("Shooter", false);
            HealthManager.instance.TakeDamage(10);
            if (HealthManager.instance.healthAmount <= 0)
            {
                gameObject.SetActive(false);
                panelPlayAgain.SetActive(true);
            }
        }

        else if (myCap.IsTouchingLayers(LayerMask.GetMask("EnemyFly")) || myBox.IsTouchingLayers(LayerMask.GetMask("EnemyFly")))
        {
            animator.SetBool("Dying", true);
            animator.SetBool("Jumping", false);
            animator.SetBool("Shooter", false);
            HealthManager.instance.TakeDamage(10);
            if (HealthManager.instance.healthAmount <= 0)
            {
                gameObject.SetActive(false);
                panelPlayAgain.SetActive(true);
            }
        }

        else if (myCap.IsTouchingLayers(LayerMask.GetMask("Suriken")) || myBox.IsTouchingLayers(LayerMask.GetMask("Suriken")))
        {
            animator.SetBool("Dying", true);
            animator.SetBool("Jumping", false);
            animator.SetBool("Shooter", false);
            HealthManager.instance.TakeDamage(10);
            if (HealthManager.instance.healthAmount <= 0)
            {
                gameObject.SetActive(false);
                panelPlayAgain.SetActive(true);
            }
        }

        else if (myCap.IsTouchingLayers(LayerMask.GetMask("Boss")) || myBox.IsTouchingLayers(LayerMask.GetMask("Boss")))
        {
            animator.SetBool("Dying", true);
            animator.SetBool("Jumping", false);
            animator.SetBool("Shooter", false);
            HealthManager.instance.TakeDamage(5);
            if (HealthManager.instance.healthAmount <= 0)
            {
                gameObject.SetActive(false);
                panelPlayAgain.SetActive(true);
            }
        }

        else if (myCap.IsTouchingLayers(LayerMask.GetMask("BulletOfEnemy")) || myBox.IsTouchingLayers(LayerMask.GetMask("BulletOfEnemy")))
        {
            animator.SetBool("Dying", true);
            animator.SetBool("Jumping", false);
            animator.SetBool("Shooter", false);
            HealthManager.instance.TakeDamage(10);
            if (HealthManager.instance.healthAmount <= 0)
            {
                gameObject.SetActive(false);
                panelPlayAgain.SetActive(true);
            }
        }

        else if (!myCap.IsTouchingLayers(LayerMask.GetMask("Enemy", "TrapGai", "Drill" , "EnemyFly" , "BulletOfTower", "Boss", "BulletOfEnemy")))
        {
            animator.SetBool("Dying", false);
        }
    }
}
