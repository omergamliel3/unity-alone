using System.Collections;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
    [Header("Weapons info")]


    [SerializeField]
    private PlayerWeapon PrimaryWeapon;
    [SerializeField]
    private PlayerWeapon SecondaryWeapon;
    [SerializeField]
    private PlayerWeapon FlameWeapon;

    // A GameObject for the primary weapon that the player holds
    [SerializeField]
    private GameObject PWeaponHolder;

    // A GameObject for the secondary weapon that the player holds
    [SerializeField]
    private GameObject SWeaponHolder;

    [SerializeField]
    private LayerMask mask;
    private float Range = 3f;

    // weapon info about the current weapon the player holds (component)
    private PlayerWeapon CurrentWeapon;

    // weapon info abount the current weapon graphics and sounds (component)
    private WeaponGraphics currentGraphics;

    // bools which are using to know which weapon the player holds
    public bool Primary = false;
    public bool Secondery = false;
    public bool flameThrower = false;

     [Header("Weapons")]

    // weapon models for Instantiate
    public GameObject ACR;
    public GameObject M4;
    public GameObject M9;
    public GameObject G36;
    public GameObject G18;
    public GameObject Deagle;
    public GameObject L96;
    public GameObject CrossHair;

    private WeaponAnimation updateAnimator;
    public Rect FoundWeapon;
    public GUISkin skin;

    private PlayerShoot _PlayerShoot;
    public GameObject _UI;
    public GameObject FlameThrowerHolder;

