using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitSpawner : MonoBehaviour {

    public GameObject bunnyPrefab;

    private SpriteRenderer[] spawnLocations;

    private void Awake()
    {
        spawnLocations = GetComponentsInChildren<SpriteRenderer>();
    }

    public void SpawnRabbit()
    {
        int spawnerIndex = GetRandomIndex();
        var rotation = transform.rotation;

        var rabbit = Instantiate(Resources.Load("Prefabs/Rabbit"), spawnLocations[spawnerIndex].transform) as GameObject;
        if (spawnerIndex >= spawnLocations.Length / 2)
        {
            rabbit.transform.Rotate(new Vector3(0, 0, 180f));
        }
    }

    private int GetRandomIndex()
    {
        return Random.Range(0, spawnLocations.Length);
    }
}
