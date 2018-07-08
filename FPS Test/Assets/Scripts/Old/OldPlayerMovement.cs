using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class OldPlayerMovement : MonoBehaviour
{

    public float playerWalkSpeed = 5f;
    public float playerRunSpeed = 10f;
    public float jumpPower = 5f;
    public float vertRotLmt = 80f;

    public float hopPower = 3f;

    float forwardMov;
    float sideMov;
    float vertVeloc;

    float vertRot = 0f;

    float hopPush = 1000f;

    public static CharacterController cc;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Looking left and right
        float horiRot = Input.GetAxis("Mouse X");
        transform.Rotate(0, horiRot, 0);

        //Looking up and down
        //Comment this section out to make it more DOOM-like
        vertRot -= Input.GetAxis("Mouse Y");
        vertRot = Mathf.Clamp(vertRot, -vertRotLmt, vertRotLmt);
        Camera.main.transform.localRotation = Quaternion.Euler(vertRot, 0, 0);

        //Moving
        forwardMov = Input.GetAxis("Vertical") * playerRunSpeed;
        sideMov = Input.GetAxis("Horizontal") * playerRunSpeed;

        //Toggle walking
        if (Input.GetKey(KeyCode.LeftControl))
        {
            forwardMov = Input.GetAxis("Vertical") * playerWalkSpeed;
            sideMov = Input.GetAxis("Horizontal") * playerWalkSpeed;
        }


        //Jumping
        vertVeloc += Physics.gravity.y * Time.deltaTime;

        if (Input.GetButton("Jump") && cc.isGrounded)
        {
            vertVeloc = jumpPower;
            Debug.Log("When I say 'jump' you say 'vertVeloc = " + vertVeloc + "'!");
        }

        //Shift-hop
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.A) && cc.isGrounded)
        {
            vertVeloc = hopPower;
            sideMov = -(hopPush);
            Debug.Log("Hopping left");
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D) && cc.isGrounded)
        {
            vertVeloc = hopPower;
            sideMov = hopPush;
            Debug.Log("Hopping right");
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && cc.isGrounded)
        {
            vertVeloc = hopPower;
            forwardMov = hopPush;
            Debug.Log("Hopping forward");
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.S) && cc.isGrounded)
        {
            vertVeloc = hopPower;
            forwardMov = -(hopPush);
            Debug.Log("Hopping backward");
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W) && cc.isGrounded)
        {
            vertVeloc = hopPower;
            sideMov = -(hopPush);
            forwardMov = hopPush;
            Debug.Log("Hopping forward-left");
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W) && cc.isGrounded)
        {
            vertVeloc = hopPower;
            sideMov = hopPush;
            forwardMov = hopPush;
            Debug.Log("Hopping forward-right");
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && cc.isGrounded)
        {
            vertVeloc = hopPower;
            sideMov = -(hopPush);
            forwardMov = -(hopPush);
            Debug.Log("Hopping backward-left");
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S) && cc.isGrounded)
        {
            vertVeloc = hopPower;
            sideMov = hopPush;
            forwardMov = -(hopPush);
            Debug.Log("Hopping backward-left");
        }


        //What actually does the moving stuff
        Vector3 playerMovement = new Vector3(sideMov, vertVeloc, forwardMov);

        cc.Move(transform.rotation * playerMovement * Time.deltaTime);
    }
}
