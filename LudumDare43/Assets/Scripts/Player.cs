using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerCharacter { Paw, Maw, Beth, Tom };
    public PlayerCharacter playerCharacter = PlayerCharacter.Paw;

    [HideInInspector]
    public PlayerController controller;
    [HideInInspector]
    public PlayerPointer pointer;

    public bool IsInDanger { get { return dangerEvents > 0; } }
    [HideInInspector]
    public int dangerEvents = 0;
    [SerializeField]
    private float dangerEventDuration = 3f;

    [HideInInspector]
    public bool canSleepOrWake = true;
    [HideInInspector]
    public bool isSleeping = false;
    private bool readyToRest = false;
    [SerializeField]
    private float restingIteration = 3f;
    private Coroutine sleepCoroutine;

    [SerializeField]
    private float freezingIteration = 3f;
    private bool isCold = false;
    private bool readyToFreeze = true;
    private Coroutine coldCoroutine;

    [HideInInspector]
    public PlayerStats stats;

    [HideInInspector]
    public bool isDrained = false;
    [HideInInspector]
    public bool isStarving = false;
    private bool readyToHungerPain = false;
    [SerializeField]
    private float hungerPainIteration = 3f;
    private Coroutine starvingCoroutine;

    public Action<int> OnHealthUpdate;
    public Action<int> OnHungerUpdate;
    public Action<int> OnEnergyUpdate;
    public Action<float> OnDangerEvent;
    public Action OnDangerClear;

    public bool isDead = false;

    private bool readyToGetHungry = true;
    private float normalHungerDuration = 5f;
    private Coroutine normalHungerCoroutine;

    private Bed playerBed;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        pointer = GetComponentInChildren<PlayerPointer>();
        stats = GetComponent<PlayerStats>();
        OnDangerEvent += PlayerInDanger;
        OnDangerClear += PlayerNoLongerInDanger;
    }

    private void Update()
    {
        if (!isDead)
        {
            if (normalHungerCoroutine == null && readyToGetHungry && !isSleeping)
            {
                normalHungerCoroutine = StartCoroutine(GettingHungry());
            }
            if (starvingCoroutine == null && !isSleeping && readyToHungerPain && isStarving)
            {
                starvingCoroutine = StartCoroutine(HungerPain());
            }
            if (sleepCoroutine == null && isSleeping && readyToRest)
            {
                sleepCoroutine = StartCoroutine(Resting());
            }
            if (coldCoroutine == null && isCold && readyToFreeze)
            {
                coldCoroutine = StartCoroutine(Freezing());
            }
        }
    }
    IEnumerator Freezing()
    {
        readyToFreeze = false;
        yield return new WaitForSeconds(freezingIteration);
        readyToFreeze = true;
        OnEnergyUpdate(-1);
        coldCoroutine = null;
    }

    private IEnumerator GettingHungry()
    {
        readyToGetHungry = false;
        yield return new WaitForSeconds(normalHungerDuration);
        OnHungerUpdate(-1);
        readyToGetHungry = true;
        normalHungerCoroutine = null;
    }
    private IEnumerator Resting()
    {
        readyToRest = false;
        yield return new WaitForSeconds(restingIteration);
        if (isDead)
        {
            playerBed.DiedInSleep(this);
        }
       else{ 
            OnEnergyUpdate(+1);
            Debug.Log(playerCharacter + " rests in bed.");
            sleepCoroutine = null;
            if (stats.EnergyPercent >= 1f)
            {
                playerBed.Wake(this);
            }
            else
            {
                readyToRest = true;
            }
        }

    }
    private IEnumerator HungerPain()
    {
        readyToHungerPain = false;
        yield return new WaitForSeconds(hungerPainIteration);
        OnHealthUpdate(-1);
        Debug.Log(playerCharacter + "'s stomach growls.");
        readyToHungerPain = true;
        starvingCoroutine = null;
    }

    public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            GetComponent<Collider2D>().enabled = false;
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            GameMessage.Instance.ShowMessage(playerCharacter + " Is Dead");
            ActivePlayerController.Instance.SwitchToNextAvailablePlayer();
            pointer.TogglePointerView(false);
            StartCoroutine(PlayerZoomCamera.Instance.ZoomToEvent(transform, dangerEventDuration));
        }
    }

    public void StartSleep(Bed bed)
    {
        if (!isSleeping)
        {
            playerBed = bed;
            isSleeping = true;
            controller.isSleeping = true;
            WarmingUp();
            Debug.Log(playerCharacter + " goes to sleep");
            readyToRest = true;
            GetComponent<Collider2D>().enabled = false;
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            pointer.TogglePointerView(false);
            ActivePlayerController.Instance.SwitchToNextAvailablePlayer();
        }
    }

    public void EndSleep()
    {
        isSleeping = false;
        if (sleepCoroutine != null)
        {
            StopCoroutine(sleepCoroutine);
        }
        GetComponent<Collider2D>().enabled = true;
        GetComponentInChildren<SpriteRenderer>().enabled = true;
        pointer.TogglePointerView(true);
        controller.isSleeping = false;
        readyToRest = false;
    }

    public void StartColdCounter()
    {
        if (!isCold)
        {
            isCold = true;
            readyToFreeze = true;
            Debug.Log(playerCharacter + " is freezing");
        }
    }
        
    public void WarmingUp()
    {
        if (coldCoroutine != null)
            StopCoroutine(coldCoroutine);
        isCold = false;
        Debug.Log(playerCharacter + " is warming up.");

    }

    public void StartHunger()
    {
        if (!isStarving)
        {
            isStarving = true;
            AudioManager.PlaySFX("starving", .5f, 0f);
            GameMessage.Instance.ShowMessage(playerCharacter + " Is Starving");
            readyToHungerPain = true;
            OnDangerEvent(dangerEventDuration);
        }
    }

    public void HungerSatiated()
    {
        if (isStarving && starvingCoroutine != null)
        {
            StopCoroutine(starvingCoroutine);

            --dangerEvents;
            isStarving = false;
            readyToHungerPain = false;
            if (!IsInDanger)
            {
                OnDangerClear();
            }
        }
    }

    public void StartEnergyDrain()
    {
        if (!isDrained)
        {
            isDrained = true;
            GameMessage.Instance.ShowMessage(playerCharacter + " Is Exhausted");
            AudioManager.PlaySFX("gotTired", .5f, 0f);
            OnDangerEvent(dangerEventDuration);
        }
    }

    public void PlayerInDanger(float time)
    {
        ++dangerEvents;
        StartCoroutine(PlayerZoomCamera.Instance.ZoomToEvent(transform, time));
        pointer.UpdatePointer(ActivePlayerController.IsActivePlayer(playerCharacter) ? PlayerPointer.PointerStatus.ACTIVE : PlayerPointer.PointerStatus.DANGER);
    }

    public void PlayerNoLongerInDanger()
    {
        pointer.UpdatePointer(ActivePlayerController.IsActivePlayer(playerCharacter) ? PlayerPointer.PointerStatus.ACTIVE : PlayerPointer.PointerStatus.INACTIVE);
    }


}
