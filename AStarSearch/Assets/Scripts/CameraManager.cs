using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera arialCam;
    public Camera followCam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ChangeCamera()
    {
        if (arialCam.enabled)
        {
            arialCam.enabled = false;
            followCam.enabled = true;
        }
        else
        {
            arialCam.enabled = true;
            followCam.enabled = false;
        }
    }
}
