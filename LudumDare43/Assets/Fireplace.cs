using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireplace : InteractableObject {
        
    Animator anim;

    public override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        //TODO: remove after testing
        ToggleFire(false);
    }

    void ToggleFire(bool enabled)
    {
        anim.SetBool("fire", enabled);
    }

    public override void Interact()
    {
        ToggleFire(!anim.GetBool("fire"));
        Debug.Log("TODO: implement fire stuff");
    }

}
