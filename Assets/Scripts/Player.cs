using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public Transform fpsController;
    public Transform viveCameraRig;
    Transform playerController;

    void Start()
    {
         if (GameInit.vrStatus == GameInit.VRStatus.None)
            playerController = Instantiate(fpsController);
        else if (GameInit.vrStatus == GameInit.VRStatus.Vive)
            playerController = Instantiate(viveCameraRig);

        playerController.parent = transform;
    }
}
