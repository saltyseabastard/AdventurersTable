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
    public LoadEnvironments.Environment environments;

    public Palette.DiceSpawnEvent DiceSpawnEvent;
    public Palette.EnvLoadEvent EnvLoadEvent;

    public void GotHit()
    {
        if (buttonType == ButtonType.Dice)
            DiceSpawnEvent.Invoke(diceSides);
        else if (buttonType == ButtonType.Environment)
            EnvLoadEvent.Invoke(environments);

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
