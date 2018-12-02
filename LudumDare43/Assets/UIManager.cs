using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SimpleSingleton<UIManager> {

    Portrait[] portraits;
    UIStatusBars[] statusBars;

    public override void Awake()
    {
        base.Awake();
        portraits = GetComponentsInChildren<Portrait>();
        statusBars = GetComponentsInChildren<UIStatusBars>();
    }

    public Portrait GetPortrait(Player.PlayerCharacter character)
    {
        return portraits[(int)character];
    }

    public UIStatusBars GetUIStatusBars(Player.PlayerCharacter character)
    {
        return statusBars[(int)character];
    }

}
