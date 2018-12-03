using System.Collections.Generic;

public class ResourceManager : SimpleSingleton<ResourceManager> {
    public enum ResourceType { RABBIT, FOOD, WOOD }

    Dictionary<ResourceType, int> resources = new Dictionary<ResourceType, int>();

    public override void Awake()
    {
        base.Awake();

        resources.Add(ResourceType.RABBIT, 1);
        resources.Add(ResourceType.FOOD, 10);
        resources.Add(ResourceType.WOOD, 5);

    }

    public void Start()
    {
        ChangeResourceValue(ResourceType.RABBIT, 5);
        ChangeResourceValue(ResourceType.FOOD, 2);
        ChangeResourceValue(ResourceType.WOOD, 2);
    }

    public int GetResourceByType(ResourceType type)
    {
        return resources[type];
    }

    public static int GetWoodAmount()
    {
        return Instance.resources[ResourceType.WOOD];
    }

    public static int GetRabbitAmount()
    {
        return Instance.resources[ResourceType.RABBIT];
    }

    public static int GetFoodAmount()
    {
        return Instance.resources[ResourceType.FOOD];
    }

    public int ChangeResourceValue(ResourceType type, int changeAmount)
    {

        resources[type] += changeAmount;
        UIManager.Instance.UpdateResourceUI(type, resources[type]);
        return resources[type];
    }

}
