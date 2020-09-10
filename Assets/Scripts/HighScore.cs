using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

    private SpawnPoint _SpawnPoint;
    public GameObject spawnpoint;
    public Text Hightscore;


    void Start () {

        _SpawnPoint = spawnpoint.GetComponent<SpawnPoint>();
        Hightscore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
	
    public void SetHighScore()
    {
        int number = _SpawnPoint.Round;
        if (number > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", number);
            Hightscore.text = "High Score: " + number.ToString();
        }
    }


}
