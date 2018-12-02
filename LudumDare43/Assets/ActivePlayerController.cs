using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayerController : SimpleSingleton<ActivePlayerController> {

    public static Player.PlayerCharacter ActivePlayerCharacter { get; private set; }
    public static Player ActivePlayer { get { return Instance.players[(int)ActivePlayerCharacter]; } }

    public Player[] players;

    public override void Awake()
    {
        base.Awake();
        players = GetComponentsInChildren<Player>();        
    }

    private void Start()
    {
        ActivePlayerCharacter = Player.PlayerCharacter.Paw;
        players[(int)ActivePlayerCharacter].pointer.UpdatePointer(PlayerPointer.PointerStatus.ACTIVE);
    }

    public void ChangePlayer(Player.PlayerCharacter player)
    {
        int prevIndex = (int)ActivePlayerCharacter;        
        // if player is in danger and inactive, pointer must be red
        players[prevIndex].pointer.UpdatePointer(players[prevIndex].isInDanger ? PlayerPointer.PointerStatus.DANGER : PlayerPointer.PointerStatus.INACTIVE);

        // update active player
        ActivePlayerCharacter = player;
        players[(int)player].pointer.UpdatePointer(PlayerPointer.PointerStatus.ACTIVE);
    }

    public static bool IsActivePlayer(Player.PlayerCharacter player)
    {
        return player == ActivePlayerCharacter;
    }

}
