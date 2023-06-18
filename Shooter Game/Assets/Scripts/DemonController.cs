using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonController : MonoBehaviour
{
    Animator animator;
    CharacterController controller;
    public float speed = 5f;
    //public TextScript textScript;

    private Vector3 moveDirection = Vector3.zero;
    private bool shouldMove = false; 
    private bool previousShouldMoveState = false; 

    private bool isDestroyed = false;  

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
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("isRunning", true);
        shouldMove = true; 
        previousShouldMoveState = shouldMove;
    }

    void MoveForward()
    {
        transform.rotation = Quaternion.Euler(0f, -90f, 0f); 
        moveDirection = transform.forward * speed;
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit collision)
    {
       
        if (collision.gameObject.CompareTag("Bullet"))
        {
            shouldMove = false;
            CancelAndPlayAnimation("Die");
            TextScript.textScript.AddScore(1);
            Destroy(gameObject, 1f); 

        }

        if (collision.gameObject.CompareTag("Chicken"))
        {
            shouldMove = false;
            animator.SetBool("collisionChicken", true);
            Destroy(collision.gameObject, 3f);
            AudioChicken.audioChicken.PlayAudio();
            StartCoroutine(ResetCollisionAnimation());
        }

        if (collision.gameObject.CompareTag("Red_Demon"))
        {
            shouldMove = false;
            animator.SetBool("isRunning", false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        shouldMove = true;
        animator.SetBool("isRunning", true);
    }


    private IEnumerator ResetCollisionAnimation()
    {
        while (animator != null && animator.GetBool("collisionChicken")) 
        {
            yield return null;
        }

        if (animator == null)
        {
  
            yield break;
        }

        animator.SetBool("collisionChicken", false);
    }

    void CancelAndPlayAnimation(string animationName)
    {
        if (animator == null)
        {
            
            return;
        }

        animator.StopPlayback(); 
        animator.Play(animationName); 
    }

    
}
