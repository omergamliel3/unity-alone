using UnityEngine;
using System.Collections;

public class chickenScript : MonoBehaviour {

    // Use this for initialization
    public float time = 60f;
    public int health = 1000;
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        time -= Time.deltaTime;

        if (time <= 0 || health <= 0)
            Destroy(gameObject);
	}

    public void ApplyDamage()
    {
        health -= 30;
    }
}
