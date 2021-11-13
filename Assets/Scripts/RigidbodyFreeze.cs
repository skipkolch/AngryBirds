using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyFreeze : MonoBehaviour
{
    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.Sleep();
        }
    }
}
