using System.Collections;
using UnityEngine;

public class AnimatorController : MonoBehaviour {

    Animator m_animator;

    public bool isMoving = false;
    public bool isAttackMelee = false;
    public bool isAttackRange = false;
    public bool isHit = false;
    public bool isDying = false;



    void Start () {
        m_animator = GetComponent<Animator>();
    }
	
	void Update () {

        m_animator.SetBool("IsWalking",isMoving);
        m_animator.SetBool("IsShooting", isAttackRange);
        m_animator.SetBool("IsStabbing", isAttackMelee);
        m_animator.SetBool("IsHit",isHit);
        m_animator.SetBool("IsDying",isDying);

	    if (isHit)
	    {
	        StartCoroutine("HitAnimation",3);
	    }

	    if (isAttackMelee)
	    {
	        StartCoroutine("StabbingAnimation", 3);
        }

	    if (isAttackRange)
	    {
	        StartCoroutine("ShootingAnimation", 3);
        }

	    if (isDying)
	    {
	        StartCoroutine("DyingAnimation", 4);
	    }
	}

    IEnumerator HitAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        isHit = false;
    }

    IEnumerator StabbingAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        isAttackMelee = false;
    }

    IEnumerator ShootingAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        isAttackRange = false;
    }

    IEnumerator DyingAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        TurnManager.RemoveUnitsFromScene();
    }
}
