using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform Player;
    public float smoothTime = 0.3f;
    private Vector2 velocity = Vector2.zero;

    private void FixedUpdate()
    {
        Vector2 targetPosition = Player.TransformPoint(new Vector2(0, 5));
        transform.position = Vector2.SmoothDamp(transform.position, Player.position, ref velocity, smoothTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}