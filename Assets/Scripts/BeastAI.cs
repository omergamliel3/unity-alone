using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeastAI : MonoBehaviour
{


    public Transform target; // the player transform
    private UnityEngine.AI.NavMeshAgent agent;
    public float moveSpeed = 3f;
    public float rotationSpeed = 3f;
    public AudioClip sound;

    public bool isNotDead = true;
    public float health = 100f;
    public GameObject Replace;
    public float damage;
    public GameObject Anim;
    public Animation anim;
    public float attackRepeatTime;
    public float time = 0f;
    public bool bait = false;
    private float distance;
    public List<GameObject> bloodList;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.stoppingDistance = 2.5f;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = Anim.GetComponent<Animation>();
        anim.wrapMode = WrapMode.Loop;

    }

    // Update is called once per frame
    void Update()
    {

       // InvokeRepeating("Check", 0f, 1f);
       if (target != null)
        agent.SetDestination(target.position);

        if (isNotDead)
        {
            // rotate to look at the player

            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), rotationSpeed * Time.deltaTime);
            //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            if (transform != null)
             distance = Vector3.Distance(target.position, transform.position);
 

            if (distance < 2f && Time.time > time)
            {
                anim.wrapMode = WrapMode.Once;

                int random = Random.Range(0,3);

                if (anim.isPlaying)
                    return;

                if (random == 0)
                {
                    anim.CrossFade("attack");
                    target.GetComponent<PlayerMotor>().ApplyDamage(Random.Range(10,20));
                    StartCoroutine(WaitTime(1.1f + Random.Range(0f, 1f)));
                    //attackRepeatTime = 1.1f + Random.Range(0f, 1f);
                    //time = Time.time + attackRepeatTime;


                }                   
                else if (random == 1)
                {
                    anim.CrossFade("attack2");
                    target.GetComponent<PlayerMotor>().ApplyDamage(Random.Range(10, 20));
                    StartCoroutine(WaitTime(1.2f + Random.Range(0f, 1f)));
                    //attackRepeatTime = 1.2f + Random.Range(0f, 1f);
                    //time = Time.time + attackRepeatTime;
                }
                else
                {
                    anim.CrossFade("attack3");
                    target.GetComponent<PlayerMotor>().ApplyDamage(Random.Range(10, 20));
                    StartCoroutine(WaitTime(1.2f + Random.Range(0f, 1f)));
                    //attackRepeatTime = 1.2f + Random.Range(0f, 1f);
                    //time = Time.time + attackRepeatTime;
                }
                   
            }
            else
            {
                // move towards to the player
                anim.wrapMode = WrapMode.Loop;
              //  transform.position += transform.forward * moveSpeed * Time.deltaTime;
                anim.CrossFade("walk");


                //int random = Random.Range(0, 2);

                //if (random == 0)
                //    anim.CrossFade("walk1");
                //else
                //    anim.CrossFade("walk2");

            }


        }
    }

    public void ApplyDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);
        
    }

    void Check()
    {
        if (transform.position.y < -2)
        {
            Destroy(gameObject);
        }

    }

    void Die()
    {
        PlayerMotor motor = target.GetComponent<PlayerMotor>();
        motor.money += 30;
        target.transform.GetChild(0).GetComponent<GameManager>().Money.text = "Money: " + motor.money.ToString() + "$";

        GameObject[] bloods = bloodList.ToArray();

        for (int i = 0; i < bloods.Length; i++)
        {
            Destroy(bloods[i].gameObject);
        }
        Destroy(gameObject);
    }
}


//if (isNotDead)
// {
//     anim.wrapMode = WrapMode.Once;
//     anim.CrossFade("die");
// }        
//     isNotDead = false;
//   }

//  Instantiate(Replace, gameObject.transform.position, gameObject.transform.rotation);
