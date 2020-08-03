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
    [SerializeField] private Animator bulletTextAnimator;
    [SerializeField] private Animator bulletInstruction;

    [Header("Score")]
    [SerializeField] private int score;
    [SerializeField] private Text scoreText;
    [SerializeField] private UnityEvent onAddPoint;
    
    private Rigidbody2D rigidbody2D;
    private Animator birdAnimator;

    public int Ammo
    {
        get => ammo;
        set => ammo = value;
    }

    public bool IsDead => isDead; // Getter isDead

    private void Start()
    {
        // Get Rigidbody2D component
        rigidbody2D = GetComponent<Rigidbody2D>();
        // Get Animator component
        birdAnimator = GetComponent<Animator>();
        // Set bulletText
        if (bulletText)
            ChangeBulletUi();
        // Start ShowBulletInstruction Coroutine
        StartCoroutine(BlinkAnimation(bulletInstruction,5f));
        StartCoroutine(BulletInstruction(5f));
    }

    private void Update()
    {
        // If bird isn't dead and left-mouse is clicked, Jump
        if (!isDead && Input.GetMouseButtonDown(0))
            Jump(); // Jump

        // If Space is pressed, Shoot!!!
        if (!isDead && Input.GetKeyDown(KeyCode.Space))
        {
            if(ammo > 0)
            {
                ammo--;
                ChangeBulletUi();
                bullet.Shoot(bulletHolder);
            }
            else
            {
                bulletTextAnimator.enabled = true;
                StartCoroutine(BlinkAnimation(bulletTextAnimator, 3f));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Stop bird animation when bird collides with other objects
        birdAnimator.enabled = false;
    }

    /// <summary>
    /// Change bullet UI 
    /// </summary>
    public void ChangeBulletUi()
    {
        bulletText.text = ammo.ToString();
    }

    /// <summary>
    /// Wait until "waitSeconds" and disable "animator"
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="waitSeconds"></param>
    /// <returns></returns>
    private IEnumerator BlinkAnimation(Animator animator, float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        if(animator)
            animator.enabled = false;
        
        // Enable bulletText after animation if it's not enabled 
        if (animator != bulletTextAnimator) yield break;
        if (!bulletText) yield break;
        if (!bulletText.enabled)
            bulletText.enabled = true;
    }

    /// <summary>
    /// Destroy bullet instruction after wait seconds
    /// </summary>
    /// <param name="waitSeconds"></param>
    /// <returns></returns>
    private IEnumerator BulletInstruction(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        Destroy(bulletInstruction);
    }

    /// <summary>
    /// Make bird die
    /// </summary>
    public void Dead()
    {
        if (!isDead)
            OnDead?.Invoke(); // Call all events on OnDead

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
    
    /// <summary>
    /// AddScore by value
    /// </summary>
    /// <param name="value"></param>
    public void AddScore(int value)
    {
        onAddPoint?.Invoke(); // Call all events on onAddPoint
        score += value; // Add score
        scoreText.text = score.ToString();  // Set scoreText

        if (score > PlayerPrefs.GetInt("highScore"))
            PlayerPrefs.SetInt("highScore", score);
    }
}
