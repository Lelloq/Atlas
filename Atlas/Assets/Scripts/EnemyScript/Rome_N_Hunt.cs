using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rome_N_Hunt : MonoBehaviour
{
    public float fMovementSpeed = 0f;

    private float fDirectionX;
    private float fDirectionY;

    private float fChangDirctionClock = 2f;
    private bool bSwitchDiection = true;
    public Transform moveLocation;
    public Transform homeLocation;
    public bool bHomePoint = true;


    public int iVisionRang = 0;
    public Rigidbody2D rb;
    private Transform target;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    private void Update()
    {
        fChangDirctionClock -= Time.deltaTime;

    }

    void FixedUpdate()
    {
        if (fChangDirctionClock <= 0)
        {
            fDirectionY = transform.position.y + Random.Range(-4f, 4f);
            fDirectionX = transform.position.x + Random.Range(-4f, 4f);

            moveLocation.position = new Vector2(fDirectionX, fDirectionY);

            fChangDirctionClock = 3;
            bSwitchDiection = true;
            bHomePoint = true;
        }

        if (bHomePoint == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveLocation.position, fMovementSpeed * Time.deltaTime);
        }
        else
        {
            if (Vector2.Distance(transform.position, target.position) > iVisionRang)
            {
                transform.position = Vector2.MoveTowards(transform.position, moveLocation.position, fMovementSpeed * Time.deltaTime);
            }
            else
            {
                if ((Vector2.Distance(transform.position, target.position) < 1))
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, 0);
                    fChangDirctionClock = 0;
                }
                else if ((Vector2.Distance(transform.position, target.position) < iVisionRang) && (bSwitchDiection == true))
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, fMovementSpeed * Time.deltaTime);
                    moveLocation.position = target.position;
                    fChangDirctionClock = 0;
                }
            }

        }



    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("EvilHome"))
        {
            moveLocation.position = new Vector2(homeLocation.position.x, homeLocation.position.y);
            fChangDirctionClock = 3f;
            Debug.Log("Hit");
            bHomePoint = false;
            bSwitchDiection = false;
        }
    }
}
