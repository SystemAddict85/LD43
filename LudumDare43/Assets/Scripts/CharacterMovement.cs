using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public float moveSpeed = 1f;

    public void Move(Vector3 dir)
    {
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

}
