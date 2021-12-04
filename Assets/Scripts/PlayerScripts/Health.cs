using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float fSpawnX;
    public float fSpawnY;


    public int iHealth = 100;

    private bool block = false;
    public int iBaseDamage = 5;
    private int iAmpifiedDamage;
    private int iAbilitys = 3;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Evil"))
        {
          collision.gameObject.GetComponent<HP_N_Attack>().TakeDamage(iAmpifiedDamage);
            Debug.Log("PlayerHit");
        }
    }

    public void DamageTaken(int amount)
    {
        iHealth -= amount;
    }

    public void attacks()
    {
        while(iHealth >= 10)
        {
            iAmpifiedDamage = iBaseDamage * 5;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (iHealth <= 0)
        {
            transform.position = new Vector2(fSpawnX, fSpawnY);
            iHealth = 100;
}
    }
}
