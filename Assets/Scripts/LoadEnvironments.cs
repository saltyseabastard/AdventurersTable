using UnityEngine;
using System.Collections;

public class LoadEnvironments : MonoBehaviour
{
	public enum Environment
	{
		Blacksmith = 0,
		Forest = 1,
		Stonehenge = 2,
		Tavern,
		Desert,
		Plain,
		Dungeon,
		LivingRoom,
		Rivendell,
		Cyberpunk,
		SeedyBar,
		Hell,
		Cave
	}

    public string[] environments;
    public Material[] MaterialRef;
    public GameObject instance;
    public Vector3[] originalPos;

    void Start()
    {
		ChangeEnvironment (Environment.Blacksmith);
    }

	public void ChangeEnvironment(Environment env)
	{
		Destroy(instance);
		RenderSettings.skybox = MaterialRef[(int)env];
		instance = Instantiate(Resources.Load(environments[(int)env], typeof(GameObject))) as GameObject;
		instance.transform.parent = transform;
		instance.transform.localPosition = originalPos[(int)env];
		instance.SetActive(true);
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
			ChangeEnvironment (Environment.Blacksmith);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
			ChangeEnvironment (Environment.Forest);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
			ChangeEnvironment (Environment.Stonehenge);
        }
    }
}
