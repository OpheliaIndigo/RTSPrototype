using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity;
    public float borderDistanceThreshold;
    public float cameraHeight;

    public Vector3 targetPoint;
    public float rayMagnitude;

    float xOffset;
    float yOffset;

    // Start is called before the first frame update
    void Start()
    {
        targetPoint = Vector3.zero;

        calculateOffsets();
        Pan(Vector3.zero);
        // Initialise camera at 
    }

    // Update is called once per frame
    void Update()
    {
        calculateCameraTerrainMagnitude();
        Vector3 translation = getCameraTranslation();
        if (translation != Vector3.zero)
        {
            Pan(translation);
        }
    }

    void calculateOffsets()
    {
        Camera camera = Camera.main;

        float radianXRotation = camera.transform.rotation.eulerAngles.x * Mathf.PI / 180;

        xOffset = cameraHeight * Mathf.Sin(radianXRotation * Mathf.PI / Mathf.PI);
        yOffset = cameraHeight * Mathf.Cos(radianXRotation * Mathf.PI / Mathf.PI);
        print("xOffset: " + xOffset.ToString() + "\nyOffset: " + yOffset.ToString());

    }

    private bool calculateCameraTerrainMagnitude()
    {
        // Correct such that camera is always a certain height from terrain directly below it. Also means can't pan out of view.
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2)), out hit))
        {
            targetPoint.y = hit.point.y;
            rayMagnitude = hit.distance;
            return true;
        }
        return false;
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
            return Vector3.back * mouseSensitivity;
        } else if (mousePos.y > Screen.height - borderDistanceThreshold)
        {
            return Vector3.forward * mouseSensitivity;
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


        // Translate point in direction
        targetPoint += direction;
        Vector3 newCamera = targetPoint;
        newCamera.x = newCamera.x - xOffset;
        newCamera.y = newCamera.y + yOffset;


        Camera main = Camera.main;
        print("Moving camera from " + main.transform.position.ToString() + " to " + newCamera.ToString());
        main.transform.position = newCamera;

    }
}
