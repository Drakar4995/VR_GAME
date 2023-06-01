using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab de la bala
    public Camera playerCamera; // Referencia a la cámara del jugador
    public float bulletSpeed = 10f; // Velocidad de la bala
    public float shootDelay = 1f; // Retraso entre disparos

    private float lastShootTime; // Tiempo del último disparo

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0) // Detectar el evento de disparo en pantalla táctil
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (Time.time >= lastShootTime + shootDelay) // Verificar si ha pasado suficiente tiempo desde el último disparo
                {
                    Shoot(); // Llamar a la función de disparo
                    lastShootTime = Time.time; // Actualizar el tiempo del último disparo
                }
            }
        }
        else if (Input.GetMouseButtonDown(0)) // Detectar el evento de disparo con clic izquierdo
        {
            if (Time.time >= lastShootTime + shootDelay) // Verificar si ha pasado suficiente tiempo desde el último disparo
            {
                Shoot(); // Llamar a la función de disparo
                lastShootTime = Time.time; // Actualizar el tiempo del último disparo
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, playerCamera.transform.position, Quaternion.identity); // Instanciar la bala en la posición de la cámara con rotación predeterminada
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>(); // Obtener el componente Rigidbody de la bala

        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = playerCamera.transform.TransformDirection(Vector3.forward) * bulletSpeed; // Aplicar velocidad a la bala en la dirección hacia la cual apunta la cámara
        }

        // Hacer que la bala sea un hijo del objeto que contiene el script
        bullet.transform.parent = transform;

        Destroy(bullet, 2f); // Destruir la bala después de 2 segundos
    }
}
