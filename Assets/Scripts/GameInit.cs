using UnityEngine;
using System.Collections;

public class GameInit : MonoBehaviour {

    public enum VRStatus
    {
        Vive,
        Oculus,
        None
    }

    public static VRStatus vrStatus;
    public GameObject viveCameraRig;
    public GameObject fpsController;

	// Use this for initialization
	void Start () {
        if (UnityEngine.VR.VRDevice.isPresent)
            vrStatus = VRStatus.Vive;
        else
            vrStatus = VRStatus.None;

        Debug.Log("Connected VR Device: " + vrStatus);

        //Enable/disable VR Camera rig (defaults to PC)
        switch (vrStatus)
        {
            case VRStatus.Vive :
                viveCameraRig.SetActive(true);
                fpsController.SetActive(false);
                break;

            case VRStatus.None:
                viveCameraRig.SetActive(false);
                fpsController.SetActive(true);
                break;

            default:
                viveCameraRig.SetActive(false);
                fpsController.SetActive(true);
                break;
        }
        

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
