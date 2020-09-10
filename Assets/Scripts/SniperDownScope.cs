using UnityEngine;
using System.Collections;

public class SniperDownScope : MonoBehaviour {

    private PlayerShoot shoot;
    public GameObject player;
   
    public GameObject Scope;
    public GameObject UI;
    private Camera cam;
    public GameObject WeaponCamera;
    public Animator OverLayScope;
    bool check = false;
    // Use this for initialization
    void Start () {

        cam = Camera.main;
        shoot = player.GetComponent<PlayerShoot>();
        //shoot.Add = 0.2f;
    }
	
	// Update is called once per frame
	void FixedUpdate() {

        //if (check)
        //    if (Input.GetButtonDown("Fire1"))
        //     {
        //        OverLayScope.SetBool("IsShoot", true);
        //     } 
        //else
        //    {
        //        OverLayScope.SetBool("IsShoot", false);
        //    }


        if (transform.GetChild(0).gameObject.activeSelf == false)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            shoot.Add = 0;
            StartCoroutine(OnScope());
        }
        else if (Input.GetMouseButtonUp(1))
        {
            shoot.Add = 0.2f;
            cam.fieldOfView = 57f;
            Scope.SetActive(false);
            WeaponCamera.SetActive(true);
            transform.GetChild(0).GetComponent<WeaponAnimation>().Scope(false);
        }

        //if (Input.GetMouseButton(1))
        //{
        //    StartCoroutine(OnScope());        
        //}

        else if (!Input.GetMouseButton(1))
        {
            cam.fieldOfView = 57f;
            UI.SetActive(false);
            Scope.SetActive(false);
            WeaponCamera.SetActive(true);
            transform.GetChild(0).GetComponent<WeaponAnimation>().Scope(false);

        }

    }

    public IEnumerator OnScope()
    {
        transform.GetChild(0).GetComponent<WeaponAnimation>().Scope(true);
        yield return new WaitForSeconds(0.4f);
        cam.fieldOfView = 15f;
        Scope.SetActive(true);
        WeaponCamera.SetActive(false);



    }

   

}
