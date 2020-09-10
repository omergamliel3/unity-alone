using UnityEngine;
using System.Collections;

public class Fade_Out : MonoBehaviour {

    public float FadeSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //  gameObject.GetComponent<Renderer>().material.color.a -= Time.deltaTime * FadeSpeed;
        Renderer rend = gameObject.GetComponent<Renderer>();
        rend.material.color -= new Color(0, 0, 0, FadeSpeed);
        Destroy(gameObject, 3f);

    }
}