// temporery variable for the current weapon
GameObject weapon;

    // pickup UI
    public string UI;

    // Use this for initialization
    void Start () {

        // in the start of the game the player hold this weapon
        CrossHair.SetActive(false);
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        _PlayerShoot = gameObject.GetComponent<PlayerShoot>();
        StartCoroutine(WaitTime(1.5f));
        updateAnimator = GetComponent<WeaponAnimation>();


    }
	
	// Update is called once per frame
	void FixedUpdate () {

        // checking Inputs for weapon switching

        if ((Input.GetKeyDown(KeyCode.Alpha1) && Primary == false) || (Input.mouseScrollDelta.y > 0f && Primary == false))
        {
            _PlayerShoot.CurrentAmmoS = _PlayerShoot.CurrentAmmo;
            SwitchToPrimary(PWeaponHolder);

        }
        //else if (Input.GetKeyDown(KeyCode.Alpha2) && Secondery == false || (Input.mouseScrollDelta.y < 0f && Secondery == false))
        //{
        //    _PlayerShoot.CurrentAmmoP = _PlayerShoot.CurrentAmmo;
        //    SwitchToFlameThrower();
        //  //  SwitchToSecondary(SWeaponHolder);

        //}



    }

    void Update()
    {
        CheckWeapon();

    }

    public PlayerWeapon GetCurrentWeapon()
    {
        return CurrentWeapon;
    }

    public WeaponGraphics GetCurrentGraphics()
    {
        return currentGraphics;
    }


  

    // SwitchToPrimary method
    void SwitchToPrimary(GameObject _Primary)
    {
        // Set Active (false) the current weapon 
         if (weapon!=null)
        {
            weapon.SetActive(false);
        }

        _PlayerShoot.CancelInvoke("Shoot");
        weapon = _Primary.transform.GetChild(0).gameObject;
        weapon.SetActive(true);
        if (weapon.tag == "L96")
            CrossHair.SetActive(false);
        else
            CrossHair.SetActive(true);

        CurrentWeapon = PrimaryWeapon;
        _PlayerShoot.CurrentAmmo = _PlayerShoot.CurrentAmmoP;
        Primary = true;
        Secondery = false;

        currentGraphics = weapon.GetComponent<WeaponGraphics>();
        _PlayerShoot.currentWeapon = CurrentWeapon;
        _PlayerShoot.weaponAnimation = currentGraphics.GetComponent<WeaponAnimation>();



    }

     // switch to flamethrower method
    void SwitchToFlameThrower()
    {
        if (weapon != null)
        {
            weapon.SetActive(false);
        }

        _PlayerShoot.CancelInvoke("Shoot");
        CrossHair.SetActive(false);
        FlameThrowerHolder.SetActive(true);
        Primary = false;
        flameThrower = true;
        CurrentWeapon = FlameWeapon;
        weapon = FlameThrowerHolder.transform.GetChild(0).gameObject;

        //currentGraphics = weapon.GetComponent<WeaponGraphics>();
        //_PlayerShoot.currentWeapon = CurrentWeapon;
        //_PlayerShoot.weaponAnimation = currentGraphics.GetComponent<WeaponAnimation>();
    }

    // SwitchToSecondary method
    void SwitchToSecondary(GameObject _Secondary)
    {
   
        if (weapon != null)
        {
            weapon.SetActive(false);
        }
        _PlayerShoot.CancelInvoke("Shoot");
        weapon = _Secondary.transform.GetChild(0).gameObject;
        weapon.SetActive(true);
        CrossHair.SetActive(true);

        CurrentWeapon = SecondaryWeapon;
        _PlayerShoot.CurrentAmmo = _PlayerShoot.CurrentAmmoS;
        Secondery = true;
        Primary = false;

        currentGraphics = weapon.GetComponent<WeaponGraphics>();
        _PlayerShoot.currentWeapon = CurrentWeapon;
        _PlayerShoot.weaponAnimation = currentGraphics.GetComponent<WeaponAnimation>();

    }

    // need to complete

    void PickUpPrimary(GameObject _weapon)
    {
   
        if (weapon != null)
        {
            weapon.SetActive(false);
        }


        weapon = _weapon.transform.GetChild(0).gameObject;
        weapon.SetActive(true);

        if (weapon.tag == "L96")
            CrossHair.SetActive(false);
        else
            CrossHair.SetActive(true);

        PrimaryWeapon = weapon.GetComponent<WeaponGraphics>().weaponInfo;
        CurrentWeapon = PrimaryWeapon;
       
        Primary = true;
        Secondery = false;
       
        currentGraphics = weapon.GetComponent<WeaponGraphics>();
        _PlayerShoot.currentWeapon = CurrentWeapon;
        _PlayerShoot.weaponAnimation = currentGraphics.GetComponent<WeaponAnimation>();

        _PlayerShoot.CurrentAmmo = currentGraphics.AmmoStored;

       // updateAnimator.animator = _PlayerShoot.weaponAnimation.GetComponent<Animator>();
        //////////////////////////////////////////////////
    }

    void PickUpSecondary(GameObject _weapon)
    {
        if (weapon != null)
        {
            weapon.SetActive(false);
        }

        weapon = _weapon.transform.GetChild(0).gameObject;
        weapon.SetActive(true);
        CrossHair.SetActive(true);


        SecondaryWeapon = weapon.GetComponent<WeaponGraphics>().weaponInfo;
        CurrentWeapon = SecondaryWeapon;     
        Secondery = true;
        Primary = false;
        currentGraphics = weapon.GetComponent<WeaponGraphics>();
        _PlayerShoot.currentWeapon = CurrentWeapon;
        _PlayerShoot.weaponAnimation = currentGraphics.GetComponent<WeaponAnimation>();
        _PlayerShoot.CurrentAmmo = currentGraphics.AmmoStored;
    }

    void CheckWeapon()
    {

   // checking weapons on the ground

        RaycastHit hit;

        // checking if we hit something
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Range, mask))
        {
            // checking the collider tag we hit
            
            //if (hit.collider.tag == "M4")
            //{
            //    UI = "Press 'E' to pick up " + hit.transform.name;

            //    if (Input.GetKeyDown(KeyCode.E))
            //    {
            //        // if the player hold a secondary weapon, switching weapon to primary before the pickup
            //        if (Secondery)
            //        {
            //            SwitchToPrimary(PWeaponHolder);
            //        }

            //        // weapon pickup
                    
            //       GameObject weaponIns = (GameObject)Instantiate(currentGraphics.weaponModel, hit.point + Vector3.up * 0.5f + Vector3.Cross(Camera.main.transform.up, Camera.main.transform.forward) * -0.5f, Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y + 90f, 90f));
            //        weaponIns.transform.name = weapon.tag;             
            //        Destroy(hit.transform.gameObject);
            //        currentGraphics.AmmoStored = gameObject.GetComponent<PlayerShoot>().CurrentAmmo;
            //        GameObject _M4 = GameObject.FindGameObjectWithTag("M4-Holder");
            //        _M4.transform.GetChild(0).GetComponent<WeaponAnimation>().ModernWeapon = false;
            //        PWeaponHolder = _M4;
            //        PickUpPrimary(_M4);
            //    }
            //}
            //else if (hit.collider.tag == "M9")
            //{
            //    UI = "Press 'E' to pick up " + hit.transform.name;

            //    // if the player hold a primary weapon, switching weapon to secondary before pickup
            //    if (Input.GetKeyDown(KeyCode.E))
            //    {
            //        if (Primary)
            //        {
            //            SwitchToSecondary(SWeaponHolder);
            //        }

                    
            //        GameObject weaponIns = (GameObject)Instantiate(currentGraphics.weaponModel, hit.point + Vector3.up * 0.5f + Vector3.Cross(Camera.main.transform.up, Camera.main.transform.forward) * -0.5f, Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y + 90f, 90f));
            //        UI = "Press 'E' to pick up " + hit.transform.name;
            //        weaponIns.transform.name = weapon.tag;
            //        Destroy(hit.transform.gameObject);
            //        currentGraphics.AmmoStored = gameObject.GetComponent<PlayerShoot>().CurrentAmmo;
            //        GameObject _M9 = GameObject.FindGameObjectWithTag("M9-Holder");
            //        _M9.transform.GetChild(0).GetComponent<WeaponAnimation>().ModernWeapon = false;
            //        SWeaponHolder = _M9;
            //        PickUpSecondary(_M9);
            //    }
            //}
            //else if (hit.collider.tag == "G36")
            //{
            //    UI = "Press 'E' to pick up " + hit.transform.name;


            //    if (Input.GetKeyDown(KeyCode.E))
            //    {
            //        // if the player hold a secondary weapon, switching weapon to primary before the pickup
            //        if (Secondery)
            //        {
            //            SwitchToPrimary(PWeaponHolder);
            //        }

                   
            //        GameObject weaponIns = (GameObject)Instantiate(currentGraphics.weaponModel, hit.point + Vector3.up * 0.5f + Vector3.Cross(Camera.main.transform.up, Camera.main.transform.forward) * -0.5f, Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y + 90f, 90f));
            //        UI = "Press 'E' to pick up " + hit.transform.name;
            //        weaponIns.transform.name = weapon.tag;
            //        Destroy(hit.transform.gameObject);
            //        currentGraphics.AmmoStored = gameObject.GetComponent<PlayerShoot>().CurrentAmmo;
            //        GameObject _G36 = GameObject.FindGameObjectWithTag("G36-Holder");
            //        _G36.transform.GetChild(0).GetComponent<WeaponAnimation>().ModernWeapon = false;
            //        PWeaponHolder = _G36;
            //        PickUpPrimary(_G36);
            //    }
            //}
            //else if (hit.collider.tag == "G18")
            //{
            //    UI = "Press 'E' to pick up " + hit.transform.name;

            //    if (Input.GetKeyDown(KeyCode.E))
            //    {
                    
            //    // if the player hold a primary weapon, switching weapon to secondary before pickup
            //        if (Primary)
            //        {
            //            SwitchToSecondary(SWeaponHolder);
            //        }

                
            //        GameObject weaponIns = (GameObject)Instantiate(currentGraphics.weaponModel, hit.point + Vector3.up * 0.5f + Vector3.Cross(Camera.main.transform.up, Camera.main.transform.forward) * -0.5f, Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y + 90f, 90f));
            //        UI = "Press 'E' to pick up " + hit.transform.name;
            //        weaponIns.transform.name = weapon.tag;
            //        Destroy(hit.transform.gameObject);
            //        currentGraphics.AmmoStored = _PlayerShoot.CurrentAmmo;
            //        GameObject _G18 = GameObject.FindGameObjectWithTag("G18-Holder");
            //        _G18.transform.GetChild(0).GetComponent<WeaponAnimation>().ModernWeapon = false;
            //        SWeaponHolder = _G18;
            //        PickUpSecondary(_G18);
            //    }
            //}
            //else if (hit.collider.tag == "ACR")
            //{
            //    UI = "Press 'E' to pick up " + hit.transform.name;

            //    if (Input.GetKeyDown(KeyCode.E))
            //    {
            //        // if the player hold a secondary weapon, switching weapon to primary before the pickup
            //        if (Secondery)
            //        {
            //            SwitchToPrimary(PWeaponHolder);
            //        }

                    
            //        GameObject weaponIns = (GameObject)Instantiate(currentGraphics.weaponModel, hit.point + Vector3.up * 0.5f + Vector3.Cross(Camera.main.transform.up, Camera.main.transform.forward) * -0.5f, Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y + 90f, 90f));
            //        UI = "Press 'E' to pick up " + hit.transform.name;
            //        weaponIns.transform.name = weapon.tag;
            //        Destroy(hit.transform.gameObject);
            //        currentGraphics.AmmoStored = _PlayerShoot.CurrentAmmo;
            //        GameObject _ACR = GameObject.FindGameObjectWithTag("ACR-Holder");
            //        _ACR.transform.GetChild(0).GetComponent<WeaponAnimation>().ModernWeapon = false;
            //        PWeaponHolder = _ACR;
            //        PickUpPrimary(_ACR);
            //    }
            //}
            /*else*/ if (hit.collider.tag == "L96MODEL")
            {
                UI = "Press 'E' to pick up " + hit.transform.name;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    // if the player hold a secondary weapon, switching weapon to primary before the pickup
                    if (Secondery)
                    {
                        SwitchToPrimary(PWeaponHolder);
                    }

                    
                    GameObject weaponIns = (GameObject)Instantiate(currentGraphics.weaponModel, hit.point + Vector3.up * 0.5f + Vector3.Cross(Camera.main.transform.up, Camera.main.transform.forward) * -0.5f, Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y + 90f, 90f));
                    UI = "Press 'E' to pick up " + hit.transform.name;
                    weaponIns.transform.name = weapon.tag;
                    Destroy(hit.transform.gameObject);
                    currentGraphics.AmmoStored = _PlayerShoot.CurrentAmmo;
                    GameObject _L96 = GameObject.FindGameObjectWithTag("L96-Holder");
                    _L96.transform.GetChild(0).GetComponent<WeaponAnimation>().ModernWeapon = true;
                    _UI.SetActive(false);
                    CrossHair.SetActive(false);
                    PWeaponHolder = _L96;
                    PickUpPrimary(_L96);
                }
            }
            else if (hit.collider.tag == "AK-47Model")
            {
                UI = "Press 'E' to pick up " + hit.transform.name;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    // if the player hold a secondary weapon, switching weapon to primary before the pickup
                    if (Secondery)
                    {
                        SwitchToPrimary(PWeaponHolder);
                    }


                    GameObject weaponIns = (GameObject)Instantiate(currentGraphics.weaponModel, hit.point + Vector3.up * 0.5f + Vector3.Cross(Camera.main.transform.up, Camera.main.transform.forward) * -0.5f, Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y + 90f, 90f));
                    UI = "Press 'E' to pick up " + hit.transform.name;
                    weaponIns.transform.name = weapon.tag;
                    Destroy(hit.transform.gameObject);
                    currentGraphics.AmmoStored = _PlayerShoot.CurrentAmmo;
                    GameObject _AK47 = GameObject.FindGameObjectWithTag("AK-47-Holder");
                    _AK47.transform.GetChild(0).GetComponent<WeaponAnimation>().ModernWeapon = true;
                    PWeaponHolder = _AK47;
                    PickUpPrimary(_AK47);
                }
            }
            else if (hit.collider.tag == "M16Model")
            {
                UI = "Press 'E' to pick up " + hit.transform.name;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    // if the player hold a secondary weapon, switching weapon to primary before the pickup
                    if (Secondery)
                    {
                        SwitchToPrimary(PWeaponHolder);
                    }


                    GameObject weaponIns = (GameObject)Instantiate(currentGraphics.weaponModel, hit.point + Vector3.up * 0.5f + Vector3.Cross(Camera.main.transform.up, Camera.main.transform.forward) * -0.5f, Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y + 90f, 90f));
                    UI = "Press 'E' to pick up " + hit.transform.name;
                    weaponIns.transform.name = weapon.tag;
                    Destroy(hit.transform.gameObject);
                    currentGraphics.AmmoStored = _PlayerShoot.CurrentAmmo;
                    GameObject _M16 = GameObject.FindGameObjectWithTag("M16-Holder");
                    _M16.transform.GetChild(0).GetComponent<WeaponAnimation>().ModernWeapon = true;
                    PWeaponHolder = _M16;
                    PickUpPrimary(_M16);
                }
            }
            else
                UI = "";

        }
        else
            UI = "";


    }


    // method for the start affect at the beginning of the game
    IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);
        GameObject _AK47 = GameObject.FindGameObjectWithTag("AK-47-Holder");
        PWeaponHolder = _AK47;

        SwitchToPrimary(PWeaponHolder);

        _PlayerShoot.CurrentAmmoP = PrimaryWeapon.maxAmmo;
        _PlayerShoot.CurrentAmmoS = SecondaryWeapon.maxAmmo;
        _PlayerShoot.CurrentAmmo = _PlayerShoot.CurrentAmmoP;

        Primary = true;

        CrossHair.SetActive(true);
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }

}
