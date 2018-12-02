using UnityEngine;

public class InteractionContextButton : MonoBehaviour
{
    SpriteRenderer rend;
    

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.enabled = false;
    }

    public void ToggleButton(bool enabled)
    {
        rend.enabled = enabled;
    }
}
