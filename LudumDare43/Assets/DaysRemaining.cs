using TMPro;
using UnityEngine;

public class DaysRemaining : MonoBehaviour
{

    TextMeshProUGUI tmpro;
    bool isTimerStarted = false;

    [HideInInspector]
    public int days = 30;
    private float iterationScale = 0f;
    private float currentime;
    private float totalTime;

    private void Awake()
    {
        tmpro = GetComponent<TextMeshProUGUI>();
        UpdateText(days);
    }

    private void Update()
    {
        
        if (isTimerStarted)
        {
            var step = Time.deltaTime;
            currentime += step;
            if(currentime >= iterationScale)
            {
                currentime = 0f;                
                UpdateText(--days);
            }
        }
    }

    public void StartTimer(float timeLength)
    {
        totalTime = timeLength;
        iterationScale = timeLength / (float)days;
        isTimerStarted = true;
    }

    private void UpdateText(int days)
    {
        tmpro.text = "Days To Spring: " + days.ToString();
        if(days == 0)
        {
            GameManager.Instance.GameOver(true);
        }
    }
}
