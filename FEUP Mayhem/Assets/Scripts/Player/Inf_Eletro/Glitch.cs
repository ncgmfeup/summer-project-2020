using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Glitch : MonoBehaviour
{
    private Animator animator;

    private float minTime = 5f, maxTime = 10f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(AnimatorGlitch());
    }

    private IEnumerator AnimatorGlitch()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));

            animator.SetTrigger("glitch");
        }
    }

}
