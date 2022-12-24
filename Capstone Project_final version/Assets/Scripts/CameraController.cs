 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 minValue, maxValue;

    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.25f;



    private void Update()
    {
        //make the camera smooth and set boundaries
        Vector3 targetPosition = target.position + offset;

        
        Vector3 boundPosition = new Vector3(Mathf.Clamp(targetPosition.x,minValue.x,maxValue.x), 
            Mathf.Clamp(targetPosition.y, minValue.y, maxValue.y), 
            Mathf.Clamp( targetPosition.z,minValue.z,maxValue.z));
           

        transform.position = Vector3.SmoothDamp(transform.position,boundPosition, ref velocity,smoothTime );
    }
}
