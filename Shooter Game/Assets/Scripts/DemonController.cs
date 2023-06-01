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
    private bool previousShouldMoveState = false; // Estado anterior de shouldMove

    private bool isDestroyed = false; // Variable para indicar si el demonio ha sido destruido

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        StartCoroutine(StartWithDelay());
    }

    void Update()
    {
        if (shouldMove && !isDestroyed) // Verificar si el demonio debe moverse y no ha sido destruido
        {
            MoveForward();
        }
    }

    IEnumerator StartWithDelay()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("isRunning", true);
        shouldMove = true; // Se habilita el movimiento después del retraso
        previousShouldMoveState = shouldMove;
    }

    void MoveForward()
    {
        transform.rotation = Quaternion.Euler(0f, -90f, 0f); // Aplicar rotación de -90 grados en el eje y
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
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            shouldMove = false;
            CancelAndPlayAnimation("Die");
            Destroy(gameObject, 3f); // Destruir el prefab del demon después de 3 segundos
            isDestroyed = true; // Marcar el demonio como destruido
        }

        if (collision.gameObject.CompareTag("Red_Demon"))
        {
            shouldMove = false;
            animator.SetBool("isRunning", false);
        }
    }

    private IEnumerator ResetCollisionAnimation()
    {
        while (animator != null && animator.GetBool("collisionChicken")) // Verificar si el animator sigue existiendo y la animación se está reproduciendo
        {
            yield return null;
        }

        if (animator == null)
        {
            // El demonio ha sido destruido, detener la rutina de animación de colisión
            yield break;
        }

        animator.SetBool("collisionChicken", false);
    }

    void CancelAndPlayAnimation(string animationName)
    {
        if (animator == null)
        {
            // El demonio ha sido destruido, no es necesario reproducir la animación
            return;
        }

        animator.StopPlayback(); // Detener la reproducción de la animación actual
        animator.Play(animationName); // Reproducir la nueva animación directamente
    }
}
