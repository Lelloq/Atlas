using System;
using UnityEngine;

public class MovementAlt2 : MonoBehaviour
{
    public float moveSpeed = 3f;

    public GameObject Player;
    public Rigidbody2D PlayerRb;
    public GameObject Sword;
    public bool canMove;

    private Vector2 movement;
    private int yDir, xDir;

    public Sprite Up; public Sprite Down;
    public Sprite Left; public Sprite Right;

    public Transform HurtBox;

    private InGameData Data;

    private KeyCode MLeft; private KeyCode MRight;
    private KeyCode MUp; private KeyCode MDown;
    private KeyCode MRun;

    private bool LeftDown = false; private bool RightDown = false;
    private bool UpDown = false; private bool DownDown = false;

    private void Start()
    {
        Data = GameObject.Find("GameData").GetComponent<InGameData>();
        MLeft = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A"));
        MRight = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D"));
        MUp = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "W"));
        MDown = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "S"));
        MRun = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Run", "LeftShift"));
    }

    // Update is called once per frame
    private void Update()
    {
        if (Data.GotSword == true)
        {
            Sword = Player.transform.Find("Sword").gameObject;
            Sword.SetActive(true);
        }
        if (Input.GetKeyDown(MLeft)) { LeftDown = true; }
        else if (Input.GetKeyUp(MLeft)) { LeftDown = false; }

        if (Input.GetKeyDown(MRight)) { RightDown = true; }
        else if (Input.GetKeyUp(MRight)) { RightDown = false; }

        if (Input.GetKeyDown(MUp)) { UpDown = true; }
        else if (Input.GetKeyUp(MUp)) { UpDown = false; }

        if (Input.GetKeyDown(MDown)) { DownDown = true; }
        else if (Input.GetKeyUp(MDown)) { DownDown = false; }

        if (Input.GetKeyDown(MRun)) { moveSpeed *= 1.5f; }
        else if (Input.GetKeyUp(MRun)) { moveSpeed /= 1.5f; }

        if (canMove)
        {
            if ((LeftDown && RightDown) || !(LeftDown && RightDown)) { xDir = 0; }
            if (LeftDown && !RightDown) { xDir = -1; }
            if (RightDown && !LeftDown) { xDir = 1; }

            if ((UpDown && DownDown) || !(UpDown && DownDown)) { yDir = 0; }
            if (DownDown && !UpDown) { yDir = -1; }
            if (UpDown && !DownDown) { yDir = 1; }

            movement.x = xDir;
            movement.y = yDir;
        }
    }

    private void FixedUpdate()
    {
        PlayerRb.MovePosition(PlayerRb.position + movement * moveSpeed * Time.fixedDeltaTime);
        if (Time.timeScale == 1)
        {
            switch (movement.x)
            {
                case -1:
                    Player.GetComponent<SpriteRenderer>().sprite = Left;
                    if (Sword != null)
                    {
                        Sword.GetComponent<SpriteRenderer>().enabled = true;
                        Sword.GetComponent<SpriteRenderer>().sortingOrder = 8;
                        Sword.GetComponent<Transform>().localPosition = new Vector2(0.2f, 1.26f);
                    }
                    break;

                case 1:
                    Player.GetComponent<SpriteRenderer>().sprite = Right;
                    if (Sword != null)
                    {
                        Sword.GetComponent<SpriteRenderer>().enabled = true;
                        Sword.GetComponent<SpriteRenderer>().sortingOrder = 8;
                        Sword.GetComponent<Transform>().localPosition = new Vector2(-0.45f, 1.26f);
                    }
                    break;
            }

            switch (movement.y)
            {
                case -1:
                    Player.GetComponent<SpriteRenderer>().sprite = Down;
                    if (Sword != null)
                    {
                        Sword.GetComponent<SpriteRenderer>().enabled = true;
                        Sword.GetComponent<SpriteRenderer>().sortingOrder = 8;
                        Sword.GetComponent<Transform>().localPosition = new Vector2(0.2f, 1.26f);
                    }
                    break;

                case 1:
                    Player.GetComponent<SpriteRenderer>().sprite = Up;
                    if (Sword != null)
                    {
                        Sword.GetComponent<SpriteRenderer>().enabled = true;
                        Sword.GetComponent<SpriteRenderer>().sortingOrder = 999;
                        Sword.GetComponent<Transform>().localPosition = new Vector2(0.2f, 1.26f);
                    }
                    break;
            }
        }
    }
}