using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float fixedY = 0f;

    void LateUpdate()
    {
        transform.position = new Vector3(
            player.position.x,
            fixedY,
            transform.position.z
        );
    }
}
