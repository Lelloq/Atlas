using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Break_Turret : MonoBehaviour
{
    public GameObject Turret;
    private KeyCode Interact;

    private void Start()
    {
        Interact = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "E"));
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.GetKeyDown(Interact))
        {
            if (collision.gameObject.name == "Player")
            {
                Turret.SetActive(false);
            }
        }
    }
}


