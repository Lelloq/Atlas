using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MovementAlt2 : MonoBehaviour
{

    public float moveSpeed = 3f;

    public GameObject Player;
    public Rigidbody2D PlayerRb;

    Vector2 movement;
    private int yDir;
    private int xDir;

    public Sprite Up;
    public Sprite Down;
    public Sprite Left;
    public Sprite Right;
    private KeyCode MLeft;
    private KeyCode MRight;
    private KeyCode MUp;
    private KeyCode MDown;
    private bool LeftDown = false;
    private bool RightDown = false;
    private bool UpDown = false;
    private bool DownDown = false;

    private void Start()
    {
        MLeft = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A"));
        MRight = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D"));
        MUp = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "W"));
        MDown = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "S"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(MLeft)) { LeftDown = true; }
        else if(Input.GetKeyUp(MLeft)) { LeftDown = false; }

        if (Input.GetKeyDown(MRight)) { RightDown = true; }
        else if (Input.GetKeyUp(MRight)) { RightDown = false; }

        if (Input.GetKeyDown(MUp)) { UpDown = true; }
        else if (Input.GetKeyUp(MUp)) { UpDown = false; }

        if (Input.GetKeyDown(MDown)) { DownDown = true; }
        else if (Input.GetKeyUp(MDown)) { DownDown = false; }

        if ((LeftDown && RightDown) || !(LeftDown && RightDown)) { xDir = 0; }
        if (LeftDown && !RightDown) { xDir = -1; }
        if(RightDown && !LeftDown) { xDir = 1; }

        if ((UpDown && DownDown) || !(UpDown && DownDown)) { yDir = 0; }
        if (DownDown && !UpDown) { yDir = -1; }
        if (UpDown && !DownDown) { yDir = 1; }

        movement.x = xDir;
        movement.y = yDir;

        switch (movement.x)
        {
            case -1:
                Player.GetComponent<SpriteRenderer>().sprite = Left;
                break;

            case 1:
                Player.GetComponent<SpriteRenderer>().sprite = Right;
                break;
        }

        switch (movement.y)
        {
            case -1:
                Player.GetComponent<SpriteRenderer>().sprite = Down;
                break;

            case 1:
                Player.GetComponent<SpriteRenderer>().sprite = Up;
                break;
        }

    }

    void FixedUpdate()
    {
        PlayerRb.MovePosition(PlayerRb.position + movement * moveSpeed * Time.fixedDeltaTime); 
    }
}
