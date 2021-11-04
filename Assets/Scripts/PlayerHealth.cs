using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int lives = 3;
    private Vector3 respawn;
    new private Rigidbody2D rigidbody2D;

    public delegate void playerLivesChangeDelegate(PlayerHealth ph);
    public event playerLivesChangeDelegate playerLivesChange;

    // Start is called before the first frame update
    void Start()
    {
        respawn = transform.position;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -Camera.main.orthographicSize) {
            die();
        }
        // some collision checks with enemies here
    }

    void die() {
        // TODO if lives == 0 game over
        --lives;
        transform.position = respawn;
        rigidbody2D.velocity = Vector2.zero;
        playerLivesChange(this);
    }
}
