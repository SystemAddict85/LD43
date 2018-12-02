using UnityEngine;
using UnityEngine.UI;

public class Portrait : MonoBehaviour
{
    public Player.PlayerCharacter playerCharacter = Player.PlayerCharacter.Paw;

    public float lookFrequency = 5f;
    private float currentLookCount = 0f;
    private float varLook;

    public float blinkFrequency = 1f;
    private float currentBlinkCount = 0f;
    private float varBlink;

    public float blinkMouthFrequency = 3f;
    private float currentBlinkMouthCount = 0f;
    private float varBlinkMouth;

    Animator anim;
    public bool canAnimate = true;
    // Use this for initialization
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();

        varBlink = RandomizeTime(blinkFrequency);
        varBlinkMouth = RandomizeTime(blinkMouthFrequency);
        varLook = RandomizeTime(lookFrequency);
        ResetAllTriggers();

        GetComponent<Button>().onClick.AddListener(ChangePlayer);
    }


    // Update is called once per frame
    void Update()
    {
        if (canAnimate)
        {
            UpdateCounts();
        }
    }

    private void ChangePlayer()
    {
        ActivePlayerController.Instance.ChangePlayer(playerCharacter);
    }

    private float RandomizeTime(float freq)
    {
        return freq * Random.Range(.8f, 1.2f);
    }

    private void UpdateCounts()
    {
        var time = Time.deltaTime;
        currentBlinkCount += time;
        currentBlinkMouthCount += time;
        currentLookCount += time;

        if(currentLookCount >= varLook)
        {
            anim.SetTrigger("look");
            RandomizeTime(varLook);
            currentLookCount = 0f;
        }
        if(currentBlinkCount >= varBlink)
        {
            anim.SetTrigger("blink");
            RandomizeTime(varBlink);
            currentBlinkCount = 0f;            
        }
        if(currentBlinkMouthCount >= varBlinkMouth)
        {
            anim.SetTrigger("blinkMouth");
            RandomizeTime(varBlinkMouth);
            currentBlinkMouthCount = 0f;
        }
    }

    private void CallAnimation(string animation)
    {

        //ResetAllTriggers();
        anim.SetTrigger(animation);
    }

    private void ResetAllTriggers()
    {
        anim.ResetTrigger("look");
        anim.ResetTrigger("blink");
        anim.ResetTrigger("blinkMouth");
    }
}
