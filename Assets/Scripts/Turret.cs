using UnityEngine;


public class Turret : MonoBehaviour {

    public  Transform target;
    public GameObject Player;

    [Header("Attributes")]

    public float range = 14f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public string playerTag = "Player";

    [Header("TurretTurn")]

    public Transform partToRotate;
    public float turnSpeed = 10f;

    [Header("BulletStats")]

    public GameObject bulletPrefab;
    public Transform firePoint;


    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);

        if (distanceToPlayer <= range)
        {
            target = Player.transform;
        }
        else
            target = null;
    }
     

   void Update()
    {
        
        if (target == null)
        {
            return;
        }

        // Lock on target

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f,rotation.y,0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}


//private Transform target;

//[Header("Attributes")]

//public float range = 15f;
//public float fireRate = 1f;
//private float fireCountdown = 0f;

//[Header("Unity Setup Fields")]

//public string enemyTag = "Enemy";

//public Transform partToRotate;
//public float turnSpeed = 10f;

//public GameObject bulletPrefab;
//public Transform firePoint;

//// Use this for initialization
//void Start () {
//	InvokeRepeating("UpdateTarget", 0f, 0.5f);
//}

//void UpdateTarget ()
//{
//	GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
//	float shortestDistance = Mathf.Infinity;
//	GameObject nearestEnemy = null;
//	foreach (GameObject enemy in enemies)
//	{
//		float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
//		if (distanceToEnemy < shortestDistance)
//		{
//			shortestDistance = distanceToEnemy;
//			nearestEnemy = enemy;
//		}
//	}

//	if (nearestEnemy != null && shortestDistance <= range)
//	{
//		target = nearestEnemy.transform;
//	} else
//	{
//		target = null;
//	}

//}

//// Update is called once per frame
//void Update () {
//	if (target == null)
//		return;

//	//Target lock on
//	Vector3 dir = target.position - transform.position;
//	Quaternion lookRotation = Quaternion.LookRotation(dir);
//	Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
//	partToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f);

//	if (fireCountdown <= 0f)
//	{
//		Shoot();
//		fireCountdown = 1f / fireRate;
//	}

//	fireCountdown -= Time.deltaTime;

//}

//void Shoot ()
//{
//	GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
//	Bullet bullet = bulletGO.GetComponent<Bullet>();

//	if (bullet != null)
//		bullet.Seek(target);
//}

//void OnDrawGizmosSelected ()
//{
//	Gizmos.color = Color.red;
//	Gizmos.DrawWireSphere(transform.position, range);
//}