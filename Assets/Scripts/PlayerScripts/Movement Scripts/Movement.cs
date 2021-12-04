using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    public GameObject Player;

    public Rigidbody2D PlayerRb;
    public Sprite Up;
    public Sprite Down;
    public Sprite Left;
    public Sprite Right;

    public int iSpd = 3;
    private int iXDirection = 0;
    private int iYDirection = 0;

    private void Update()
    {
        switch (iXDirection)
        {
            case -1:
                Player.GetComponent<SpriteRenderer>().sprite = Left;
                break;

            case 1:
                Player.GetComponent<SpriteRenderer>().sprite = Right;
                break;
        }

        switch (iYDirection)
        {
            case -1:
                Player.GetComponent<SpriteRenderer>().sprite = Down;
                break;

            case 1:
                Player.GetComponent<SpriteRenderer>().sprite = Up;
                break;
        }

        PlayerRb.velocity = new Vector2(iXDirection * iSpd, iYDirection * iSpd);
        if (Input.GetKeyDown(KeyCode.W))
        {
            iYDirection = 1;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            iXDirection = -1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            iYDirection = -1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            iXDirection = 1;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            iYDirection = 0;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            iXDirection = 0;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            iYDirection = 0;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            iXDirection = 0;
        }
    }
}