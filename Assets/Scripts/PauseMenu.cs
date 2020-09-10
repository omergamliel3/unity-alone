using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject option;
    
	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Resume()
    {
       GetComponent<Animator>().SetBool("End", true);
    }

    void resume()
    {
       Time.timeScale = 1f;
        gameObject.SetActive(false);
        Cursor.visible = false;

    }

    public void Rerty()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Options()
    {
        if (option == null)
            return;

        option.SetActive(true);
    }

    public void Menu()
    {       
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        // quit the game
        Application.Quit();
    }
}
