using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float destructionDelay = 1f; 

    private void OnCollisionEnter(Collision collision)
    {
        Invoke("DestroyObject", destructionDelay); 
    }

    private void DestroyObject()
    {
        Destroy(gameObject); 
    }
}
