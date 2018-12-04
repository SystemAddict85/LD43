using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarknessBar : SimpleSingleton<DarknessBar> {

    Image bar;

    [SerializeField]
    private float fullDarknessAmount = 120f;

    private bool isConsuming = false;

    public float DarknessRemainingPercent {
        get {
            return currentDarknessRemaining / fullDarknessAmount;
        }
    }
    public float currentDarknessRemaining = 0f;
    private bool isReadyToConsume = true;
    private bool isConsumingHealth;
    [SerializeField]
    private float healthConsumeIteration = 2f;

    private Coroutine consumeHealthCoor;

    public override void Awake()
    {
        base.Awake();
        bar = GetComponent<Image>();        
    }
    private void Start()
    {
        
    }
    public void Update()
    {
        if(isConsuming)
        {            
            Consume();      
            if(currentDarknessRemaining <= 0f)
            {
                StartConsumingHeath();
            }

        }else if (isConsumingHealth && isReadyToConsume && consumeHealthCoor == null)
        {
            consumeHealthCoor = StartCoroutine(ConsumeHealth());
        }
    }

    private void StartConsumingHeath()
    {
        isConsuming = false;
        isConsumingHealth = true;
        consumeHealthCoor = StartCoroutine(ConsumeHealth());
        GameMessage.Instance.ShowMessage("I Will Eat Your Soul!", important:true);
    }

    IEnumerator ConsumeHealth()
    {
        isReadyToConsume = false;
        yield return new WaitForSeconds(healthConsumeIteration);
            
        foreach (var p in ActivePlayerController.Instance.players)
        {
            if (!p.isDead)
            {
                p.OnHealthUpdate(-1);
            }
        }
        consumeHealthCoor = null;
        isReadyToConsume = true;
    }

    private void Consume()
    {
        var step = Time.deltaTime;
        currentDarknessRemaining -= step;
        UpdateBar();
    }

    public void UpdateBar()
    {
        bar.fillAmount = DarknessRemainingPercent;
    }

    public void StartBar(float timeLength) {
        fullDarknessAmount = timeLength;
        currentDarknessRemaining = fullDarknessAmount;
        ToggleBar(true);
    }

    public void ToggleBar(bool enabled)
    {        
        isConsuming = enabled;
        
    }

    public static void Refill()
    {
        Instance.currentDarknessRemaining = Instance.fullDarknessAmount;
        Instance.UpdateBar();
        GameMessage.Instance.ShowMessage("The Darkness Subsides");
        Instance.ToggleBar(true);
    }
}
