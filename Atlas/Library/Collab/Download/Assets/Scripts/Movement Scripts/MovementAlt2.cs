using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAlt2 : MonoBehaviour
{

    public float moveSpeed = 3f;

    public GameObject Player;
    public Rigidbody2D PlayerRb;

    Vector2 movement;

    public bool canMove;

    public Sprite Up;
    public Sprite Down;
    public Sprite Left;
    public Sprite Right;

    // Update is called once per frame
    void Update()
    {

        if (!canMove)
        {
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

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

        if (!canMove)
        {
            return;
        }

        PlayerRb.MovePosition(PlayerRb.position + movement * moveSpeed * Time.fixedDeltaTime); 
    }

}
