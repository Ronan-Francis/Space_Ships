using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistOnLoad : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
