using UnityEngine;

public class Table : InteractableObject
{
    private static Animator anim;

    // in order to update when more food is made
    public static void PutFoodOnTable()
    {
        anim.SetBool("hasFood", true);
    }

    public override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
    }

    public void Start()
    {
    }

    public override void Interact()
    {
        // prevent full players from overeating
        if (ResourceManager.FoodCount > 0 && ActivePlayerController.ActivePlayer.stats.HungerPercent <= 0.8f)
        {
            EatFood();
        }
        else
        {
            GameMessage.Instance.ShowMessage("There's no more food", important: true);
        }
        //TableState();
    }

    public override bool CanInteract(GameObject go)
    {
        var player = go.GetComponent<Player>();

        // prevent full players from overeating
        if (player && !RifleController.IsActivePlayerCarryingGun && ActivePlayerController.ActivePlayer.stats.HungerPercent <= 0.8f)
        {
            return playersAllowedToInteract.Contains(player.playerCharacter);
        }
        else
        {
            return false;
        }
    }

    //private void TableState()
    //{
    //if (!isFoodOnTable & ResourceManager.FoodCount > 0)
    //{
    //    PlaceFood();

    //}
    //else if (isFoodOnTable)
    //{
    //EatFood();
    //ResourceManager.Instance.ChangeResourceValue(ResourceManager.ResourceType.FOOD, -1);
    //ActivePlayerController.ActivePlayer.OnHealthUpdate(+2);
    //ActivePlayerController.ActivePlayer.OnHungerUpdate(+6);
    //}
    // }

    //private void PlaceFood()
    //{
    //    isFoodOnTable = true;
    //    anim.SetBool("hasFood", true);
    //}

    private void EatFood()
    {
        AudioManager.PlaySFX("eatFood", .5f, 0f);
        ResourceManager.Instance.ChangeResourceValue(ResourceManager.ResourceType.FOOD, -1);
        ActivePlayerController.ActivePlayer.OnHealthUpdate(+2);
        ActivePlayerController.ActivePlayer.OnHungerUpdate(+6);

        anim.SetBool("hasFood", ResourceManager.FoodCount > 0);
    }

}
