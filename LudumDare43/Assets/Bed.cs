using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : InteractableObject {


    public override void Interact()
    {
        Sleep();
    }

    public void Sleep()
    {
        Debug.Log("TODO: sleep");
    }       

}
