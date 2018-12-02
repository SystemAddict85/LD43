using System;
using UnityEngine;

public class InteractionBounds : MonoBehaviour
{
    private InteractableObject intObject;
    private int numInRange = 0;

    private void Awake()
    {
        intObject = GetComponentInParent<InteractableObject>();
    }

    private void Update()
    {
        if(numInRange > 0 && intObject.readyToInteract && intObject.AllowedToInteract)
        {
            GetInput();
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (intObject.CanInteract(col.gameObject))
        {
            if (++numInRange == 1)
            {
                intObject.ToggleContextButton(true);
            }
            intObject.readyToInteract = true;
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (intObject.CanInteract(col.gameObject))
        {
            if (--numInRange == 0)
            {
                intObject.readyToInteract = false;
                intObject.ToggleContextButton(false);
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
}
