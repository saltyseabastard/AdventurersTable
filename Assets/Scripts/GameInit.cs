using UnityEngine;
using System.Collections;

public class GameInit : MonoBehaviour {

    public enum VRStatus
    {
        Vive,
        Oculus,
        None
    }

    public static VRStatus vrStatus = VRStatus.None;
    public GameObject viveCameraRig;
    public GameObject fpsController;

	/*
	public void SetActiveRig(VRStatus rig)
	{
		if (rig == VRStatus.Vive)
		{
			this.viveCameraRig.SetActive (true);
			this.fpsController.SetActive (false);
		} else
		{
			this.viveCameraRig.SetActive(false);
			if (fpsController)
				this.fpsController.SetActive(true);
		}
	}
	*/


	// Use this for initialization
	void Start ()
    {

        if (UnityEngine.VR.VRDevice.isPresent)
            vrStatus = VRStatus.Vive;
        else
            vrStatus = VRStatus.None;

        //Enable/disable VR Camera rig (defaults to PC)
        
    }

}
