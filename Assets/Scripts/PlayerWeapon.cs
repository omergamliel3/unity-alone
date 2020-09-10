using UnityEngine;
[System.Serializable]
public class PlayerWeapon {

    public string name = "HandGun";

    public int damage = 10;
    public int range = 100;
    public float ReloadTime;
    public float fireRate = 1f;
    public int maxAmmo = 31;

    public GameObject graphics;

}
