using UnityEngine;

public class SlowMotion : MonoBehaviour {

    public float slowdownFactor = 0.05f;
    public float slowdownLength = 2f;

	
	void Update () {

        if (Input.GetButtonDown("Fire1"))
            DoSlowMotion();

        Time.timeScale += (1f / slowdownFactor) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
	}

    public void DoSlowMotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
}
