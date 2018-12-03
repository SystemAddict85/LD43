using TMPro;
using UnityEngine;

public class UIResources : MonoBehaviour
{

    TextMeshProUGUI tmpro;

    [SerializeField]
    private ResourceManager.ResourceType resourceType = ResourceManager.ResourceType.RABBIT;

    private void Awake()
    {
        tmpro = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateResource(int amount)
    {
        tmpro.text = amount.ToString();
    }
}
