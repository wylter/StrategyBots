using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float horizontalResolutionReference = 1920;
    public float verticalResolutionReference = 1080;

    void Start(){
        Camera cam = GetComponent<Camera>();

        float referenceAspect = verticalResolutionReference / horizontalResolutionReference;

        if (referenceAspect > cam.aspect) {
            cam.orthographicSize *= referenceAspect / cam.aspect;
        }
    }

}
