using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class CharController : MonoBehaviour
{
    protected CharacterMovement move;
    [HideInInspector]
    public bool canMove = true;

    protected virtual void Awake()
    {
        move = GetComponent<CharacterMovement>();
    }

    protected virtual void Update()
    {
        if (canMove)
        {
            var dir = GetInput();
            if (dir != Vector2.zero)
            {
                move.Move(dir);
                RotateCharacter(dir);
            }
        }
    }

    protected virtual void RotateCharacter(Vector2 dir)
    {
        if (dir.x == 1 && dir.y == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90f);
        }
        else if (dir.x == 0 && dir.y == -1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0f);
        }
        else if (dir.x == -1 && dir.y == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90f);
        }
        else if (dir.x == 0 && dir.y == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180f);
        }

    }



    protected virtual Vector2 GetInput() { return Vector2.zero; }
}
