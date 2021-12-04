using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{

    public GameObject Turret;
    public GameObject Turret1;

    public GameObject Fireball;
    public GameObject ThankYou;

    public GameObject TextBox;

    void FixedUpdate()
    {
        if (!TextBox.activeSelf)
        {
            Destroy(Turret);
            Destroy(Turret);
            Fireball.SetActive(true);
            ThankYou.SetActive(true);
        }
    }
}
