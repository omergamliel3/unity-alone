using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMotor : MonoBehaviour
{

    private Vector3 velocity = new Vector3(0, 0, 2);
    private Vector3 spawn;
    private Vector3 rotation;
    private Vector3 CameraRoration;
    private Rigidbody rb;
    public static float health;
    public Camera cam;
    public LayerMask mask;

    public AudioClip[] footsteps;
    public AudioSource audioSource;

    public GameObject Unlocked;
    public GameObject notNeeded;

    public GameObject bar;
    public GameObject HP;
    public GameObject _Armor;
    public static int indecator;
    private bool _armor = false;
    private float countdown = 5f; // shield countdown

    private PlayerShoot player;
    private GameManager manager;

    // armor and hp check
    private bool armor = false;
    private bool hp = false;

    public bool move;
    private bool load;

    public bool LadderBoost;

    public float time = 0.5f;
    public bool isMoving = false;
    private PlayerController controll;

    public GameObject Chicken;
    public Transform ChickenDropPoint;
    public GameObject SpawnPoint;
    SpawnPoint spawnPoint;
    public GameObject Shop;
    private Shop shop;

    public int baits;
    public int money;

    public GameObject PauseMenu;
    public GameObject UIControlls;

    private bool set;

    public GameObject gameOver;
    public Camera deathCam;
    public Canvas canvas;

    public Text GodText;
    public float TimeGod;
    private bool IsGod;

    public GameObject Scope;
    public GameObject HighScore;
    public bool TestRange;
    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        baits = 3;
        money = 300;
        controll = GetComponent<PlayerController>();
        load = true;
        move = false;
        rb = gameObject.GetComponent<Rigidbody>();
        player = gameObject.GetComponent<PlayerShoot>();
        spawn = transform.position;
        manager = gameObject.transform.GetChild(0).GetComponent<GameManager>();
        health = 100;
        GroundCheck();
        GameManager.currentHealth = health.ToString();
        shop = Shop.GetComponent<Shop>();
        
        if (TestRange)
            return;

        spawnPoint = SpawnPoint.GetComponent<SpawnPoint>();     
       
    }



    // Update is called once per frame
    void FixedUpdate()
    {


        if (Input.GetKey(KeyCode.Escape))
        {
            if (PauseMenu.activeSelf)
                PauseMenu.GetComponent<Animator>().SetBool("End", true);
            PauseMenuToggle(!PauseMenu.activeSelf);

        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            UIControlls.SetActive(!UIControlls.activeSelf);
        }


        if (Input.GetKeyDown(KeyCode.Q) && baits != 0)
        {
           
            Instantiate(Chicken, ChickenDropPoint.position, ChickenDropPoint.rotation);
            spawnPoint.SetBaitTrue();         
            baits--;
            manager.Baits.text = "Baits: " + baits.ToString();
        }

        if (rb.velocity.magnitude > 0)
            isMoving = true;
        else
            isMoving = false;

        time -= Time.deltaTime;

        if (time <= 0 && isMoving)
        {
            GroundCheck();
            time = 0.5f;
        }

        SetHealthBar(health);

        if (transform.position.y < -0.5)
        {
            Die();
        }



        if (_armor == true)
            Armor();

        if (IsGod)
        if (TimeGod > 0)
        {
            TimeGod -= Time.deltaTime;
            GodText.text = string.Format("{0:0.0}", TimeGod) + " seconds left";
        }
        else
        {
            TimeGod = 0f;
            GodText.text = "";
            IsGod = false;
            GameManager.currentHealth = "100";
            health = 100f;
        }

        ///////////////////////////// key inputs ////////////////////////////


        ///////////////////////////// key inputs ////////////////////////////
    }

    // Set the Camera Field Of View when Aim Down Sight
    public void SetCameraFieldOfView(float FieldOfView)
    {
        cam.fieldOfView = FieldOfView;
    }

    // Gets a momvent vector
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    // Gets a rotation vector
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    // Gets a Camera rotation vector
    public void RotateCamera(Vector3 _rotationCamera)
    {
        CameraRoration = _rotationCamera;
    }

    void PerformMovment()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    void PerformRotetion()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        if (cam != null)
        {
            cam.transform.Rotate(-CameraRoration);
        }

    }


    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "HP")
        {
            if (health != 100)
                GetHP(other);

            Destroy(HP, 0f);

            hp = true;
        }

        if (indecator == 1)
            return;

        if (other.transform.tag == "Enemy")
        {
            Die();

        }

        else if (other.transform.tag == "Armor")
        {
            _armor = true;
            Destroy(_Armor, 0f);

            armor = true;
        }
        else if (other.transform.tag == "Ammo")
        {
            player.Ammo += 30;
            Destroy(other.gameObject);
        }
        else if (other.transform.tag == "InfinityAmmo")
        {
            player.CurrentAmmo = 10000000;
            player.CurrentAmmoP = 10000000;
            player.CurrentAmmoS = 10000000;
            Destroy(other.gameObject);
        }
        else if (other.transform.tag == "Shop")
        {
            shop.Toggle(true);
        }

    }
    void OnCollisionExit(Collision other)
    {
        if (other.transform.tag == "Shop")
            shop.Toggle(false);
    }



    void Die()
    {
        transform.position = spawn;
        health = 100;

  //      player.CurrentAmmo = gameObject.GetComponent<WeaponGraphics>().weaponInfo.maxAmmo;

        if (armor == true)
        {
            //Instantiate(_Armor1, armor_.transform.position, armor_.transform.rotation);
            armor = false;
        }


        if (hp == true)
        {
            //Instantiate(HP1, hp_.transform.position, hp_.transform.rotation);
            hp = false;
        }

    }

    public static void playerDamage(int damage)
    {
        health -= damage;
    }


    void GainHealth()
    {
        health++;
    }

    void SetHealthBar(float _health)
    {
        _health = _health / 100;
        // bar.transform.localScale = new Vector3(_health, bar.transform.localScale.y, bar.transform.localScale.z);
    }

    void GetHP(Collision other)
    {
        if (health >= 50)
            health = 100;
        else
            health += 50;

        GameManager.currentHealth = health.ToString();
        Destroy(other.gameObject);

    }

    void Armor()
    {                                  // shield method for countdown seconds
        if (countdown > 0)
            indecator = 1;
        else
        {
            indecator = 0;
            _armor = false;
        }


        countdown -= Time.deltaTime;

    }

    public void Open()
    {
        // need to change
        GameManager.CompleteLevel();
    }

    public Transform target;

    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "ladder")
        {
            target = gameObject.transform;

            if (Input.GetKey(KeyCode.W))
            {
                target.Translate(Vector3.up * Time.deltaTime * 5, Space.World);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                target.Translate(Vector3.down * Time.deltaTime * 5, Space.World);
            }
            if (target != null)
            {
                gameObject.GetComponent<Rigidbody>().useGravity = false;
            }
            else
            {
                gameObject.GetComponent<Rigidbody>().useGravity = true;
            }
        }


    }

    void OnTriggerEnter(Collider other)
    {
        //if (load)
        //    if (other.transform.tag == "goal")
        //    {
        //        Open();

        //        load = false;
        //    }


        //if (other.transform.tag == "Unlocked")
        //{
        //    Destroy(notNeeded, 1f);
        //    move = true;
        //}

        if (other.transform.tag == "ladder")
        {

            target = gameObject.transform;

            if (Input.GetKey(KeyCode.W))
            {
                target.Translate(Vector3.up * Time.deltaTime * 5, Space.World);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                target.Translate(Vector3.down * Time.deltaTime * 5, Space.World);
            }

        }

       
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "ladder")
        {
            if (Input.GetKey(KeyCode.W))
                target.Translate(target.forward * Time.deltaTime * 20);

            target = null;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }

    }



    void GroundCheck()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Vector3.down, out hit, 2f, mask))
        {
            print("Ground");
            if (hit.collider.tag == "Ground")
            {
                int random = Random.Range(0, footsteps.Length);
                audioSource.clip = footsteps[random];
                audioSource.PlayOneShot(footsteps[random], 1f);

            }
        }
        else
            print("not Ground");
    }

    IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public void ApplyDamage(float damage)
    {
        health -= damage;
        GameManager.currentHealth = health.ToString();
        if (health <= 0) // Game Over Screen + 'Pause Game Menu' with option to play again or return the main manu
        {          
            /*StartCoroutine(*/GameOver()/*)*/;
            //health = 100;
            //transform.position = spawn;
           // Cursor.visible = true;
           // SceneManager.LoadScene(0);

        }
    }

    // In Game Store Script
    void Store()
    {

    }

    void PauseMenuToggle(bool set)
    {
        Cursor.visible = set;
        PauseMenu.SetActive(set);

        if (set == true)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;

    }

    public void StartGodMode()
    {
        TimeGod = 40f;
        health = 100000000000000;
        GameManager.currentHealth = "Infinity";
        IsGod = true;
        // StartCoroutine(GodMode());
    }

     IEnumerator GodMode()
    {
            health = 100000000000000;
            GameManager.currentHealth = "Infinity";
            yield return new WaitForSeconds(40f);
            health = 100f;
            GameManager.currentHealth = "100";
    }

    /*IEnumerator*/void GameOver()
    {
        Cursor.visible = true;
        gameOver.SetActive(true);
        HighScore.GetComponent<HighScore>().SetHighScore();
        canvas.gameObject.SetActive(false);
        Scope.SetActive(false);
        Camera.main.enabled = false;
        deathCam.gameObject.SetActive(true);      
        deathCam.enabled = true;
       // yield return new WaitForSeconds(0.3f);
        deathCam.gameObject.GetComponent<Animator>().SetBool("Move", true);
        spawnPoint.DisableAllEnemis();
        Destroy(gameObject);

    }
}


//#pragma strict
////the person on the ladder
//private var target:Transform;



//function OnTriggerStay(other:Collider)
//{
//    //the person this touching is on the ladder
//    //if the person hits the down key this moves them up counteracting the ladder moving them down
//    target = other.transform;
//    if (Input.GetKey(KeyCode.DownArrow))
//    {
//        target.Translate(Vector3.up * Time.deltaTime * 5, Space.World);
//    }
//}



//function OnTriggerEnter(other:Collider)
//{
//    //the person this touching is on the ladder
//    //if the person hits the down key this moves them up counteracting the ladder moving them down
//    target = other.transform;
//    if (Input.GetKey(KeyCode.DownArrow))
//    {
//        target.Translate(Vector3.up * Time.deltaTime * 5, Space.World);
//    }
//}


////gets rid of the target if they are not touching the ladder
//function OnTriggerExit(other:Collider)
//{
//    target = null;
//}