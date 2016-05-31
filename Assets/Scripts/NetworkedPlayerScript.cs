using UnityEngine;
using UnityEngine.Networking;

public class NetworkedPlayerScript : NetworkBehaviour
{
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsController;
    public Camera fpsCamera;
    public AudioListener audioListener;
    public PickupObject pickupScript;
    public Palette paletteScript;
    //public Transform viveCameraRig;
    //Transform playerController;
    //public ShootingScript shootingScript;
    //public GunMaterialSwitcher gunMaterialSwitcher;

    //Renderer[] renderers;

    void Start()
    {
        //renderers = GetComponentsInChildren<Renderer>();
    }

    public override void OnStartLocalPlayer()
    {
        if (GameInit.vrStatus == GameInit.VRStatus.None)
        {
            fpsController.enabled = true;
            fpsCamera.enabled = true;
            audioListener.enabled = true;
            pickupScript.enabled = true;
            paletteScript.enabled = true;
            //shootingScript.enabled = true;
            //gunMaterialSwitcher.SwitchMaterial(true);
        }

        gameObject.name = "LOCAL Player";
        base.OnStartLocalPlayer();
    }
}