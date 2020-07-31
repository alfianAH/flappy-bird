using UnityEngine;
using UnityEngine.Events;

public class Bird : MonoBehaviour
{
    // Global variables
    [SerializeField] private float upForce = 100;
    [SerializeField] private bool isDead;
    [SerializeField] private UnityEvent OnJump, OnDead;

    private Rigidbody2D rigidbody2D;
    private Animator animator;
    
    public bool IsDead => isDead; // Getter isDead

    private void Start()
    {
        // Get Rigidbody2D component
        rigidbody2D = GetComponent<Rigidbody2D>();
        // Get Animator component
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // If bird isn't dead and left-mouse is clicked, ...
        if (!isDead && Input.GetMouseButtonDown(0))
        {
            Jump(); // Jump
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Stop bird animation when bird collides with other objects
        animator.enabled = false;
    }

    /// <summary>
    /// Make bird die
    /// </summary>
    public void Dead()
    {
        if (!isDead)
        {
            // Call all events on OnDead
            OnDead?.Invoke();
        }

        isDead = true; // Set isDead to true
    }

    private void Jump()
    {
        // Check rigidbody2D
        if (rigidbody2D)
        {
            // Stop bird's velocity when falling
            rigidbody2D.velocity = Vector2.zero;
            
            // Add Force y-axis to Jump
            rigidbody2D.AddForce(new Vector2(0, upForce));
        }
        
        // Check null variable
        OnJump?.Invoke(); // Call all events on OnJump
    }
}
