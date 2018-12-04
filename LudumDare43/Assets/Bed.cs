using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : InteractableObject {


    private Animator anim;    
    
    public override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
    }
    public override bool CanInteract(GameObject go)
    {
        var player = go.GetComponent<Player>();

        if (player && !RifleController.IsActivePlayerCarryingGun && ActivePlayerController.ActivePlayer.stats.EnergyPercent < 1f)
        {
            return playersAllowedToInteract.Contains(player.playerCharacter);
        }
        else
        {
            return false;
        }
    }
    public override void Interact()
    {
        var player = ActivePlayerController.ActivePlayer;
        if (player.canSleepOrWake)
        {
            if (player.isSleeping)
            {
                Wake(player);
            }
            else
            {
                Sleep(player);
            }
        }
    }

    public void Sleep(Player player)
    {       
        var animation = player.playerCharacter.ToString().ToLower() + "Sleep";
        anim.SetBool(animation, true);
        SafeZone.Instance.ExitHouse(player);
        player.StartSleep(this);        
    }

    public void Wake(Player player)
    {
        var animation = player.playerCharacter.ToString().ToLower() + "Sleep";
        anim.SetBool(animation, false);

        SafeZone.Instance.EnterHouse(player);

        player.EndSleep();
    }

    public void DiedInSleep(Player player)
    {
        var animation = player.playerCharacter.ToString().ToLower() + "Sleep";
        anim.SetBool(animation, false);
    }
}
