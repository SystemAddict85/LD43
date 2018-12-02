using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayerController : SimpleSingleton<ActivePlayerController> {

    public static Player.PlayerCharacter ActivePlayer { get; private set; }

    public Player[] players;

    public override void Awake()
    {
        base.Awake();
        players = GetComponentsInChildren<Player>();        
    }

    private void Start()
    {
        ActivePlayer = Player.PlayerCharacter.Paw;
        players[(int)ActivePlayer].pointer.UpdatePointer(PlayerPointer.PointerStatus.ACTIVE);
    }

    public void ChangePlayer(Player.PlayerCharacter player)
    {
        int prevIndex = (int)ActivePlayer;        
        // if player is in danger and inactive, pointer must be red
        players[prevIndex].pointer.UpdatePointer(players[prevIndex].isInDanger ? PlayerPointer.PointerStatus.DANGER : PlayerPointer.PointerStatus.INACTIVE);

        // update active player
        ActivePlayer = player;
        players[(int)player].pointer.UpdatePointer(PlayerPointer.PointerStatus.ACTIVE);
    }

    public static bool IsActivePlayer(Player.PlayerCharacter player)
    {
        return player == ActivePlayer;
    }

}
