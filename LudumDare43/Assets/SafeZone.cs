using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : SimpleSingleton<SafeZone> {

    public List<Player.PlayerCharacter> playersInside = new List<Player.PlayerCharacter>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            Debug.Log(player.playerCharacter + " entered the house.");
            playersInside.Add(player.playerCharacter);
            if (Fireplace.isFireOn)
            {
                player.WarmingUp();
            }
            else
            {
                player.StartColdCounter();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            playersInside.Remove(player.playerCharacter);
            player.StartColdCounter();
        }
    }
}
