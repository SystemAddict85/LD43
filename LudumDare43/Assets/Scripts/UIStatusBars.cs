using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class UIStatusBars : MonoBehaviour {

    public enum StatusBarType { HEALTH, ENERGY, HUNGER };
        
    private Image[] bars;

    [SerializeField]
    private Sprite[] frameSprites;
    private Image barFrames;

    private int currentBinaryCode = 0;

    private void Awake()
    {
        var images = GetComponentsInChildren<Image>();
        barFrames = images[0];
        bars = images.Skip(1).ToArray();
    }

    public void UpdateBar(StatusBarType type, float percentAmount)
    {
        var bar = bars[(int)type];
        bool wasEmpty = (bar.fillAmount == 0f);

        // use binary to encode image sprites
        if (wasEmpty && percentAmount > 0f)
        {
            // subtract two to the power of type to remove icons
            int code = -(int)Mathf.Pow(2f, (int)type);
            Debug.Log(code);
            UpdateEmptyBarSprites(code);
        } else if(!wasEmpty && percentAmount == 0f)
        {
            // add two to the power of type to add icons
            int code = (int)Mathf.Pow(2f,(int)type);
            UpdateEmptyBarSprites(code);
        }
        bars[(int)type].fillAmount = percentAmount;
    }

    private void UpdateEmptyBarSprites(int amount)
    {
        currentBinaryCode += amount;
        barFrames.sprite = frameSprites[currentBinaryCode];
    }
	
}
