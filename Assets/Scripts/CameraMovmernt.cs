using UnityEngine;

public class CameraMovmernt : MonoBehaviour
{
    Transform player; 
    public Transform player1; 
    public Transform player2; 
    public Vector3 offset; 
    public float smoothSpeed = 0.125f;
    private void Start()
    {
        player = player1.gameObject.activeSelf?player1 : player2;
    }
    void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        if(smoothedPosition.x < 0)
        {
            smoothedPosition.x = 0;
        }
        if(smoothedPosition.y < 0)
        {
            smoothedPosition.y = 0;
        }
        if (smoothedPosition.x > 95)
        {
            smoothedPosition.x = 95;
        }
        if (smoothedPosition.y > 25)
        {
            smoothedPosition.y = 25;
        }
        transform.position = new (smoothedPosition.x , smoothedPosition.y , transform.position.z);

    }
}
