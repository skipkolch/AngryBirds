using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [Header("Set in Inspector")] // Заголовок для инспектора
    public GameObject prefabProjectile; // префаб шарика
    public float velocityMult = 8f;
    
    private GameObject launchPoint; // объект точки запуска
    private GameObject projectile; // объект шарика
    private Vector3 launchPos;
    private bool aimMode;
    private float maxMagnitude;
    private Rigidbody projectileRigidbody;
    private void Awake()
    {
        Transform launchPointTransform = transform.Find("LaunchPoint");
        launchPoint = launchPointTransform.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTransform.position;
        maxMagnitude = GetComponent<SphereCollider>().radius; // получить радиус сферы за которые нельзя выходить
    }

    private void OnMouseDown()
    {
        aimMode = true; // если игрок нажал на кнопку, то включается режим прилецеливания
        projectile = Instantiate(prefabProjectile); // создать шарик
        projectile.transform.position = launchPos; // поместить шарик в точку спавна
        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true; // сделать его кинематическим
    }

    public void Update()
    {
        if(!aimMode) return; // если не в состоянии прицеливании то выходим

        Vector3 mousePos2D = Input.mousePosition; // преобразование экранных координат в мировые
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        
        Vector3 mouseDelta = mousePos3D - launchPos; // Найти вектор между позиции мышки и позиции спавна
        
        if (mouseDelta.magnitude > maxMagnitude) // если длина вектора больше чем максимальное отклонение
        {
            mouseDelta.Normalize(); // нормализуем вектор (длина = 1) и умножаем его на значение радиуса
            mouseDelta *= maxMagnitude;
        }

        Vector3 projectilePos = launchPos + mouseDelta; // прибавляем вектор к стартовой позиции спавна
        projectile.transform.position = projectilePos; // присваиваем это позицию созданному шарику
        if (Input.GetMouseButtonUp(0)) // отпускание мышки
        {
            aimMode = false;
            projectileRigidbody.isKinematic = false;
            projectileRigidbody.velocity = -mouseDelta * velocityMult;
            FollowingCamera.point = projectile; // передача шарика в слежение камеры
            projectile = null;
        }
        
    }

    private void OnMouseEnter()
    {
       print("Slingshot OnMouseEnter");
       launchPoint.SetActive(true);
    }

    private void OnMouseExit()
    {
        print("Slingshot OnMouseExit");
        launchPoint.SetActive(false);
    }
}
