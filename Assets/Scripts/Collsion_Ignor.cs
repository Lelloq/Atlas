using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collsion_Ignor : MonoBehaviour
{
    void Update()
    {
        Physics2D.IgnoreLayerCollision(14,15);
    }
}
