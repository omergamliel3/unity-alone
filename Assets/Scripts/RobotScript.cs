using UnityEngine;

public class RobotScript : MonoBehaviour
{

    public float moveSpeed;
    private float maxSpeed = 40f;
   // public Object Player;
    public static float health;
    private Vector3 input;
    private Vector3 spawn;
    public GameObject deathParticals;
    public GameObject Unlocked;
    public GameObject notNeeded;
    public GameObject healthBar;
    public GameObject HP;
    public GameObject _Armor;
    public static int indecator;
    private bool _armor = false;
    private float countdown = 5f; // shield countdown




    // Use this for initialization
    void Start()
    {
        health = 100;
        spawn = transform.position;

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        // health bar Setup
        // health bar = 100f
        SetHealthBar(health);

        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        if (GetComponent<Rigidbody>().velocity.magnitude < maxSpeed)
        {
            GetComponent<Rigidbody>().AddForce(input * moveSpeed);
        }
        
        if (transform.position.y < -1)
        {
            Die();
        }

        if (health <= 0)
        {
            health = 100;
            transform.position = spawn;

        }
        //else if (health < 100)
        //{

        //}
        if (_armor == true)
        Armor();


    }
    
    void OnCollisionEnter(Collision other)
    {
         if (other.transform.tag == "HP")
        {
            if (health != 100)
            GetHP();

            Destroy(HP, 0f);
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
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "goal")
        {
           // GameManager.CompleteLevel();         
        }


        if (other.transform.tag == "Unlocked")
        {
            Destroy(Unlocked, 1f);
            Destroy(notNeeded, 1f);
        }

    }


    void Die()
    {
        //Instantiate(deathParticals, transform.position, Quaternion.Euler(270, 0, 0));
        transform.position = spawn;
        health = 100;


        //Destroy(HP, 0f);
        //Destroy(_Armor, 0f);

        //Instantiate(HP, HP.transform.position, HP.transform.rotation);
        //Instantiate(_Armor, _Armor.transform.position, _Armor.transform.rotation);


        

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
        healthBar.transform.localScale = new Vector3(_health, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

    void GetHP()
    {  
         if (health >= 50)
            health = 100;
        else
            health += 50;
       
    }

    void Armor()
    {                                      // shield method for countdown seconds
            if (countdown > 0)
                indecator = 1;
            else
        {
            indecator = 0;
            _armor = false;
        }
                

            countdown -= Time.deltaTime;

    }
}


