using UnityEngine;
using Random = UnityEngine.Random;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bird bird;
    [SerializeField] private Pipe pipe;
    [SerializeField] private float speed = 1;
    [SerializeField] private int minNumber = 0,
        maxNumber = 20;
    
    private void Start()
    {
        speed = pipe.Speed;
    }

    private void Update()
    {
        if (!bird.IsDead)
        {
            // Move this game object to the left
            transform.Translate(Vector3.left * (speed * Time.deltaTime), Space.World);
        }
    }

    public void Spawn(Vector3 pipePosition, Quaternion pipeRotation, float yAxis)
    {
        int rng = Random.Range(minNumber, maxNumber); // Generate random number

        if(rng < (maxNumber / 4))
        {
            GameObject bulletClone = Instantiate(gameObject, pipePosition, pipeRotation);
            bulletClone.SetActive(true);
            bulletClone.transform.position += Vector3.up * yAxis;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // If hit bird, ...
        if (!other.gameObject.CompareTag("Player")) return;
        Destroy(gameObject); // Destroy bullet
        bird.Ammo += 1; // Add ammo
        bird.ChangeBulletUi(); // Change UI
    }
}
