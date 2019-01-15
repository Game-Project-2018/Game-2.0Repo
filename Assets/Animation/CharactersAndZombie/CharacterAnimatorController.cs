using System.Collections;
using UnityEngine;

public class CharacterAnimatorController : MonoBehaviour {

    Animator m_animator;

    public bool isMoving = false;
    public bool isShooting = false;
    private bool isHit = false;
    private bool isDying = false;
    public bool isPunching = false;
    public bool lateHitAnimation = false;
    public bool lateDeathAnimation = false;

    private Weapon weapon;

    void Start () {
        m_animator = GetComponent<Animator>();
        weapon = GameObject.FindGameObjectWithTag("ActivePlayer").GetComponent<Weapon>();
    }
	
	void Update () {

        m_animator.SetBool("IsWalking",isMoving);
        m_animator.SetBool("IsShooting", isShooting);
        //m_animator.SetBool("IsStabbing", isAttackMelee);
        m_animator.SetBool("IsPunching",isPunching);
        m_animator.SetBool("IsHit",isHit);
        m_animator.SetBool("IsDying",isDying);

	    if (isHit)
	        StartCoroutine("HitAnimation",3);

	    if (isPunching)
	        StartCoroutine("PunchingAnimation", 0.5);

	    if (isShooting)
	        StartCoroutine("ShootingAnimation", 2.5);

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

    IEnumerator PunchingAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        isPunching = false;
    }

    IEnumerator ShootingAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        isShooting = false;
        weapon.setGunVisible(false);
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
