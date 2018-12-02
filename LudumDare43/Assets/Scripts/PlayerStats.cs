using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private Player player;

    [SerializeField]
    private int maxHealth = 10;
    private int currentHealth;
    public float HealthPercent { get { return (float)currentHealth / (float)maxHealth; } }

    [SerializeField]
    private int maxHunger = 10;
    private int currentHunger;
    public float HungerPercent { get { return (float)currentHunger / (float)maxHunger; } }

    [SerializeField]
    private int maxEnergy = 10;
    private int currentEnergy;

    

    public float EnergyPercent { get { return (float)currentEnergy / (float)maxEnergy; } }

    private void Awake()
    {
        player = GetComponent<Player>();
        player.OnHealthUpdate += UpdateHealth;
        player.OnHungerUpdate += UpdateHunger;
        player.OnEnergyUpdate += UpdateEnergy;

        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
        currentHunger = maxHunger;
    }

    private void Start()
    {
        UpdateHealth(-4);
        UpdateHunger(-2);
        UpdateEnergy(0);
    }

    private void Update()
    {
        //testing
        if (Input.GetMouseButtonDown(1))
        {
            player.OnHealthUpdate(-1);
            player.OnEnergyUpdate(-1);
            player.OnHungerUpdate(-1);
            if(currentHealth <= 0)
            {
                currentHealth = maxHealth;
            }
            if (currentEnergy <= 0)
            {
                currentEnergy = maxEnergy;
            }
            if (currentHunger <= 0)
            {
                currentHunger = maxHunger;
            }
        }
    }

    public void UpdateHealth(int change)
    {
        currentHealth += change;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            player.Die();
        }
        else if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIManager.Instance.GetUIStatusBars(player.playerCharacter).UpdateBar(UIStatusBars.StatusBarType.HEALTH, HealthPercent);
    }

    public void UpdateHunger(int change)
    {
        currentHunger += change;

        if(currentHunger <= 0)
        {
            currentHunger = 0;
            player.StartHunger();
        } else if(currentHunger > maxHunger)
        {
            currentHunger = maxHunger;
        }
        else
        {
            player.HungerSatiated();
        }
        
        UIManager.Instance.GetUIStatusBars(player.playerCharacter).UpdateBar(UIStatusBars.StatusBarType.HUNGER, HungerPercent);

    }

    public void UpdateEnergy(int change)
    {
        currentEnergy += change;        

        if (currentEnergy <= 0)
        {
            currentEnergy = 0;
            player.StartEnergyDrain();
        }
        else if (currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
        else
        {
            player.isDrained = false;
        }

        UIManager.Instance.GetUIStatusBars(player.playerCharacter).UpdateBar(UIStatusBars.StatusBarType.ENERGY, EnergyPercent);
    }



}
