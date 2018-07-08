using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{

    public float playerWalkSpeed = 5f;
    public float playerRunSpeed = 10f;
    public float jumpPower = 5f;
    public float vertRotLmt = 80f;

    public float hopLateralPower = 10f;
    public float hopVerticalPower = 10f;

    private float forwardMov;
    private float sideMov;
    private float vertVeloc;

    private float vertRot = 0f;

    //Used to store the hop motion vector
    private Vector3 extraForce;

    //Why is this static?
    //Also, if it's public then why bother setting it using GetComponent on line 30? A public variable should be set using the Unity inspector
    //It's already a required component, so make it a private, non-static member and get it with GetComponent (line 30)
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


        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //If you want to normalize the input vector
        input.Normalize();

        //Toggle walking
        if (Input.GetKey(KeyCode.LeftControl))
        {
            forwardMov = input.y * playerWalkSpeed;
            sideMov = input.x * playerWalkSpeed;
        }
        else
        {
            //Moving
            forwardMov = input.y * playerRunSpeed;
            sideMov = input.x * playerRunSpeed;
        }


        //Jumping
        vertVeloc += Physics.gravity.y * Time.deltaTime;


        if (Input.GetButton("Jump") && cc.isGrounded)
        {
            vertVeloc = jumpPower;
            Debug.Log("When I say 'jump' you say 'vertVeloc = " + vertVeloc + "'!");
        }

        //Shift-hop
        //Press Shift (Down)
        //There must be some kind of input
        //The player must be grounded
        if (Input.GetKeyDown(KeyCode.LeftShift) && input != Vector2.zero && cc.isGrounded)
        {
            //Calculate the hop vector based on the input
            Vector3 force = new Vector3(input.x * hopLateralPower, 0f, input.y * hopLateralPower);
            //Add this to the extra force
            extraForce += force;

            //Add the jump force from the hop
            vertVeloc += hopVerticalPower;
        }


        //What actually does the moving stuff
        Vector3 playerMovement = new Vector3(sideMov, vertVeloc, forwardMov);

        //Add the extra force to the movement vector
        playerMovement += extraForce;

        cc.Move(transform.rotation * playerMovement * Time.deltaTime);

        //Decay the extra force
        extraForce = Vector3.Lerp(extraForce, Vector3.zero, 5f * Time.deltaTime);
    }
}
