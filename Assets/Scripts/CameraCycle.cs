using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraCycle : MonoBehaviour
{
    private Camera prevCamera;
    public Camera[] cameras = new Camera[10];

    private KeyCode[] keyCodes = {
        KeyCode.Alpha0,
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9
     };

    // Start is called before the first frame update
    void Start()
    {
        prevCamera = Camera.main;
        foreach(Camera cam in cameras)
        {
            cam.enabled = false;
        }
        prevCamera.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<cameras.Length && i<keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                prevCamera.enabled = false;
                cameras[i].enabled = true;
                prevCamera = cameras[i];
            }
        }
    }
}
