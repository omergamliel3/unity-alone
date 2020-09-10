using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {

    public GameObject _Weapons;
    public GameObject _Spacial;
    private PlayerShoot playershoot;
    private PlayerMotor motor;
    public GameObject player;
    public GameObject shopbuttons;
    public GameObject UI;
    public GameObject CroosAir;
    private GameManager gameManager;


    // Use this for initialization
    void Start () {


        playershoot = player.GetComponent<PlayerShoot>();
        motor = player.GetComponent<PlayerMotor>();
        gameManager = player.transform.GetChild(0).GetComponent<GameManager>();



    }
	
	// Update is called once per frame
	void Update () {

      


    }

    public void Weapons()
    {
        _Weapons.SetActive(true);
        shopbuttons.SetActive(false);
    }

    public void Ammo()
    {
        if (!AbleToBuy(200))
            return;

        playershoot.Ammo += 123;
        
        motor.money -= 200;
        gameManager.Money.text = "Money: " + motor.money.ToString() + "$";
    }

    public void Health()
    {
        if (!AbleToBuy(200) || PlayerMotor.health == 100)
            return;

        PlayerMotor.health = 100;
        GameManager.currentHealth = "100";
        motor.money -= 200;
        gameManager.Money.text = "Money: " + motor.money.ToString() + "$";
    }

    public void Baits()
    {
        if (!AbleToBuy(50))
            return;

        
        motor.baits++;
        gameManager.Baits.text = "Baits: " + motor.baits.ToString();
        motor.money -= 50;
        gameManager.Money.text = "Money: " + motor.money.ToString() + "$";
    }

    public void Spacial()
    {
        _Spacial.SetActive(true);
        shopbuttons.SetActive(false);
    }

    public void Quit()
    {     
        Toggle(false);
    }

    public void Back(GameObject backfrom)
    {
        backfrom.SetActive(false);
        shopbuttons.SetActive(true);
    }

    public void Toggle(bool set)
    {
        UI.SetActive(set);
        Cursor.visible = set;

        if (player.GetComponent<PlayerShoot>().currentWeapon.name == "L96")
            return;

        CroosAir.SetActive(!set);

        //if (!set)
        //    Time.timeScale = 1f;
        //else
        //    Time.timeScale = 0f;
    }

    public void BuyItems(string item)
    {
      if (item.Equals("Turret"))
        {
            //// gives turret function

            if (!AbleToBuy(300))
                return;
            TurretActive();
            motor.money -= 300;
            gameManager.Money.text = "Money: " + motor.money.ToString() + "$";
        }
        else if (item.Equals("Ammo"))
        {
            // gives endless ammo for 60 seconds function 

            if (!AbleToBuy(300))
                return;

            playershoot.StartAmmoInfinity();
            motor.money -= 300;
            gameManager.Money.text = "Money: " + motor.money.ToString() + "$";
        }
      else
        {
            // gives the ability to be a god for 60 seconds

            if (!AbleToBuy(500))
                return;

            motor.StartGodMode();

            motor.money -= 500;
            gameManager.Money.text = "Money: " + motor.money.ToString() + "$";
        }


    }

     bool AbleToBuy(int cost)
    {
        if (motor.money - cost >= 0)
            return true;
        else
            return false;
    }

    void TurretActive()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");

        for (int i = 0; i < turrets.Length; i++)
        {
            turrets[i].gameObject.GetComponent<Turret1>().StartTurretActive();
        }
    }


}
