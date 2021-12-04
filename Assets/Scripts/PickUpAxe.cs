using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAxe : MonoBehaviour
{
    public bool Collect = false;
    public KeyCode interact;

    // Update is called once per frame
    void Update()
    {
        if (Collect == true)
        {
            if (Input.GetKeyDown(interact))
            {
                PlayerState.bAxeCollect = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collect = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collect = false;
        }
    }
}
