using System.Collections;
using UnityEngine;

public class MovementAlt : MonoBehaviour
{
    [SerializeField]
    public GameObject Player;

    public Rigidbody2D PlayerRb;
    public Sprite Up;
    public Sprite Down;
    public Sprite Left;
    public Sprite Right;
    public LayerMask solidObjectsLayer;
    public float moveSpeed = 1;

    private bool isMoving = false;
    private Vector2 input;

    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0)
            {
                input.y = 0;
            }

            if (input != Vector2.zero)
            {
                switch (input.x)
                {
                    case -1:
                        Player.GetComponent<SpriteRenderer>().sprite = Left;
                        break;

                    case 1:
                        Player.GetComponent<SpriteRenderer>().sprite = Right;
                        break;
                }

                switch (input.y)
                {
                    case -1:
                        Player.GetComponent<SpriteRenderer>().sprite = Down;
                        break;

                    case 1:
                        Player.GetComponent<SpriteRenderer>().sprite = Up;
                        break;
                }

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos)) StartCoroutine(Move(targetPos));
            }
        }
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.1f, solidObjectsLayer) != null)
        {
            return false;
        }
        else return true;
    }

    private IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }
}