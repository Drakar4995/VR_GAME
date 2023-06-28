using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonController : MonoBehaviour
{
    Animator animator;
    CharacterController controller;
    public float speed = 5f;

    private float elapedTime = 0f;
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
        animator.SetBool("isRunning", true);
        shouldMove = true;
        previousShouldMoveState = shouldMove;
    }

    void Update()
    {
        if (shouldMove)  
        {
            MoveForward();
            
        }
        else
        {
            elapedTime += Time.deltaTime;
            if (elapedTime >= 1.5f)
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator StartWithDelay()
    {
        yield return new WaitForSeconds(0f);
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
                    shouldMove = false;
                    CancelAndPlayAnimation("Die");
                    TextScript.textScript.AddScore(2);
                    Destroy(gameObject, 1f);
                }

            }
        }

        if (collision.gameObject.CompareTag("Chicken"))
        {
            shouldMove = false;
            animator.SetBool("collisionChicken", true);
            SpawnDemons.spawnDemons.UpdateSpawnPosition(collision.gameObject.name);
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
