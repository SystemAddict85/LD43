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
        player.StartSleep();
    }
    public void Wake(Player player)
    {
        var animation = player.playerCharacter.ToString().ToLower() + "Sleep";
        anim.SetBool(animation, false);
        player.EndSleep();
    }

}
