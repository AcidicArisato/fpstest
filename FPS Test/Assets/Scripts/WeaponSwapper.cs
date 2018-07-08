using UnityEngine;

public class WeaponSwapper : MonoBehaviour {

    public int curWeapon = 0;

	void Start () {
        SelectWeapon();
	}
	
	void Update () {

        int prevWeapon = curWeapon;

        //Press number keys to get to weapons
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            curWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) /*add check to see if player has this weapon*/)
        {
            curWeapon = 1;
        }

        //Scroll wheel functionality
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (curWeapon >= transform.childCount - 1)
                curWeapon = 0;
            else
                curWeapon++;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (curWeapon <= 0)
                curWeapon = transform.childCount - 1;
            else
                curWeapon--;
        }

        //Checks to see if desired weapon selection is different from current weapon
        //If yes, then switch. This prevents players from switching to their current weapon

        if (prevWeapon != curWeapon)
            SelectWeapon();
    }

    //This method runs through each child of the object this script is attached to
    //It disables every child not equal to 'curWeapon', and enables the one that is
    void SelectWeapon()
    {

        int i = 0;

        foreach (Transform weapon in transform)
        {
            if (i == curWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }

    }

}
