using UnityEngine;

public class BGFollow : MonoBehaviour
{
    public Transform cam;
    public float followSpeed = 0.5f;

    private float startX;

    void Start()
    {
        startX = transform.position.x;
    }

    void LateUpdate()
    {
        float newX = startX + cam.position.x * followSpeed;
        transform.position = new Vector3(
            newX,
            transform.position.y,
            transform.position.z
        );
    }
}
