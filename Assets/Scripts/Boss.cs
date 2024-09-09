using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] float idleMovementSpeed;
    [SerializeField] Vector2 idleMovementDirection;

    [SerializeField] Transform groundCheckUp;
    [SerializeField] Transform groundCheckDown;
    [SerializeField] Transform groundCheckWall;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;

    private bool isTouchingUp;
    private bool isTouchingDown;
    private bool isTouchingWall;

    private bool facingLeft = true;
    private bool goingUp = true;
    private Rigidbody2D enemyRB;

    private bool isSpeedChanged = false;
    private bool isSpeedIncrease = true;

    private void Start()
    {
        idleMovementDirection.Normalize();
        enemyRB = GetComponent<Rigidbody2D>();

        StartCoroutine(ChangeSpeedRoutine());
    }

    private void Update()
    {
        isTouchingUp = Physics2D.OverlapCircle(groundCheckUp.position, groundCheckRadius, groundLayer);
        isTouchingDown = Physics2D.OverlapCircle(groundCheckDown.position, groundCheckRadius, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(groundCheckWall.position, groundCheckRadius, groundLayer);

        IdleState();
    }

    void IdleState()
    {
        if (isTouchingUp && goingUp)
        {
            ChangeDirection();
        }
        else if (isTouchingDown && !goingUp)
        {
            ChangeDirection();
        }

        if (isTouchingWall)
        {
            Flip();
        }

        float speed;
        if (isSpeedChanged) {
            if (isSpeedIncrease) {
                speed = 3f;
            } else {
                speed = 11f;
            }
        } else {
            speed = 11f;
        }

        enemyRB.velocity = speed * idleMovementDirection;
    }

    private void ChangeDirection()
    {
        goingUp = !goingUp;
        idleMovementDirection.y *= -1;
    }

    private void Flip()
    {
        facingLeft = !facingLeft;
        idleMovementDirection.x *= -1;
        transform.Rotate(0, 180, 0);
    }

    private IEnumerator ChangeSpeedRoutine()
    {
        while (true)
        {
            isSpeedChanged = true;
            isSpeedIncrease = true;
            yield return new WaitForSeconds(10f); // Th?i gian t?c ?? t?ng lên

            isSpeedIncrease = false;
            yield return new WaitForSeconds(5f); // Th?i gian t?c ?? gi?m xu?ng
        }
    }
}
