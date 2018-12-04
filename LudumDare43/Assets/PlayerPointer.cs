using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointer : MonoBehaviour {

    public enum PointerStatus { INACTIVE, ACTIVE, DANGER };

    private PointerStatus status = PointerStatus.INACTIVE;
    private Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void UpdatePointer(PointerStatus newStatus)
    {
        status = newStatus;
        anim.SetInteger("pointerColor", (int)status);
    }

    public void TogglePointerView(bool enabled)
    {
        GetComponent<SpriteRenderer>().enabled = enabled;
    }
}
