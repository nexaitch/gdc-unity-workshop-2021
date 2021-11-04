using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public bool isGameOver { get; private set; }
    public GameObject gameOverScreen;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerHealth>()
            .playerLivesChange += OnLivesChange;
    }

    void OnLivesChange(PlayerHealth ph) {
        if(ph.lives <= 0) {
            isGameOver = true;
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);
        }
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
}
