using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherLog : InteractableObject {

    public override void Interact()
    {
        ChopLog();
    }

    private void ChopLog()
    {
        ResourceManager.Instance.ChangeResourceValue(ResourceManager.ResourceType.WOOD, 1);
        AudioManager.PlaySFX("chopWood", 0.5f, 0f);
        ActivePlayerController.ActivePlayer.OnHungerUpdate(-1);
        ActivePlayerController.ActivePlayer.OnEnergyUpdate(-1);
    }
}
