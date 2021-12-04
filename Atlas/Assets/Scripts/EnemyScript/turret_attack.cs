using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret_attack : MonoBehaviour
{
    public GameObject carrotBullet;
    private Transform playerLocate;
    public AudioSource turretShoot;
    public int iAttackRange = 10;

    public float fAttackSpeed;
    private float fNewBullet;

    // Start is called before the first frame update
    void Start()
    {
        playerLocate = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        fNewBullet = fAttackSpeed;

    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if ((Vector2.Distance(transform.position, playerLocate.position) < iAttackRange))
        {
            if (fNewBullet <= 0)
            {
                turretShoot.Play();
                Instantiate(carrotBullet, transform.position, Quaternion.Euler(new Vector3(0, 180, -90)));
                fNewBullet = fAttackSpeed;
            }
            else
            {
                fNewBullet -= Time.deltaTime;
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, iAttackRange);
    }
}
