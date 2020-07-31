using System;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy object when collide with this object
        Destroy(other.gameObject);
    }
}
