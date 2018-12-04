using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : InteractableObject
{

    private bool isDead = false;

    private Animator anim;

    private Coroutine deathTimer;

    private AnimalController controller;

    public override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        controller = GetComponent<AnimalController>();
    }


    public override void Interact()
    {
        RabbitState();
    }

    private void RabbitState()
    {
        AudioManager.PlaySFX("pickup", .5f, 0f);
        if (!isDead)
        {
            CatchRabbit();
        }
        else if (isDead)
        {
            HarvestRabbit();
        }
    }

    private void CatchRabbit()
    {
        ResourceManager.Instance.ChangeResourceValue(ResourceManager.ResourceType.RABBIT, +1);
        Destroy(gameObject);
    }

    private void HarvestRabbit()
    {
        StopCoroutine(deathTimer);        
        ResourceManager.Instance.ChangeResourceValue(ResourceManager.ResourceType.FOOD, +1);
        Destroy(gameObject);
    }

    public void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<AnimalController>().isDead = true;
        isDead = true;
        anim.SetTrigger("die");
        Debug.Log("killed " + name);
        // only Paw can harvest
        playersAllowedToInteract = new List<Player.PlayerCharacter> { Player.PlayerCharacter.Paw };
        deathTimer = StartCoroutine(DeathTimer());
    }

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    public override bool CanInteract(GameObject go)
    {
        var player = go.GetComponent<Player>();

        if (player)
        {
            return playersAllowedToInteract.Contains(player.playerCharacter);
        }
        else
        {
            return false;
        }
    }
}
