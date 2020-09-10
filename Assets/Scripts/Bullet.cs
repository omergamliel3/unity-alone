using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;

    public GameObject impactEffect;
    public float speed = 70f;
    
    public void Start()
    {
       
    }

    public void Seek (Transform _target)
    {
        target = _target;
    }


    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            target.GetComponent<ZombieAI>().ApplyDamage(Random.Range(20, 35));
     		return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }


    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);

        if (PlayerMotor.indecator != 1)
        {
            Destroy(effectIns, 2f);
           // PlayerMotor.playerDamage(34);
        }
            
        Destroy(gameObject);
    }
}














//private Transform target;

//public float speed = 70f;
//public GameObject impactEffect;

//public void Seek (Transform _target)
//{
//	target = _target;
//}

//// Update is called once per frame
//void Update () {

//	if (target == null)
//	{
//		Destroy(gameObject);
//		return;
//	}

//	Vector3 dir = target.position - transform.position;
//	float distanceThisFrame = speed * Time.deltaTime;

//	if (dir.magnitude <= distanceThisFrame)
//	{
//		HitTarget();
//		return;
//	}

//	transform.Translate(dir.normalized * distanceThisFrame, Space.World);

//}

//void HitTarget ()
//{
//	GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
//	Destroy(effectIns, 2f);

//	Destroy(target.gameObject);
//	Destroy(gameObject);
//}