using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    // Start is called before the first frame update
    Camera mainCam;
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
            Vector3 mousePos = Input.mousePosition;
            Vector3 alvoPos = mainCam.WorldToScreenPoint(transform.localPosition);
            Vector2 diff = new Vector2(mousePos.x - alvoPos.x, mousePos.y - alvoPos.y);
            float angulo = Mathf.Atan2(diff.x, diff.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, -angulo);
    }
}
