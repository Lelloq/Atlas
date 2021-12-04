using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public int damagePoint = 1;
    public float pushForce = 2.0f;

    public GameObject GO;

    private float cooldown = 0.5f;
    private float lastSwing;

    KeyCode SwingSword;

    //SFX

    [SerializeField]
    new AudioSource swingClip;
    [SerializeField]
    private AudioClip swing;

    void Start()
    {
        SwingSword = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Attack", "Mouse0"));
        swingClip = (AudioSource)gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(SwingSword))
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    private void Swing()
    {
        Debug.Log("Swing");
    }

    void OnCollisionEnter(Collision coll)
    {
        print(coll.transform.name);
        coll.transform.GetComponent<MeshRenderer>().material.color = Color.red;
        swingClip.PlayOneShot(swing, 0.7F);
    }
}
