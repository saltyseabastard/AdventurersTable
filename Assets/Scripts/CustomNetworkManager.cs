using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkManager
{
    [SerializeField]
    private GameObject sceneCamera;
    public GameObject fpsPrefab;
    public GameObject vivePrefab;

    public override void OnStartClient(NetworkClient client)
    {
        HideSceneCamera();
    }

    public override void OnStartHost()
    {
        HideSceneCamera();
    }

    public override void OnClientConnect(NetworkConnection conn)
    {       
        if (GameInit.vrStatus == GameInit.VRStatus.None)
        {
            playerPrefab = fpsPrefab;
        }
        else if (GameInit.vrStatus == GameInit.VRStatus.Vive)
        {
            playerPrefab = vivePrefab;

        }
        base.OnClientConnect(conn);
    }


    public override void OnStopClient()
    {
        ShowSceneCamera();
    }

    public override void OnStopHost()
    {
        ShowSceneCamera();
    }

    private void HideSceneCamera()
    {
        if (sceneCamera)
            sceneCamera.SetActive(false);
    }

    private void ShowSceneCamera()
    {
        if (sceneCamera)
            sceneCamera.SetActive(true);
    }
}