using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float destructionDelay = 1f; // Retraso en segundos antes de destruir el objeto

    private void OnCollisionEnter(Collision collision)
    {
        Invoke("DestroyObject", destructionDelay); // Llamar a la función DestroyObject después del retraso especificado
    }

    private void DestroyObject()
    {
        Destroy(gameObject); // Destruir el objeto que tiene este script
    }
}
