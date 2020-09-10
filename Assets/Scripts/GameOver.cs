using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour {

    public Text text;
    public AudioClip clip;
    public AudioSource source;

    void Start()
    {
        source.clip = clip;
        source.Play();
    }

    // not working, need to do animations instead
    public IEnumerator FadeText()
    {
        float a = 0f;

        while (a < 1f && text.color.a < 164)
        {
            a += Time.deltaTime * 1f;
            text.color = new Color(text.color.r, text.color.g, text.color.b, a);
            yield return 0;
        }

        yield return 0;
    }
    public void Rerty()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       // gameObject.SetActive(false);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        // quit the game
        Application.Quit();
    }
}
