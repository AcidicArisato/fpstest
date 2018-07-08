using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour {

    Vector3 camDir;
	
	// Update is called once per frame
	void Update () {

        camDir = Camera.main.transform.forward;
        camDir.y = 0;
        transform.rotation = Quaternion.LookRotation(camDir);

	}
}
