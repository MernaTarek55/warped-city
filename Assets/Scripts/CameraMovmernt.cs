using UnityEngine;

public class CameraMovmernt : MonoBehaviour
{
    Transform player; 
    public Transform player1; 
    public Transform player2; 
    public Vector3 offset; 
    public float smoothSpeed = 0.125f;
    [SerializeField] float limtposX1;
    [SerializeField] float limtposY1;
    [SerializeField] float limtposX2;
    [SerializeField] float limtposY2;
    private void Start()
    {
        player = player1.gameObject.activeSelf?player1 : player2;
    }
    void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        if(smoothedPosition.x < limtposX1)
        {
            smoothedPosition.x = limtposX1;
        }
        if(smoothedPosition.y < limtposY1)
        {
            smoothedPosition.y = limtposY1;
        }
        if (smoothedPosition.x > limtposX2)
        {
            smoothedPosition.x = limtposX2;
        }
        if (smoothedPosition.y > limtposY2)
        {
            smoothedPosition.y = limtposY2;
        }
        transform.position = new (smoothedPosition.x , smoothedPosition.y , transform.position.z);

    }
}
