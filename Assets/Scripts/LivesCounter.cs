using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesCounter : MonoBehaviour
{
    private Text myText;
    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth ph = GameObject.FindGameObjectWithTag("Player")
                                    .GetComponent<PlayerHealth>();
        ph.playerLivesChange += onPlayerLivesChange;
        myText = GetComponent<Text>();
        onPlayerLivesChange(ph);
    }

    void onPlayerLivesChange(PlayerHealth ph) {
        myText.text = "Lives: " + ph.lives;
    }
}
