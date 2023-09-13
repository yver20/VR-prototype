using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedLimiter : MonoBehaviour
{
    private bool controllersChecked = false;

    public List<UnityEngine.XR.InputDevice> inputDevices;
    void Start()
    {
        ControllerCountEnsurance();
    }

    void ControllerCheck() //checks what controllers are connected
    {
        inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);

        foreach (var device in inputDevices)
        {
            Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.characteristics.ToString()));
        }
    }
    
    void ControllerCountEnsurance() //performs a controllercheck and signals if there are more than or less than 2 present
    {
        ControllerCheck();

        if (inputDevices.Count <= 2)
        {
            Debug.LogWarning("Problem: missing controllers! check if they're all connected pls");
        }
        else
        {
            controllersChecked = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (controllersChecked == false) //this will make sure enough controllers are connected before starting to play/run the rest of the code
        {
            ControllerCountEnsurance();
        }

    }
}
