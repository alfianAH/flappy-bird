using System;
using System.Collections;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    // Global variables 
    [SerializeField] private Bird bird;
    [SerializeField] private Pipe pipeUp, pipeDown;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float holeSize = 1f;
    [SerializeField] private float maxMinOffset = 1f;
    [SerializeField] private Point point;
    
    // Coroutine variable
    private Coroutine crSpawn;

    private void Start()
    {
        StartSpawn();
    }

    private void StartSpawn()
    {
        // Start Spawn Coroutine
        if (crSpawn == null)
            crSpawn = StartCoroutine(Spawn());
        
    }
    
    private IEnumerator Spawn()
    {
        while (true)
        {
            // If bird is dead, Stop Spawn
            if(bird.IsDead)
                StopSpawn();
            
            SpawnPipe(); // Spawn new pipe
            
            // Wait for seconds to spawn new pipe
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void StopSpawn()
    {
        // Stop coroutine Spawn 
        if (crSpawn != null)
            StopCoroutine(crSpawn);
    }

    private void SpawnPipe()
    {
        // Duplicate pipeUp and place it in the same position but rotated 180 degrees
        Pipe newPipeUp = Instantiate(pipeUp, transform.position, Quaternion.Euler(0, 0, 180));
        
        // Activate newPipeUp
        newPipeUp.gameObject.SetActive(true);
        
        // Duplicate pipeDown and place it in the same position
        Pipe newPipeDown = Instantiate(pipeDown, transform.position, Quaternion.identity);
        
        // Activate newPipeDown
        newPipeDown.gameObject.SetActive(true);

        // Make hole in the middle of pipe
        newPipeUp.transform.position += Vector3.up * (holeSize / 2); 
        newPipeDown.transform.position += Vector3.down * (holeSize / 2);
        
        // Place pipe's position so that its position adjusts to the Sin function
        float y = maxMinOffset * Mathf.Sin(Time.time);
        newPipeUp.transform.position += Vector3.up * y;
        newPipeDown.transform.position += Vector3.up * y;

        Point newPoint = Instantiate(point, transform.position, Quaternion.identity);
        
        newPoint.gameObject.SetActive(true);
        newPoint.SetSize(holeSize);
        newPoint.transform.position += Vector3.up * y;
    }
}
