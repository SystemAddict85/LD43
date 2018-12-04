using UnityEngine;

public class AnimalStats : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 1;
    private int currentHealth;

    public bool canBeHarmed = false;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            GetComponent<Rabbit>().Die();
        }
    }
    
}
