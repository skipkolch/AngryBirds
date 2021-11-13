using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public static GameObject point; // объект за которым идем слежение
    
    [Header("Set in Dinamically")] 
    public float cameraZ;
    public float relief = 0.05f; // коэфиц для интерполяции
    public Vector2 minXY = Vector2.zero;
    
    void Start()
    {
        cameraZ = this.transform.position.z;
    }

    private void FixedUpdate()
    {
        // if(point == null) return; // если шарика нет то ничего не делаем
        // Vector3 destination = point.transform.position; // берем позицию шарика

        Vector3 destination;

        if (point == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            destination = point.transform.position;
            if (point.CompareTag("Projectile") 
                && point.GetComponent<Rigidbody>().IsSleeping())
            {
                point = null;
                return;
            }
        }

        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        
        destination = Vector3.Lerp(transform.position, destination, relief);
        destination.z = cameraZ;
        transform.position = destination;
        Camera.main.orthographicSize = destination.y + 10;
    }

    void Update()
    {
        
    }
}
