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
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        StartCoroutine(StartWithDelay());
    }

    void Update()
    {
        Debug.Log(shouldMove);
        Debug.Log(previousShouldMoveState);
        if (shouldMove)
        {
            MoveForward();
        }
        else if (shouldMove != previousShouldMoveState)
        {
            // El estado de shouldMove ha cambiado, restaurar la animación y el estado anterior
            animator.SetBool("isRunning", previousShouldMoveState);
            previousShouldMoveState = shouldMove;
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
            //animator.SetBool("isDead", true);
            shouldMove = false;
            CancelAndPlayAnimation("Die");
            Destroy(gameObject, 3f); // Destruir el prefab del demon después de 3 segundos
        }
        if (collision.gameObject.CompareTag("Red_Demon"))
        {
            shouldMove = false;
            animator.SetBool("isRunning", false);
        }
    }

    private IEnumerator ResetCollisionAnimation()
    {
        while (animator.GetBool("collisionChicken"))
        {
            yield return null;
        }
    }
    void CancelAndPlayAnimation(string animationName)
    {
        animator.StopPlayback(); // Detener la reproducción de la animación actual
        animator.Play(animationName); // Reproducir la nueva animación directamente
    }
}
