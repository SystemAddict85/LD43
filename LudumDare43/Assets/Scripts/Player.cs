using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerCharacter { Paw, Maw, Girl, Boy };
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

    [HideInInspector]
    public PlayerStats stats;

    [HideInInspector]
    public bool isDrained = false;
    [HideInInspector]
    public bool isHungry = false;
    private bool readyToHungerPain = false;
    [SerializeField]
    private float hungerPainIteration = 3f;
    private Coroutine hungerCoroutine;

    public Action<int> OnHealthUpdate;
    public Action<int> OnHungerUpdate;
    public Action<int> OnEnergyUpdate;
    public Action<float> OnDangerEvent;
    public Action OnDangerClear;

    public bool isDead = false;

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
        if (readyToHungerPain && isHungry)
        {
            hungerCoroutine = StartCoroutine(HungerPain());
        }
    }

    private IEnumerator HungerPain()
    {
        readyToHungerPain = false;
        yield return new WaitForSeconds(hungerPainIteration);
        OnHealthUpdate(-1);
        OnEnergyUpdate(-1);
        Debug.Log(playerCharacter + "'s stomach growls.");
        readyToHungerPain = true;
    }

    public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            Debug.Log(playerCharacter + " has died.");
            StartCoroutine(PlayerZoomCamera.Instance.ZoomToEvent(transform, dangerEventDuration));
        }
    }

    public void StartHunger()
    {
        if (!isHungry)
        {
            isHungry = true;
            Debug.Log(playerCharacter + " is starving.");            
            readyToHungerPain = true;
            OnDangerEvent(dangerEventDuration);
        }
    }

    public void HungerSatiated()
    {
        if (isHungry && hungerCoroutine != null)
        {
            StopCoroutine(hungerCoroutine);

            --dangerEvents;
            isHungry = false;
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
            Debug.Log(playerCharacter + " is exhausted.");
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
