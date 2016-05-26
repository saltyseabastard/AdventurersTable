using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Palette : MonoBehaviour {

    public enum DiceSides
    {
        d4,
        d6,
        d8,
        d10,
        d12,
        d20
    }

	public LoadEnvironments envLoader;
	public Button[] diceButtons;
	public GameObject[] dicePrefabs;

	//the pickupObject script on the character controller
	public PickupObject pickupObject;
	int faceUp = 0;

    private UnityAction buttonListener;
    private DiceSpawnEvent mDiceSpawnEvent;

    [System.Serializable]
    public class DiceSpawnEvent : UnityEvent<DiceSides>
    {
    }

    void Awake()
    {
        if (mDiceSpawnEvent == null)
            mDiceSpawnEvent = new DiceSpawnEvent();

        //assign the event instance to the buttons that will use it
        foreach (Button btn in diceButtons)
        {
            btn.DiceSpawnEvent = mDiceSpawnEvent;
        }
    }

    void SpawnSingleDie(DiceSides sides)
    {
        GameObject die = (GameObject)Instantiate(dicePrefabs[(int)sides], transform.position, transform.rotation);
        pickupObject.AddObjectToHand(die);
    }

    void SpawnDiceFromKeyboard()
	{
		if(Input.GetKeyDown (KeyCode.Alpha1))
		{
            SpawnSingleDie(DiceSides.d4);
			//TODO: animate button
		}
		if(Input.GetKeyDown (KeyCode.Alpha2))
		{
            SpawnSingleDie(DiceSides.d6);
		}
		if(Input.GetKeyDown (KeyCode.Alpha3))
		{
            SpawnSingleDie(DiceSides.d8);
		}
		if(Input.GetKeyDown (KeyCode.Alpha4))
		{
            //TODO: Multiple colors of dice here
            SpawnSingleDie(DiceSides.d10);
		}
		if(Input.GetKeyDown (KeyCode.Alpha5))
		{
            SpawnSingleDie(DiceSides.d12);
		}
		if(Input.GetKeyDown (KeyCode.Alpha6))
		{
            SpawnSingleDie(DiceSides.d20);
		}
	}

	void AnimateButton(GameObject button)
	{
		//iTween.MoveFrom(
	}

	void SwapEnvironmentsFromKeyboard()
	{
		if(Input.GetKeyDown (KeyCode.Alpha1))
		{
			envLoader.ChangeEnvironment (LoadEnvironments.Environment.Blacksmith);
			//TODO: animate button
		}
		if(Input.GetKeyDown (KeyCode.Alpha2))
		{
			envLoader.ChangeEnvironment (LoadEnvironments.Environment.Forest);
		}
		if(Input.GetKeyDown (KeyCode.Alpha3))
		{
			envLoader.ChangeEnvironment (LoadEnvironments.Environment.Stonehenge);
		}
	}

	void RotatePallette()
	{
		if (Input.GetKeyDown (KeyCode.L))
		{
			//transform.Rotate(new Vector3 (0, 90, 0), Space.Self);
			iTween.RotateBy(gameObject, iTween.Hash("y", -0.25f, "islocal", true, "time", 0.25f, "easetype", iTween.EaseType.easeOutQuad));

			faceUp = faceUp == 3 ? 0 : faceUp + 1;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		RotatePallette ();

		switch (faceUp)
		{
		case 0: 
			SpawnDiceFromKeyboard ();
			break;
		
		case 1:
			SwapEnvironmentsFromKeyboard ();
			break;
		}
	}

    void OnEnable()
    {
        mDiceSpawnEvent.AddListener(SpawnSingleDie);
    }

    void OnDisable()
    {
        mDiceSpawnEvent.RemoveListener(SpawnSingleDie);
    }
}
