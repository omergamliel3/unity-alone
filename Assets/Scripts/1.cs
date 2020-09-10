using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

    void Start()
    {
        PlayerDamage(95);

        if (weaponType == WeaponType.Sword)
        {
            print("i'm using a Sword");
        }

        StartCoroutine(Go(5f, "Omer", "Gamliel"));
    }

   IEnumerator Go (float wait, string name1, string name2)
    {
        print(Time.time + " " + name1);
        yield return new WaitForSeconds(wait);
        print(Time.time + " " + name2);
    }

    public int health;
    public enum WeaponType
    {
        Sword,
        Mace,
        Shield,
        BowAndArrow,
        Dagger
    }

    public WeaponType weaponType;

    void Update() // פונקציה זו מתבצעת כל פריים במשחק
    {
        switch (weaponType)
        {
            case WeaponType.Sword:
                print("Sword");
                break;

            case WeaponType.Mace:
                print("Mace");
                break;

            case WeaponType.Shield:
                print("Shield");
                break;

            case WeaponType.BowAndArrow:
                print("BowAndArrow");
                break;

            case WeaponType.Dagger:
                print("Dagger");
                break;

            default:
                print("lololololo");
                break;
        }

        if (health <= 0)
        {
            health = 0;
            Destroy(gameObject, 5f);
            Debug.Log("the player had died");
        }
    }

    public void PlayerDamage(int damage)
    {
        health -= damage;
    }

}

