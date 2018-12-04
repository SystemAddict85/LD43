using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SimpleSingleton<GameManager> {

    [SerializeField]
    private int startingFood = 4;
    [SerializeField]
    private int startingWood = 4;
    [SerializeField]
    private int startingRabbits = 4;
    [SerializeField]
    private float roundLengthInSeconds = 180f;
    [SerializeField]
    private float darknessBarLengthInSeconds = 180f;

    [SerializeField]
    private float rabbitSpawnIteration = 1f;
    private float currentTimer = 0f;
    public bool canSpawnRabbits = true;

    private RabbitSpawner spawner;

    public override void Awake()
    {
        base.Awake();
        spawner = GetComponentInChildren<RabbitSpawner>();
    }
    // Use this for initialization
    void Start () {
        ResourceManager.Instance.ChangeResourceValue(ResourceManager.ResourceType.FOOD, startingFood);
        ResourceManager.Instance.ChangeResourceValue(ResourceManager.ResourceType.WOOD, startingWood);
        ResourceManager.Instance.ChangeResourceValue(ResourceManager.ResourceType.RABBIT, startingRabbits);
        UIManager.Instance.daysRemaining.StartTimer(roundLengthInSeconds);
        UIManager.Instance.darknessBar.StartBar(darknessBarLengthInSeconds);
    }

    public void Update()
    {
        if (canSpawnRabbits)
        {
            currentTimer += Time.deltaTime;
            if(currentTimer >= rabbitSpawnIteration)
            {
                canSpawnRabbits = false;
                spawner.SpawnRabbit();
                currentTimer = 0f;
                canSpawnRabbits = true;
            }
        }
    }
    public void GameOver(bool winEnabled)
    {
        Time.timeScale = 0f;
        string message = "Game Over\n";
        if (winEnabled)
        {
            Debug.Log("player wins");
            message += "You Win!\nYou made it to spring with:\n";

            List<string> alive = new List<string>();
            foreach (var p in ActivePlayerController.Instance.players)
            {
                if (!p.isDead)
                {
                    alive.Add(p.playerCharacter.ToString());
                }
            }
            var array = alive.ToArray();
            string names = string.Join(", ", array);
            message += names;
        }
        else
        {
            Debug.Log("player loses");
            var daysRemaining = UIManager.Instance.daysRemaining.days;
            message += "You Lose!\nOnly " + daysRemaining + " days to spring...";
        }
        UIManager.Instance.GetComponentInChildren<RestartButton>().ShowGameOver(message);
    }
}
