using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour {

   
    public Camera cam;
    public LayerMask mask;
    public GameObject[] enemies;
    private Enemy enemy;
    public GameObject WeaponGFX;
    public string fireRate;
 

    public int CurrentAmmoP;
    public int CurrentAmmoS;
    public int CurrentAmmo;

    public int Ammo;
    private bool reload = false;
    private bool WhichWeapon;
    private int ReduceAmmo;

    private bool ShotWaitTime = false;

    private PlayerWeapon SecondWeapon;
    private WeaponManager weaponManager;


    public PlayerWeapon currentWeapon;
    public WeaponAnimation weaponAnimation;

    public float Add;
    public int impactForce;
    public GameObject BulletHole;

    public Vector3 FirePosForward;
    //public bool Primary = false;
    //public bool Secondery = false;
    public GameObject Shop;
    public GameObject pause;

    public float InfinityTime;
    public bool infinity = false;
    public Text infinityText;

    public GameObject blood;

    // Use this for initialization
    void Start () {

        weaponManager = GetComponent<WeaponManager>();
        infinityText.text = "";
        //Primary = true;

    }


    // Update is called once per frame
    void FixedUpdate()
    {

        //if (CurrentAmmo > 31)
        //{
        //    CurrentAmmo = 31;
        //}


            // יעילות!!!!!!!!!!!!!!!!!!!!
            //currentWeapon = weaponManager.GetCurrentWeapon();
            //weaponAnimation = weaponManager.GetCurrentGraphics().GetComponent<WeaponAnimation>();


        if (reload)
            return;

        if (currentWeapon.fireRate == 0f)
        {
   

            if (Input.GetButtonDown("Fire1"))
            {
                if (currentWeapon.name == "L96")
                    if (ShotWaitTime)
                        return;

                StartCoroutine(WaitTime());
                Shoot();
            }


        }
        else
        {
      
            if (Input.GetMouseButtonDown(0) && reload == false)
            {
         
                InvokeRepeating("Shoot", 0f, 1f / currentWeapon.fireRate);
            }
            if (Input.GetMouseButtonUp(0))
            {

                CancelInvoke("Shoot");
                weaponAnimation.StopFire();
            }
        }

        if (CurrentAmmo != currentWeapon.maxAmmo)
        if (Input.GetKeyDown("r") && Ammo > 0 && !reload)
            {
                CancelInvoke("Shoot");
                Reload();
                
            }


        if (InfinityTime > 0)
        {
            InfinityTime -= Time.deltaTime;
            infinityText.text = string.Format("{0:0.0}", InfinityTime) + " seconds left";
        }
        else
        {
            InfinityTime = 0f;
            infinity = false;
        }
    }

    void Shoot()
    {
        if (Shop.activeSelf || pause.activeSelf)
            return;
        
        if (!infinity)
       CurrentAmmo--;

        if (CurrentAmmo <= 0)
        {
            CurrentAmmo = 0;


            // empty magazine sound
            weaponManager.GetCurrentGraphics().PlaySound(1);

           // weapon reload animation
            if (Ammo > 0)
                Reload();

            return;
        }
           
        // muzzleflash effect, called every time we shoot
        weaponManager.GetCurrentGraphics().muzzleFlash.Play();

        // shoot sound
        weaponManager.GetCurrentGraphics().PlaySound(0);

        // weapon shoot animation
        weaponAnimation.Fire();


        Vector3 FirePos = cam.transform.position;
         FirePosForward = cam.transform.forward + (Random.Range(-Add , Add) * Vector3.Cross(cam.transform.up, cam.transform.forward)) + (Random.Range(-Add, Add) * cam.transform.up);


        RaycastHit hit;

        // checking what collider we shoot
        if (Physics.Raycast(FirePos, FirePosForward, out hit, currentWeapon.range, mask))
        {
            // particales affect, whatever we hit
            GameObject _hitEffect = (GameObject)Instantiate(weaponManager.GetCurrentGraphics().hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(_hitEffect, 1f);

            GameObject _BulletHole = (GameObject)Instantiate(BulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            _BulletHole.transform.SetParent(hit.transform);
            Destroy(_BulletHole, 5f);

            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(-hit.normal * impactForce);


            // we hit enemy?
            if (hit.collider.tag == "Enemy")
            {
                // check the enemy we hit
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (hit.transform.name == ("Enemy" + i))
                    {
                        enemy = enemies[i].GetComponent<Enemy>();
                    }

                }

                // shoot method
                ShootPlayer(currentWeapon.damage);
            }
            else if (hit.collider.tag == "EnemyAI")
            {
                hit.transform.gameObject.GetComponent<AIHealth>().TakeDamamge(currentWeapon.damage);
                Destroy(_BulletHole);
            }
            else if (hit.collider.tag == "Explosive")
            {
                hit.transform.GetComponent<explosive>().Damage(10f);
            }
            else if (hit.collider.tag == "Box")
            {
                hit.transform.GetComponent<crate>().Damage(10f);
            }
            else if (hit.collider.tag == "zombie")
            {
                Destroy(_BulletHole);
                hit.transform.gameObject.GetComponent<ZombieAI>().ApplyDamage(currentWeapon.damage);
                GameObject _blood = (GameObject)Instantiate(blood, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                hit.transform.gameObject.GetComponent<ZombieAI>().bloodList.Add(_blood);
                _blood.transform.SetParent(hit.transform);

                Destroy(_blood, 3f);
            }
            else if (hit.collider.tag == "zombieHead")
            {
                Destroy(_BulletHole);
                hit.transform.gameObject.GetComponent<ZombieAI>().ApplyDamage(currentWeapon.damage * 3); // headShot 
                GameObject _blood = (GameObject)Instantiate(blood, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                hit.transform.gameObject.GetComponent<ZombieAI>().bloodList.Add(_blood);
                _blood.transform.SetParent(hit.transform);
                Destroy(_blood, 3f);
            }
            else if (hit.collider.tag == "Beast")
            {
                Destroy(_BulletHole);
                hit.transform.gameObject.GetComponent<BeastAI>().ApplyDamage(currentWeapon.damage);
                hit.transform.gameObject.GetComponent<BeastAI>().anim.CrossFade("hit");
                GameObject _blood = (GameObject)Instantiate(blood, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                hit.transform.gameObject.GetComponent<BeastAI>().bloodList.Add(_blood);
                _blood.transform.SetParent(hit.transform);
                Destroy(_blood, 3f);
            }
            else if (hit.collider.tag == "BeastHead")
            {
                Destroy(_BulletHole);
                hit.transform.gameObject.GetComponent<BeastAI>().ApplyDamage(currentWeapon.damage * 3); // headShot 
                hit.transform.gameObject.GetComponent<BeastAI>().anim.CrossFade("hit");
                GameObject _blood = (GameObject)Instantiate(blood, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                hit.transform.gameObject.GetComponent<BeastAI>().bloodList.Add(_blood);
                _blood.transform.SetParent(hit.transform);
                Destroy(_blood, 3f);
            }


        }
    }

    // damaging the enemy
    void ShootPlayer(int damage)
    {
        
        enemy.TakeDamage(damage);

    }
    

    // dont need //
    void Reload()
    {
        CancelInvoke("Shoot");
        weaponAnimation.StopFire();
        reload = true;
        
        weaponAnimation.Reloading(true);
        StartCoroutine(_Reload());
        

    }


    IEnumerator _Reload()
    {
        if (Ammo == 0 || infinity)
            yield break;

        yield return new WaitForSeconds(currentWeapon.ReloadTime);
        weaponAnimation.Reloading(false);

        if (Ammo < currentWeapon.maxAmmo)
        {
            if (CurrentAmmo + Ammo > 31)
            {
                Ammo -= (31 - CurrentAmmo);
                CurrentAmmo = currentWeapon.maxAmmo;

            }
            else
            {
                CurrentAmmo += Ammo;
                Ammo = 0;
            }
          
        }     
        else
        {
            Ammo -= (currentWeapon.maxAmmo - CurrentAmmo);
            CurrentAmmo = currentWeapon.maxAmmo;
        }


        //CurrentAmmo = currentWeapon.maxAmmo;
   
        reload = false;
    }

    void AmmoStart()
    {
        CurrentAmmo = currentWeapon.maxAmmo; 
    }

    public void StartAmmoInfinity()
    {
        InfinityTime = 40f;
        infinity = true;
        //StartCoroutine(InfinityAmmo());
    }

  

    IEnumerator InfinityAmmo()
    {
        infinity = true;
        yield return new WaitForSeconds(40f);
        infinity = false;
    }

    IEnumerator WaitTime()
    {
        ShotWaitTime = true;
        yield return new WaitForSeconds(0.7f);
        weaponAnimation.StopFire();
        ShotWaitTime = false;
    }
}
