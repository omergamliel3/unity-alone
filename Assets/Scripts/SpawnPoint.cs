using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

    [Header("Round")]

    public float timeBetweenSpawn = 0.1f;
    public float TimeBetweenRounds;
    public bool RoundEnded = false;
    public int Round;

    public int RandomOfEnemies;

    // adds a number to random spawn of enemeies
    public int random;

    public bool GameWon;

    [Header("AI")]

    public GameObject Zombie;
    public GameObject Beast;
 
    public Transform[] Area;

    public float ZombieHealth;
    public float BeastHealth;

    public GameObject[] ZombiesAlive;
    public GameObject[] BeastsAlive;

  
    public bool isBait;
    private GameObject AI;
    public GameObject _GameManager;

    [Header("Wave")]
    // Waves
    [SerializeField]
    private Wave[] waves;
    private int WaveIndex = -1;


    
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void FixedUpdate() {

        if (GameWon)
            return;

        // when the round ended
        if (RoundEnded)
        if (Input.GetKeyDown(KeyCode.E) && TimeBetweenRounds > 5f)
        {
            TimeBetweenRounds = 5f;
        }


        ZombiesAlive = GameObject.FindGameObjectsWithTag("zombie");
        BeastsAlive = GameObject.FindGameObjectsWithTag("Beast");

        // when the round didnt over
        if (!RoundEnded)
        {
           

            if (ZombiesAlive.Length == 0 && BeastsAlive.Length == 0)
            {
                RoundEnded = true;

                for (int i = 0; i < enemieSign.Length; i++)
                {
                    if (enemieSign[i] == true)
                    {
                        GameObject EnemieSign = transform.GetChild(i).GetChild(2).gameObject;
                        EnemieSign.SetActive(false);
                    }
                }
                _GameManager.transform.GetChild(0).GetComponent<GameManager>()._Round = Round;

            }
            else
            {
                RoundEnded = false;
            }              

        }
        else
        {

            if (!(ZombiesAlive.Length == 0 && BeastsAlive.Length == 0))
            {
                RoundEnded = false;
            }

            TimeBetweenRounds -= Time.deltaTime;

            if (TimeBetweenRounds <= 0)
            {
                RandomSpawn();
                ZombieHealth += 10;
                BeastHealth += 10;
                RoundEnded = false;
                Round++;
                //_GameManager.transform.GetChild(0).GetComponent<GameManager>()._Round = Round;
                TimeBetweenRounds = 30;
            }

        }

    }

        // game rounds function
    void RandomSpawn()
    {

        RandomOfEnemies += 1;
        WaveIndex+=2;
        _GameManager.transform.GetChild(0).GetComponent<GameManager>().RoundNum.text = "Round: " + WaveIndex;
        WaveIndex--;

        if (WaveIndex > waves.Length -1)
        {
            print("You won the game!");
            GameWon = true;
            _GameManager.transform.GetChild(0).GetComponent<GameManager>().RoundNum.text = "You Won!";
            _GameManager.transform.GetChild(0).GetComponent<GameManager>().RoundEndTime.text = "";
            return;
        }

        SpawnAI(Random.Range(0, 4));
    }


    bool[] enemieSign = new bool[5];


    // spawn random of enemies, according to the given area, and set enemies signs.
    void SpawnAI(int Area)
    {

        GameObject EnemieSign = transform.GetChild(Area).GetChild(2).gameObject;
        EnemieSign.SetActive(true);
        enemieSign[Area] = true;

        // spawn zombies according to the wave array
        if (waves[WaveIndex].NumOFZombies != 0)
        for (int i = 0; i < Random.Range(waves[WaveIndex].NumOFZombies, waves[WaveIndex].NumOFZombies + random); i++)
        {
                GameObject AI = (GameObject)Instantiate(Zombie, transform.GetChild(Area).GetChild(0).position + new Vector3(Random.Range(1f,10f), 0f, 0f), transform.GetChild(Area).GetChild(0).rotation);
            // update health and damage
                AI.GetComponent<ZombieAI>().health = waves[WaveIndex].ZombieHealth;
                AI.GetComponent<ZombieAI>().damage = waves[WaveIndex].ZombieDamage;

            if (isBait)
                    AI.GetComponent<ZombieAI>().bait = true;
        }

        // spawn beasts according to the wave array
        if (waves[WaveIndex].NumOFBeast == 0)
            return;

        for (int j = 0; j < Random.Range(waves[WaveIndex].NumOFBeast, waves[WaveIndex].NumOFBeast + random); j++)
            {
                GameObject AI = (GameObject)Instantiate(Beast, transform.GetChild(Area).GetChild(1).position+ new Vector3(Random.Range(1f, 10f), 0f, 0f), transform.GetChild(Area).GetChild(1).rotation);
                // update health and damage
                AI.GetComponent<BeastAI>().health = waves[WaveIndex].BeastHealth;
                AI.GetComponent<BeastAI>().damage = waves[WaveIndex].BeastDamage;

            if (isBait)
                    AI.GetComponent<BeastAI>().bait = true;
            }        
    }


    // set all the alive enemies to be aware of the bait
    public void SetBaitTrue()
    {
        isBait = true;

        if (ZombiesAlive.Length == 0 && BeastsAlive.Length == 0)
            return;

        if (ZombiesAlive.Length != 0)
        for (int i = 0; i < ZombiesAlive.Length; i++)
        {
            ZombiesAlive[i].GetComponent<ZombieAI>().bait = true;
        }

        if (BeastsAlive.Length != 0)
        for (int i = 0; i < BeastsAlive.Length; i++)
        {
            BeastsAlive[i].GetComponent<BeastAI>().bait = true;
        }
    }

    public void DisableAllEnemis()
    {
        for (int i = 0; i < ZombiesAlive.Length; i++)
        {
            ZombiesAlive[i].GetComponent<ZombieAI>().enabled = false;
        }

        if (BeastsAlive.Length != 0)
            for (int i = 0; i < BeastsAlive.Length; i++)
            {
                BeastsAlive[i].GetComponent<BeastAI>().enabled = false;
            }
    }
}
