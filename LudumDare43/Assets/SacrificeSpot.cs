using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeSpot : InteractableObject {

    public override void Interact()
    {
        Sacrifice();
    }

    void Sacrifice()
    {
        Debug.Log("TODO: sacrifice stuff");
    }
}
