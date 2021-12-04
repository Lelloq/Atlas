using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float fMoveSpeed;
    public float fBulletDration;
    public int iDamage = 100;

    private Transform playerLocate;
    private Vector2 target;
    public AudioSource shotPlayer;


    // Start is called before the first frame update
    void Start()
    {
        playerLocate = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        target = new Vector2(playerLocate.position.x, playerLocate.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        fBulletDration -= Time.deltaTime;

        if (fBulletDration <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, playerLocate.position, fMoveSpeed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<HP_and_Attack>().TakeDamage(iDamage);
            shotPlayer.Play();
            Destroy(gameObject);
        }
    }
}
