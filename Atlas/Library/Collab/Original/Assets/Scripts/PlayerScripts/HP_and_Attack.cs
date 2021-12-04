using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HP_and_Attack : MonoBehaviour
{

    public int iPlayerHealth = 100;
    private int iHealth;


    public float fAttackSpeed;
    private float fAttackWait;
    private bool bAttack = true;


    public int iPlayerBaseAttack;
    private int iChangeAttack;
    public float fAttackRange;
    public Transform attackCenter;
    public Transform player;
    public LayerMask enemiesLayer;


    public bool bSword = false;
    public bool bTexAxe = false;
    public bool bBlocking = false;

    private KeyCode MLeft; private KeyCode MRight;
    private KeyCode MUp; private KeyCode MDown;
    private KeyCode SwingSword; private KeyCode Block;

    // Start is called before the first frame update
    void Start()
    {
        MLeft = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A"));
        MRight = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D"));
        MUp = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "W"));
        MDown = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "S"));
        SwingSword = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Attack", "Mouse0"));
        Block = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Block", "Mouse1"));

        iHealth = iPlayerHealth;
        iChangeAttack = iPlayerBaseAttack;
    }

    // Update is called once per frame
    void Update()
    {
        if (fAttackWait <= 0)
        {
            fAttackWait = fAttackSpeed;
            bAttack = true;
        }
        else
        {
            fAttackWait -= Time.deltaTime;
        }




        if (bSword == true)
        {
            iChangeAttack = iPlayerBaseAttack * 3;
        }
        if (bTexAxe == true)
        {
            iChangeAttack = iPlayerBaseAttack * 5;
        }
    }

    private void FixedUpdate()
    {

        if (iHealth <= 0)
        {
            transform.position = new Vector2(-2, -2);
        }

        if (bAttack == true)
        {
            if (Input.GetKey(SwingSword))
            {
                Collider2D[] enmiesToDamage = Physics2D.OverlapCircleAll(attackCenter.position, fAttackRange, enemiesLayer);
                for (int i = 0; i < enmiesToDamage.Length; i++)
                {
                    enmiesToDamage[i].GetComponent<HP_N_Attack>().TakeDamage(iChangeAttack);
                }

                bAttack = false;
            }
        }

        if (Input.GetKey(MUp))
        {
            attackCenter.position = new Vector2(player.position.x, player.position.y + 0.5f);
        }
        if (Input.GetKey(MLeft))
        {
            attackCenter.position = new Vector2(player.position.x + -0.5f, player.position.y);
        }
        if (Input.GetKey(MDown))
        {
            attackCenter.position = new Vector2(player.position.x, player.position.y + -0.5f);
        }
        if (Input.GetKey(MRight))
        {
            attackCenter.position = new Vector2(player.position.x + 0.5f, player.position.y);
        }


        if (Input.GetKeyDown(Block))
        {
            bBlocking = true;
        }
        if (Input.GetKeyUp(Block))
        {
            bBlocking = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackCenter.position, fAttackRange);
    }

    public void TakeDamage(int damage)
    {
        if (bBlocking == false)
        {
            iHealth -= damage;
            Debug.Log("Player hit");

        }
    }
}
