using System.Collections;
using UnityEngine;

public class Fireplace : InteractableObject
{

    Animator anim;

    [SerializeField]
    private float fireDuration = 3f;

    public static bool isFireOn = false;

    private float coldCounter = 0f;
    [SerializeField]
    private float freezeIteration = 3f;

    public override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        //TODO: remove after testing
        StartColdCounters();
    }

    void IgniteFire()
    {
        isFireOn = true;
        ResourceManager.Instance.ChangeResourceValue(ResourceManager.ResourceType.WOOD, -1);
        anim.SetBool("fire", true);
        AudioManager.PlaySFX("startFire", .5f, 0f);
        WarmUpHouse();
        StartCoroutine(FireBurning());
    }

    void ExtinguishFire()
    {
        anim.SetBool("fire", false);
        isFireOn = false;
        AudioManager.PlaySFX("fireOut", .5f, 0f);
        StartColdCounters();
    }


    IEnumerator FireBurning()
    {
        yield return new WaitForSeconds(fireDuration);
        ExtinguishFire();
    }

    public override void Interact()
    {
        if (!isFireOn && ResourceManager.GetWoodAmount() > 0)
        {
            IgniteFire();
        }

    }
    public void WarmUpHouse()
    {
        // every player inside when 
        foreach (var p in ActivePlayerController.Instance.players)
        {
            if (SafeZone.Instance.playersInside.Contains(p.playerCharacter))
            {
                p.WarmingUp();
            }
        }
    }
    // being inside with fire prevents energy loss
    public void StartColdCounters()
    {
        // every player inside when 
        foreach(var p in ActivePlayerController.Instance.players)
        {
            if (SafeZone.Instance.playersInside.Contains(p.playerCharacter))
            {
                p.StartColdCounter();
            }
        }
    }

}
