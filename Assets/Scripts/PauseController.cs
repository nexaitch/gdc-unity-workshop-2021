using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {
    public bool isPaused {get; private set;}
    public GameObject pausePanel;
    private GameOverController gameOver;

    void Start() {
        gameOver = GetComponent<GameOverController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause")) {
            TogglePause();
        }
    }

    public void TogglePause() {
        if (!gameOver.isGameOver) {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : 1;
            pausePanel.SetActive(isPaused);
        }
    }
}
