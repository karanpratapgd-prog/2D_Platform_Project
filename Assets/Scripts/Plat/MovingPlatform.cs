using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;     // Platform speed
    public float distance = 3f;  // How far it moves

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float move = Mathf.PingPong(Time.time * speed, distance);
        transform.position = new Vector3(
            startPos.x + move,
            startPos.y,
            startPos.z
        );
    }
}
