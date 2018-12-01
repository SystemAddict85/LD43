using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerCharacter { Paw, Maw, Boy, Girl };
}

public class PlayerController : CharController {
    
    protected override Vector2 GetInput()
    {
        var x = Input.GetAxisRaw("Horizontal");

        // prioritize horizontal movement
        var y = Mathf.Abs(x) > 0 ? 0 : Input.GetAxisRaw("Vertical");

        Vector2 moveVec = new Vector2(x, y);
        
        return moveVec;
    }
}
