using UnityEngine;
using System.Collections;

public class CursorHideShow : MonoBehaviour {

    bool isLocked;

	void Start () {
        SetCursorLock(false);

    }
	
    void SetCursorLock(bool isLocked)
    {
        this.isLocked = isLocked;

        Cursor.visible = isLocked;

    }

	void FixedUpdate() {

        if (Input.GetKeyDown(KeyCode.Alpha3))
            SetCursorLock(!isLocked);

	}
}
