using UnityEngine;

public class AnimalController : CharController
{
    [SerializeField]
    private float rotationSpeed = 100f;
    [SerializeField]
    private bool turnLeft;
    [SerializeField]
    private bool isTurning = false;

    private float sightRange = 2f;

    protected override Vector2 GetInput()
    {
        return transform.right;
    }

    protected override void RotateCharacter(Vector2 dir)
    {
        if (isTurning)
        {
            int direction = turnLeft ? 1 : -1;
            transform.Rotate(new Vector3(0, 0, direction * Time.deltaTime * rotationSpeed));
        }
    }

    
}
