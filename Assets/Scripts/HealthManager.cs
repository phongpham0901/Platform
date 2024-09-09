using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HealthManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Image healthBar;
    [SerializeField] public float healthAmount = 100f;

    public static HealthManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {

    }
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        if (healthAmount > 0)
        {
            healthAmount -= damage;
            healthBar.fillAmount = healthAmount / 100;
        }
    }

    public void Health(float healingAmount)
    {
        if (healthAmount < 100 && healthAmount > 0)
        {
            healthAmount += healingAmount;
            healingAmount = Mathf.Clamp(healthAmount, 0, 100);

            if (healthBar != null)
            {
                healthBar.fillAmount = healthAmount / 100f;
            }
        }
    }
}
