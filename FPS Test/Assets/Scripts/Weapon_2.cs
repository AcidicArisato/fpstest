using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script needs an Audio Source attached to the same object, otherwise it won't work
[RequireComponent(typeof(AudioSource))]
public class Weapon_2 : MonoBehaviour
{

    //Variables
    public float fireballDamage;
    public float fireballRange;
    public float fireballSpeed;

    public GameObject fireballPrefab;
    public GameObject scorchMark;

    public Transform fireballSpawn;

    public Sprite idleHands;
    public Sprite throw1;
    public Sprite throw2;

    public AudioClip fireballEmpty;
    public AudioClip fireballFire;
    AudioSource source;

    public Text ammoText;

    public int maxFireMana;
    public int fireManaCost;
    int curFireMana;

    bool isShooting;

    //Awake stuff
    void Awake()
    {
        source = GetComponent<AudioSource>();
        curFireMana = maxFireMana;
    }

    //Checking for fire
    void Update()
    {
        ammoText.text = curFireMana / 10 + " Fireballs ";

        if (Input.GetButtonDown("Fire1") && isShooting == false && curFireMana > 0)
        {
            StartCoroutine("ThrowFireball");
        }

        if (Input.GetButtonDown("Fire1") && isShooting == false && curFireMana <=0)
        {
            source.PlayOneShot(fireballEmpty);
        }
    }

    /*void throwFireball()
    {
        //Create the fireball
        var fireball = (GameObject)Instantiate(fireballPrefab, fireballSpawn.position, fireballSpawn.rotation);

        //Apply velocity
        fireball.GetComponent<Rigidbody>().velocity = fireball.transform.forward * fireballSpeed;
    }*/

    //"""Animation"""

    IEnumerator ThrowFireball()
    {
        isShooting = true;

        //First part of animation
        source.PlayOneShot(fireballFire);
        GetComponent<SpriteRenderer>().sprite = throw1;
        yield return new WaitForSeconds(0.2f);

        //Create the fireball
        var fireball = (GameObject)Instantiate(fireballPrefab, fireballSpawn.position, fireballSpawn.rotation);
        curFireMana -= fireManaCost;

        //Apply velocity
        fireball.GetComponent<Rigidbody>().velocity = fireball.transform.forward * fireballSpeed;
        
        //Second part of animation
        GetComponent<SpriteRenderer>().sprite = throw2;
        yield return new WaitForSeconds(0.4f);
        GetComponent<SpriteRenderer>().sprite = throw1;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().sprite = idleHands;

        isShooting = false;
    }

}
