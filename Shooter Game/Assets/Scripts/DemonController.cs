using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonController : MonoBehaviour
{
    Animator animator;
    CharacterController controller;
    public float speed = 5f;
    //public TextScript textScript;

    private int redDemonLifes = 1;
    private int blueDemonLifes = 2;
    private int hitsBlueDemon = 0;
    private Vector3 moveDirection = Vector3.zero;
    private bool shouldMove = false; 
    private bool previousShouldMoveState = false;
    private List<GameObject> collidedBullets = new List<GameObject>();
    private bool hitted = false;  

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

        if(this.name.Contains("Fiery"))
        {
            if (collision.gameObject.CompareTag("Bullet") && !hitted)
            {
                Destroy(collision.gameObject);
                hitted = true;
                shouldMove = false;
                CancelAndPlayAnimation("Die");
                TextScript.textScript.AddScore(1);
                Destroy(gameObject, 1f);
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Bullet") && hitsBlueDemon<=2 && !collidedBullets.Contains(collision.gameObject))
            {
                collidedBullets.Add(collision.gameObject);
                Destroy(collision.gameObject);
                hitsBlueDemon++;
                if(hitsBlueDemon == 2)
                {
                    hitted = true;
                    shouldMove = false;
                    CancelAndPlayAnimation("Die");
                    TextScript.textScript.AddScore(2);
                    Destroy(gameObject, 1f);
                }

            }
        }
        /*
        if (collision.gameObject.CompareTag("Bullet"))
        {
            int scoreToAdd = 0;
            int demonLifes = 0;
            Destroy(collision.gameObject);
            if (this.name.Contains("Fiery"))
            {
                demonLifes = --redDemonLifes;
                scoreToAdd = 1;
            }
            else
            {
                demonLifes = --blueDemonLifes;
                scoreToAdd = 2;
            }

            if (demonLifes <= 0)
            {
                hitted = true;
                shouldMove = false;
                CancelAndPlayAnimation("Die");
                TextScript.textScript.AddScore(scoreToAdd);
                Destroy(gameObject, 1f);
            }

        }*/

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
