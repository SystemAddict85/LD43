using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerCharacter { Paw, Maw, Girl, Boy };

    public PlayerController controller;
    public PlayerPointer pointer;

    public PlayerCharacter playerCharacter = PlayerCharacter.Paw;
    
    public bool isInDanger = false;

    private void Awake()
    {       
        controller = GetComponent<PlayerController>();
        pointer = GetComponentInChildren<PlayerPointer>();
    }
}
