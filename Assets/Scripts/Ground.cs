using UnityEngine;

// Require Component will add Component and make that component unremovable
[RequireComponent(typeof(BoxCollider2D))]
public class Ground : MonoBehaviour
{
    // Global variables
    [SerializeField] private Bird bird;
    [SerializeField] private float speed = 1;
    [SerializeField] private Transform nextPos;

    private void Update()
    {
        // Check bird if null or isn't dead
        if (bird == null || (bird != null && !bird.IsDead))
        {
            // Make ground move to left
            transform.Translate(Vector3.left * (speed * Time.deltaTime), Space.World);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Make bird is dead when collide with ground
        if (bird && !bird.IsDead)
        {
            bird.Dead();
        }
    }

    /// <summary>
    /// Place ground in the nextPos
    /// </summary>
    /// <param name="ground"></param>
    public void SetNextGround(GameObject ground)
    {
        if (ground)
        {
            // Place next ground in the nextPos
            ground.transform.position = nextPos.position;
        }
    }
}
