using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float destructionDelay = 1f; 

    private void OnCollisionEnter(Collision collision)
    {
     //   Destroy(this.gameObject);
    }
}
