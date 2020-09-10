using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour {

    public static int LoadlastScene;

    int SurvivalScene = 1;
    int TestRangeScene = 5;
    int TutorialScene;
    public bool turotial;

    public void Play(bool Mode)
    { 
        GameManager.Mode = Mode;
        // load the first level 
        SceneManager.LoadScene(SurvivalScene);
    }

   public void Quit()
    {
        // quit the game
        Application.Quit();

    }

    public void Survival()
    {
        SceneManager.LoadScene(SurvivalScene);

    }
    public void Tutorial()
    {
        if (turotial)
        SceneManager.LoadScene(TutorialScene);

    }

    public void NewGame()
    {

    }

    
    public void Options()
    {

    }
}




 //   public GUISkin skin;

	//void OnGUI()
 //   {
 //       GUI.skin = skin;

 //       GUI.Label(new Rect(900, 100, 400, 45), "My Game");

 //       if (GUI.Button(new Rect(900, 150, 100, 45), "Play"))
 //       {
 //           //GameManager.startTime = 50f;
 //           SceneManager.LoadScene(1);
          

 //       }

 //       if (GUI.Button(new Rect(900, 205, 100, 45), "Quit"))
 //       {
 //           Application.Quit();
 //       }
