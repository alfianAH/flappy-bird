using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10000f;
    
    public void Shoot(Transform bulletHolder)
    {
        GameObject bulletClone = Instantiate(gameObject); // Clone self
        // Set position equal to bulletHolder's position
        bulletClone.transform.position = bulletHolder.position;
        // Get bulletClone's Rigidbody component
        Rigidbody2D bulletRigidbody = bulletClone.GetComponent<Rigidbody2D>();
        // Add Force to bulletClone
        bulletRigidbody.AddForce(new Vector2(bulletSpeed * Time.deltaTime, 0f));
    }

    /// <summary>
    /// When collide with other object, except Bird, destroy self and that object
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject) return;
        if (other.gameObject.name == "Bird") return;
        Destroy(other.gameObject); // Destroy other gameObject
        Destroy(gameObject); // Destroy self
    }
}
