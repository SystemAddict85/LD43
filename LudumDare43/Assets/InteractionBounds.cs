﻿using System.Collections.Generic;
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
            if (charactersInRange.Count > 0)
            {
                PlayersEntered();
            }
        }
    }

    private void PlayersEntered()
    {
        intObject.ToggleContextButton(true);
        intObject.readyToInteract = true;
    }

    private void PlayersExited()
    {
        intObject.ToggleContextButton(false);
        intObject.readyToInteract = false;
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (intObject.CanInteract(col.gameObject))
        {
            charactersInRange.Remove(col.GetComponent<Player>().playerCharacter);
            if (charactersInRange.Count == 0)
            {
                PlayersExited();
            }

        }
    }

    public void GetInput()
    {
        if (Input.GetButtonDown("Interact"))
        {
            intObject.CheckForInteraction();
        }
    }

    private void CheckForActivePlayerInBounds(Player.PlayerCharacter player)
    {
        if (charactersInRange.Contains(player))
        {
            PlayersEntered();
        }
        else
        {
            PlayersExited();
        }
    }
}
