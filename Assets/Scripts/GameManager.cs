using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int correntScore;
    public static string currentHealth = "Health : ";
    public static string currentAmmo;
    public static int highScore;
    public static bool Mode;
    private static int currentLevel = 1;
    private static int NumofSc = 4;

    //public GUISkin skin;
    //public Rect HealthRect;
    //public Rect AmmoRect;
    //public Rect HealthBar;
    //public Rect FoundWeapon;
    //public Rect Timer;
    //public Rect Round;
    //public Rect Survival;
    public int _Round;
    public float startTime;
    public string alone = "Alone";
    public string CurrentTime;
    public GameObject FadeOut;
    public float FadeSpeed;
    private PlayerShoot stats;
    private int MaxAmmo;
    public GameObject player;
    public GameObject SpawnPoint;
    public int _currentAmmo;
    public int _Ammo;

    private WeaponManager _UI;
    public string UI;


    [Header("UI")]

    public Text health;
    public Text Ammo;
    public Text Baits;
    public Text Money;
    public Text UIText;
    public Text RoundNum;
    public Text RoundEndTime;

    private PlayerMotor motor;
    public GameObject _SpawnPoint;
    private SpawnPoint point;
    private PlayerShoot shoot;


    public bool TextRange;

    public GameObject SpawnRandomPoints;

    void Start()
    {
       // DontDestroyOnLoad(gameObject);
        MaxAmmo = 50;
        _UI = player.GetComponent<WeaponManager>();
        motor = player.GetComponent<PlayerMotor>();
        point = _SpawnPoint.GetComponent<SpawnPoint>();
        shoot = player.GetComponent<PlayerShoot>();

        //if (Mode)
        //    _SpawnPoint.SetActive(true);
        //else if (!Mode)
        //    SpawnRandomPoints.SetActive(true);

        //  HealthCanvas.GetComponent<GUIText>().text = "asdasd";

    }

    void Update()

    {

        // fade out effect when the game begins

        if (FadeOut != null)
        {
            Renderer rend = FadeOut.GetComponent<Renderer>();
            rend.material.color -= new Color(0, 0, 0, FadeSpeed);
        }
  
        Destroy(FadeOut, 3.5f);



       
    }
    /// <summary>
    /// יעילות !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    /// </summary>
    void FixedUpdate()
    {
        // UI variables:

        // UI text for picking out weapons 
        UIText.text = _UI.UI;

        // UI Ammo 
        if (_currentAmmo < 0)
        {
            _currentAmmo = 0;
            Ammo.text = "Ammo : " + _Ammo + " / Realoding";
        }
        else
        {
            if (shoot.infinity)
                Ammo.text = "Infinity Ammo";
            else
            Ammo.text = "Ammo : " + _currentAmmo + " / " + _Ammo;
        }
            
        
        // UI Health
        health.text = currentHealth;

        if (PlayerMotor.health >= 60)
            health.color = Color.green;
        else if (PlayerMotor.health > 40 && PlayerMotor.health < 60)
            health.color = Color.yellow;
        else
            health.color = Color.red;


        if (TextRange)
            return;


        // UI Round End Time
        if (point.RoundEnded)
        {
            startTime = point.TimeBetweenRounds;
            RoundEndTime.text = string.Format("{0:0.0}", startTime);
        }
        else
        {
            RoundEndTime.text = point.ZombiesAlive.Length + point.BeastsAlive.Length + " Enemies Left";
        }


        // UI Round Num

      //  RoundNum.text = "Round: " + _Round.ToString();

        // UI money
       // Money.text = "Money: " + motor.money.ToString() + "$";

        // UI baits
      //  Baits.text = "Baits: " + motor.baits.ToString();

            UI = _UI.UI;
        _currentAmmo = shoot.CurrentAmmo;
        _Ammo = shoot.Ammo;

   

       
        //startTime -= Time.deltaTime;
        
        //currentHealth = "Health : " + PlayerMotor.health.ToString();

    }

    public static void CompleteLevel()
    {
        currentLevel++;
       
        if (currentLevel >= NumofSc)
        {
            print("no more levels yet");
        }
        else
        {
            print(currentLevel);
            SceneManager.LoadScene(currentLevel);
            Main_Menu.LoadlastScene = currentLevel;
       
        }
            

    }


    // dont need anymore
    //void OnGUI()
    //{
    //    GUI.skin = skin;

    //    if (PlayerMotor.health >= 60 )
    //    GUI.Label(HealthRect, currentHealth, skin.GetStyle("HealthG"));

    //    else if (PlayerMotor.health > 40 && PlayerMotor.health < 60)
    //        GUI.Label(HealthRect, currentHealth, skin.GetStyle("HealthO"));

    //    else
    //        GUI.Label(HealthRect, currentHealth, skin.GetStyle("HealthR"));

    //    GUI.Label(AmmoRect, currentAmmo, skin.GetStyle("Ammo"));


    //    GUI.Label(FoundWeapon,UI, skin.GetStyle("PickUp"));

    //    if (TextRange)
    //        return;

    //    if (SpawnPoint.GetComponent<SpawnPoint>().RoundEnded)
    //        GUI.Label(Timer, CurrentTime, skin.GetStyle("Timer"));
    //    else
    //        GUI.Label(Timer, "", skin.GetStyle("Timer"));

    //    GUI.Label(Round, "Round: " + _Round.ToString(), skin.GetStyle("Start"));

    //    //if (startTime > 196.5f)
    //    //GUI.Label(Survival, "Survival", skin.GetStyle("Start"));



    //}


}
