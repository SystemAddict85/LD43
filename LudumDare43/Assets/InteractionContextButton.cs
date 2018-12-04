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
        // protects against any rabbits dying or being picked up
    
        if(rend)
            rend.enabled = enabled;
    }
}
