using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Camera playerCamera; 
    public float bulletSpeed = 10f; 
    public float shootDelay = 1f; 

    private float lastShootTime; 

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0) 
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (Time.time >= lastShootTime + shootDelay) 
                {
                    Shoot(); 
                    lastShootTime = Time.time; 
                }
            }
        }
        else if (Input.GetMouseButtonDown(0)) 
        {
            if (Time.time >= lastShootTime + shootDelay) 
            {
                Shoot(); 
                lastShootTime = Time.time; 
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, playerCamera.transform.position, Quaternion.identity); 
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = playerCamera.transform.TransformDirection(Vector3.forward) * bulletSpeed; 
        }

        
        bullet.transform.parent = transform;

    }
}
