using UnityEngine;
using System.Collections;

public class Animations : MonoBehaviour {

    public Animator anim;
    public GameObject scope;
    bool flag = false;
	// Use this for initialization
	void Start () {
        anim.enabled = false;
    }
	
	// Update is called once per frame
	void FixedUpdate() {

      
      
    if (scope.activeSelf)
        {
            if (Input.GetButton("Fire1") && !flag)
            {
                flag = true;
                anim.enabled = true;
                anim.SetBool("Shoot", true);
                StartCoroutine(SetAnim());
            }
        }
	}

    IEnumerator SetAnim()
    {
        yield return new WaitForSeconds(0.8f);
        anim.SetBool("Shoot", false);
        anim.enabled = false;
        flag = false;
    }
}
