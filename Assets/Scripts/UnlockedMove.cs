using UnityEngine;
using System.Collections;


// this class used for moving mothods

public class UnlockedMove : MonoBehaviour {

    public float moveSpeed = 1f;
    public Transform movePoint;
    public GameObject player;
// 25 lock
// 12 point
    void FixedUpdate()
    {
        if (transform.position == movePoint.transform.position)
            return;

        if (player.GetComponent<PlayerMotor>().move == true)
        transform.position = Vector3.MoveTowards(transform.position, movePoint.transform.position, moveSpeed * Time.deltaTime);
    }

}
