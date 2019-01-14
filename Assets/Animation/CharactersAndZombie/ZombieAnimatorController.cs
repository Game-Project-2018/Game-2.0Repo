using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimatorController : MonoBehaviour {

    Animator m_animator;

    public bool isMoving = false;
    private bool isHit = false;
    private bool isDying = false;
    public bool isBiting = false;
    public bool lateHitAnimation = false;
    public bool lateDeathAnimation = false;

    void Start () {
        m_animator = GetComponent<Animator>();
    }
	
	void Update () {

        m_animator.SetBool("IsWalking", isMoving);
        m_animator.SetBool("IsBiting", isBiting);
        m_animator.SetBool("IsHit", isHit);
        m_animator.SetBool("IsDying", isDying);

        if (isHit)
            StartCoroutine("HitAnimation", 3);

        if (isBiting)
            StartCoroutine("BitingAnimation", 3);

        if (isDying)
            StartCoroutine("DyingAnimation", 4);

	    if (lateHitAnimation)
	        StartCoroutine("LateHitAnimation", 1);

	    if (lateDeathAnimation)
	        StartCoroutine("LateDeathAnimation", 2);

	}

    IEnumerator HitAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        isHit = false;
    }

    IEnumerator BitingAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        isBiting = false;
    }

    IEnumerator DyingAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        TurnManager.RemoveUnitsFromScene();
    }

    IEnumerator LateHitAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        isHit = true;
        lateHitAnimation = false;
    }

    IEnumerator LateDeathAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        isDying = true;
        lateDeathAnimation = false;
    }
}
