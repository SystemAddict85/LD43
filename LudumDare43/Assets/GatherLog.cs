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
        Debug.Log("TODO: chop log");
    }
}
