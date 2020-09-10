using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour {

    public Animator animator;
    public bool ModernWeapon;
    public GameObject UI;
    private PlayerShoot shoot;
 
    bool scope;

	// Use this for initialization
	void Start () {

        shoot = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShoot>();

    }
	
	// Update is called once per frame
	void Update () {

        

        if (gameObject.transform.tag != "L96")
        if (Input.GetMouseButtonDown(1))
        {
           Scope(true);
           scope = true;

        }
        else if (Input.GetMouseButtonUp(1))
            {
                Scope(false);
                scope = false;
            }

        if (scope)
            if (Input.GetMouseButtonDown(0))
                animator.SetBool("ShootWhileAim", true);
            else if (Input.GetMouseButtonDown(0))
                animator.SetBool("ShootWhileAim", false);
        else
            {
                animator.SetBool("ShootWhileAim", false);
            }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("IsRun", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("IsRun", false);
        }

    }

    public void StopFire()
    {
        animator.SetBool("IsShoot", false);
    }

    public void Fire()
    {
        animator.SetBool("IsShoot", true);
    }


    public void Reloading(bool set)
    {
        animator.SetBool("IsReload", set);
    }

    public void Scope(bool set)
    {
        animator.SetBool("IsScoped", set);
        DisableUI(!set);
    }

    void DisableUI(bool set)
    {
        UI.SetActive(set);
    }

   public IEnumerator AnimationDelay(float time)
    {
        animator.enabled = false;
        yield return new WaitForSeconds(time);
        animator.enabled = true;
    }

    public void AimAdd(float add)
    {
        add = 0;
    }
   //IEnumerator ScopeFade(bool set)
   // {
   //     float POV = 
   //     if (set)
   //     {

   //     }
   //     else
   //     {

   //     }
   // }
}
