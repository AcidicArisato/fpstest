using UnityEngine;
using System.Collections;



public class HeadBob : MonoBehaviour
{
    public float bobbingSpeed = 0.18f;
    public float bobbingHeight = 0.2f;
    public float midpoint = 1.8f;
    public bool isBobbing = true;

    //private Vector3 startPos;

    private float timer = 0.0f;

    private void Start()
    {
        //startPos = transform.localPosition;
    }

    void Update()
    {
        Vector3 cSharpConversion = transform.localPosition;

        if (PlayerMovement.cc.isGrounded)
        {
            float waveslice = 0.0f;
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
            {
                timer = 0.0f;
            }
            else
            {
                waveslice = Mathf.Sin(timer);
                timer = timer + bobbingSpeed;
                if (timer > Mathf.PI * 2)
                {
                    timer = timer - (Mathf.PI * 2);
                }
            }
            if (waveslice != 0)
            {
                float translateChange = waveslice * bobbingHeight;
                float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
                totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
                translateChange = totalAxes * translateChange;

                if (isBobbing)
                    cSharpConversion.y = midpoint + translateChange;
                else
                    cSharpConversion.x = translateChange;
            }
            else
            {
                if (isBobbing)
                    cSharpConversion.y = midpoint;
                else
                    cSharpConversion.x = 0;
            }

            transform.localPosition = cSharpConversion;
        }
        else
        {

        }

    }



}
