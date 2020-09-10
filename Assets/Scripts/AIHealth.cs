using UnityEngine;
using System.Collections;

public class AIHealth : MonoBehaviour {

    public int health = 100;
    public Transform target;
    public float moveSpeed = 3f;
    public float rotationSpeed = 3f;
    public float attackRange = 1.5f;
    public float chaseRange = 10f;
    public float giveUpRange = 20f;
    public float attackRepeatTime = 1;
    public GameObject anim;
    public float dontComeCloserRange = 3;
    public float maxDamage = 5;
    public float minDamage = 5;
    public AudioClip attack;

    private string idleAnim = "idle";
    private string walkAnim = "walk";
    private string attackAnim = "attack";
    private string attackAnim2 = "crouchLook";
    private string hitAnim = "hit";

    private bool chasing = false;
    private float attackTime;
    private bool checking = false;

    public float attackdelay = 0.8f;
    public float delayBeforeJump = 1f;
    public AudioClip[] walkSound;
    public float audioStepLength = 0.25f;
    private bool isPlaying = false;
    private bool attemptToJump = false;

    public int maximumHitPoints = 100;
    public int hitPoints = 100;

    private float gotHitTimer = -1f;
    public float detonationDelay = 0;
    public Rigidbody deadReplacement;
    public bool grounded = false;
    public float gravity = 10f;

    private Transform myTransform;

    void Awake()
    {
        myTransform = transform; //cache transform data for easy access/preformance
        gameObject.GetComponent<Rigidbody>().freezeRotation = true;
    }

    // Use this for initialization
    void Start () {

        target = GameObject.FindWithTag("Player").transform;
        anim.GetComponent<Animation>().wrapMode = WrapMode.Loop;
        anim.GetComponent<Animation>()[attackAnim].wrapMode = WrapMode.Once;
        anim.GetComponent<Animation>()[hitAnim].wrapMode = WrapMode.Once;
        anim.GetComponent<Animation>()[attackAnim].layer = 2;
        anim.GetComponent<Animation>()[hitAnim].layer = 1;
        anim.GetComponent<Animation>().Stop();
    }
	
    void FixedUpdate()
    {
        if (target)
        {
            // check distance to target (every frame)
            var distance = (target.position - myTransform.position).magnitude;

            if (distance < dontComeCloserRange)
            {
                moveSpeed = 0;
            }
            else {
                moveSpeed = Random.Range(3, 6);
            }

            if (chasing)
            {

                //rotate to look at the player
                myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

                if (distance > attackRange && !attemptToJump)
                {
                    myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
                    if (grounded)
                    {
                        anim.GetComponent<Animation>().CrossFade(walkAnim);
                        if (!isPlaying)
                        {
                            playWalkSounds();
                        }
                    }
                }

                // give up
                if (distance > giveUpRange)
                {
                    chasing = false;
                    GetComponent<AudioSource>().Stop();
                }

                // attack
                if (distance < attackRange)
                {
                    anim.GetComponent<Animation>().CrossFade(attackAnim2);
                    if (Time.time > attackTime)
                    {
                        checkInDelay();
                        attackTime = Time.time + attackRepeatTime;
                    }
                }
            }
            else {
                anim.GetComponent<Animation>().CrossFade(idleAnim);
                GetComponent<AudioSource>().Stop();
                // start chasing if target comes close enough
                if (distance < chaseRange)
                {
                    chasing = true;
                }
            }

            // Gravity 
           gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, -gravity * GetComponent< Rigidbody > ().mass, 0));
            grounded = false;
        }
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    void checkInDelay()
    {
        if (checking)
            return;

        checking = true;
        attemptToJump = true;
        WaitTime(delayBeforeJump);
       // yield WaitForSeconds(delayBeforeJump);
        GetComponent< Rigidbody > ().AddRelativeForce(0, 30000, 40000);
        anim.GetComponent<Animation > ().CrossFade(attackAnim);
        //    yield WaitForSeconds (attackdelay);
        WaitTime(attackdelay);
        attemptToJump = false;
        if ((target.position - myTransform.position).magnitude < 1.5)
        {
            //target.SendMessage( "PlayerDamage", Random.Range(minDamage, maxDamage));
            GetComponent<AudioSource>().PlayOneShot(attack, 1.0f / GetComponent<AudioSource>().volume);
            GetComponent<Rigidbody>().AddRelativeForce(0, 2000, -10000);
        }
        else {
            checking = false;
            return;
        }
        checking = false;
    }

    void playWalkSounds()
    {
        isPlaying = true;
       // GetComponent< AudioSource > ().clip = walkSound[Random.Range(0, walkSound.Length-1)];
       // GetComponent< AudioSource > ().Play();
        WaitTime(audioStepLength);
   //     yield WaitForSeconds (audioStepLength);
        isPlaying = false;
    }

    IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);
    }
    // Update is called once per frame
    void Update () {
	
        if (health <=0)
        {
            health = 0;
            Destroy(gameObject);
        }

	}
   public void TakeDamamge(int Damage)
    {
        health -= Damage;
    }
}
