using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 3f;
    public Vector3 offset;

    public Animator camShake;
   
    [SerializeField] Transform cam;
    private void Start() {
        //cam = GetComponentInParent<Transform>();
    }
    
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        cam.position = smoothedPosition;
    }
    public void CameraShake() {
        camShake.SetTrigger("Shake");
    }    
}
