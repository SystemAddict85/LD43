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
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("killed " + name);
        ResourceManager.Instance.ChangeResourceValue(ResourceManager.ResourceType.FOOD, 5);
        Destroy(gameObject, 1.5f);
    }
}
