using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class CharController : MonoBehaviour
{
    protected CharacterMovement move;
    [HideInInspector]
    public bool canMove = true;

    protected Transform rotationTransform;

    protected virtual void Awake()
    {
        move = GetComponent<CharacterMovement>();

        // top sprite renderer gets rotated by default
        rotationTransform = GetComponentInChildren<SpriteRenderer>().transform;
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
            rotationTransform.rotation = Quaternion.Euler(0, 0, 90f);
        }
        else if (dir.x == 0 && dir.y == -1)
        {
            rotationTransform.rotation = Quaternion.Euler(0, 0, 0f);
        }
        else if (dir.x == -1 && dir.y == 0)
        {
            rotationTransform.rotation = Quaternion.Euler(0, 0, -90f);
        }
        else if (dir.x == 0 && dir.y == 1)
        {
            rotationTransform.rotation = Quaternion.Euler(0, 0, 180f);
        }

    }



    protected virtual Vector2 GetInput() { return Vector2.zero; }
}
