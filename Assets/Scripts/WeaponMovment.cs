using UnityEngine;
using System.Collections;

public class WeaponMovment : MonoBehaviour {

    public float MoveAmount = 1;
    public float MoveSpeed = 2;
    public GameObject Hand;
    public float MoveOnX;
    public float MoveOnY;

    public Vector3 DefaultPos;
    public Vector3 NewGunPos;

	// Use this for initialization
	void Start () {

        DefaultPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {

        MoveOnX = Input.GetAxis("Mouse X") * Time.deltaTime * MoveAmount;

        MoveOnY = Input.GetAxis("Mouse Y") * Time.deltaTime * MoveAmount;

        NewGunPos = new Vector3(DefaultPos.x + MoveOnX, DefaultPos.y + MoveOnY, DefaultPos.z);

        Hand.transform.localPosition = Vector3.Lerp(Hand.transform.localPosition, NewGunPos, MoveSpeed * Time.deltaTime);
    }
}

//public var MoveAmount : float = 1;
	
//	public var MoveSpeed : float = 2;
	
//	public var GUN: GameObject;
	
//	public var MoveOnX : float;
	
//	public var MoveOnY : float;
	  
//	public var DefaultPos : Vector3;
	
//	public var NewGunPos : Vector3;
	
//function Start()
//{

//    DefaultPos = transform.localPosition;

//}


//function Update()
//{

//    MoveOnX = Input.GetAxis("Mouse X") * Time.deltaTime * MoveAmount;

//    MoveOnY = Input.GetAxis("Mouse Y") * Time.deltaTime * MoveAmount;

//    NewGunPos = new Vector3(DefaultPos.x + MoveOnX, DefaultPos.y + MoveOnY, DefaultPos.z);

//    GUN.transform.localPosition = Vector3.Lerp(GUN.transform.localPosition, NewGunPos, MoveSpeed * Time.deltaTime);

//}