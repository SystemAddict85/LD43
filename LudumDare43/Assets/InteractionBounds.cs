using System.Collections.Generic;
using UnityEngine;

public class InteractionBounds : MonoBehaviour
{
    private InteractableObject intObject;
    private List<Player.PlayerCharacter> charactersInRange = new List<Player.PlayerCharacter>();

    private void Awake()
    {
        intObject = GetComponentInParent<InteractableObject>();
    }

    private void Start()
    {
        ActivePlayerController.Instance.OnPlayerChange += CheckForActivePlayerInBounds;
    }

    private void Update()
    {
        if (intObject.AllowedToInteract && intObject.readyToInteract)
        {
            GetInput();
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (intObject.CanInteract(col.gameObject))
        {
            charactersInRange.Add(col.GetComponent<Player>().playerCharacter);
            PlayerEntered();
        }
    }

    private void PlayerEntered()
    {
        intObject.ToggleContextButton(true);
        intObject.readyToInteract = true;
    }

    private void PlayerExited()
    {
        intObject.ToggleContextButton(false);
        intObject.readyToInteract = false;
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        var player = col.GetComponent<Player>();
        if (player)
        {
            PlayerExited();
            if (charactersInRange.Contains(player.playerCharacter))
                charactersInRange.Remove(player.playerCharacter);

        }
    }

    public void GetInput()
    {
        if (charactersInRange.Contains(ActivePlayerController.ActivePlayerCharacter) && Input.GetButtonDown("Interact"))
        {
            intObject.CheckForInteraction();
        }
    }

    private void CheckForActivePlayerInBounds(Player.PlayerCharacter player)
    {
        if (charactersInRange.Contains(player) && intObject.CanInteract(ActivePlayerController.ActivePlayer.gameObject))
        {
            PlayerEntered();
        }
        else
        {
            PlayerExited();
        }
    }
}
