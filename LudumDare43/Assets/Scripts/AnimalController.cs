using System.Collections;
using UnityEngine;

public class AnimalController : CharController
{
    [SerializeField]
    private float rotationSpeed = 100f;

    public bool isTurning = false;
    
    public bool isDead = false;

    //[SerializeField]
    //private float sightRange = 30f;
    //private bool readyToLook = true;

    private SightRange sightRange;

    [SerializeField]
    private LayerMask layersToAvoid;

    
    protected override void Awake()
    {
        base.Awake();
        sightRange = GetComponentInChildren<SightRange>();
    }

    protected override Vector2 GetInput()
    {        
        return isDead ? Vector3.zero : transform.right;
    }

    protected override void RotateCharacter(Vector2 dir)
    {
        if (!isDead && isTurning)
        {
            //LookForObstacles();        
            //int direction = turnLeft ? 1 : -1;
            transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotationSpeed));
        }
    }

    //void LookForObstacles()
    //{
    //    StartCoroutine(WaitToLook());
    //    var hit = Physics2D.Raycast(transform.position, transform.right, sightRange, layersToAvoid.value);
    //    if (hit)
    //    {
    //        Debug.Log(hit.collider.gameObject.name);
    //    }
    //}

    //IEnumerator WaitToLook()
    //{
    //    readyToLook = false;
    //    yield return new WaitForSeconds(0.2f);
    //    readyToLook = true;
    //}
    
}
