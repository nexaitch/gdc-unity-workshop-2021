using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{

    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    [Range(0.01f, 1)]
    public float cooldownTime = 0.5f;
    private float cooldownTimer = 0f;

    void Update()
    {

        if (cooldownTimer > 0f) {
            cooldownTimer -= Time.deltaTime;
        }

        if (cooldownTimer <= 0f && Input.GetButton("Fire1")) {
            cooldownTimer += cooldownTime;

            GameObject bullet = Instantiate(bulletPrefab);
            Vector2 velocity;
            if (transform.localScale.x > 0) {
                velocity = Vector2.right * bulletSpeed;
            } else {
                velocity = Vector2.left * bulletSpeed;
            }

            bullet.transform.position = transform.position;
            bullet.GetComponent<Rigidbody2D>()
                .velocity = velocity;
        }
    }
}
