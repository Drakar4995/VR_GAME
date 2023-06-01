using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonController : MonoBehaviour
{
    Animator animator;
    CharacterController controller;

    public float speed = 5f;

    private Vector3 moveDirection = Vector3.zero;
    private bool shouldMove = false; // Variable para indicar si debe moverse o no

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        StartCoroutine(StartWithDelay());
    }

    void Update()
    {
        if (shouldMove)
        {
            MoveForward();
        }
    }

    IEnumerator StartWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isRunning", true);
        shouldMove = true; // Se habilita el movimiento después del retraso
    }

    void MoveForward()
    {
        moveDirection = transform.forward * speed;
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit collision)
    {
        if (collision.gameObject.CompareTag("Chicken"))
        {
            shouldMove = false;
            animator.SetBool("collisionChicken", true);
            StartCoroutine(ResetCollisionAnimation());
            Debug.Log("Colisión detectada entre Chicken y Demon. Contador: ");
        }
        if (collision.gameObject.CompareTag("Red_Demon"))
        {
            shouldMove = false;
            animator.SetBool("isRunning", false);
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            animator.SetBool("isDead", true);
            Destroy(collision.gameObject, 3f); // Destruir el prefab de la bala después de 3 segundos
            Destroy(gameObject, 3f); // Destruir el prefab del demon después de 3 segundos
        }
    }

    private IEnumerator ResetCollisionAnimation()
    {
        while (animator.GetBool("collisionChicken"))
        {
            yield return null;
        }
    }
}
