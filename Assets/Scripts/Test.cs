using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

    private UnityEngine.AI.NavMeshAgent agent;
    public Transform target;

	// Use this for initialization
	void Start () {

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

    }
	
	// Update is called once per frame
	void Update () {

        agent.SetDestination(target.position);
	}
}
