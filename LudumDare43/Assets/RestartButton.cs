using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponentInChildren<Button>().onClick.AddListener(() => RestartScene());
        GetComponent<CanvasGroup>().alpha = 0f;
	}

    public void ShowGameOver(string message)
    {
        GetComponent<CanvasGroup>().alpha = 1f;
        GetComponentInChildren<TMPro.TextMeshProUGUI>().text = message;
    }
	
	void RestartScene()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
