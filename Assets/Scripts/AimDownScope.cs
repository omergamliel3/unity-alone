using UnityEngine;
using System.Collections;

// need to fix!!!!!!!!!!

public class AimDownScope : MonoBehaviour {

    public Vector3 AimDownSight;
    private Vector3 HipFire;
    public GameObject CrossHair;
    private PlayerShoot shoot;
    public GameObject player;

    [SerializeField]
    private float speed = 12f;
    //public PlayerMotor cam;
   // public GameObject Player;
    public Camera cam;

    private GameObject Sniper;

    public bool ModernWeapons;

    float x1;
    float y1;
    float z1;

 //   void Start()
 //   {
 //       HipFire = transform.localPosition;
 //       shoot = player.GetComponent<PlayerShoot>();
 //       shoot.Add = 0.05f;
 //       Sniper = GameObject.FindGameObjectWithTag("L96-Holder");
 //       Sniper = Sniper.transform.GetChild(0).gameObject;
 //   }
	//// Update is called once per frame
	//void FixedUpdate () {

 //       if (Sniper.activeSelf)
 //           return;

 //       if (Input.GetMouseButtonDown(1))
 //       {
 //           shoot.Add = 0.005f;
 //       }

 //       else if (Input.GetMouseButtonUp(1))
 //       {
 //           shoot.Add = 0.05f;
 //       }


 //       if (Input.GetMouseButton(1))
 //       {

 //           CrossHair.transform.GetChild(0).gameObject.SetActive(false);
 //           cam.fieldOfView = 40f;
 //           transform.localPosition = Vector3.Slerp(transform.localPosition, AimDownSight, speed * Time.fixedDeltaTime);
 //       }

 //       else if (!Input.GetMouseButton(1))
 //       {
 //           CrossHair.transform.GetChild(0).gameObject.SetActive(true);
 //           cam.fieldOfView = 57f;
 //           transform.localPosition = Vector3.Slerp(transform.localPosition, HipFire, speed * Time.fixedDeltaTime);
 //       }
 //   }
}


