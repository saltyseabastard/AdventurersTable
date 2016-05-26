using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Button : SteamVR_InteractableObject
{
    public enum ButtonType
    {
        Dice,
        Environment
    }

    public ButtonType buttonType;
    public Palette.DiceSides diceSides;

    public Palette.DiceSpawnEvent DiceSpawnEvent;

    public void GotHit()
    {
        DiceSpawnEvent.Invoke(diceSides);
    }

    public override void StartUsing(GameObject usingObject)
    {
        base.StartUsing(usingObject);
        GotHit();
    }

    public override void StopUsing(GameObject usingObject)
    {
        base.StopUsing(usingObject);
        GotHit();
    }
}
