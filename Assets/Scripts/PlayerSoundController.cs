using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioClip hurtSound;
    private AudioSource audioSource;
    private int oldPlayerLives;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayerHealth ph = GetComponent<PlayerHealth>();
        oldPlayerLives = ph.lives;
        ph.playerLivesChange += HandleHealthChange;
    }

    void HandleHealthChange(PlayerHealth ph) {
        if (ph.lives < oldPlayerLives) {
            audioSource.PlayOneShot(hurtSound);
        }
        oldPlayerLives = ph.lives;
    }
}
