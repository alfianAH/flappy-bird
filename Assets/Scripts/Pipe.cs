using System;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    // Global variables
    [SerializeField] private Bird bird;
    [SerializeField] private float speed = 1;

    private void Update()
    {
        // Check if bird isn't dead yet
        if (!bird.IsDead)
        {
            // Make pipe move to left
            transform.Translate(Vector3.left * (speed * Time.deltaTime), Space.World);
        }
    }

    // Make bird is dead when collide with this object and make it falling down
    private void OnCollisionEnter2D(Collision2D other)
    {
        Bird bird = other.gameObject.GetComponent<Bird>();

        if (bird)
        {
            // Get collider component
            Collider2D collider = GetComponent<Collider2D>();

            if (collider)
                collider.enabled = false; // Deactivate collider
            bird.Dead();
        }
    }
}
