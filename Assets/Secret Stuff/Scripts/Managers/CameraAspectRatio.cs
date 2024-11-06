using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspectRatio : MonoBehaviour
{
    Camera _mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = GetComponent<Camera>();
        _mainCamera.depth = 1;
        SetAspectRatio();
        var backCam = new GameObject().AddComponent<Camera>();
        backCam.transform.parent = _mainCamera.transform;
        backCam.backgroundColor = Color.black;
        backCam.cullingMask = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void SetAspectRatio(){
        // Desired Aspect Ratio
        float aspectRatio = 16.0f / 9.0f;

        // Get current aspect ratio
        float currentAspect = (float)Screen.width / (float)Screen.height;

        // Get the scale difference 
        float viewportHeight = currentAspect / aspectRatio;

        // If viewport height is less than current height, add letterbox
        if (viewportHeight != 1f){
            if (viewportHeight < 1.0f)
            {  
                Rect rect = _mainCamera.rect;

                rect.width = 1.0f;
                rect.height = viewportHeight;
                rect.x = 0;
                rect.y = (1.0f - viewportHeight) / 2.0f;
                
                _mainCamera.rect = rect;
            }
            else // add pillarbox
            {
                float scalewidth = 1.0f / viewportHeight;

                Rect rect = _mainCamera.rect;

                rect.width = scalewidth;
                rect.height = 1.0f;
                rect.x = (1.0f - scalewidth) / 2.0f;
                rect.y = 0;

                _mainCamera.rect = rect;
            }
        }
    }
}
