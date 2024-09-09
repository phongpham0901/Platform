using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform transforml;
    [SerializeField] float timeDelay;
    [SerializeField] Animator animator;
    public bool shoot;
    public static Shooter instance;
    BoxCollider2D myBox;
    Rigidbody2D rb;
    public float fireRate = 0.5F;
    private float nextFire = 0.0F;

    void Start()
    {
        myBox = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            shoot = true;
            Debug.Log(shoot);
            Invoke("ShooterBullet", timeDelay);
            animator.SetBool("Shooter", true);
            StartCoroutine(delay());
            if (shoot && !myBox.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                animator.SetBool("Jumping", false);
                animator.SetBool("Shooter", true);
                StartCoroutine(delay());
            }

            //if(myBox.IsTouchingLayers(LayerMask.GetMask("Ground")))
        }
    }
    /*
    public void UpShooter()
    {
        Debug.Log(shoot);
        animator.SetBool("Shooter", false);
        if (!myBox.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            animator.SetBool("Jumping", true);
        }
    }
    */
    void ShooterBullet()
    {
        Instantiate(bullet, transforml.transform.position, Quaternion.identity);
    }

    IEnumerator delay()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        animator.SetBool("Shooter", false);
        if (!myBox.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            animator.SetBool("Jumping", true);
        }
    }

}
