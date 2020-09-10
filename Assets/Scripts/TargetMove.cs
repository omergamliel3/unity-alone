using UnityEngine;
using System.Collections;

public class TargetMove : MonoBehaviour {


    public Transform movePoint;
    private Vector3 startPoint;
    public float speedMove;

	// Use this for initialization
	void Start () {

        startPoint = transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.position == startPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speedMove * Time.deltaTime);
        }
        else if (transform.position == movePoint.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPoint, speedMove * Time.deltaTime);
        }
	
	}
}
//transform.position = Vector3.MoveTowards(transform.position, patrolPoints[correctPoint].position, moveSpeed* Time.deltaTime);