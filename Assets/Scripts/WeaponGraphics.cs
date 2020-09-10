using UnityEngine;

public class WeaponGraphics : MonoBehaviour {

    public ParticleSystem muzzleFlash;
    public GameObject hitEffect;
    public AudioSource audioSource;
    public Transform WeaponHolder;
    public GameObject weaponModel;
    public int AmmoStored;

 
    public PlayerWeapon weaponInfo;

    // Array reminder:
    // 0 - fire , 1 - empty, 2 - reaload, 

    public AudioClip[] audioClip;

    
    public void PlaySound(int indexClip)
    {
      
        audioSource.clip = audioClip[indexClip];
        audioSource.PlayOneShot(audioClip[indexClip], 0.1f);


    }
    

 
}
