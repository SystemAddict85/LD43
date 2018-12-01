using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class UIStatusBars : MonoBehaviour {

    public enum StatusBarType { HEALTH, HUNGER, ENERGY };

    [SerializeField]
    private Image[] bars;
    private bool readyToTest = true;

    private void Awake()
    {
        var images = GetComponentsInChildren<Image>();
        bars = images.Skip(1).ToArray();
    }

    private void Start()
    {
        UpdateBar(StatusBarType.HEALTH, .75f);
        UpdateBar(StatusBarType.ENERGY, .25f);
        UpdateBar(StatusBarType.HUNGER, 0f);
    }


    private void Update()
    {
        //testing
        if (readyToTest)
        {
            readyToTest = false;
            StartCoroutine(WaitToTest());
            for (int i = 0; i < 3; ++i)
            {
                var testFill = bars[i].fillAmount -.05f;
                if (testFill < 0f)
                    testFill = 1f;
                UpdateBar((StatusBarType)i, testFill);
            }
        }
    }

    private IEnumerator WaitToTest()
    {
        yield return new WaitForSeconds(0.1f);
        readyToTest = true;

    }

    public void UpdateBar(StatusBarType type, float percentAmount)
    {
        bars[(int)type].fillAmount = percentAmount;
        
    }
	
}
