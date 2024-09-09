using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDie : MonoBehaviour
{
    [SerializeField] float lives = 100;
    [SerializeField] GameObject Boss;
    [SerializeField] Animator animator;
    [SerializeField] GameObject panelWin;
    [SerializeField] Image redHealth;
    [SerializeField] GameObject effectBoss;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(lives);
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            animator.SetBool("TakeDamage", true);
            TruMau();
            if (lives <= 0)
            {
                Instantiate(effectBoss, transform.position, Quaternion.identity);
                Destroy(Boss);
                redHealth.fillAmount = 0;
                panelWin.SetActive(true);
            }
        }
    }

    private void TruMau()
    {
        lives -= 5;
        redHealth.fillAmount = lives / 100;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            animator.SetBool("TakeDamage", false);
            Destroy(collision.gameObject);
        }
    }

}
