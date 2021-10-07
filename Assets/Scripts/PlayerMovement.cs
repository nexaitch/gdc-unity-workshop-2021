using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    new private Rigidbody2D rigidbody;
    private BoxCollider2D boxCollider;
    private Animator animator;

    public float moveSpeed = 5f;
    public float jumpSpeed = 10f;
    [Range(0,1)]
    public float holdJumpGravityFactor = 0.5f;
    [Range(1,5)]
    public float fallGravityFactor = 2f;

    private float baseGravity;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        baseGravity = rigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        // movement
        Vector2 velocity = rigidbody.velocity;
        float inputX = Input.GetAxisRaw("Horizontal");
        velocity.x = moveSpeed * inputX;

        if (Input.GetButtonDown("Jump") && isGrounded()) {
            velocity.y = jumpSpeed;
        }

        animator.SetFloat("vx", velocity.x);
        animator.SetFloat("vy", velocity.y);

        rigidbody.velocity = velocity;

        // jump gravity mechanics
        float gravityFactor = 1f;

        if (velocity.y < 0) {
            gravityFactor *= fallGravityFactor;
        }

        if (Input.GetButton("Jump")) {
            gravityFactor *= holdJumpGravityFactor;
        }

        rigidbody.gravityScale = baseGravity * gravityFactor;
    }

    private bool isGrounded() {
        RaycastHit2D hit = Physics2D.Raycast(
            boxCollider.bounds.center,
            Vector2.down,
            boxCollider.bounds.extents.y + 0.1f,
            LayerMask.GetMask("Ground"));
        return hit.collider != null;
    }
}
