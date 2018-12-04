using System.Collections.Generic;

public class ResourceManager : SimpleSingleton<ResourceManager>
{
    public enum ResourceType { RABBIT, FOOD, WOOD }

    Dictionary<ResourceType, int> resources = new Dictionary<ResourceType, int>() {
        { ResourceType.RABBIT, 0 },
        { ResourceType.WOOD, 0 },
        { ResourceType.FOOD, 0 }
    };

    public static int WoodCount
    {
        get
        {
            return Instance.resources[ResourceType.WOOD];
        }
    }

    public static int RabbitCount
    {
        get
        {
            return Instance.resources[ResourceType.RABBIT];
        }
    }

    public static int FoodCount
    {
        get
        {
            return Instance.resources[ResourceType.FOOD];
        }
    }

    public override void Awake()
    {
        base.Awake();
    }

    public int GetResourceByType(ResourceType type)
    {
        return resources[type];
    }

    public int ChangeResourceValue(ResourceType type, int changeAmount)
    {

        resources[type] += changeAmount;
        UIManager.Instance.UpdateResourceUI(type, resources[type]);
        if(type == ResourceType.FOOD && resources[type] > 0)
        {
            Table.PutFoodOnTable();
        }

        return resources[type];
    }

}
