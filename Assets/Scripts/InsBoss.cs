using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsBoss : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject boss;
    void Start()
    {
        Invoke("KhoiTaoBoss", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void KhoiTaoBoss()
    {
        boss.SetActive(true);
        panel.SetActive(false);
    }
}
