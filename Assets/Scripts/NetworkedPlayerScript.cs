using UnityEngine;
using UnityEngine.Networking;

public class NetworkedPlayerScript : NetworkBehaviour
{
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsController;
    public Camera fpsCamera;
    public AudioListener audioListener;
    public PickupObject pickupScript;
    //public ShootingScript shootingScript;
    //public GunMaterialSwitcher gunMaterialSwitcher;
	Player player;

    Renderer[] renderers;

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
		player = GetComponent<Player> ();
	}

    public override void OnStartLocalPlayer()
    {
		if (GameInit.vrStatus == GameInit.VRStatus.None)
		{
			player.fpsController.SetActive (true);
			player.viveCameraRig.SetActive (false);

			fpsController.enabled = true;
			fpsCamera.enabled = true;
			audioListener.enabled = true;
			pickupScript.enabled = true;
		}

		else if (GameInit.vrStatus == GameInit.VRStatus.Vive)
		{
			player.viveCameraRig.SetActive (true);
			player.fpsController.SetActive (false);
		}

        //shootingScript.enabled = true;
        //gunMaterialSwitcher.SwitchMaterial(true);

        gameObject.name = "LOCAL Player";
        base.OnStartLocalPlayer();
    }
}