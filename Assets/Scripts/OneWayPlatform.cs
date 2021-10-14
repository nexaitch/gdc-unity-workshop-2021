using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private Collider2D[] thisColliders;
    private Collider2D playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        thisColliders = GetComponents<Collider2D>();
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxisRaw("Vertical") < 0) {
            foreach(Collider2D collider in thisColliders){
                Physics2D.IgnoreCollision(collider, playerCollider, true);
            }
        } else {
            foreach (Collider2D collider in thisColliders) {
                Physics2D.IgnoreCollision(collider, playerCollider, false);
            }
        }
    }
}
