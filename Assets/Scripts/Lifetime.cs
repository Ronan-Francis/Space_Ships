using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    public float lifetime = 2.0f; // Lifetime of this GameObject in seconds

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy gameObject after 'lifetime' seconds
    }
}
