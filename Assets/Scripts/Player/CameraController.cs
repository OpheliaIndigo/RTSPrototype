using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity;
    public float borderDistanceThreshold;
    public float cameraHeight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Pan(getCameraTranslation());
    }

    
    private float calculateCameraTerrainMagnitude()
    {
        // Correct such that camera is always a certain height from terrain directly below it. Also means can't pan out of view.
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0.5f)), out hit))
        {
            Vector3 point = hit.point;
            print("found distance " + hit.distance);
            return hit.distance;
        }
        else
        {
            return -1;
        }
    }

    private Vector3 getCameraTranslation()
    {
        Vector3 mousePos = readMousePosition();
        if (mousePos.x < borderDistanceThreshold)
        {
            return Vector3.left * mouseSensitivity;
        } else if (mousePos.x > Screen.width - borderDistanceThreshold)
        {
            return Vector3.right * mouseSensitivity;
        } else if (mousePos.y < borderDistanceThreshold)
        {
            return Vector3.down * mouseSensitivity;
        } else if (mousePos.y > Screen.height - borderDistanceThreshold)
        {
            return Vector3.up * mouseSensitivity;
        } else
        {
            return Vector3.zero;
        }
    }

    private Vector3 readMousePosition()
    {
        return Input.mousePosition;
    }

    private void Pan(Vector3 direction)
    {
        Camera camera = Camera.main;
        camera.transform.Translate(direction);
        float magnitude = calculateCameraTerrainMagnitude();
        if (Mathf.Abs(magnitude - cameraHeight) > 1)
        {
            // Modify z such that magnitude == camera height
            float newY = Mathf.Sqrt(magnitude - Mathf.Pow(Camera.main.transform.position.x,2) - Mathf.Pow(Camera.main.transform.position.z, 2));
            print("new value " + newY.ToString() + " in Y axis");
            print("panning " + (newY - Camera.main.transform.position.y).ToString());
            // Zoom 
            Pan(new Vector3(0, Camera.main.transform.position.y - newY, 0));
        }
    }
}
