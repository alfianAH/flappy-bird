using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    // Global variables
    [SerializeField] private float upForce = 100;
    [SerializeField] private bool isDead;
    [SerializeField] private UnityEvent OnJump, OnDead;

    [Header("Shoot")]
    [SerializeField] private int ammo = 3;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform bulletHolder;
    [SerializeField] private Text bulletText;
    [SerializeField] private GameObject bulletInstruction;

    [Header("Score")]
    [SerializeField] private int score;
    [SerializeField] private Text scoreText;
    [SerializeField] private UnityEvent onAddPoint;
    
    private Rigidbody2D rigidbody2D;
    private Animator animator;
    
    public bool IsDead => isDead; // Getter isDead

    private void Start()
    {
        // Get Rigidbody2D component
        rigidbody2D = GetComponent<Rigidbody2D>();
        // Get Animator component
        animator = GetComponent<Animator>();
        // Set bulletText
        if(bulletText)
            bulletText.text = ammo.ToString();
        // Start ShowBulletInstruction Coroutine
        StartCoroutine(ShowBulletInstruction());
    }

    private void Update()
    {
        // If bird isn't dead and left-mouse is clicked, Jump
        if (!isDead && Input.GetMouseButtonDown(0))
            Jump(); // Jump

        // If Space is pressed, Shoot!!!
        if (isDead || !Input.GetKeyDown(KeyCode.Space) || ammo <= 0) return;
        ammo--;
        bulletText.text = ammo.ToString();
        bullet.Shoot(bulletHolder);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Stop bird animation when bird collides with other objects
        animator.enabled = false;
    }

    /// <summary>
    /// Wait for 5 seconds and destroy bullet instruction
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShowBulletInstruction()
    {
        yield return new WaitForSeconds(5f);
        Destroy(bulletInstruction);
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
    
    public void AddScore(int value)
    {
        score += value; // Add score

        onAddPoint?.Invoke(); // Call all events on onAddPoint

        scoreText.text = score.ToString();  // Set scoreText
    }
}
