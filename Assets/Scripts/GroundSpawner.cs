using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class GroundSpawner : MonoBehaviour
{
    // Save groundRef
    [SerializeField] private Ground groundRef;
    
    // Save previous ground
    private Ground prevGround;

    /// <summary>
    /// SpawnGround will make a new ground
    /// </summary>
    private void SpawnGround()
    {
        if (prevGround)
        {
            // Duplicate groundRef
            Ground newGround = Instantiate(groundRef);
            
            // Activate newGround
            newGround.gameObject.SetActive(true);
            
            // Place newGround in the nextGround position from prevGround so that its position is parallel to the previous Ground
            prevGround.SetNextGround(newGround.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Ground ground = other.GetComponent<Ground>();

        if (ground)
        {
            prevGround = ground; // Set prevGround
            
            SpawnGround(); // Make new Ground
        }
    }
}
