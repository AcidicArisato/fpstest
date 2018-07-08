using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCrosshair : MonoBehaviour {

    static public float spread = 0;

    public const int pistolShootingSpread = 20;

    public GameObject crosshair;
    GameObject top;
    GameObject bottom;
    GameObject left;
    GameObject right;

    float initialPos;

    void Start()
    {
        top = crosshair.transform.Find("TopPart").gameObject;
        bottom = crosshair.transform.Find("BottomPart").gameObject;
        left = crosshair.transform.Find("LeftPart").gameObject;
        right = crosshair.transform.Find("RightPart").gameObject;

        initialPos = top.GetComponent<RectTransform>().localPosition.y;
    }

    void Update()
    {
        if(spread != 0)
        {
            top.GetComponent<RectTransform>().localPosition = new Vector3(0, initialPos + spread, 0);
            bottom.GetComponent<RectTransform>().localPosition = new Vector3(0, -(initialPos + spread), 0);
            left.GetComponent<RectTransform>().localPosition = new Vector3(-(initialPos + spread), 0, 0);
            right.GetComponent<RectTransform>().localPosition = new Vector3(initialPos + spread, 0, 0);
            spread -= 1;
        }
    }
}
