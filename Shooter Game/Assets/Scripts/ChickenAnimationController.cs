using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimationController : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(StartAnimation());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator StartAnimation()
    {
        while (true)
        {
            float randomDelay = Random.Range(1f, 5f);
            yield return new WaitForSeconds(randomDelay);

            animator.SetBool("isEating", true);

            randomDelay = Random.Range(2.5f, 5f);

            yield return new WaitForSeconds(randomDelay);
            animator.SetBool("isEating", false);
            randomDelay = Random.Range(3f, 5f);

            yield return new WaitForSeconds(randomDelay);
            animator.SetBool("turnHead", true);
        }
    }
}
