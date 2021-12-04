using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_N_Attack : MonoBehaviour
{
    //Melee enemieys

    public int iEvilHealth = 100;
    private int iHealth;

    public float fAttackSpeed;
    private float fAttackWait;
    private bool bAttack = true;
    public AudioSource PlayerHit;

    public int iEvilBaseAttack = 5;
    public float fAttackRange;
    public Transform attackCenter;
    public LayerMask enemiesLayer;

    [SerializeField]
    new AudioSource soundClip;
    [SerializeField]
    private AudioClip attack;

    void Start()
    {
        iHealth = iEvilHealth;
        soundClip = (AudioSource)gameObject.AddComponent<AudioSource>();
    }

    public void FixedUpdate()
    {

        if (fAttackWait <= 0)
        {
            fAttackWait = fAttackSpeed;
            bAttack = true;
        }
        else
        {
            fAttackWait = fAttackWait - Time.deltaTime;
        }

        if (iHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (bAttack == true)
        {
            
            Collider2D[] enmiesToDamage = Physics2D.OverlapCircleAll(attackCenter.position, fAttackRange, enemiesLayer);
            for (int i = 0; i < enmiesToDamage.Length; i++)
            {
                enmiesToDamage[0].GetComponent<HP_and_Attack>().TakeDamage(iEvilBaseAttack);
                PlayerHit.Play();
            }
            bAttack = false;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (bAttack = true)
        {
            soundClip.PlayOneShot(attack, 0.7F);
        }
    }

    public void TakeDamage(int damage)
    {
        iHealth -= damage;

        Debug.Log("Evil hit");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackCenter.position, fAttackRange);
    }

}
