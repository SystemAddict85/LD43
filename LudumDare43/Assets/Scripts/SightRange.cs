using System.Collections;
using UnityEngine;


public class SightRange :MonoBehaviour
{
    private AnimalController controller;
    private Collider2D col;

    private void Awake()
    {
        controller = GetComponentInParent<AnimalController>();
        col = GetComponent<Collider2D>();
        col.enabled = false;
    }


    private void Start()
    {
        StartLookingForObstacles();
    }

    public void StartLookingForObstacles()
    {
        StartCoroutine(WaitToLook());
    }

    IEnumerator WaitToLook() {
        yield return new WaitForSeconds(1f);
        col.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        controller.isTurning = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        controller.isTurning = false;
    }
}