using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fryer : InteractableObject {

    private bool isFryerOn = false;
    private Animator anim;

    public override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
    }

    public override void Interact()
    {
        FryerState();    
    }

    private void FryerState()
    {
        if(!isFryerOn & ResourceManager.WoodCount > 0)
        {
            TurnFryerOn();
            ResourceManager.Instance.ChangeResourceValue(ResourceManager.ResourceType.WOOD,-1);
        } else if(isFryerOn & ResourceManager.RabbitCount > 0)
        {
            TurnFryerOff();
            AudioManager.PlaySFX("frying", .5f, 0f);
            ResourceManager.Instance.ChangeResourceValue(ResourceManager.ResourceType.RABBIT, -1);
            ResourceManager.Instance.ChangeResourceValue(ResourceManager.ResourceType.FOOD, +3);
        }
    }

    private void TurnFryerOn()
    {
        isFryerOn = true;
        anim.SetBool("fryerOn", true);
        AudioManager.PlaySFX("startFire", .5f, 0f);
    }

    private void TurnFryerOff()
    {
        isFryerOn = false;
        anim.SetBool("fryerOn", false);
    }




}
