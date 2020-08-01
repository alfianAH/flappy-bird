using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Point : MonoBehaviour
{
    [SerializeField] private Bird bird;
    [SerializeField] private float speed = 1;
    // [SerializeField] private Pipe pipe;
    //
    // private void Start()
    // {
    //     speed = pipe.Speed;
    // }

    private void Update()
    {
        if (!bird.IsDead)
        {
            // Move this game object to the left
            transform.Translate(Vector3.left * (speed * Time.deltaTime));
        }
    }

    public void SetSize(float size)
    {
        // Get BoxCollider2D component
        BoxCollider2D collider = GetComponent<BoxCollider2D>();

        if (collider)
        {
            // Change collider size 
            collider.size = new Vector2(collider.size.x, size);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Get Bird component
        Bird bird = other.gameObject.GetComponent<Bird>();

        if (bird && !bird.IsDead)
            bird.AddScore(1);
    }
}
