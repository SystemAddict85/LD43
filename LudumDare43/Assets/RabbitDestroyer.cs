using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitDestroyer : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var rabbit = collision.gameObject.GetComponent<Rabbit>();
        if (rabbit)
        {
            Destroy(rabbit.gameObject);
        }
    }
}
