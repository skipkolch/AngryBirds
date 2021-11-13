using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public static bool targetGet = false;// флажок столкновения

    private void OnTriggerEnter(Collider foreing)
    {
        if (foreing.gameObject.CompareTag("Projectile"))
        {
            targetGet = true;
            Material mat = GetComponent<Renderer>().material;
            Color color = mat.color;
            color.g = 255;
            mat.color = color;
        }
    }
}
