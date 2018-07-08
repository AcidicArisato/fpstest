using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script needs an Audio Source attached to the same object, otherwise it won't work
    [RequireComponent(typeof(AudioSource))]
public class Weapon_1 : MonoBehaviour {

    //Variables
    public float pistolDamage;
    public float pistolRange;
    public float pistolSpreadFactor;
    public GameObject bulletHole;
    public Sprite idlePistol;
    public Sprite firePistol;

    public AudioClip pistolReload;
    public AudioClip pistolEmpty;
    public AudioClip pistolFire;
    AudioSource source;

    public Text ammoText;

    public int maxAmmoAmt;
    public int clipSize;
    int heldAmmo;
    int ammoInClip;

    bool isShot;
    bool isReloading;

    //Awake stuff
    void Awake()
    {
        source = GetComponent<AudioSource>();
        heldAmmo = maxAmmoAmt;
        ammoInClip = clipSize;
    }

    //Checking for fire
    void Update()
    {
        ammoText.text = ammoInClip + " / " + heldAmmo;

        if (Input.GetButtonDown("Fire1") && isReloading == false)
            isShot = true;

        if (Input.GetKeyDown(KeyCode.R) && isReloading == false && ammoInClip != clipSize)
            Reload();

        if (Input.GetKeyDown(KeyCode.P))
            heldAmmo += 10;
    }

    //The actual firing mechanism
    void FixedUpdate()
    {
        Vector2 bulletOffset = Random.insideUnitCircle * pistolSpreadFactor;
        Vector3 randomTarget = new Vector3(Screen.width / 2 + bulletOffset.x, Screen.height / 2 + bulletOffset.y, 0);
        Ray ray = Camera.main.ScreenPointToRay(randomTarget);
        RaycastHit hit;
        if (isShot == true && ammoInClip > 0 && isReloading == false)
        {
            isShot = false;
            //DynamicCrosshair.spread += DynamicCrosshair.pistolShootingSpread;
            ammoInClip--;
            source.PlayOneShot(pistolFire);
            StartCoroutine("shot");
            if (Physics.Raycast(ray, out hit, pistolRange))
            {
                Debug.Log("I've collided with " + hit.collider.gameObject.name);
                hit.collider.gameObject.SendMessage("PistolHit", pistolDamage, SendMessageOptions.DontRequireReceiver);
                if(!hit.collider.gameObject.CompareTag("Enemy"))
                    Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up,hit.normal));
            } 
        }
        else if(isShot == true && ammoInClip <= 0 && isReloading == false)
        {
            isShot = false;
            Reload();
        }
    }

    //Reload function
    void Reload()
    {
        int bulletsToReload = clipSize - ammoInClip;

        if (heldAmmo >= bulletsToReload) //Case 1: The player has more than enough bullets to reload the clip
        {
            StartCoroutine("ReloadWeapon");
            heldAmmo -= bulletsToReload;
            ammoInClip = clipSize;
        } else if (heldAmmo < bulletsToReload && heldAmmo > 0) //Case 2: Player has enough bullets to put some in the clip, but not enough to completely reload it
        {
            StartCoroutine("ReloadWeapon");
            ammoInClip += heldAmmo;
            heldAmmo = 0;
        } else if (heldAmmo <= 0) //Case 3: Player does not have any bullets
        {
            source.PlayOneShot(pistolEmpty);
        }

    }

    //"""Animation"""
    IEnumerator shot()
    {
        GetComponent<SpriteRenderer>().sprite = firePistol;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().sprite = idlePistol;
    }

    IEnumerator ReloadWeapon()
    {
        isReloading = true;
        source.PlayOneShot(pistolReload);
        yield return new WaitForSeconds(1);
        isReloading = false;
    }
}
