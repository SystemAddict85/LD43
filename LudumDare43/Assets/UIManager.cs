using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SimpleSingleton<UIManager> {

    Portrait[] portraits;
    UIStatusBars[] statusBars;
    UIResources[] resources;
    public DaysRemaining daysRemaining;
    public DarknessBar darknessBar;

    public override void Awake()
    {
        base.Awake();
        daysRemaining = GetComponentInChildren<DaysRemaining>();
        portraits = GetComponentsInChildren<Portrait>();
        statusBars = GetComponentsInChildren<UIStatusBars>();
        resources = GetComponentsInChildren<UIResources>();
        darknessBar = GetComponentInChildren<DarknessBar>();
    }
    
    public Portrait GetPortrait(Player.PlayerCharacter character)
    {
        return portraits[(int)character];
    }

    public UIStatusBars GetUIStatusBars(Player.PlayerCharacter character)
    {
        return statusBars[(int)character];
    }

    public void UpdateResourceUI(ResourceManager.ResourceType type, int amount)
    {
        resources[(int)type].UpdateResource(amount);
    }

}
