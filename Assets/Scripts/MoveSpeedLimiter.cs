using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MoveSpeedLimiter : MonoBehaviour
{
    private bool controllersChecked = false;
    private bool controllersAssigned = false;
    private bool tempCheck = false;
    
    private InputDevice leftController;
    private InputDevice rightController;

    List<UnityEngine.XR.InputDevice> leftControllers = new List<InputDevice>();
    List<UnityEngine.XR.InputDevice> rightControllers = new List<InputDevice>();

    void Start()
    {
        ControllerCountEnsurance();
    }

    void ControllerCheck() //checks what controllers are connected
    {
        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.Left;
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.Right;
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, leftControllers);
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, rightControllers);
        //these should fill with one controller each. one for the left and one for the right.

        foreach (var device in leftControllers)
        {
            Debug.Log(string.Format("Device found with name '{0}' and characteristics '{1}'", device.name, device.characteristics.ToString()));
        }
        foreach (var device in rightControllers)
        {
            Debug.Log(string.Format("Device found with name '{0}' and characteristics '{1}'", device.name, device.characteristics.ToString()));
        }
    }
    
    void ControllerCountEnsurance() //performs a controllercheck and signals if there are more than or less than 2 present
    {
        ControllerCheck();

        if (leftControllers.Count + rightControllers.Count < 1)
        {
            Debug.LogWarning("Problem: missing controllers! check if they're all connected pls");
        }
        else
        {
            controllersChecked = true;
        }
        //as long as at least one controller is connected, we'll be fine
    }

    // Update is called once per frame
    void Update()
    {
        if (!controllersChecked) //this will make sure enough controllers are connected before starting to play/run the rest of the code
        {
            ControllerCountEnsurance();
        }

        if (controllersChecked && (!controllersAssigned) && (leftControllers.Count  + rightControllers.Count >= 1))
        {
            foreach (var device in leftControllers)
            {
                Debug.Log("we're putting " + device + " in as the left controller");
                leftController = device;
            }
            foreach (var device in rightControllers)
            {
                Debug.Log("we're putting " + device + " in as the right controller");
                rightController = device;
            }

            Debug.Log("[In forEach loop check] our left controller should be: " + leftController.characteristics.ToString() + " And our right controller should be: " + rightController.characteristics.ToString());

            if ((leftController != null) && (rightController != null))
            {
                controllersAssigned = true;
            }
        }

        

        if (controllersAssigned && (!tempCheck))
        {
            Debug.Log("[normal check] our left controller should be: " + leftController.characteristics.ToString() + " And our right controller should be: " + rightController.characteristics.ToString());
            tempCheck = true;
        }

    }
}
