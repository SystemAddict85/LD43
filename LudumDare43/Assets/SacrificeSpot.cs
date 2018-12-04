using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeSpot : InteractableObject {

    private float currentDarkness = 0f;
    private float totalDarkness = 60f;

    
    private Animator anim;

    public override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();

    }
    public override void Interact()
    {
        if(ResourceManager.RabbitCount > 0)
            Sacrifice();
        else
        {
            GameMessage.Instance.ShowMessage("I Demand Fresh Blood", 2f);
        }
    }

    private void Sacrifice()
    {
        anim.SetTrigger("sacrifice");
        AudioManager.PlaySFX("sacrifice", .5f, 0f);
        ResourceManager.Instance.ChangeResourceValue(ResourceManager.ResourceType.RABBIT, -1);
        DarknessBar.Refill();
    }
}
