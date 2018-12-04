using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    private InteractionContextButton button;
    [HideInInspector]
    private bool allowedToInteract = true;
    public bool AllowedToInteract
    {
        get { return allowedToInteract; }
        set
        {            
            allowedToInteract = value;
            ToggleContextButton(value);            
        }
    }
    
    [HideInInspector]
    public bool readyToInteract = false;
    public List<Player.PlayerCharacter> playersAllowedToInteract = new List<Player.PlayerCharacter>();

    public virtual void Awake()
    {
        button = GetComponentInChildren<InteractionContextButton>();
    }

    public virtual bool CanInteract(GameObject go)
    {
        var player = go.GetComponent<Player>();

        if (player && !RifleController.IsActivePlayerCarryingGun && player.stats.EnergyPercent > 0f)
        {
            return playersAllowedToInteract.Contains(player.playerCharacter);
        }
        else
        {
            return false;
        }
    }

    public virtual void CheckForInteraction()
    {
        if (!readyToInteract)
        {
            return;
        }

        Interact();

    }

    public virtual void Interact()
    {
        Debug.Log("Interaction function needs overridden");
    }

    public void ToggleContextButton(bool enabled)
    {
        if(AllowedToInteract)
            button.ToggleButton(enabled);
    }


}
