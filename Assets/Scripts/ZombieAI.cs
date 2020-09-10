using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombieAI : MonoBehaviour
{

    public Transform target; // the player transform
    private UnityEngine.AI.NavMeshAgent agent;
    public float moveSpeed = 3f;
    public float rotationSpeed = 3f;
    public AudioClip sound;
    private bool Bool = true;
    public bool isNotDead = true;
    public float health = 100f;
    public int damage = 15;
    public Animation anim;
    public string attackAnim;
    public string deathAnim;
    public string walkAnim;
    public float walkspeed;
    public float deathspeed;
    public float attackRepeatTime;

    private float time;
    [SerializeField]
    private bool stuck = false;
    public float speed;
    public bool bait = false;

    public List<GameObject> bloodList;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.stoppingDistance = 2.5f;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = gameObject.GetComponent<Animation>();
        anim.wrapMode = WrapMode.Loop;
        anim[walkAnim].speed = walkspeed;
        anim[deathAnim].speed = deathspeed;

       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Bool)
        { 
            return;
        }
          

        if (isNotDead)
        {
            // rotate to look at the player

            if (target == null)
                target = GameObject.FindGameObjectWithTag("Player").transform;



            if (target != null)
                agent.SetDestination(target.position);
         
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), rotationSpeed * Time.deltaTime);
            //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            float distance = Vector3.Distance(target.position, transform.position);


            if (distance < 2.5f && Time.time > attackRepeatTime)
            {

                    anim.wrapMode = WrapMode.Once;

                if (anim.IsPlaying(walkAnim))
                {
                    anim.Stop(walkAnim);
                }


                if (anim.IsPlaying(attackAnim))
                    return;

             //   attackRepeatTime = 1.5f + Time.time;
                anim.CrossFade(attackAnim);

                if (target.tag == "Player")
                    target.GetComponent<PlayerMotor>().ApplyDamage(Random.Range(damage, damage + 5));
                else if (target.tag == "bait")
                    target.GetComponent<chickenScript>().ApplyDamage();

                // time = Time.time + attackRepeatTime;

                //  StartCoroutine(WaitTime(1.1f + Random.Range(0f, 1f)));



            }
            else
            {
                if (bait)
                {
                    // checking the shortest disnance to the closest bait

                    GameObject[] baits = GameObject.FindGameObjectsWithTag("bait");

                    if (baits == null)
                    {
                        bait = false;
                        target = GameObject.FindGameObjectWithTag("Player").transform;
                    }
                    else
                        bait = true;

                    float shortestDistance = Mathf.Infinity;
                    GameObject nearestBait = null;

                    foreach (GameObject _bait in baits)
                    {
                        float distanceToBait = Vector3.Distance(transform.position, _bait.transform.position);

                        if (distanceToBait < shortestDistance)
                        {
                            shortestDistance = distanceToBait;
                            nearestBait = _bait;
                        }
                    }

                    if (nearestBait != null)
                    {
                        target = nearestBait.transform;
                    }
                    else
                    {
                        target = null;
                        target = GameObject.FindGameObjectWithTag("Player").transform;
                    }
                }

                 // move towards to the player
                anim.wrapMode = WrapMode.Loop;
              //  transform.position += transform.forward * moveSpeed * Time.deltaTime;
                anim.CrossFade(walkAnim);
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

    void OnCollisionEnter(Collision other)

    {
        if (other.transform.tag == "Beast")
        {
            Physics.IgnoreCollision(gameObject.GetComponent<CapsuleCollider>(), other.gameObject.GetComponent<CapsuleCollider>());
        }
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
        if (isNotDead)
        {
            GameObject[] bloods = bloodList.ToArray();

            for (int i = 0; i < bloods.Length; i++)
            {
                Destroy(bloods[i].gameObject);
            }
            //Physics.IgnoreCollision(gameObject.GetComponent<CapsuleCollider>(), target.GetComponent<CapsuleCollider>());
            //Physics.IgnoreCollision(transform.GetChild(0).GetComponent<BoxCollider>(), target.GetComponent<CapsuleCollider>());
            Bool = false;

            if (target.tag == "bait")
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>().money += 30;
            }
            else
            {
                PlayerMotor motor = target.GetComponent<PlayerMotor>();
                motor.money += 30;
                target.transform.GetChild(0).GetComponent<GameManager>().Money.text = "Money: " + motor.money.ToString() + "$";
            }
              //  target.GetComponent<PlayerMotor>().money += 30;

            target = null;
            agent.speed = 0;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            anim.Stop();
            anim.wrapMode = WrapMode.Once;
            anim.Play(deathAnim);

     
            //GameObject[] bloods = GameObject.FindGameObjectsWithTag("blood");

            //foreach (GameObject blood in bloods)
            //{
            //    if (blood.transform.IsChildOf(transform))               
            //        Destroy(blood);
                
            //}
            Destroy(gameObject, 1f);
        }
        isNotDead = false;
    }
}
