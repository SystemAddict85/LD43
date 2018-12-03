using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleHook : InteractableObject {

    private bool isRifleHanging = true;
    private Animator anim;


    public override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
    }

    public override void Interact()
    {
        ToggleRifleHanging(!isRifleHanging);
    }

    private void ToggleRifleHanging(bool enabled)
    {
        isRifleHanging = enabled;
        anim.SetBool("rifleHanging", enabled);
        if (enabled)
        {
            UnequipRifle();
        }
        else
        {
            EquipRifle();
        }
    }

    private void UnequipRifle()
    {
        Debug.Log("TODO: Remove rifle");
        ActivePlayerController.ActivePlayer.GetComponentInChildren<RifleController>().ToggleRifle(false);
    }

    private void EquipRifle()
    {
        Debug.Log("TODO: Equip rifle");
        var rifleController = ActivePlayerController.ActivePlayer.GetComponentInChildren<RifleController>();
        rifleController.ToggleRifle(true);
    }

    public override bool CanInteract(GameObject go)
    {
        var player = go.GetComponent<Player>();
        if (isRifleHanging)
        {
            if (player)
            {
                return playersAllowedToInteract.Contains(player.playerCharacter);
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (player && RifleController.IsActivePlayerCarryingGun )
            {
                return playersAllowedToInteract.Contains(player.playerCharacter);
            }
            else
            {
                return false;
            }
        }
    }
}
