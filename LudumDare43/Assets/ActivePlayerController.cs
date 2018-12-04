using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayerController : SimpleSingleton<ActivePlayerController> {

    public static Player.PlayerCharacter ActivePlayerCharacter { get; private set; }
    public static Player ActivePlayer { get { return Instance.players[(int)ActivePlayerCharacter]; } }

    public System.Action<Player.PlayerCharacter> OnPlayerChange;

    public Player[] players;

    public override void Awake()
    {
        base.Awake();
        players = GetComponentsInChildren<Player>();
        OnPlayerChange += ChangePlayer;
    }

    private void Start()
    {
        ActivePlayerCharacter = Player.PlayerCharacter.Paw;
        players[(int)ActivePlayerCharacter].pointer.UpdatePointer(PlayerPointer.PointerStatus.ACTIVE);
    }

    public void ChangePlayer(Player.PlayerCharacter player)
    {
        if (players[(int)player].isDead)
        {
            // switch if current player is dead and trying to switch, otherwise do nothing
            if(ActivePlayer.isDead)
                SwitchToNextAvailablePlayer();
        }
        else
        {            
            int prevIndex = (int)ActivePlayerCharacter;
            // if player is in danger and inactive, pointer must be red
            if(!players[prevIndex].isDead)
                players[prevIndex].pointer.UpdatePointer(players[prevIndex].IsInDanger ? PlayerPointer.PointerStatus.DANGER : PlayerPointer.PointerStatus.INACTIVE);

            // update active player
            ActivePlayerCharacter = player;
            players[(int)player].pointer.UpdatePointer(PlayerPointer.PointerStatus.ACTIVE);
        }
    }

    public static bool IsActivePlayer(Player.PlayerCharacter player)
    {
        return player == ActivePlayerCharacter;
    }

    public void SwitchToNextAvailablePlayer()
    {
        int j = (int)ActivePlayerCharacter;
        bool isPlayerFound = false;
        for(int i = 0; i < players.Length; ++i)
        {
            if(i != j)
            {
                if (!players[i].isDead)
                {
                    OnPlayerChange(players[i].playerCharacter);
                    isPlayerFound = true;
                }
            }
        }
        // no switch occurred, check if active player is dead
        if (!isPlayerFound && ActivePlayer.isDead)
        {
            Debug.Log("GAME OVER: ALL DEAD");
            GameManager.Instance.GameOver(false);
        }
        else
        {
            Debug.Log("sole survivor");
        }
    }
}
