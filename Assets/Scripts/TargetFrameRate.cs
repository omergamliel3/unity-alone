using UnityEngine;
using System.Collections;

public class TargetFrameRate : MonoBehaviour {

    public int target = 60;

	// Use this for initialization
	void Start () {

        QualitySettings.vSyncCount = 0;
   
	}
	
	// Update is called once per frame
	void Update () {

        if (target != Application.targetFrameRate)
            Application.targetFrameRate = target;
	}
}
