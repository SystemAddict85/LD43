using UnityEngine;

public class PlayerController : CharController
{

    private Player player;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
    }
    protected override void Update()
    {
        if (canMove && ActivePlayerController.IsActivePlayer(player.playerCharacter))
        {
            var dir = GetInput();
            if (dir != Vector2.zero)
            {
                move.Move(dir);
                RotateCharacter(dir);
            }
        }
    }

    protected override Vector2 GetInput()
    {

        var x = Input.GetAxisRaw("Horizontal");

        // prioritize horizontal movement
        var y = Mathf.Abs(x) > 0 ? 0 : Input.GetAxisRaw("Vertical");

        Vector2 moveVec = new Vector2(x, y);

        return moveVec;
    }
}
