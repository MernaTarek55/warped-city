using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints;

    [SerializeField]
    private float moveSpeed = 2f;

    private int waypointIndex = 0;
    private Animator animator;

    private void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
        animator = GetComponent<Animator>(); 
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypoints.Length == 0) return;

        transform.position = Vector2.MoveTowards(transform.position,
            waypoints[waypointIndex].transform.position,
            moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, waypoints[waypointIndex].transform.position) < 0.1f)
        {
            animator.Play("Drone");
            waypointIndex += 1;

            if (waypointIndex >= waypoints.Length)
            {
                waypointIndex = 0;
            }
        }
    }
}
